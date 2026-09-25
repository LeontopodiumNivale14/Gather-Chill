using Dalamud.Interface.Textures;
using Dalamud.Interface.Textures.TextureWraps;
using GatherChill.Enums;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Utility;
using Lumina.Excel;
using Lumina.Excel.Sheets;
using System.Collections.Generic;

namespace GatherChill.Utilities.GatheringHelpers;

public static partial class Gather_Util
{
    public class GatherPointInfo
    {
        public uint Type { get; set; }
        public uint Level { get; set; }
        public uint TerritoryId { get; set; }
        public string ZoneName { get; set; }
        public string PlaceName { get; set; }
        public uint ExpId { get; set; }
        public string ExpansionName { get; set; }
        public SortedSet<uint> NodeIds { get; set; }
        public List<uint> ItemIds { get; set; }
        public int Radius { get; set; }
        public List<EorzeaTimeWindow> TimedInfo { get; set; } = new();
        public MapInfo Map { get; set; } = new();
        public FolkloreInfo Folklore { get; set; } = new();
    }
    public readonly struct EorzeaTimeWindow
    {
        public int Start { get; init; }  // raw HHMM, e.g. 800 = 8:00 ET
        public int End { get; init; }    // raw HHMM

        private int StartMinutes => Start / 100 * 60 + Start % 100;
        private int EndMinutes => End / 100 * 60 + End % 100;

        // Length of the window in Eorzea minutes
        public int DurationEt
            => (EndMinutes - StartMinutes + Utils.EorzeaMinutesPerDay) % Utils.EorzeaMinutesPerDay;

        public TimeSpan DurationReal => Utils.EorzeaMinutesToReal(DurationEt);

        public string StartFormatted => $"{Start / 100:D2}:{Start % 100:D2}";
        public string EndFormatted => $"{End / 100:D2}:{End % 100:D2}";

        public override string ToString() => $"{StartFormatted} – {EndFormatted} ET";

        public bool IsUp()
            => TimeUntilEnd() > TimeSpan.Zero;

        /// <summary>
        /// Real time left in this window, or TimeSpan.Zero if it isn't currently up.
        /// </summary>
        public TimeSpan TimeUntilEnd()
        {
            var now = Utils.CurrentEorzeaMinutes();
            var elapsed = (now - StartMinutes + Utils.EorzeaMinutesPerDay) % Utils.EorzeaMinutesPerDay;

            if (elapsed >= DurationEt)
                return TimeSpan.Zero;

            return Utils.EorzeaMinutesToReal(DurationEt - elapsed);
        }

        /// <summary>
        /// Real time until this window next opens, or TimeSpan.Zero if it's up right now.
        /// </summary>
        public TimeSpan TimeUntilStart()
        {
            var now = Utils.CurrentEorzeaMinutes();
            var wait = (StartMinutes - now + Utils.EorzeaMinutesPerDay) % Utils.EorzeaMinutesPerDay;

            // Currently inside the window: it's not "waiting" for anything.
            if (IsUp())
                return TimeSpan.Zero;

            return Utils.EorzeaMinutesToReal(wait);
        }
    }
    public readonly struct MapInfo
    {
        public float X { get; init; } // Raw X
        public float Y { get; init; } // Raw Y
        public int Radius { get; init; } // Radius
        public uint TerritoryId { get; init; } // Territory

        public void OpenMap(string? tooltip = "Node Location")
        {
            Utils.SetGatheringRing(TerritoryId, X, Y, Radius, tooltip);
        }
    }
    public class ExpacIcon
    {
        public string ExpacName { get; set; } = "";
        public uint IconId { get; set; } = 0;
        public IDalamudTextureWrap? Icon => Svc.Texture.GetFromGameIcon(IconId).GetWrapOrEmpty();
    }
    public class ItemClass
    {
        /// <summary>
        /// All the routeIds that are associated with this itemId
        /// </summary>
        public List<uint> RouteInfo { get; set; } = new();
        public ISharedImmediateTexture Icon { get; set; } = null;
        public uint IconId { get; set; }
        public string Name { get; set; } = "";
        public int Level { get; set; } = 1;
        public int Star { get; set; } = 0;
        public List<EorzeaTimeWindow> TimedSlots { get; set; } = new();
        public uint Expansion { get; set; } = 0;
    }
    public class FolkloreInfo
    {
        public Item SheetInfo { get; set; }
        public uint ItemId { get; set; } = 0;
        public ISharedImmediateTexture Icon { get; set; } = null;
        public uint IconId { get; set; } = 0;
        public string Name { get; set; } = "";
        public Item FolkloreSheet => ExcelHelper.Sheet_Item.GetRow(ItemId);
    }

