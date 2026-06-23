using ECommons.Logging;
using GatherChill.Enums;
using GatherChill.Items;
using Dalamud.Interface.Textures;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Utility;
using Lumina.Excel.Sheets;
using Pictomancy;
using System.Collections.Generic;

namespace GatherChill.Utilities.GatheringHelpers;

public static partial class Gather_Util
{
    public class GatherPointInfo
    {
        public CombinedGatherType Type { get; set; }
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
    }
    public readonly struct EorzeaTimeWindow
    {
        public int Start { get; init; }  // raw ET minutes, e.g. 800 = 8:00 ET
        public int End { get; init; }    // raw ET minutes

        // How long the window lasts in Eorzea minutes
        public int DurationEt => End - Start;

        // Convert to real-world TimeSpan for display
        public TimeSpan DurationReal => TimeSpan.FromSeconds(DurationEt * (175.0 / 60.0));

        // ET formatted strings for display
        public string StartFormatted => $"{Start / 100:D2}:{Start % 100:D2}";
        public string EndFormatted => $"{End / 100:D2}:{End % 100:D2}";

        public override string ToString() => $"{StartFormatted} – {EndFormatted} ET";
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
    public static Dictionary<uint, uint> Job_IconIds = new()
    {
        [16] = 62510, // MIN
        [17] = 62511, // BTN,
        [18] = 62512, // FSH
    };
    public static Dictionary<uint, ISharedImmediateTexture> JobIcons = new();

    public static Dictionary<uint, GatherPointInfo> SheetInfo = new();
    public static void UpdateSheetInfo()
    {
        var sheet_gatherPoint = ExcelHelper.Sheet_GatherPoint;
        var sheet_TerritoryType = ExcelHelper.Sheet_TerritoryType;
        var sheet_PlaceName = ExcelHelper.Sheet_PlaceName;

        var sheet_nodeTimed = ExcelHelper.Sheet_GatherPointTransient;
        var sheet_rareTimed = ExcelHelper.Sheet_GatherTimeTable;

        int RoundUpToHour(int time) => time % 100 == 0 ? time : (time / 100 + 1) * 100;

        foreach (var gatherPoint in sheet_gatherPoint)
        {
            uint routeId = 0;

            uint nodeId = gatherPoint.RowId;
            uint type = 0;
            CombinedGatherType combinedType = CombinedGatherType.UNKNOWN;
            uint level = 0;
            uint territoryId = 0;
            uint expansion = 0;
            string expansionName = "ARealmReborn";
            string zoneName = "???";
            string placeName = "???";
            List<uint> itemIds = new();
            int radius = 0;


            if (gatherPoint.GatheringPointBase.Value.GatheringLevel == 0)
            {
                // This isn't actually a node, so continueing on
                continue;
            }
            else
            {
                var gatherPointBase = gatherPoint.GatheringPointBase.Value; // Sheet: GatherPointBase
                routeId = gatherPointBase.RowId;
                type = gatherPointBase.GatheringType.Value.RowId; // Column 1
                if (type is 0 or 1)
                    combinedType = CombinedGatherType.Miner;
                else if (type is 2 or 3)
                    combinedType = CombinedGatherType.Botanist;
                else if (type is 4 or 5)
                    combinedType = CombinedGatherType.Fisher;

                level = gatherPointBase.GatheringLevel; // Column 2

                for (int i = 0; i <= 7; i++) // Items 0-7
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

                // some items aren't in GatheringPointBase; look up each point in GatheringItemPoint to make sure
                // this produces a dictionary of (GatheringPoint, [GatheringItems])
                var gatherItemPoints = ExcelHelper.Sheet_GatheringItemPoint.SelectMany(g => g).GroupBy(row => row.GatheringPoint.RowId).ToDictionary(group => group.Key, group => group.Select(g => g.RowId).Distinct().ToList());
                if (gatherItemPoints.TryGetValue(gatherPoint.RowId, out var giList))
                {
                    foreach (uint gatheringItemId in giList)
                    {
                        if (ExcelHelper.Sheet_GatheringItem.TryGetRow(gatheringItemId, out var gatherItem))
                        {
                            var itemId = gatherItem.Item.RowId;
                            if (itemId != 0 && !itemIds.Contains(itemId))
                                itemIds.Add(itemId);
                        }
                    }
                }

                if (type == 18)
                    IceLogging.Verbose($"RouteID: {routeId} | Item Count: {itemIds.Count()}");

                if (itemIds.Count == 0)
                    continue;
            }

            if (gatherPoint.TerritoryType.Value.RowId != 0 || gatherPoint.TerritoryType.Value.RowId != 1)
            {
                var territoryType = gatherPoint.TerritoryType.Value;
                territoryId = territoryType.RowId;
                if (territoryType.Map.IsValid)
                    zoneName = territoryType.PlaceName.Value.Name.ToString();

                if (zoneName == string.Empty)
                    continue;

                expansion = territoryType.ExVersion.Value.RowId;
            }
            else
            {
                // These shouldn't exist, and probably exist currently for leve purposes. So skipping them
                continue;
            }

            if (gatherPoint.PlaceName.IsValid)
            {
                var placeNameSheet = gatherPoint.PlaceName.Value;
                placeName = placeNameSheet.Name.ToString();
            }

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

            if (SheetInfo.TryGetValue(routeId, out var routeInfo))
            {
                if (zoneName != "???")
                    routeInfo.ZoneName = zoneName;
                if (placeName != "???")
                    routeInfo.PlaceName = placeName;
                routeInfo.NodeIds.Add(nodeId);
            }
            else
            {
                SheetInfo.Add(routeId, new GatherPointInfo()
                {
                    Type = combinedType,
                    Level = level,
                    TerritoryId = territoryId,
                    ZoneName = zoneName,
                    PlaceName = placeName,
                    NodeIds = new() { nodeId },
                    ItemIds = itemIds,
                    Radius = radius,
                    ExpId = expansion,
                    ExpansionName = expansionName,
                    Map = mapInfo,
                });
            }

            // assign item data that we couldn't get from just Item and GatheringItem, but can here
            foreach (uint itemId in itemIds)
            {
                if (P.allItems.TryGetValue(itemId, out var globalItem))
                {
                    globalItem.Type = combinedType;
                    if (zoneName != "???")
                    {
                        if (globalItem.ZoneName != "" && globalItem.ZoneName != zoneName)
                            globalItem.ZoneName = "(various)"; // todo
                        else globalItem.ZoneName = zoneName;
                    }
                    if (placeName != "???" && placeName != "" && !globalItem.PlaceNames.Contains(placeName))
                        globalItem.PlaceNames.Add(placeName);
                    if (!globalItem.RouteIds.Contains(routeId)) globalItem.RouteIds.Add(routeId);
                    P.allItems[itemId] = globalItem;
                }
            }
        }

        foreach (var route in SheetInfo)
        {
            List<EorzeaTimeWindow> timeInfo = new();

            var firstNode = route.Value.NodeIds.First();
            if (sheet_nodeTimed.TryGetRow(firstNode, out var timedInfo))
            {
                if (timedInfo.EphemeralStartTime != 65535 && timedInfo.EphemeralEndTime != 65535)
                {
                    timeInfo.Add(new()
                    {
                        Start = timedInfo.EphemeralStartTime,
                        End = RoundUpToHour(timedInfo.EphemeralEndTime),
                    });
                }
                else if (timedInfo.GatheringRarePopTimeTable.RowId != 0)
                {
                    var rareTime = timedInfo.GatheringRarePopTimeTable.Value;
                    for (int i = 0; i < 3; i++)
                    {
                        var startTime = rareTime.StartTime[i];
                        if (startTime != 65535)
                        {
                            timeInfo.Add(new()
                            {
                                Start = startTime,
                                End = RoundUpToHour(startTime + rareTime.Duration[i]),
                            });
                        }
                    }
                }
            }

            route.Value.TimedInfo = timeInfo;
        }

        // Updating Icon Dictionary here for quick usage
        foreach (var jobIcon in Job_IconIds)
        {
            if (Svc.Texture.TryGetFromGameIcon(jobIcon.Value, out var texture))
                JobIcons.TryAdd(jobIcon.Key, texture);
        }
    }

    public static void LoadAllItems()
    {
        if (ExcelHelper.Sheet_GatheringItem == null)
        {
            PluginLog.Error("GatheringItem sheet not initialized");
            return;
        }

        // check for valid row IDs and names and eliminate possible duplicates
        P.allItems = ExcelHelper.Sheet_GatheringItem
            .Where(gi => gi.Item.RowId != 0 && gi.Item.RowId < 1000000)  // valid and known row IDs
            .Where(gi => gi.Item.TryGetValue<Item>(out var i) && !i.Name.IsEmpty) // linked to a valid item
            .Where(gi => gi.Item.TryGetValue<Item>(out var i) && i.FilterGroup != 50) // not a cosmic exploration material
            .GroupBy(gi => gi.Item.RowId).Select(group => group.First()) // prevent duplicates
            .ToDictionary(gi => gi.Item.RowId, gi => new GatherableItem(gi));
    }
}
