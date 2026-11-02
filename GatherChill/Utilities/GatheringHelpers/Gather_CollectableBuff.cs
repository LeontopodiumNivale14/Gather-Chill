using ECommons.ExcelServices;
using ECommons.GameHelpers;
using ECommons.UIHelpers.AddonMasterImplementations;
using GatherChill.Enums;
using GatherChill.Utilities.Utility;

namespace GatherChill.Utilities.GatheringHelpers;

public static partial class Gather_Util
{
    public enum CollectablePreset
    {
        /// <summary>1000 GP rotation (TC guide): Scrutiny rounds, then 2x Solid/Ageless + Collect.</summary>
        ThousandGp,

        /// <summary>800/900 GP rotation (TC guide): Priming Touch in the build, then 1x Solid/Ageless + Collect.</summary>
        EightHundredGp,

        /// <summary>600/800 GP min stat rotation (TC guide). For when Scour is under 200 collectability.</summary>
        SixHundredMinStat,

        /// <summary>400/600 GP low stat rotation (TC guide).</summary>
        FourHundredGpLowStat,

        /// <summary>0 GP rotation (TC guide). Tops out around 400 collectability.</summary>
        ZeroGpEphemeral,
        None,
    }

    public readonly record struct CollectableState(
        int CharacterLevel,
        int AvailableGp,
        int Progression,           // current collectability, 0 - 1000
        int Attempts,              // attempts (integrity) left on the node
        int MaxAttempts,
        bool HasCollectorStandard, // star proc (2418): steers toward Meticulous
        bool HasHighStandard,      // swirl proc (3911): unlocks Brazen
        bool HasScrutiny,
        bool HasPriming,
        bool HasEureka,
        int MinCollectability = 0, // lowest breakpoint, the game rejects Collect below this
        int MidCollectability = 0, // 0 = this item doesn't have this tier
        int HighCollectability = 0,
        int MeticulousPower = 0,   // collectability each action gives right now, straight from the addon
        int ScourPower = 0,
        int BrazenPowerMin = 0)
    {
        /// <summary>True when Collect would be accepted by the game (never at 0, never under the minimum).</summary>
        public bool MeetsMinimum => Progression > 0 && Progression >= MinCollectability;

        /// <summary>The breakpoints this item actually has, lowest first (tiers that are 0 don't exist).</summary>
        public int[] Tiers => new[] { MinCollectability, MidCollectability, HighCollectability }
            .Where(tier => tier > 0)
            .Distinct()
            .OrderBy(tier => tier)
            .ToArray();

        /// <summary>Highest breakpoint this item has, or 0 if none.</summary>
        public int TopTier => Tiers.LastOrDefault();

        /// <summary>The closest breakpoint still above current collectability, or 0 if we're past all of them.</summary>
        public int NextTier
        {
            get
            {
                foreach (var tier in Tiers)
                {
                    if (tier > Progression)
                        return tier;
                }

                return 0;
            }
        }

        /// <summary>How many of the item's breakpoints current collectability has already reached.</summary>
        public int TiersReached
        {
            get
            {
                var progression = Progression;
                return Tiers.Count(tier => tier <= progression);
            }
        }
    }

    private const uint CollectorStandardStatusId = 2418;     // White Star, steers toward Meticulous
    private const uint CollectorHighStandardStatusId = 3911; // Orange Buff, unlocks Brazen

    private static bool HasStatus(GatherBuffId buff)
        => GathActionDict.TryGetValue(buff, out var entry) && Utils.HasStatusId(entry.StatusId);

    public static CollectableState BuildState(AddonMaster.GatheringMasterpiece collectable)
    {
        return new CollectableState(
            CharacterLevel: Player.Level,
            AvailableGp: Utils.GetGp(),
            Progression: (int)collectable.CurrentCollectability,
            Attempts: (int)collectable.CurrentIntegrity,
            MaxAttempts: (int)collectable.TotalIntegrity,
            HasCollectorStandard: Utils.HasStatusId(CollectorStandardStatusId),
            HasHighStandard: Utils.HasStatusId(CollectorHighStandardStatusId),
            HasScrutiny: HasStatus(GatherBuffId.Scrutiny),
            HasPriming: HasStatus(GatherBuffId.Priming),
            HasEureka: HasStatus(GatherBuffId.BonusIntegrity_Chance),
            MinCollectability: (int)collectable.MinCollectability,
            MidCollectability: (int)collectable.MidCollectability,
            HighCollectability: (int)collectable.HighCollectability,
            MeticulousPower: (int)collectable.MeticulousPower,
            ScourPower: (int)collectable.ScourPower,
            BrazenPowerMin: (int)collectable.BrazenPowerMin);
    }

