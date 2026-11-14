using ECommons.ExcelServices;
using ECommons.GameHelpers;
using ECommons.UIHelpers.AddonMasterImplementations;
using GatherChill.Enums;
using GatherChill.Utilities.Utility;
using System.Collections.Generic;

namespace GatherChill.Utilities.GatheringHelpers;

public static partial class Gather_Util
{
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
        int BrazenPowerMin = 0,
        int RemainingNeeded = 0,   // items still needed for the goal, 0 = unknown / ignore (reducible items)
        CollectableTarget Target = CollectableTarget.High) // which breakpoint the user wants to aim for
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

        /// <summary>
        /// Index into Tiers of the breakpoint the setting asks for, or -1 if the item has none.
        /// High = last, Low = first, Middle = the middle one (with 2 tiers that's the higher one).
        /// An item with fewer tiers than the setting expects just uses what it has.
        /// </summary>
        public int TargetIndex
        {
            get
            {
                var count = Tiers.Length;
                if (count == 0)
                    return -1;

                return Target switch
                {
                    CollectableTarget.Low => 0,
                    CollectableTarget.Middle => count / 2,
                    _ => count - 1,
                };
            }
        }

        /// <summary>The breakpoint value the setting asks for, or 0 if the item has none.</summary>
        public int TargetTier
        {
            get
            {
                var index = TargetIndex;
                return index < 0 ? 0 : Tiers[index];
            }
        }

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

    /// <summary>
    /// What the solver decided: which breakpoint it is aiming for, how much GP the cheapest route needs,
    /// and the action to press right now. TargetTier == 0 means no breakpoint is reachable (free hits only).
    /// </summary>
    public readonly record struct CollectablePlan(int TargetTier, int GpCost, GatherBuffId? NextAction)
    {
        public bool Feasible => TargetTier > 0;
    }

    private const uint CollectorStandardStatusId = 2418;     // White Star, steers toward Meticulous
    private const uint CollectorHighStandardStatusId = 3911; // Orange Buff, unlocks Brazen

    private static bool HasStatus(GatherBuffId buff)
        => GathActionDict.TryGetValue(buff, out var entry) && Utils.HasStatusId(entry.StatusId);

    public static CollectableState BuildState(AddonMaster.GatheringMasterpiece collectable, int remainingNeeded = 0, CollectableTarget target = CollectableTarget.High)
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
            BrazenPowerMin: (int)collectable.BrazenPowerMin,
            RemainingNeeded: remainingNeeded,
            Target: target);
    }

    public static class CollectableSolver
    {
        // ---- Planner assumptions. Tune these if the real numbers differ. ----

        /// <summary>Scrutiny multiplies hit power by this while it's active.</summary>
        private const double ScrutinyMultiplier = 1.5;

        /// <summary>How many hits one Scrutiny cast covers (assumed, check against the game).</summary>
        private const int ScrutinyHits = 3;

        /// <summary>Attempts are tracked in quarters so Meticulous' free-hit chance can be priced as an expected cost.</summary>
        private const int UnitsPerAttempt = 4;

        /// <summary>Expected attempt cost of one Meticulous hit: 3/4 of an attempt (25% chance it's free).</summary>
        private const int MeticulousUnits = 3;

        /// <summary>Expected attempt cost of a Meticulous hit under Priming Touch: 2/4 of an attempt (chance doubled to 50%).</summary>
        private const int PrimedMeticulousUnits = 2;

        /// <summary>Priming Touch affects the next use of Meticulous only.</summary>
        private const int PrimingHits = 1;

        /// <summary>Hard cap on planned actions, keeps the search small and stops Solid Reason loops.</summary>
        private const int MaxPlanSteps = 12;

        private readonly record struct SimState(
            int Progression,
            int Units,         // attempts left, in quarters (Attempts * UnitsPerAttempt)
            int Gp,
            int ScrutinyLeft,
            int PrimingLeft,   // Meticulous hits that still get the doubled free-hit chance
            bool HighStandard, // swirl proc, only good for the first hit
            int StepsLeft);

        private readonly record struct Powers(int Meticulous, int Scour, int Brazen);

        private readonly record struct PlanCost(int Gp, int Steps)
        {
            public static readonly PlanCost Infeasible = new(int.MaxValue, int.MaxValue);

            public bool IsInfeasible => Gp == int.MaxValue;

            /// <summary>Cheaper GP wins, then fewer actions.</summary>
            public bool IsBetterThan(PlanCost other)
                => Gp < other.Gp || (Gp == other.Gp && Steps < other.Steps);
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

        /// <summary>Best next action as a GatherBuffId, or null when nothing is usable.</summary>
        public static GatherBuffId? GetNextAction(in CollectableState s)
        {
            // Wise to the World comes first in every rotation.
            if (CanUse(GatherBuffId.BonusIntegrity_Chance, s))
                return GatherBuffId.BonusIntegrity_Chance;

            var plan = GetPlan(s);

            // Nothing reachable: spend no GP. Collect if the game allows it, otherwise keep hitting for free.
            if (!plan.Feasible)
                return s.MeetsMinimum ? Use(s, GatherBuffId.Collect) : FreeHit(s);

            if (plan.NextAction is not { } action)
                return null;

            if (action != GatherBuffId.Collect)
                return CanUse(action, s) ? action : FreeHit(s);

            // Target reached: Collect, buying one more attempt first when that's worth it.
            return CollectOrTopUp(s);
        }

        /// <summary>
        /// At the selected breakpoint nothing more is wanted, so spare GP goes into one more attempt (one more item)
        /// whenever we're down to the last attempt. Below it the GP might still be needed, so only top up once
        /// most of the wanted breakpoints are done. Never top up when the attempts left already cover every
        /// item we still need.
        /// </summary>
        private static GatherBuffId? CollectOrTopUp(in CollectableState s)
        {
            // Each Collect is one attempt and one item, so enough attempts left means an extra one is wasted GP.
            // RemainingNeeded == 0 means unknown (reducible items), so this guard is skipped for them.
            if (s.RemainingNeeded > 0 && s.Attempts >= s.RemainingNeeded)
                return Use(s, GatherBuffId.Collect);

            var targetTier = s.TargetTier;
            var atTargetTier = targetTier > 0 && s.Progression >= targetTier;
            var lastAttempt = s.Attempts <= 1;

            var wantTopUp = atTargetTier ? lastAttempt : WorthTopUp(s);
            if (wantTopUp && Use(s, GatherBuffId.BonusIntegrity) is { } topUp)
                return topUp;

            return Use(s, GatherBuffId.Collect);
        }

        /// <summary>
        /// Looks at every breakpoint the item has, highest first, and returns the first one that is reachable
        /// with the GP and attempts we have right now, along with the cheapest route to it.
        /// </summary>
        public static CollectablePlan GetPlan(in CollectableState s)
        {
            var tiers = s.Tiers;
            if (tiers.Length == 0)
                return new CollectablePlan(0, 0, null);

            // Powers from the addon already include Scrutiny if it's up, so strip it back out to get the base.
            var divisor = s.HasScrutiny ? ScrutinyMultiplier : 1.0;
            var powers = new Powers(
                (int)(s.MeticulousPower / divisor),
                (int)(s.ScourPower / divisor),
                (int)(s.BrazenPowerMin / divisor));

            var start = new SimState(
                s.Progression,
                s.Attempts * UnitsPerAttempt,
                s.AvailableGp,
                s.HasScrutiny ? ScrutinyHits : 0,
                s.HasPriming ? PrimingHits : 0,
                s.HasHighStandard,
                MaxPlanSteps);

            // tiers is sorted lowest first. Start at the selected tier and walk down toward the lowest.
            for (var i = s.TargetIndex; i >= 0; i--)
            {
                var memo = new Dictionary<SimState, (PlanCost Cost, GatherBuffId? First)>();
                var (cost, first) = Solve(start, tiers[i], powers, s.CharacterLevel, s.MaxAttempts, memo);

                if (cost.IsInfeasible)
                    continue;

                return new CollectablePlan(tiers[i], cost.Gp, first);
            }

            return new CollectablePlan(0, 0, null);
        }

        #region Planner

        /// <summary>
        /// Cheapest (GP, then fewest actions) way to get Progression to the target while still having a full attempt
        /// left to Collect with. Meticulous costs 3/4 of an attempt (2/4 when primed), other hits a full one.
        /// Scrutiny, Priming Touch and Solid Reason cost GP only: the two buffs never take durability.
        /// </summary>
        private static (PlanCost Cost, GatherBuffId? First) Solve(
            SimState st, int target, Powers p, int level, int maxAttempts,
            Dictionary<SimState, (PlanCost Cost, GatherBuffId? First)> memo)
        {
            // Target reached (>= means "at or above") and we still have a whole attempt to Collect with.
            if (st.Progression >= target && st.Units >= UnitsPerAttempt)
                return (new PlanCost(0, 0), GatherBuffId.Collect);

            // Node is over (<= means "at or below zero") or out of planning budget.
            if (st.Units <= 0 || st.StepsLeft <= 0)
                return (PlanCost.Infeasible, null);

            if (memo.TryGetValue(st, out var cached))
                return cached;

            var bestCost = PlanCost.Infeasible;
            GatherBuffId? bestFirst = null;

            void Try(GatherBuffId action, SimState next, int gpSpent)
            {
                var (sub, _) = Solve(next, target, p, level, maxAttempts, memo);
                if (sub.IsInfeasible)
                    return;

                var total = new PlanCost(sub.Gp + gpSpent, sub.Steps + 1);
                if (!total.IsBetterThan(bestCost))
                    return;

                bestCost = total;
                bestFirst = action;
            }

            // Hits. Free ones get tried first so they win ties.
            var scrutinyBonus = st.ScrutinyLeft > 0 ? ScrutinyMultiplier : 1.0;
            foreach (var (hit, basePower) in new[]
            {
                (GatherBuffId.Meticulous, p.Meticulous),
                (GatherBuffId.Scour, p.Scour),
                (GatherBuffId.Brazen, p.Brazen),
            })
            {
                if (basePower <= 0 || !LevelOk(hit, level))
                    continue;

                if (hit == GatherBuffId.Brazen && !st.HighStandard)
                    continue;

                var gp = Cost(hit);
                if (st.Gp < gp)
                    continue;

                var primed = hit == GatherBuffId.Meticulous && st.PrimingLeft > 0;
                var unitCost = hit == GatherBuffId.Meticulous
                    ? (primed ? PrimedMeticulousUnits : MeticulousUnits)
                    : UnitsPerAttempt;

                if (st.Units < unitCost)
                    continue;

                Try(hit, st with
                {
                    Progression = st.Progression + (int)(basePower * scrutinyBonus),
                    Units = st.Units - unitCost,
                    Gp = st.Gp - gp,
                    ScrutinyLeft = Math.Max(0, st.ScrutinyLeft - 1),
                    PrimingLeft = primed ? st.PrimingLeft - 1 : st.PrimingLeft,
                    HighStandard = false,
                    StepsLeft = st.StepsLeft - 1,
                }, gp);
            }

            // Scrutiny: a buff, costs GP only.
            if (st.ScrutinyLeft == 0 && LevelOk(GatherBuffId.Scrutiny, level))
            {
                var gp = Cost(GatherBuffId.Scrutiny);
                if (st.Gp >= gp)
                {
                    Try(GatherBuffId.Scrutiny, st with
                    {
                        Gp = st.Gp - gp,
                        ScrutinyLeft = ScrutinyHits,
                        StepsLeft = st.StepsLeft - 1,
                    }, gp);
                }
            }

            // Priming Touch: a buff, costs GP only. Doubles the free-hit chance of the next Meticulous.
            // Pointless without Meticulous.
            if (st.PrimingLeft == 0 && p.Meticulous > 0 && LevelOk(GatherBuffId.Priming, level))
            {
                var gp = Cost(GatherBuffId.Priming);
                if (st.Gp >= gp)
                {
                    Try(GatherBuffId.Priming, st with
                    {
                        Gp = st.Gp - gp,
                        PrimingLeft = PrimingHits,
                        StepsLeft = st.StepsLeft - 1,
                    }, gp);
                }
            }

            // Solid Reason / Ageless Words: buys one attempt back (capped at the node's max).
            if (st.Units < maxAttempts * UnitsPerAttempt && LevelOk(GatherBuffId.BonusIntegrity, level))
            {
                var gp = Cost(GatherBuffId.BonusIntegrity);
                if (st.Gp >= gp)
                {
                    Try(GatherBuffId.BonusIntegrity, st with
                    {
                        Units = Math.Min(maxAttempts * UnitsPerAttempt, st.Units + UnitsPerAttempt),
                        Gp = st.Gp - gp,
                        StepsLeft = st.StepsLeft - 1,
                    }, gp);
                }
            }

            var result = (bestCost, bestFirst);
            memo[st] = result;
            return result;
        }

        #endregion

        #region Fallback and top-up

        /// <summary>The strongest hit that costs no GP, for when no breakpoint is reachable.</summary>
        private static GatherBuffId? FreeHit(in CollectableState s)
        {
            GatherBuffId? best = null;
            var bestPower = 0;

            foreach (var (hit, power) in new[]
            {
                (GatherBuffId.Meticulous, s.MeticulousPower),
                (GatherBuffId.Scour, s.ScourPower),
                (GatherBuffId.Brazen, s.BrazenPowerMin),
            })
            {
                if (Cost(hit) > 0 || !CanUse(hit, s))
                    continue;

                if (power <= bestPower) // keep only a strictly stronger hit
                    continue;

                best = hit;
                bestPower = power;
            }

            // No power data from the addon: any free hit is better than doing nothing.
            return best ?? Use(s, GatherBuffId.Scour, GatherBuffId.Meticulous);
        }

        /// <summary>
        /// Solid Reason / Ageless Words costs GP that could go into another node, so below the top breakpoint
        /// only buy the extra attempt once at least 2/3 of the breakpoints (rounded up) are done: 1 of 1, 2 of 2, 2 of 3.
        /// </summary>
        private static bool WorthTopUp(in CollectableState s)
        {
            var wantedCount = s.TargetIndex + 1;
            if (wantedCount <= 0)
                return false;

            var reached = Math.Min(s.TiersReached, wantedCount);
            return reached >= (2 * wantedCount + 2) / 3;
        }

        #endregion

        #region Helpers

        private static int Cost(GatherBuffId buff) => (int)GathActionDict[buff].RequiredGp;

        private static GatherBuffId? Use(in CollectableState s, GatherBuffId a)
            => CanUse(a, s) ? a : null;

        private static GatherBuffId? Use(in CollectableState s, GatherBuffId a, GatherBuffId b)
            => Use(s, a) ?? Use(s, b);

        private static bool LevelOk(GatherBuffId buff, int level)
            => GathActionDict.TryGetValue(buff, out var entry) && level >= entry.RequiredLv;

        /// <summary>Level / buff requirements only.</summary>
        private static bool IsAvailable(GatherBuffId buff, in CollectableState s)
        {
            if (!LevelOk(buff, s.CharacterLevel))
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
                GatherBuffId.Priming => !s.HasPriming && s.Attempts > 0, // buff costs no durability, Attempts > 0 just means the node is still alive
                GatherBuffId.BonusIntegrity => s.Attempts < s.MaxAttempts,
                GatherBuffId.BonusIntegrity_Chance => s.Attempts < s.MaxAttempts && s.HasEureka,
                GatherBuffId.Collect => s.Attempts > 0 && s.MeetsMinimum, // the game rejects Collect at 0 / under the minimum
                _ => s.Attempts > 0, // Scour, Brazen, Meticulous
            };
        }

        #endregion
    }
}