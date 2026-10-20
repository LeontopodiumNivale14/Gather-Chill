using ECommons.GameHelpers;
using GatherChill.Enums;
using GatherChill.Utilities.Utility;
using System.Collections.Generic;

namespace GatherChill.Utilities.GatheringHelpers;

public static partial class Gather_Util
{
    // Credit goes where it's due. Used this as the baseline for getting the math... well math'd out
    // https://github.com/fdashx/FFXIVGathering

    /// <summary>
    /// Returns a plan that is both the cheapest GP buff list, while also giving the greatest yield of items from the current state it's in <br></br>
    /// Profile = Is the profile that a user can edit to select which buffs they do/don't want to use. <br></br>
    /// baseYield = 1 | Default. Until I can be asked to get the amount of items that you would get from that node <br></br>
    /// If the the amount of items needed can be gotten with no buffs, then it'll just return a clean profile. Nothing at all
    /// If it can't be reached, it'll just use the best instance of buffs
    /// </summary>
    public static (GatherProfile Profile, double ExpectedYield, int Gp, bool MeetsNeed) PlanBuffs(
        int level,
        int gp,
        int attempts,
        int gatherChance,
        int boonChance,
        int itemsNeeded,
        ProfileClass profile,
        int baseYield = 1,
        string name = "Planned")
    {
        var bountifulBonus = profile.BYII_Amount;
        var tidingsMinBoon = profile.TidingsChance;
        bool hasWise = Utils.HasStatusId(GathActionDict[GatherBuffId.BonusIntegrity_Chance].StatusId);

        var node = new BuffPlanner.NodeState(
            level,
            gp,
            attempts,
            gatherChance / 100.0,
            boonChance / 100.0,
            baseYield,
            Math.Clamp(bountifulBonus, 1, 3),
            hasWise);

        // boonChance >= tidingsMinBoon reads "boon is at least the minimum", which is when Tidings is worth considering
        var tidingsAllowed = boonChance >= tidingsMinBoon;

        bool Usable(GatherBuffId id) => (id != GatherBuffId.Tidings || tidingsAllowed) && (profile.BuffsEnabled.GetValueOrDefault(id, true));

        var plan = BuffPlanner.Plan(node, itemsNeeded, Usable);

        return (BuffPlanner.ToProfile(plan, name, node.Wise, Usable), plan.ExpectedYield, plan.Gp, plan.MeetsNeed);
    }

    private static class BuffPlanner
    {
        private const double Epsilon = 1e-9;
        private const int MaxAgelessCasts = 10;

        public enum BuffKind { Chance, Yield, Boon, Tidings, Bountiful, Ageless }

        /// <summary>
        /// Amount meaning depends on the kind:
        /// Chance = added gather chance, Boon = added boon chance, Yield = added base yield,
        /// Tidings = added boon bonus, Bountiful = extra yield on the attempt it's cast for (BY I value; see NodeState for 68+),
        /// Ageless = extra attempts.
        /// </summary>
        public sealed record GatherBuff(GatherBuffId Id, BuffKind Kind, int Level, int Gp, double Amount);

        public sealed record NodeState(int Level, int Gp, int Attempts, double Chance, double Boon, int BaseYield, double BountifulBonus = 1, bool? HasWise = null)
        {
            public bool Wise => HasWise ?? Level >= GatherBuffs.WiseLevel;

            // BY I is always +1; BY II's bonus varies with the node, so it comes from the user's setting.
            public double EffectiveBountifulBonus => Level >= GatherBuffs.BountifulIILevel ? BountifulBonus : GatherBuffs.Bountiful.Amount;

            public int GpRegenPerAttempt => Level < 80 ? 5 : 6;
        }

        public sealed record GatherPlan(IReadOnlyList<GatherBuffId> Buffs, double ExpectedYield, int Gp, bool MeetsNeed);

        private readonly record struct Loadout(IReadOnlyList<GatherBuff> OneShots, int Ageless, int Bountiful);

        public static class GatherBuffs
        {
            public const int WiseLevel = 90;
            public const int BountifulIILevel = 68;
            public const double WiseChance = 0.5;
            public const int BaseBoonBonus = 1;

            // Only one tier of each of these can be active at once (they share a status), so the planner picks at most one.
            public static readonly IReadOnlyList<GatherBuff> ChanceTiers = new[]
            {
                new GatherBuff(GatherBuffId.FieldMasteryI, BuffKind.Chance, 4, 50, 0.05),
                new GatherBuff(GatherBuffId.FieldMasteryII, BuffKind.Chance, 5, 100, 0.15),
                new GatherBuff(GatherBuffId.FieldMasteryIII, BuffKind.Chance, 10, 250, 0.50),
            };