    public static class CollectableSolver
    {
        public static int RecommendedGp(CollectablePreset preset) => preset switch
        {
            CollectablePreset.ThousandGp => 1000,
            CollectablePreset.EightHundredGp => 800,
            CollectablePreset.SixHundredMinStat => 600,
            CollectablePreset.FourHundredGpLowStat => 400,
            CollectablePreset.ZeroGpEphemeral => 0,
            CollectablePreset.None => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(preset)),
        };

        // The 0 GP rotation can't realistically get past this collectability.
        private const int ZeroGpCeiling = 400;

        // Scour below this means "min stat": use the 600/800 rotation instead of 400/600.
        private const int NormalScourPower = 200;

        /// <summary>
        /// Picks the rotation for a node from the GP you have right now. Call it once when the node starts.
        /// - 1000+ GP: 1000. 800+ GP: 800/900.
        /// - 600+ GP with a weak Scour (min stat): 600/800.
        /// - Low GP on an ephemeral node (or under 400 GP) whose minimum is 400 or less: 0 GP.
        /// - Otherwise: 400/600.
        /// </summary>
        public static CollectablePreset ChoosePreset(in CollectableState s, bool isEphemeral)
        {
            if (s.AvailableGp >= 1000)
                return CollectablePreset.ThousandGp;

            if (s.AvailableGp >= 800)
                return CollectablePreset.EightHundredGp;

            if (s.AvailableGp >= 600 && s.ScourPower < NormalScourPower)
                return CollectablePreset.SixHundredMinStat;

            var zeroGpCanReachMin = s.MinCollectability <= ZeroGpCeiling;
            if (zeroGpCanReachMin && (isEphemeral || s.AvailableGp < 400))
                return CollectablePreset.ZeroGpEphemeral;

            return CollectablePreset.FourHundredGpLowStat;
        }

        /// <summary>Returns the buffID of the class i'm looking for [BTN/MIN].</summary>
        public static uint? GetActionId(GatherBuffId buff, Job job)
        {
            if (!GathActionDict.TryGetValue(buff, out var entry))
                return null;

            if (!entry.ClassAction.TryGetValue(job, out var details))
                return null;

            return details.ActionId;
        }

        /// <summary>Best next action as a GatherBuffId, or null when nothing is usable (or no preset is set up).</summary>
        public static GatherBuffId? GetNextAction(CollectablePreset preset, in CollectableState s)
        {
            if (preset == CollectablePreset.None)
                return null;

            // Wise to the World comes first in every rotation.
            if (CanUse(GatherBuffId.BonusIntegrity_Chance, s))
                return GatherBuffId.BonusIntegrity_Chance;

            return Rotate(preset, s);
        }

        #region Rotation