    public static Dictionary<uint, uint> Job_IconIds = new()
    {
        [16] = 62510, // MIN
        [17] = 62511, // BTN,
        [18] = 62512, // FSH
    };

    public static Dictionary<uint, ISharedImmediateTexture> Sheet_JobInfo = new();
    public static Dictionary<ExpansionEnum, ExpacIcon> Sheet_Expansion = new();
    public static Dictionary<uint, GatherPointInfo> Sheet_RouteInfo = new();
    public static Dictionary<uint, ItemClass> Sheet_ItemInfo = new();

    public static void UpdateSheetInfo()
    {
        CreateInitialDictionaries();

        UpdateTimes();
        UpdateItemDictionary();
        UpdateItemLevel();
        UpdateJobIcons();
        UpdateExpansionIcon();
        UpdateFolklore();
        UpdateItemTimeSlots();
        UpdateItemExpansions();
    }

    // TO WHOEVER MIGHT BE READING THIS (including future you)
    // You might be wondering "man why did they separate all these into functions instead of just throwing it in one big one"
    // And to answer, it's easier for me to atleast sort out wtf went wrong where, and can atleast look at said function instead of just... 
    // Jumble fucking them all together and me losing track

    /// <summary>
    /// This is a headache and a half. And I think lost about 4 hours of time trying to figure out how to actually get this to work<br></br>
    /// Creates a dictionary that contains all the route + item info that a route would need. <br></br>
    /// This is created like this because there are hidden items that aren't normally attached to the routes, <br></br>
    /// and is a pain to loop back to the routes it's done to make sure all is caught
    /// </summary>
    private static void CreateInitialDictionaries()
    {
        var sheet_gatherPoint = ExcelHelper.Sheet_GatherPoint;

        // Pre-built dictionaries here, used to find the normal items + the hidden items
        var gatheringItemPointDict = ExcelHelper.Sheet_GatherItemPoint.Flatten()
            .GroupBy(gip => gip.GatheringPoint.RowId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.RowId).ToList());

