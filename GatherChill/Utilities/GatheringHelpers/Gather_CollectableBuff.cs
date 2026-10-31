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

        /// <summary>0 GP rotation for Ephemeral nodes (TC guide). Tops out around 400 collectability [USUALLY. . .].</summary>
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
        int MinCollectability = 0, // lowest breakpoint, the game rejects Collect below this
        int MidCollectability = 0, // 0 = this item doesn't have this tier
        int HighCollectability = 0,
        int MeticulousPower = 0,   // collectability each action gives
        int ScourPower = 0,
        int BrazenPowerMin = 0)
    {
        /// <Summary>Returns true when the game is able to collect [x > 0, and also progress is more than the min progress]</Summary>
        public bool MeetsMinimum => Progression > 0 && Progression >= MinCollectability;

        /// <summary>Gives all the breakpoints of the item that's being gathered. If it's 0, it isn't added [epheral nodes as ex]</summary>
        public int[] Tiers => new[] { MinCollectability, MidCollectability, HighCollectability }
            .Where(tier => tier > 0)
            .Distinct()
            .OrderBy(tier => tier)
            .ToArray();

        /// <summary>Highest breakpoint the item has, returns 0 if none (should never happen unless the addon breaks)</summary>
        public int TopTier => Tiers.LastOrDefault();

        /// <summary>
        /// How many of the item's breakpoints current collectability has already reached.<br></br>
        /// Mainly used to prevent Integrity Bonus from being active when I don't want it to
        /// </summary>
        public int TiersReached
        {
            get
            {
                var progression = Progression;
                return Tiers.Count(tier => tier <= progression);
            }
        }
    }

    private const uint CollectorStandardStatusId = 3911;

    public static CollectableState BuildState(AddonMaster.GatheringMasterpiece collectable)
    {
        return new CollectableState(
            CharacterLevel: Player.Level,
            AvailableGp: Utils.GetGp(),
            Progression: (int)collectable.CurrentCollectability,
            Attempts: (int)collectable.CurrentIntegrity,
            MaxAttempts: (int)collectable.TotalIntegrity,
            HasCollectorStandard: Utils.HasStatusId(CollectorStandardStatusId),
            HasScrutiny: Utils.HasStatusId(GathActionDict[GatherBuffId.Scrutiny].StatusId),
            HasEureka: Utils.HasStatusId(GathActionDict[GatherBuffId.BonusIntegrity_Chance].StatusId),
            MinCollectability: (int)collectable.MinCollectability,
            MidCollectability: (int)collectable.MidCollectability,
            HighCollectability: (int)collectable.HighCollectability,
            MeticulousPower: (int)collectable.MeticulousPower,
            ScourPower: (int)collectable.ScourPower,
            BrazenPowerMin: (int)collectable.BrazenPowerMin);
    }

    public static class CollectableSolver
    {
        // Lowest GP the 1000/700 rotation is meant to be used at.
        private const int ThousandGpMinGp = 700;

        // The 0 GP rotation can't realistically get past this collectability.
        private const int ZeroGpCeiling = 400;

        // Flat collectability Scrutiny adds to a build action (Scour 200 -> 450, Meticulous 150 -> 400).
        // This is 100% observation, and really... only with max stats
        // Nobody should be gathering w/o max gear >.>
        private const int ScrutinyBonus = 250;

        private const int MaxCollectability = 1000;

        /// <summary>
        /// Picks the rotation for a node from the GP you have right now. Call it once when the node starts.
        /// - 700+ GP: 1000/700.
        /// - Ephemeral node below that, and the item's minimum is 400 or less: 0 GP.
        /// - Otherwise: 400/600.
        /// </summary>
        public static CollectablePreset ChoosePreset(in CollectableState s, bool isEphemeral)
        {
            if (s.AvailableGp >= ThousandGpMinGp)
                return CollectablePreset.ThousandGp;

            if (isEphemeral && s.MinCollectability <= ZeroGpCeiling)
                return CollectablePreset.ZeroGpEphemeral;

            return CollectablePreset.FourHundredGpLowStat;
        }

        /// <summary>ActionID for the job that is currently being gathered by.</summary>
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

            // Thousand and FourHundred share the planner, only the ephemeral rotation is fixed.
            return preset == CollectablePreset.ZeroGpEphemeral
                ? ZeroGpEphemeral(s)
                : PlanNextAction(s);
        }

        #region Planner

        // Order matters: when two plans cost the same GP and take the same number of steps, the earlier action wins.
        // Meticulous goes first because it has a 25% chance of not using node durability (a free extra hit).
        // The search treats it as always using durability, so that chance is a bonus the re-plan picks up on the next tick.
        // We love free integrity no loss in this house
        private static readonly GatherBuffId[] BuildActions =
        {
            GatherBuffId.Meticulous,
            GatherBuffId.Scour,
            GatherBuffId.Brazen,
        };

        /// <summary>Result of a search: can the target be reached, what it costs, and which action starts the plan.</summary>
        private readonly record struct Plan(bool Reachable, int Gp, int Steps, GatherBuffId? First)
        {
            public static readonly Plan Unreachable = new(false, int.MaxValue, int.MaxValue, null);
            public static readonly Plan Done = new(true, 0, 0, null);
        }

        /// <summary>
        /// Picks the next action by planning, not by fixed thresholds:
        /// 1. Try the highest breakpoint ahead of us first, then lower ones.
        /// 2. A breakpoint only counts if the attempts (one is kept for Collect) and the GP can actually get there.
        /// 3. The cheapest plan in GP wins, so Scrutiny is only cast when a plain hit wouldn't do.
        /// 4. If nothing ahead is affordable, finish (top up an attempt only if worth it, then Collect) instead of burning GP.
        /// </summary>
        private static GatherBuffId? PlanNextAction(in CollectableState s)
        {
            var noPowerData = s.ScourPower <= 0 && s.MeticulousPower <= 0 && s.BrazenPowerMin <= 0;
            if (noPowerData) // this should NEVER happen... but on the off chance for some reason the addon breaks
                return Use(s, GatherBuffId.Scour, GatherBuffId.Meticulous);

            // Tiers are sorted lowest first, so walk backwards to try the highest breakpoint first.
            var tiers = s.Tiers;
            for (var i = tiers.Length - 1; i >= 0; i--)
            {
                if (tiers[i] <= s.Progression) // already reached this one, so every lower one is reached too
                    break;

                var plan = Search(s, tiers[i], s.Progression, s.Attempts, s.AvailableGp, s.HasScrutiny);
                if (plan.Reachable && plan.First is { } first)
                    return first;
            }

            // Nothing ahead is affordable (or we're past every breakpoint): done, if the game lets us collect.
            if (s.MeetsMinimum)
            {
                if (WorthTopUp(s) && Use(s, GatherBuffId.BonusIntegrity) is { } topUp)
                    return topUp;

                return Use(s, GatherBuffId.Collect);
            }

            // Still under the minimum and it isn't reachable on paper. Best effort: biggest hit we can afford. [Go big or go home]
            return BestEffortBuild(s);
        }

        /// <summary>
        /// Solid Reason / Ageless Words costs GP that could go into another node, so only buy the extra attempt<br></br>
        /// once at least 2/3 of the breakpoints (rounded up) are done: 1 of 1, 2 of 2, 2 of 3.<br></br>
        /// Doing this because IDEALLY you don't want to waste gp... and ephem nodes you typically want to go for the max, and collectables you want to ATLEAST aim for the 2nd break if not 3rd<br></br>
        /// usually 3rd, but if we're desperate... 
        /// </summary>
        private static bool WorthTopUp(in CollectableState s)
        {
            var tierCount = s.Tiers.Length;
            return tierCount > 0 && s.TiersReached >= (2 * tierCount + 2) / 3;
        }

        /// <summary>
        /// Cheapest way to reach <paramref name="target"/> from the given state, keeping one attempt for Collect.
        /// Scrutiny is a buff, so it costs GP but no attempt, and it only boosts the very next hit.
        /// </summary>
        private static Plan Search(in CollectableState s, int target, int progression, int attempts, int gp, bool scrutinyActive)
        {
            if (progression >= target) // already there
                return Plan.Done;

            if (attempts <= 1) // only the Collect attempt is left
                return Plan.Unreachable;

            var best = Plan.Unreachable;
            var scrutinyCost = Cost(GatherBuffId.Scrutiny);
            var canCastScrutiny = !scrutinyActive && IsAvailable(GatherBuffId.Scrutiny, s);

            foreach (var action in BuildActions)
            {
                if (!IsAvailable(action, s))
                    continue;

                var power = BasePower(action, s);

                // cast 0: hit as-is (already boosted if Scrutiny is active). cast 1: cast Scrutiny first, then hit.
                for (var cast = 0; cast < 2; cast++)
                {
                    var casting = cast == 1;
                    if (casting && !canCastScrutiny)
                        continue;

                    var spend = Cost(action) + (casting ? scrutinyCost : 0);
                    if (gp < spend) // can't afford this step
                        continue;

                    var boosted = scrutinyActive || casting;
                    var gain = power + (boosted ? ScrutinyBonus : 0);
                    var next = Search(s, target, Math.Min(progression + gain, MaxCollectability), attempts - 1, gp - spend, false);
                    Consider(ref best, next, spend, casting ? GatherBuffId.Scrutiny : action);
                }
            }

            return best;
        }

        /// <summary>Keeps the cheaper plan (GP first, then fewer steps).</summary>
        private static void Consider(ref Plan best, Plan next, int spentNow, GatherBuffId first)
        {
            if (!next.Reachable)
                return;

            var candidate = new Plan(true, spentNow + next.Gp, next.Steps + 1, first);

            var cheaper = candidate.Gp < best.Gp;
            var sameCostFewerSteps = candidate.Gp == best.Gp && candidate.Steps < best.Steps;

            if (!best.Reachable || cheaper || sameCostFewerSteps)
                best = candidate;
        }

        /// <summary>What an action gives with no Scrutiny.</summary>
        private static int BasePower(GatherBuffId action, in CollectableState s)
        {
            var shown = action switch
            {
                GatherBuffId.Scour => s.ScourPower,
                GatherBuffId.Meticulous => s.MeticulousPower,
                GatherBuffId.Brazen => s.BrazenPowerMin, // only the guaranteed part
                _ => 0,
            };

            return s.HasScrutiny ? Math.Max(shown - ScrutinyBonus, 0) : shown;
        }

        /// <summary>Used only when a breakpoint can't be reached on paper but we're still under the minimum.</summary>
        private static GatherBuffId? BestEffortBuild(in CollectableState s)
        {
            if (Use(s, GatherBuffId.Scrutiny) is { } scrutiny)
                return scrutiny;

            return s.MeticulousPower > s.ScourPower
                ? Use(s, GatherBuffId.Meticulous, GatherBuffId.Scour)
                : Use(s, GatherBuffId.Scour, GatherBuffId.Meticulous);
        }

        #endregion

        private static GatherBuffId? ZeroGpEphemeral(in CollectableState s)
        {
            // This rotation can't build past ~400, so collect once we're there (or out of attempts).
            var reachedStop = s.Progression >= Math.Min(s.TopTier, ZeroGpCeiling);
            var lastAttempt = s.Attempts <= 1;

            if ((reachedStop || lastAttempt) && Use(s, GatherBuffId.Collect) is { } collect)
                return collect;

            if (!reachedStop && s.HasCollectorStandard && Use(s, GatherBuffId.Meticulous) is { } meticulous)
                return meticulous;

            return Use(s, GatherBuffId.Scour);
        }

        #region Helpers

        private static int Cost(GatherBuffId buff) => (int)GathActionDict[buff].RequiredGp;

        private static GatherBuffId? Use(in CollectableState s, GatherBuffId a)
            => CanUse(a, s) ? a : null;

        private static GatherBuffId? Use(in CollectableState s, GatherBuffId a, GatherBuffId b)
            => Use(s, a) ?? Use(s, b);

        /// <summary>
        /// Level / buff requirements only. GP and attempts are tracked separately (by the search, or by CanUse).<br></br>
        /// IDEALLY THIS SHOULDN'T BE A PROBLEM. But i know this'll come bite me come EC SO JUST TO MAKE SURE
        /// </summary>
        private static bool IsAvailable(GatherBuffId buff, in CollectableState s)
        {
            if (!GathActionDict.TryGetValue(buff, out var entry))
                return false;

            if (s.CharacterLevel < entry.RequiredLv)
                return false;

            return buff != GatherBuffId.Brazen || s.HasCollectorStandard;
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
                GatherBuffId.BonusIntegrity => s.Attempts < s.MaxAttempts,
                GatherBuffId.BonusIntegrity_Chance => s.Attempts < s.MaxAttempts && s.HasEureka,
                GatherBuffId.Collect => s.Attempts > 0 && s.MeetsMinimum, // the game rejects Collect at 0 / under the minimum
                _ => s.Attempts > 0, // Scour, Brazen, Meticulous
            };
        }

        #endregion
    }
}