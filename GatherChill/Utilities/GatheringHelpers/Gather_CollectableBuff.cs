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
        /// <summary>1000/700 GP rotation (TC guide).</summary>
        ThousandGp,

        /// <summary>400/600 GP rotation for low gathering stats (TC guide).</summary>
        FourHundredGpLowStat,

        /// <summary>0 GP rotation for Ephemeral nodes (TC guide).</summary>
        ZeroGpEphemeral,
        None,
    }

    public readonly record struct CollectableState(
        int CharacterLevel,
        int AvailableGp,
        int Progression,           // current collectability, 0 - 1000
        int Attempts,              // attempts (integrity) left on the node
        int MaxAttempts,
        bool HasCollectorStandard,
        bool HasScrutiny,
        bool HasEureka,
        int TargetCollectability = 1000);

    private const uint CollectorStandardStatusId = 3911;

    public static Gather_Util.CollectableState BuildState(AddonMaster.GatheringMasterpiece collectable, int target)
    {
        return new Gather_Util.CollectableState(
            CharacterLevel: Player.Level,
            AvailableGp: Utils.GetGp(),
            Progression: (int)collectable.CurrentCollectability,
            Attempts: (int)collectable.CurrentIntegrity,
            MaxAttempts: (int)collectable.TotalIntegrity,
            HasCollectorStandard: Utils.HasStatusId(CollectorStandardStatusId),
            HasScrutiny: Utils.HasStatusId(Gather_Util.GathActionDict[GatherBuffId.Scrutiny].StatusId),
            HasEureka: Utils.HasStatusId(Gather_Util.GathActionDict[GatherBuffId.BonusIntegrity_Chance].StatusId),
            TargetCollectability: target);
    }

    public static class CollectableSolver
    {
        public static int RecommendedGp(CollectablePreset preset) => preset switch
        {
            CollectablePreset.ThousandGp => 1000,
            CollectablePreset.FourHundredGpLowStat => 400,
            CollectablePreset.ZeroGpEphemeral => 0,
            CollectablePreset.None => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(preset)),
        };

        // Lowest GP each rotation is meant to be used at (the lower number in the TC names:
        // "1000/700" works from 700 GP, "400/600" from 400 GP; more GP just gives it room).
        private const int ThousandGpMinGp = 700;
        private const int FourHundredGpMinGp = 400;

        /// <summary>
        /// Picks the rotation for a node from the GP you have right now. Call it once when the node
        /// starts and keep the result, otherwise the preset changes mid-node as GP is spent.
        /// - 700+ GP: 1000/700.
        /// - Ephemeral node below that: 0 GP (per the TC guide notes).
        /// - Otherwise: 400/600. Below 400 GP it still works, it just falls back to Scour / Meticulous.
        ///   (The 0 GP rotation stops at 400 collectability, so it is only used on ephemeral nodes.)
        /// </summary>
        public static CollectablePreset ChoosePreset(in CollectableState s, bool isEphemeral)
        {
            var enoughForThousand = s.AvailableGp >= ThousandGpMinGp;
            if (enoughForThousand)
                return CollectablePreset.ThousandGp;

            if (isEphemeral)
                return CollectablePreset.ZeroGpEphemeral;

            return CollectablePreset.FourHundredGpLowStat;
        }

        /// <summary>Game action id to execute for this job, or null if the job (MIN/BTN only) or buff isn't in the dictionary.</summary>
        public static uint? GetActionId(GatherBuffId buff, Job job)
        {
            if (!GathActionDict.TryGetValue(buff, out var entry))
                return null;

            if (!entry.ClassAction.TryGetValue(job, out var details))
                return null;

            return details.ActionId;
        }

        /// <summary>Best next action id for this job, or null when nothing is usable (e.g. node is out of attempts).</summary>
        public static uint? GetNextActionId(CollectablePreset preset, in CollectableState s, Job job)
        {
            var buff = GetNextAction(preset, s);
            if (buff == null)
                return null;

            return GetActionId(buff.Value, job);
        }

        /// <summary>Best next action as a GatherBuffId, or null when nothing is usable (or no preset is set up).</summary>
        public static GatherBuffId? GetNextAction(CollectablePreset preset, in CollectableState s)
        {
            if (preset == CollectablePreset.None)
                return null;

            // Wise to the World comes first in every rotation.
            if (CanUse(GatherBuffId.BonusIntegrity_Chance, s))
                return GatherBuffId.BonusIntegrity_Chance;

            return preset switch
            {
                CollectablePreset.ThousandGp => ThousandGp(s),
                CollectablePreset.FourHundredGpLowStat => FourHundredGpLowStat(s),
                CollectablePreset.ZeroGpEphemeral => ZeroGpEphemeral(s),
                _ => throw new ArgumentOutOfRangeException(nameof(preset)),
            };
        }


        // Gets the rotation -> buff we should be using
        // If for some reason the first buff we get is null/nil, then we continue onto the next... ect
        private static GatherBuffId? ThousandGp(in CollectableState s)
        {
            var reachedTarget = s.Progression >= s.TargetCollectability;
            var below800 = s.Progression < 800;
            var below850 = s.Progression < 850;
            var lastAttempt = s.Attempts <= 1; // 1 or 0 attempts left

            // Done: top up an attempt if we can (Ageless Words / Solid Reason), then collect.
            if (reachedTarget && Use(s, GatherBuffId.BonusIntegrity, GatherBuffId.Collect) is { } finish)
                return finish;

            // Last attempt: just collect.
            if (lastAttempt && Use(s, GatherBuffId.Collect) is { } collect)
                return collect;

            // Still building collectability (below 800): Scrutiny, then a big hit.
            if (below800)
            {
                var bigHit = s.HasCollectorStandard ? GatherBuffId.Brazen : GatherBuffId.Meticulous;
                if (Use(s, GatherBuffId.Scrutiny, bigHit) is { } build)
                    return build;

                return Use(s, GatherBuffId.Meticulous);
            }

            // 800 and up from here.
            // With Collector's Standard, or at 850+: Meticulous (Scour as backup only while below 850).
            if (s.HasCollectorStandard || !below850)
                return Use(s, GatherBuffId.Meticulous) ?? (below850 ? Use(s, GatherBuffId.Scour) : null);

            // 800 - 849 without Collector's Standard: Scour for the small, safe bump.
            return Use(s, GatherBuffId.Scour, GatherBuffId.Meticulous);
        }

        private static GatherBuffId? FourHundredGpLowStat(in CollectableState s)
        {
            var readyToCollect = s.Progression >= s.TargetCollectability || s.Attempts <= 1;
            if (readyToCollect && Use(s, GatherBuffId.Collect) is { } collect)
                return collect;

            if (s.HasCollectorStandard && Use(s, GatherBuffId.Scrutiny, GatherBuffId.Meticulous) is { } withStandard)
                return withStandard;

            return Use(s, GatherBuffId.Scrutiny, GatherBuffId.Scour);
        }

        private static GatherBuffId? ZeroGpEphemeral(in CollectableState s)
        {
            // Ephemeral nodes never get far, so 400 is the ceiling; a lower target wins.
            var collectAt = Math.Min(s.TargetCollectability, 400);
            var readyToCollect = s.Progression >= collectAt || s.Attempts <= 1;
            if (readyToCollect && Use(s, GatherBuffId.Collect) is { } collect)
                return collect;

            var belowCollectAt = s.Progression < collectAt;
            if (belowCollectAt && s.HasCollectorStandard && Use(s, GatherBuffId.Meticulous) is { } meticulous)
                return meticulous;

            return Use(s, GatherBuffId.Scour);
        }

        #region Helpers

        private static GatherBuffId? Use(in CollectableState s, GatherBuffId a)
            => CanUse(a, s) ? a : null;

        private static GatherBuffId? Use(in CollectableState s, GatherBuffId a, GatherBuffId b)
            => Use(s, a) ?? Use(s, b);

        private static bool CanUse(GatherBuffId buff, in CollectableState s)
        {
            var entry = GathActionDict[buff];
            if (s.CharacterLevel < entry.RequiredLv) // character level is below what the action needs
                return false;

            if (s.AvailableGp < entry.RequiredGp) // not enough GP
                return false;

            return buff switch
            {
                GatherBuffId.Scrutiny => !s.HasScrutiny,
                GatherBuffId.BonusIntegrity => s.Attempts < s.MaxAttempts,
                GatherBuffId.BonusIntegrity_Chance => s.Attempts < s.MaxAttempts && s.HasEureka,
                _ => s.Attempts > 0, // Scour, Brazen, Meticulous, Collect
            };
        }

        #endregion
    }
}