        var gatheringPointDict = sheet_gatherPoint
            .Where(gp => gp.PlaceName.RowId > 0)
            .GroupBy(gp => gp.GatheringPointBase.RowId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.RowId).ToList());

        // Used to make sure we don't loop through an already-processed.... *-sighs-*
        var processedBases = new HashSet<uint>();

        foreach (var gatherPoint in sheet_gatherPoint)
        {
            var gatherPointBase = gatherPoint.GatheringPointBase.Value;
            uint routeId = gatherPointBase.RowId;

            // Skip if already processed this base
            if (processedBases.Contains(routeId))
                continue;
            processedBases.Add(routeId);

            if (gatherPointBase.GatheringLevel == 0)
                continue;

            uint type = 0;
            uint level = gatherPointBase.GatheringLevel;
            uint territoryId = 0;
            uint expansion = 0;
            string expansionName = "ARealmReborn";
            string zoneName = "???";
            string placeName = "???";
            List<uint> itemIds = new();
            int radius = 0;

            // Map gathering type
            uint baseType = gatherPointBase.GatheringType.Value.RowId;
            if (baseType is 0 or 1)
                type = 16;
            else if (baseType is 2 or 3)
                type = 17;
            else if (baseType is 4 or 5)
                type = 18;

            // Adding the base items 
            for (int i = 0; i <= 7; i++)
            {
                var gatheringItemId = gatherPointBase.Item[i].RowId;
                if (ExcelHelper.Sheet_GatheringItem.TryGetRow(gatheringItemId, out var gatherItem))
                {
                    var itemId = gatherItem.Item.RowId;
                    if (itemId != 0)
                        itemIds.Add(itemId);
                }
                else if (ExcelHelper.Sheet_SpearfishingItem.TryGetRow(gatheringItemId, out var spearfishItem))
                {
                    var itemId = spearfishItem.Item.RowId;
                    if (itemId != 0)
                        itemIds.Add(itemId);
                }
            }

            // THE PAIN POINT. Adding the hidden items.
            if (gatheringPointDict.TryGetValue(routeId, out var nodeList))
            {
                foreach (var gatherPointId in nodeList)
                {
                    if (gatheringItemPointDict.TryGetValue(gatherPointId, out var hiddenItemIds))
                    {
                        foreach (var gatheringItemId in hiddenItemIds)
                        {
                            if (ExcelHelper.Sheet_GatheringItem.TryGetRow(gatheringItemId, out var gatherItem))
                            {
                                var itemId = gatherItem.Item.RowId;
                                if (itemId != 0 && !itemIds.Contains(itemId))
                                    itemIds.Add(itemId);
                            }
                        }
                    }
                }
            }

            if (itemIds.Count == 0)
                continue;

            if (type == 18)
                IceLogging.Verbose($"RouteID: {routeId} | Item Count: {itemIds.Count()}");

            // Getting the associated territory info here
            var firstNodeId = nodeList?.FirstOrDefault() ?? 0;
            if (firstNodeId != 0 && sheet_gatherPoint.TryGetRow(firstNodeId, out var firstNode))
            {
                if (firstNode.TerritoryType.Value.RowId != 0)
                {
                    var territoryType = firstNode.TerritoryType.Value;
                    territoryId = territoryType.RowId;
                    if (territoryType.Map.IsValid)
                        zoneName = territoryType.PlaceName.Value.Name.ToString();

                    if (zoneName == string.Empty)
                        continue;

                    expansion = territoryType.ExVersion.Value.RowId;
                }

                if (firstNode.PlaceName.IsValid)
                    placeName = firstNode.PlaceName.Value.Name.ToString();
            }

            if (zoneName == "???")
                continue;

            switch (expansion)
            {
                case 0: expansionName = "ARealmReborn"; break;
                case 1: expansionName = "Heavensward"; break;
                case 2: expansionName = "Stormblood"; break;
                case 3: expansionName = "Shadowbringers"; break;
                case 4: expansionName = "Endwalker"; break;
                case 5: expansionName = "Dawntrail"; break;
                default: expansionName = "???"; break;
            }

            MapInfo mapInfo = new();
            if (ExcelHelper.Sheet_ExportedGatherPoints.TryGetRow(routeId, out var exportSheet))
            {
                mapInfo = new MapInfo
                {
                    X = exportSheet.X,
                    Y = exportSheet.Y,
                    Radius = exportSheet.Radius,
                    TerritoryId = territoryId,
                };
            }

            if (Ignore_Routes.Contains(routeId))
                continue;

            if (!Sheet_RouteInfo.ContainsKey(routeId))
            {
                Sheet_RouteInfo.Add(routeId, new GatherPointInfo()
                {
                    Type = type,
                    Level = level,
                    TerritoryId = territoryId,
                    ZoneName = zoneName,
                    PlaceName = placeName,
                    NodeIds = new SortedSet<uint>(nodeList ?? Enumerable.Empty<uint>()),
                    ItemIds = itemIds,
                    Radius = radius,
                    ExpId = expansion,
                    ExpansionName = expansionName,
                    Map = mapInfo,
                });
            }
        }
    }
    private static void UpdateTimes()
    {
        foreach (var route in Sheet_RouteInfo)
        {
            var windows = new List<EorzeaTimeWindow>();

            foreach (var nodeId in route.Value.NodeIds)
                windows.AddRange(GetTimeWindows(nodeId));

            route.Value.TimedInfo = MergeWindows(windows);
        }
    }
    private static List<EorzeaTimeWindow> GetTimeWindows(uint nodeId)
    {
        var timeInfo = new List<EorzeaTimeWindow>();

        if (!ExcelHelper.Sheet_GatherPointTransient.TryGetRow(nodeId, out var timedInfo))
            return timeInfo;

        var start = timedInfo.EphemeralStartTime;
        var end = timedInfo.EphemeralEndTime;

        // Ephemeral nodes: single window. 65535 is the "not set" sentinel.
        var hasEphemeral = start != 65535 && end != 65535 && start != end && start <= 2400 && end <= 2400;
        if (hasEphemeral)
        {
            timeInfo.Add(new() { Start = start, End = end });
            return timeInfo;
        }

        // Rare pop nodes: up to 3 windows
        if (timedInfo.GatheringRarePopTimeTable.RowId == 0)
            return timeInfo;

        var rareTime = timedInfo.GatheringRarePopTimeTable.Value;
        foreach (var (duration, startTime) in rareTime.Duration.Zip(rareTime.StartTime))
        {
            if (duration == 0)
                continue;

            // 160 is a data quirk that GatherBuddy treats as a 2 hour window.
            var actualDuration = duration == 160 ? 200 : duration;

            timeInfo.Add(new()
            {
                Start = startTime,
                End = (startTime + actualDuration) % 2400,
            });
        }

        return timeInfo;
    }
    private static List<EorzeaTimeWindow> MergeWindows(List<EorzeaTimeWindow> windows)
    {
        if (windows.Count <= 1)
            return windows;

        // One flag per Eorzea hour. Wraparound windows (e.g. 22 -> 02) fill naturally.
        var up = new bool[24];
        foreach (var window in windows)
        {
            for (var hour = window.Start / 100; hour != window.End / 100; hour = (hour + 1) % 24)
                up[hour] = true;
        }

        // Nothing up, or every hour up: nothing meaningful to merge.
        if (up.All(x => !x) || up.All(x => x))
            return windows;

        var merged = new List<EorzeaTimeWindow>();

        // Start on a down hour so a wrapping window isn't split in two.
        var first = Array.IndexOf(up, false);
        for (var i = 1; i <= 24; i++)
        {
            var hour = (first + i) % 24;
            if (!up[hour])
                continue;

            var start = hour;
            while (up[hour])
                hour = (hour + 1) % 24;

            merged.Add(new() { Start = start * 100, End = hour * 100 });
            i += (hour - start + 24) % 24;
        }

        return merged;
    }
    private static void UpdateItemDictionary()
    {
        // Item Dictionary Info Population
        foreach (var route in Sheet_RouteInfo)
        {
            var key = route.Key;
            if (Ignore_Routes.Contains(key))
            {
                IceLogging.Verbose($"Skipping Route: {key}", "Item Dictionary Update");
                continue;
            }
            else
            {
                IceLogging.Verbose($"Checking Items for route: {key}", "Item Dictionary Update");
            }

            var routeInfo = route.Value;
            foreach (var item in routeInfo.ItemIds)
            {
                if (Sheet_ItemInfo.TryGetValue(item, out var itemInfo))
                {
                    if (!itemInfo.RouteInfo.Contains(key))
                        itemInfo.RouteInfo.Add(key);
                }
                else
                {
                    if (ExcelHelper.Sheet_Item.TryGetRow(item, out var itemSheet))
                    {
                        var icon = Svc.Texture.GetFromGameIcon((int)itemSheet.Icon);
                        var name = itemSheet.Name.ToString();

                        Sheet_ItemInfo[item] = new()
                        {
                            Icon = icon,
                            Name = name,
                            IconId = itemSheet.Icon,
                            RouteInfo = new() { key },
                            // Level = itemLevel,
                            // Star = stars,
                        };
                    }
                }
            }
        }
    }
    private static void UpdateItemLevel()
    {
        // Updating all the item info to have the stars / levels assigned to them
        foreach (var item in Sheet_ItemInfo)
        {
            var gatherItem = ExcelHelper.Sheet_GatheringItem.FirstOrDefault(x => x.Item.RowId == item.Key);
            if (gatherItem.RowId != 0)
            {
                var levelSheet = gatherItem.GatheringItemLevel.Value;
                var itemLevel = (int)levelSheet.GatheringItemLevel;
                var stars = (int)levelSheet.Stars;

                item.Value.Level = itemLevel;
                item.Value.Star = stars;
            }
        }
    }
    private static void UpdateJobIcons()
    {
        // Updating Icon Dictionary here for quick usage
        foreach (var jobIcon in Job_IconIds)
        {
            if (Svc.Texture.TryGetFromGameIcon(jobIcon.Value, out var texture))
                Sheet_JobInfo.TryAdd(jobIcon.Key, texture);
        }
    }
    private static void UpdateExpansionIcon()
    {
        foreach (var expac in ExcelHelper.Sheet_Expansion)
        {
            var id = expac.RowId switch
            {
                0 => ExpansionEnum.ARR,
                1 => ExpansionEnum.HW,
                2 => ExpansionEnum.StB,
                3 => ExpansionEnum.ShB,
                4 => ExpansionEnum.EW,
                5 => ExpansionEnum.DT,
                _ => ExpansionEnum.ARR,
            };
            var name = expac.Name.ToString();
            var iconId = expac.Icon;

            Sheet_Expansion[id] = new()
            {
                ExpacName = name,
                IconId = iconId
            };
        }
    }
    private static void UpdateFolklore()
    {
        foreach (var route in Sheet_RouteInfo)
        {
            foreach (var nodeId in route.Value.NodeIds)
            {
                if (ExcelHelper.Sheet_GatherPoint.TryGetRow(nodeId, out var gatherpoint))
                {
                    if (gatherpoint.GatheringSubCategory.IsValid)
                    {
                        var gathersubCategory = gatherpoint.GatheringSubCategory;
                        if (gathersubCategory.Value.RowId != 0)
                        {
                            IceLogging.Verbose($"RouteId: {route.Key} is valid for folklore");

                            var item = ExcelHelper.Sheet_Item.GetRow(gathersubCategory.Value.Item.RowId);
                            if (item.RowId != 0)
                            {
                                FolkloreInfo itemInfo = new()
                                {
                                    SheetInfo = item,
                                    ItemId = item.RowId,
                                    Name = item.Name.ToString(),
                                    Icon = Svc.Texture.GetFromGameIcon((int)item.Icon),
                                    IconId = item.Icon
                                };
                                route.Value.Folklore = itemInfo;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
    private static void UpdateItemTimeSlots()
    {
        // Items that have permanent uptime for routes
        // These are ones that I don't *-want-* to be included in a timed list, but still have valid ids
        // AKA. Just crystals/shards [but still allowing clusers]
        List<uint> ignoreList = new() { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

        foreach (var item in Sheet_ItemInfo)
        {
            if (ignoreList.Contains(item.Key))
                continue;

            List<EorzeaTimeWindow> uptimes = new();
            var validRoutes = Sheet_RouteInfo.Where(x => x.Value.ItemIds.Contains(item.Key)).ToList();

            foreach (var route in validRoutes)
            {
                foreach (var time in route.Value.TimedInfo)
                {
                    if (!uptimes.Contains(time))
                        uptimes.Add(time);
                }

                item.Value.TimedSlots = uptimes;
            }
        }
    }
    private static void UpdateItemExpansions()
    {
        // Updating this to assign each item an expansion when it first appeared
        foreach (var item in Sheet_ItemInfo)
        {
            var firstRoute = Sheet_RouteInfo.Where(x => x.Value.ItemIds.Contains(item.Key)).FirstOrDefault();
            item.Value.Expansion = firstRoute.Value.ExpId;
        }
    }

    private static ExpansionEnum ExpansionConverter(uint Ex)
    {
        return Ex switch
        {
            0 => ExpansionEnum.ARR,
            1 => ExpansionEnum.HW,
            2 => ExpansionEnum.StB,
            3 => ExpansionEnum.ShB,
            4 => ExpansionEnum.EW,
            5 => ExpansionEnum.DT,
            _ => ExpansionEnum.ARR,
        };
    }
}
