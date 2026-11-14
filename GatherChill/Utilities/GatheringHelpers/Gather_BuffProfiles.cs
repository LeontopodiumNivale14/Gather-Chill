using GatherChill.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GatherChill.Utilities.GatheringHelpers;

public static partial class Gather_Util
{
    public class BuffSettings
    {
        public bool Enabled { get; set; } = true;

        /// <summary>0 = no cap. Only meaningful for repeatable buffs (Ageless Words, Bountiful).</summary>
        public uint MaxUse { get; set; } = 0;
        public uint MinIntegrity { get; set; } = 0;
    }

    /// <summary>
    /// Planner OUTPUT only. Not user-editable.
    /// BuffPriority is the cast order, MaxUses is how many casts the plan wants (0 = no cap).
    /// </summary>
    public class GatherProfile
    {
        public string Name { get; set; } = "Planned";
        public List<GatherBuffId> BuffPriority { get; set; } = new();
        public Dictionary<GatherBuffId, uint> MaxUses { get; set; } = new();
    }

    /// <summary>One set of planner settings. Used for the default profile and inside every rule.</summary>
    public class ProfileClass
    {
        public Dictionary<GatherBuffId, BuffSettings> Buffs { get; set; } = CreateDefaultBuffs();

        public int BYII_Amount { get; set; } = 3;

        /// <summary>Tidings is only planned when the item's boon chance is at least this. Serialized under the old name so existing configs keep their value.</summary>
        [JsonProperty("TidingsChance")]
        public int TidingsMinBoon { get; set; } = 70;

        /// <summary>Legacy toggles. Only filled when an old config is loaded; Migrate() consumes it.</summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<GatherBuffId, bool>? BuffsEnabled { get; set; }

        public bool IsEnabled(GatherBuffId id) => !Buffs.TryGetValue(id, out var settings) || settings.Enabled;

        public int CastCap(GatherBuffId id)
        {
            if (!Buffs.TryGetValue(id, out var settings) || settings.MaxUse == 0)
                return int.MaxValue;

            return (int)settings.MaxUse;
        }
        public bool IsAllowed(GatherBuffId id, int maxIntegrity)
        {
            if (!Buffs.TryGetValue(id, out var settings))
                return true;

            // maxIntegrity >= MinIntegrity reads "the node is at least as big as the minimum"
            return settings.Enabled && maxIntegrity >= settings.MinIntegrity;
        }

        /// <summary>One-time move of the old bool toggles into Buffs. Returns true if something changed (save the config).</summary>
        public bool Migrate()
        {
            if (BuffsEnabled is null)
                return false;

            foreach (var (id, enabled) in BuffsEnabled)
            {
                if (!Buffs.TryGetValue(id, out var settings))
                {
                    settings = new BuffSettings();
                    Buffs[id] = settings;
                }

                settings.Enabled = enabled;
            }

            BuffsEnabled = null;
            return true;
        }

        public static Dictionary<GatherBuffId, BuffSettings> CreateDefaultBuffs() => new()
        {
            [GatherBuffId.BoonIncrease_1] = new(),
            [GatherBuffId.BoonIncrease_2] = new(),
            [GatherBuffId.Tidings] = new(),
            [GatherBuffId.YieldI] = new() { MinIntegrity = 12 },
            [GatherBuffId.YieldII] = new() { MinIntegrity = 8 },
            [GatherBuffId.BonusIntegrity] = new(),
            [GatherBuffId.BonusIntegrity_Chance] = new(),
            [GatherBuffId.BYII] = new(),
            [GatherBuffId.FieldMasteryI] = new(),
            [GatherBuffId.FieldMasteryII] = new(),
            [GatherBuffId.FieldMasteryIII] = new(),
        };

        /// <summary>One line per planner buff: available, or the first reason the planner drops it. Used by the calculator export.</summary>
    }

    public class BuffFolder
    {
        public int Id { get; set; }
        public string Name { get; set; } = "New Folder";
    }

    public class BuffRule
    {
        public string Name { get; set; } = "New Rule";
        public bool Enabled { get; set; } = true;
        public int? FolderId { get; set; }
        public GatherNodeKind? Kind { get; set; }
        public int? MinIntegrity { get; set; }
        public int? MaxIntegrity { get; set; }
        public ProfileClass Profile { get; set; } = new();
    }