        /// <summary>
        /// Every tick: finish if we're done (or on the last attempt), otherwise take the next build step of the preset.
        /// The guide rotations are decision trees on collectability, so they work from the current state alone.
        /// </summary>
        private static GatherBuffId? Rotate(CollectablePreset preset, in CollectableState s)
        {
            var zeroGp = preset == CollectablePreset.ZeroGpEphemeral;
            var stopAt = zeroGp ? Math.Min(s.TopTier, ZeroGpCeiling) : s.TopTier;
            var hitsDone = s.MaxAttempts - s.Attempts;

            // The 0 GP rotation is two hits, then collect. Everything else builds until the top breakpoint.
            var finishing = s.Progression >= stopAt || s.Attempts <= 1 || (zeroGp && hitsDone >= 2);

            // Collect is gated on the minimum inside CanUse, so this can't fire at 0 or under the minimum.
            if (finishing && s.MeetsMinimum)
                return Finish(preset, s);

            // Last attempt and still under the minimum: buy another attempt, otherwise keep building.
            if (s.Attempts <= 1 && Use(s, GatherBuffId.BonusIntegrity) is { } buyAttempt)
                return buyAttempt;

            var build = preset switch
            {
                CollectablePreset.ThousandGp => GuideBuild(s, priming: false),
                CollectablePreset.EightHundredGp => GuideBuild(s, priming: true),
                CollectablePreset.SixHundredMinStat => ScrutinyRounds(s),
                CollectablePreset.FourHundredGpLowStat => ScrutinyRounds(s),
                CollectablePreset.ZeroGpEphemeral => ZeroGpHit(s),
                _ => null,
            };

            // Nothing left to build with (GP, attempts): collect what we have if the game allows it.
            return build ?? (s.MeetsMinimum ? Finish(preset, s) : null);
        }

        /// <summary>
        /// The "Solid/Ageless + Collect spam" end of the 1000 and 800/900 rotations.
        /// The number of casts limits itself through GP (the build rounds spend most of it first).
        /// </summary>
        private static GatherBuffId? Finish(CollectablePreset preset, in CollectableState s)
        {
            var rotationTopsUp = preset is CollectablePreset.ThousandGp or CollectablePreset.EightHundredGp;
            if (rotationTopsUp && WorthTopUp(s) && Use(s, GatherBuffId.BonusIntegrity) is { } topUp)
                return topUp;

            return Use(s, GatherBuffId.Collect);
        }

        /// <summary>
        /// Solid Reason / Ageless Words costs GP that could go into another node, so only buy the extra attempt
        /// once at least 2/3 of the breakpoints (rounded up) are done: 1 of 1, 2 of 2, 2 of 3.
        /// Trying to prevent the usage of it if I can... might go back and disreguard this
        /// </summary>
        private static bool WorthTopUp(in CollectableState s)
        {
            var tierCount = s.Tiers.Length;
            return tierCount > 0 && s.TiersReached >= (2 * tierCount + 2) / 3;
        }

        /// <summary>1000 and 800/900 build: Scrutiny + big hit rounds, then Scour / Meticulous depending on collectability.</summary>
        private static GatherBuffId? GuideBuild(in CollectableState s, bool priming)
        {
            var below800 = s.Progression < 800;
            var below850 = s.Progression < 850;

            GatherBuffId? hit;
            if (below800) // Brazen when High Standard [Orange dude buff] procced, otherwise Meticulous
                hit = Use(s, s.HasHighStandard ? GatherBuffId.Brazen : GatherBuffId.Meticulous, GatherBuffId.Meticulous);
            else if (s.HasCollectorStandard || !below850) // 800 & Standard [White Star], or 850+: Meticulous (Scour as backup only below 850)
                hit = Use(s, GatherBuffId.Meticulous) ?? (below850 ? Use(s, GatherBuffId.Scour) : null);
            else // 800 - 849 without the star: Scour for the small, safe bump
                hit = Use(s, GatherBuffId.Scour, GatherBuffId.Meticulous);

            if (hit is null)
                return null;

            if (priming && hit == GatherBuffId.Meticulous && WantPriming(s) && Use(s, GatherBuffId.Priming) is { } prime)
                return prime;

            if (below800 && NeedsScrutiny(s) && Use(s, GatherBuffId.Scrutiny) is { } scrutiny)
                return scrutiny;

            return hit;
        }

        /// <summary>400/600 and 600/800 build: Scrutiny (only if needed), then the hit that closes the gap.</summary>
        private static GatherBuffId? ScrutinyRounds(in CollectableState s)
        {
            if (NeedsScrutiny(s) && Use(s, GatherBuffId.Scrutiny) is { } scrutiny)
                return scrutiny;

            return PickHit(s);
        }

        /// <summary>0 GP: Scour, then Meticulous if the star (Collector's Standard) procced, else Scour.</summary>
        private static GatherBuffId? ZeroGpHit(in CollectableState s)
            => s.HasCollectorStandard
                ? Use(s, GatherBuffId.Meticulous, GatherBuffId.Scour)
                : Use(s, GatherBuffId.Scour, GatherBuffId.Meticulous);