            public static readonly IReadOnlyList<GatherBuff> YieldTiers = new[]
            {
                new GatherBuff(GatherBuffId.YieldI, BuffKind.Yield, 30, 400, 1),
                new GatherBuff(GatherBuffId.YieldII, BuffKind.Yield, 40, 500, 2),
            };

            // Separate statuses, so both can be active together.
            public static readonly GatherBuff GiftI = new(GatherBuffId.BoonIncrease_1, BuffKind.Boon, 15, 50, 0.10);
            public static readonly GatherBuff GiftII = new(GatherBuffId.BoonIncrease_2, BuffKind.Boon, 50, 100, 0.30);

            public static readonly GatherBuff Tidings = new(GatherBuffId.Tidings, BuffKind.Tidings, 81, 200, 1);

            // Repeatable. Bountiful only lasts for the next gather attempt, so each cast covers one attempt.
            // BY I and BY II are the same action (it upgrades at 68), so there is a single entry; Amount is the BY I bonus.
            public static readonly GatherBuff Bountiful = new(GatherBuffId.BYII, BuffKind.Bountiful, 24, 100, 1);
            public static readonly GatherBuff AgelessWords = new(GatherBuffId.BonusIntegrity, BuffKind.Ageless, 25, 300, 1);

            // Each family contributes at most one buff to a loadout. Order here is the cast order.
            public static readonly IReadOnlyList<IReadOnlyList<GatherBuff>> OneShotFamilies = new IReadOnlyList<GatherBuff>[]
            {
                ChanceTiers,
                YieldTiers,
                new[] { GiftI },
                new[] { GiftII },
                new[] { Tidings },
            };
        }

        public static class YieldModel
        {
            public static double Expected(double chance, double boon, int boonBonus, int baseYield, int attempts, int wiseRolls, int bountifulCasts, double bountifulBonus = 1)
            {
                var perAttempt = baseYield + boon * boonBonus;
                var probability = Math.Pow(1 - GatherBuffs.WiseChance, wiseRolls); // P(0 successes)
                var total = 0d;

                for (var successes = 0; successes <= wiseRolls; successes++)
                {
                    var totalAttempts = attempts + successes;
                    var items = perAttempt * totalAttempts + bountifulBonus * Math.Min(totalAttempts, bountifulCasts);
                    total += probability * items * chance;

                    // P(k + 1) = P(k) * (n - k) / (k + 1) * p / (1 - p), and p / (1 - p) = 1 for p = 0.5
                    probability = probability * (wiseRolls - successes) / (successes + 1);
                }

                return total;
            }
        }

        /// <param name="isUsable">Respects the user's toggle for the Wise proc buff. Null allows it.</param>
        public static GatherProfile ToProfile(GatherPlan plan, string name, bool wise, Func<GatherBuffId, bool>? isUsable = null)
        {
            var profile = new GatherProfile { Name = name };

            foreach (var group in plan.Buffs.GroupBy(buff => buff))
            {
                profile.GatheringBuffs[group.Key] = new BuffSettings
                {
                    Enabled = true,
                    MaxUse = (uint)group.Count(),
                };
            }

            var priority = plan.Buffs.Distinct().ToList();

            var agelessIndex = priority.IndexOf(GatherBuffs.AgelessWords.Id);

            if (agelessIndex < 0 || !wise)
            {
                profile.BuffPriority = priority;
                return profile;
            }

            if (isUsable?.Invoke(GatherBuffId.BonusIntegrity_Chance) ?? true)
            {
                profile.GatheringBuffs[GatherBuffId.BonusIntegrity_Chance] = new BuffSettings { Enabled = true };
                priority.Insert(agelessIndex, GatherBuffId.BonusIntegrity_Chance);
            }

            profile.BuffPriority = priority;
            return profile;
        }

        public static GatherPlan Plan(NodeState node, int itemsNeeded, Func<GatherBuffId, bool>? isUsable = null)
        {
            isUsable ??= _ => true;
            node = node with { Gp = Math.Max(node.Gp, 0) };

            var best = Evaluate(node, new Loadout(Array.Empty<GatherBuff>(), 0, 0), itemsNeeded)!;

            if (best.MeetsNeed)
            {
                return best;
            }

            var maxAgeless = IsAvailable(GatherBuffs.AgelessWords, node, isUsable) ? MaxAgelessCasts : 0;
            var canBountiful = IsAvailable(GatherBuffs.Bountiful, node, isUsable);

            foreach (var oneShots in OneShotCombos(node, isUsable))
            {
                for (var ageless = 0; ageless <= maxAgeless; ageless++)
                {
                    var maxBountiful = canBountiful ? node.Attempts + ageless : 0;

                    for (var bountiful = 0; bountiful <= maxBountiful; bountiful++)
                    {
                        var candidate = Evaluate(node, new Loadout(oneShots, ageless, bountiful), itemsNeeded);

                        // every extra cast only costs more, so once a loadout can't be paid for, bigger ones can't either
                        if (candidate == null)
                        {
                            break;
                        }

                        if (IsBetter(candidate, best))
                        {
                            best = candidate;
                        }
                    }
                }
            }

            return best;
        }