    public static class RuleMatcher
    {
        /// <summary>Returns null when the rule matches, otherwise the reason it doesn't (used for the log).</summary>
        public static string? WhyNot(BuffRule rule, GatherNodeKind kind, int maxIntegrity)
        {
            if (!rule.Enabled)
                return "rule is disabled";

            if (rule.Kind is { } ruleKind && ruleKind != kind)
                return $"node is {kind}, rule wants {ruleKind}";

            // integrity is BELOW the minimum -> reject
            if (rule.MinIntegrity is { } min && maxIntegrity < min)
                return $"max integrity {maxIntegrity} is below the minimum {min}";

            // integrity is ABOVE the maximum -> reject
            if (rule.MaxIntegrity is { } max && maxIntegrity > max)
                return $"max integrity {maxIntegrity} is above the maximum {max}";

            return null;
        }

        /// <summary>
        /// Most specific match wins: a rule with a kind beats one without, then the narrower integrity range wins,
        /// then the earlier rule in the list. No match = the fallback (default) profile.
        /// </summary>
        public static (ProfileClass Profile, BuffRule? Rule) Resolve(
            ProfileClass fallback,
            IReadOnlyList<BuffRule> rules,
            GatherNodeKind kind,
            int maxIntegrity)
        {
            BuffRule? best = null;

            foreach (var rule in rules)
            {
                if (WhyNot(rule, kind, maxIntegrity) is not null)
                    continue;

                if (best is null || IsMoreSpecific(rule, best))
                    best = rule;
            }

            return (best?.Profile ?? fallback, best);
        }

        private static bool IsMoreSpecific(BuffRule candidate, BuffRule current)
        {
            if (candidate.Kind.HasValue != current.Kind.HasValue)
                return candidate.Kind.HasValue;

            // strictly narrower wins; equal width keeps the earlier rule
            return Width(candidate) < Width(current);
        }

        private static int Width(BuffRule rule)
        {
            if (rule.MinIntegrity is { } min && rule.MaxIntegrity is { } max)
                return max - min;

            if (rule.MinIntegrity is not null || rule.MaxIntegrity is not null)
                return int.MaxValue - 1; // open-ended: wide

            return int.MaxValue; // no integrity condition: widest
        }
    }

    public static List<(GatherBuffId Id, int Level, int Gp, string? Reason)> ExplainBuffs(
    int level, int maxIntegrity, int boonChance, ProfileClass profile)
    {
        var all = new List<BuffPlanner.GatherBuff>();
        all.AddRange(BuffPlanner.GatherBuffs.ChanceTiers);
        all.AddRange(BuffPlanner.GatherBuffs.YieldTiers);
        all.Add(BuffPlanner.GatherBuffs.GiftI);
        all.Add(BuffPlanner.GatherBuffs.GiftII);
        all.Add(BuffPlanner.GatherBuffs.Tidings);
        all.Add(BuffPlanner.GatherBuffs.Bountiful);
        all.Add(BuffPlanner.GatherBuffs.AgelessWords);

        var result = new List<(GatherBuffId, int, int, string?)>();

        foreach (var buff in all)
        {
            string? reason = null;
            profile.Buffs.TryGetValue(buff.Id, out var settings);

            // level is BELOW what the buff requires
            if (level < buff.Level)
                reason = $"locked: needs level {buff.Level}, estimate is {level}";
            else if (settings is { Enabled: false })
                reason = "disabled in profile";
            // max integrity is BELOW this buff's minimum
            else if (settings is not null && maxIntegrity < settings.MinIntegrity)
                reason = $"max integrity {maxIntegrity} is below the buff's minimum {settings.MinIntegrity}";
            // boon chance is BELOW the Tidings threshold
            else if (buff.Id == GatherBuffId.Tidings && boonChance < profile.TidingsMinBoon)
                reason = $"boon chance {boonChance}% is below the Tidings minimum {profile.TidingsMinBoon}%";

            result.Add((buff.Id, buff.Level, buff.Gp, reason));
        }

        return result;
    }
}