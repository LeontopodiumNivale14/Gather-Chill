using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GatherChill.Utilities.GatheringHelpers.GatherListUtils;

internal static partial class GatherList_Util
{
    public class ImportResult
    {
        public int Added { get; set; }
        public List<string> NotFound { get; } = new();
        public List<string> NotGatherable { get; } = new();

        /// <summary>
        /// Names that were added through the closest-match fallback: (Teamcraft name, matched name).
        /// </summary>
        public List<(string Input, string Matched)> FuzzyMatches { get; } = new();
    }

    private static readonly Regex TeamcraftLine = new(@"^(\d+)\s*[xX×]\s+(.+)$", RegexOptions.Compiled);
    private static readonly Regex NonAlphaNumeric = new(@"[^\p{L}\p{N}]+", RegexOptions.Compiled);

    private sealed class NameIndex
    {
        /// <summary>Normalized name -> itemId (gatherable entries win over non-gatherable ones).</summary>
        public Dictionary<string, uint> Exact { get; } = new();

        /// <summary>Gatherable items only, used for the closest-match fallback.</summary>
        public List<(string Normalized, string Display, uint ItemId)> Candidates { get; } = new();
    }

    private static string NormalizeName(string name) => NonAlphaNumeric.Replace(name.ToLowerInvariant(), " ").Trim();

    private static bool HasRoutes(Gather_Util.ItemClass info) => info.RouteInfo.Count != 0;

    // This gets created upon each import, that way if a route gets updated post, atleast it'll find the item names
    private static NameIndex BuildNameIndex()
    {
        var index = new NameIndex();
        var exactHasRoutes = new HashSet<string>();

        foreach (var (itemId, info) in Gather_Util.Sheet_ItemInfo)
        {
            if (string.IsNullOrWhiteSpace(info.Name))
                continue;

            var normalized = NormalizeName(info.Name);
            if (normalized.Length == 0)
                continue;

            var hasRoutes = HasRoutes(info);

            if (hasRoutes)
                index.Candidates.Add((normalized, info.Name, itemId));

            // First entry always wins, later duplicates only replace it if they are
            // gatherable and the stored one isn't
            if (!index.Exact.ContainsKey(normalized))
            {
                index.Exact[normalized] = itemId;
                if (hasRoutes)
                    exactHasRoutes.Add(normalized);
                continue;
            }

            if (hasRoutes && !exactHasRoutes.Contains(normalized))
            {
                index.Exact[normalized] = itemId;
                exactHasRoutes.Add(normalized);
            }
        }

        return index;
    }

    /// <summary>
    /// Standard Levenshtein distance. Bails out early once the distance is guaranteed
    /// to be greater than maxDistance, returning maxDistance + 1.
    /// </summary>
    private static int EditDistance(string a, string b, int maxDistance)
    {
        // Length difference alone is already more than allowed
        if (Math.Abs(a.Length - b.Length) > maxDistance)
            return maxDistance + 1;

        var previous = new int[b.Length + 1];
        var current = new int[b.Length + 1];

        for (var j = 0; j <= b.Length; j++)
            previous[j] = j;

        for (var i = 1; i <= a.Length; i++)
        {
            current[0] = i;
            var rowMin = current[0];

            for (var j = 1; j <= b.Length; j++)
            {
                var cost = a[i - 1] == b[j - 1] ? 0 : 1;
                current[j] = Math.Min(Math.Min(current[j - 1] + 1, previous[j] + 1), previous[j - 1] + cost);
                rowMin = Math.Min(rowMin, current[j]);
            }

            // Every cell in this row is greater than maxDistance, so the final result will be too
            if (rowMin > maxDistance)
                return maxDistance + 1;

            (previous, current) = (current, previous);
        }

        return previous[b.Length];
    }

    /// <summary>
    /// Resolves a Teamcraft item name to an itemId. Exact (normalized) match first,
    /// then the single closest gatherable item within a length-scaled edit distance.
    /// </summary>
    private static bool TryResolveName(NameIndex index, string name, out uint itemId, out string matchedName, out bool wasFuzzy)
    {
        itemId = 0;
        matchedName = name;
        wasFuzzy = false;

        var normalized = NormalizeName(name);
        if (normalized.Length == 0)
            return false;

        if (index.Exact.TryGetValue(normalized, out itemId))
            return true;

        // ~1 edit per 6 characters, at least 1
        var maxDistance = Math.Max(1, normalized.Length / 6);

        var bestDistance = maxDistance + 1;
        var bestCount = 0;
        var bestId = 0u;
        var bestName = "";

        foreach (var candidate in index.Candidates)
        {
            var distance = EditDistance(normalized, candidate.Normalized, maxDistance);

            // distance > maxDistance: candidate is too far away, skip it
            if (distance > maxDistance)
                continue;

            // distance < bestDistance: strictly closer than the current best, it becomes the new best
            if (distance < bestDistance)
            {
                bestDistance = distance;
                bestCount = 1;
                bestId = candidate.ItemId;
                bestName = candidate.Display;
                continue;
            }

            // distance == bestDistance: tie with the current best, so the match is ambiguous
            if (distance == bestDistance)
                bestCount++;
        }

        // Nothing close enough, or more than one equally close candidate
        if (bestCount != 1)
            return false;

        itemId = bestId;
        matchedName = bestName;
        wasFuzzy = true;
        return true;
    }

    /// <summary>
    /// Looks up an item by its display name (normalized exact match, then closest match).
    /// </summary>
    public static bool TryGetItemIdByName(string name, out uint itemId) => TryResolveName(BuildNameIndex(), name, out itemId, out _, out _);

    /// <summary>
    /// Imports a Teamcraft list. Any line in the "15x Item Name" format is picked up,
    /// headers are ignored, and anything without a gather route is reported in NotGatherable.
    /// Will save the config afterwards
    /// </summary>
    public static ImportResult ImportFromTeamcraft(string text)
    {
        var result = new ImportResult();
        if (string.IsNullOrWhiteSpace(text))
            return result;

        var index = BuildNameIndex();

        foreach (var rawLine in text.Split('\n'))
        {
            var match = TeamcraftLine.Match(rawLine.Trim());
            if (!match.Success || !int.TryParse(match.Groups[1].Value, out var amount))
                continue;

            var name = match.Groups[2].Value.Trim();

            if (!TryResolveName(index, name, out var itemId, out var matchedName, out var wasFuzzy))
            {
                result.NotFound.Add(name);
                continue;
            }

            if (!CanGather(itemId))
            {
                result.NotGatherable.Add(name);
                continue;
            }

            if (!TryAdd(itemId, amount))
            {
                result.NotFound.Add(name);
                continue;
            }

            result.Added++;
            if (wasFuzzy)
                result.FuzzyMatches.Add((name, matchedName));
        }

        return result;
    }
}