        private static List<List<GatherBuff>> OneShotCombos(NodeState node, Func<GatherBuffId, bool> isUsable)
        {
            var combos = new List<List<GatherBuff>> { new() };

            foreach (var family in GatherBuffs.OneShotFamilies)
            {
                var tiers = family.Where(tier => IsAvailable(tier, node, isUsable)).ToList();

                combos = combos
                    .SelectMany(combo => tiers
                        .Select(tier => new List<GatherBuff>(combo) { tier })
                        .Prepend(combo)) // Prepend = the "none of this family" option
                    .ToList();
            }

            return combos;
        }

        private static GatherPlan? Evaluate(NodeState node, Loadout loadout, int itemsNeeded)
        {
            var oneShotGp = loadout.OneShots.Sum(buff => buff.Gp);
            var remainingGp = node.Gp - oneShotGp;

            if (remainingGp < 0)
            {
                return null;
            }

            var attempts = node.Attempts;

            for (var i = 0; i < loadout.Ageless; i++)
            {
                if (!TryPay(GatherBuffs.AgelessWords, ref remainingGp, attempts, node))
                {
                    return null;
                }

                attempts++;
            }

            for (var i = 0; i < loadout.Bountiful; i++)
            {
                if (!TryPay(GatherBuffs.Bountiful, ref remainingGp, attempts, node))
                {
                    return null;
                }
            }

            double Added(BuffKind kind) => loadout.OneShots.Where(buff => buff.Kind == kind).Sum(buff => buff.Amount);

            var chance = Math.Min(node.Chance + Added(BuffKind.Chance), 1);
            var boon = Math.Min(node.Boon + Added(BuffKind.Boon), 1);
            var boonBonus = GatherBuffs.BaseBoonBonus + (int)Added(BuffKind.Tidings);
            var baseYield = node.BaseYield + (int)Added(BuffKind.Yield);
            var wiseRolls = node.Wise ? loadout.Ageless : 0;

            var expected = YieldModel.Expected(chance, boon, boonBonus, baseYield, attempts, wiseRolls, loadout.Bountiful, node.EffectiveBountifulBonus);

            var gp = oneShotGp
                   + loadout.Ageless * GatherBuffs.AgelessWords.Gp
                   + loadout.Bountiful * GatherBuffs.Bountiful.Gp;

            return new GatherPlan(BuildBuffList(loadout), expected, gp, expected + Epsilon >= itemsNeeded);
        }

        private static bool TryPay(GatherBuff buff, ref int remainingGp, int attempts, NodeState node)
        {
            var available = remainingGp + Math.Max(attempts - 1, 0) * node.GpRegenPerAttempt;

            if (available < buff.Gp)
            {
                return false;
            }

            remainingGp -= buff.Gp;
            return true;
        }

        private static bool IsBetter(GatherPlan candidate, GatherPlan best)
        {
            if (candidate.MeetsNeed != best.MeetsNeed)
            {
                return candidate.MeetsNeed;
            }

            if (candidate.MeetsNeed)
            {
                // both reach the goal: fewer GP wins, then more yield
                if (candidate.Gp != best.Gp)
                {
                    return candidate.Gp < best.Gp;
                }

                return candidate.ExpectedYield > best.ExpectedYield + Epsilon;
            }

            // neither reaches the goal: more yield wins, then fewer GP
            if (Math.Abs(candidate.ExpectedYield - best.ExpectedYield) > Epsilon)
            {
                return candidate.ExpectedYield > best.ExpectedYield;
            }

            return candidate.Gp < best.Gp;
        }

        private static IReadOnlyList<GatherBuffId> BuildBuffList(Loadout loadout)
        {
            return
            [
                .. loadout.OneShots.Select(buff => buff.Id),
                .. Enumerable.Repeat(GatherBuffs.AgelessWords.Id, loadout.Ageless),
                .. Enumerable.Repeat(GatherBuffs.Bountiful.Id, loadout.Bountiful),
            ];
        }

        private static bool IsAvailable(GatherBuff buff, NodeState node, Func<GatherBuffId, bool> isUsable)
        {
            return node.Level >= buff.Level && isUsable(buff.Id);
        }
    }
}