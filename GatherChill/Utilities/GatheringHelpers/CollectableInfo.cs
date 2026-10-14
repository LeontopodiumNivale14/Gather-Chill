using ECommons.ExcelServices;
using Lumina.Excel.Sheets;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Utilities.GatheringHelpers;

public static partial class Gather_Util
{

    public enum ScripType
    {
        Unknown,
        Purple,
        Orange,
    }

    /// <param name="MinQuality">Lowest collectability that earns this reward.</param>
    /// <param name="MaxQuality">Highest collectability that still earns this reward, or null if there is no cap.</param>
    public readonly record struct CollectableReward(int MinQuality, int? MaxQuality, int Scrips, long Experience);

    public sealed record CollectableInfo(
        uint ItemId,
        Job ClassJob,
        int ExpArrayIndex,
        string JobAbbreviation,
        int LevelMin,
        int LevelMax,
        ScripType ScripType,
        CollectableReward Low,
        CollectableReward Mid,
        CollectableReward High)
    {
        /// <summary>All reward tiers, ordered lowest to highest quality.</summary>
        public IReadOnlyList<CollectableReward> Rewards { get; } = [Low, Mid, High];

        public int MinScrips => Low.Scrips;
        public int MaxScrips => High.Scrips;
        public long MinExperience => Low.Experience;
        public long MaxExperience => High.Experience;

        public bool IsInLevelRange(int level) => LevelMin <= level && level <= LevelMax;

        public CollectableReward? GetRewardForQuality(int quality)
        {
            if (quality < Low.MinQuality) return null;   // below the lowest threshold = no reward
            if (quality < Mid.MinQuality) return Low;    // reached Low, not yet Mid
            if (quality < High.MinQuality) return Mid;   // reached Mid, not yet High
            return High;
        }
    }

    public static class CollectableDatabase
    {
        private static readonly Dictionary<uint, Job> ShopRowToClassJob = new()
        {
            [31] = Job.MIN,
            [32] = Job.BTN,
            [14] = Job.FSH,
        };

        private static Dictionary<uint, CollectableInfo> items = new();

        public static IReadOnlyDictionary<uint, CollectableInfo> Items => items;

        public static bool TryGet(uint itemId, out CollectableInfo info)
            => items.TryGetValue(itemId, out info!);

        public static void Build()
        {
            if (items.Count > 0) return;

            // ParamGrow row ID == level
            var expToNext = Svc.Data.GetExcelSheet<ParamGrow>().ToDictionary(row => (int)row.RowId, row => (long)row.ExpToNext);

            var classJobs = Svc.Data.GetExcelSheet<ClassJob>();
            var built = new Dictionary<uint, CollectableInfo>();

            foreach (var subrows in Svc.Data.GetSubrowExcelSheet<CollectablesShopItem>())
            {
                foreach (var entry in subrows)
                {
                    if (!ShopRowToClassJob.TryGetValue(entry.RowId, out var classJobId)) continue;
                    if (entry.Item.RowId == 0) continue;
                    if (entry.CollectablesShopRefine.RowId == 0) continue;
                    if (entry.CollectablesShopRewardScrip.RowId == 0) continue;
                    if (built.ContainsKey(entry.Item.RowId)) continue;

                    var levelMax = (int)entry.LevelMax;
                    if (!expToNext.TryGetValue(levelMax, out var levelExp)) continue;

                    var job = classJobs.GetRow((uint)classJobId);
                    var refine = entry.CollectablesShopRefine.Value;
                    var scrip = entry.CollectablesShopRewardScrip.Value;

                    // Each tier ends one point before the next tier starts.
                    // For example, Mid starting at 700 means Low covers up to 699.
                    int lowMin = refine.LowCollectability;
                    int midMin = refine.MidCollectability;
                    int highMin = refine.HighCollectability;

                    built[entry.Item.RowId] = new CollectableInfo(
                        ItemId: entry.Item.RowId,
                        ClassJob: classJobId,
                        ExpArrayIndex: job.ExpArrayIndex,
                        JobAbbreviation: job.Abbreviation.ToString(),
                        LevelMin: entry.LevelMin,
                        LevelMax: levelMax,
                        ScripType: GetScripType(scrip.Currency),
                        Low: new CollectableReward(
                            lowMin,
                            midMin - 1,
                            scrip.LowReward,
                            (long)scrip.ExpRatioLow * levelExp / 1000),
                        Mid: new CollectableReward(
                            midMin,
                            highMin - 1,
                            scrip.MidReward,
                            (long)scrip.ExpRatioMid * levelExp / 1000),
                        High: new CollectableReward(
                            highMin,
                            null,
                            scrip.HighReward,
                            (long)scrip.ExpRatioHigh * levelExp / 1000));
                }
            }

            items = built;
        }

        // I know i'm going to wonder wtf this is later so
        // It's really odd how they assign the values, but 2 lowest numbers are the craft/gather scripts (below lv. cap)
        // 2 highest numbers are the ones that are lv. cap scripts
        // So for DT, it was purple for lower, oragne for lv. cap
        private static ScripType GetScripType(int currency) => currency switch
        {
            2 or 4 => ScripType.Purple,
            6 or 7 => ScripType.Orange,
            _ => ScripType.Unknown,
        };
    }
}