        /// <summary>
        /// The guide's "check your collectability" table, using the real powers: the first hit (Meticulous first,
        /// for its 25% free-hit chance) that reaches the next breakpoint, otherwise the strongest one we can use.
        /// </summary>
        private static GatherBuffId? PickHit(in CollectableState s)
        {
            var gap = s.NextTier - s.Progression;

            GatherBuffId? best = null;
            var bestPower = -1;

            foreach (var (hit, power) in new[]
            {
                (GatherBuffId.Meticulous, s.MeticulousPower),
                (GatherBuffId.Scour, s.ScourPower),
                (GatherBuffId.Brazen, s.BrazenPowerMin), // only the guaranteed part, and only with the swirl
            })
            {
                if (!CanUse(hit, s))
                    continue;

                if (power >= gap)
                    return hit;

                if (power > bestPower)
                {
                    best = hit;
                    bestPower = power;
                }
            }

            return best;
        }

        /// <summary>Scrutiny is only worth its GP when the next breakpoint is further than a plain hit reaches.</summary>
        private static bool NeedsScrutiny(in CollectableState s)
        {
            if (s.HasScrutiny)
                return false;

            var nextTier = s.NextTier;
            if (nextTier == 0)
                return false;

            var plainPower = Math.Max(s.MeticulousPower, Math.Max(s.ScourPower, s.HasHighStandard ? s.BrazenPowerMin : 0));
            if (plainPower <= 0) // no power data from the addon, just use it
                return true;

            return nextTier - s.Progression > plainPower;
        }

        /// <summary>
        /// Priming Touch, per the 800/900 guide: only open with it at 900 GP. Later on, only if the next Scrutiny
        /// and a Solid Reason are still affordable afterwards.
        /// </summary>
        private static bool WantPriming(in CollectableState s)
        {
            if (!CanUse(GatherBuffId.Priming, s))
                return false;

            if (s.Progression == 0)
                return s.AvailableGp >= 900;

            var reserve = Cost(GatherBuffId.Scrutiny) + Cost(GatherBuffId.BonusIntegrity);
            return s.AvailableGp >= Cost(GatherBuffId.Priming) + reserve;
        }

        #endregion

        #region Helpers

        private static int Cost(GatherBuffId buff) => (int)GathActionDict[buff].RequiredGp;

        private static GatherBuffId? Use(in CollectableState s, GatherBuffId a)
            => CanUse(a, s) ? a : null;

        private static GatherBuffId? Use(in CollectableState s, GatherBuffId a, GatherBuffId b)
            => Use(s, a) ?? Use(s, b);

        /// <summary>Level / buff requirements only.</summary>
        private static bool IsAvailable(GatherBuffId buff, in CollectableState s)
        {
            if (!GathActionDict.TryGetValue(buff, out var entry))
                return false;

            if (s.CharacterLevel < entry.RequiredLv)
                return false;

            return buff != GatherBuffId.Brazen || s.HasHighStandard; // Brazen needs the swirl proc
        }

        /// <summary>Everything needed to cast it right now: requirements, GP, and the node's current state.</summary>
        private static bool CanUse(GatherBuffId buff, in CollectableState s)
        {
            if (!IsAvailable(buff, s))
                return false;

            if (s.AvailableGp < Cost(buff)) // not enough GP
                return false;

            return buff switch
            {
                GatherBuffId.Scrutiny => !s.HasScrutiny,
                GatherBuffId.Priming => !s.HasPriming && s.Attempts > 0,
                GatherBuffId.BonusIntegrity => s.Attempts < s.MaxAttempts,
                GatherBuffId.BonusIntegrity_Chance => s.Attempts < s.MaxAttempts && s.HasEureka,
                GatherBuffId.Collect => s.Attempts > 0 && s.MeetsMinimum, // the game rejects Collect at 0 / under the minimum
                _ => s.Attempts > 0, // Scour, Brazen, Meticulous
            };
        }

        #endregion
    }
}