using Lumina.Excel.Sheets;
using System.Collections.Generic;

namespace GatherChill.Utilities;

public static class Utils
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
    }

    public static Dictionary<uint, GatherPointInfo> SheetInfo = new();
    public static void UpdateSheetInfo()
    {
        var sheet_gatherPoint = Svc.Data.GetExcelSheet<GatheringPoint>();
        var sheet_TerritoryType = Svc.Data.GetExcelSheet<TerritoryType>();
        var sheet_PlaceName = Svc.Data.GetExcelSheet<PlaceName>();

        foreach (var gatherPoint in sheet_gatherPoint)
        {
            uint routeId = 0;

            uint nodeId = gatherPoint.RowId;
            uint type = 0;
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
                level = gatherPointBase.GatheringLevel; // Column 2

                for (int i = 0; i <= 7; i++) // Items 0-7
                {
                    var gatheringItemId = gatherPointBase.Item[i].RowId;
                    if (Svc.Data.GetExcelSheet<GatheringItem>().TryGetRow(gatheringItemId, out var gatherItem))
                    {
                        var itemId = gatherItem.Item.RowId;
                        if (itemId != 0)
                            itemIds.Add(itemId);
                    }
                }

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

            if (Svc.Data.GetExcelSheet<ExportedGatheringPoint>().TryGetRow(routeId, out var exportSheet))
            {
                radius = exportSheet.Radius;
            }

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
                    Type = type, 
                    Level = level,
                    TerritoryId = territoryId,
                    ZoneName = zoneName,
                    PlaceName = placeName,
                    NodeIds = new() { nodeId },
                    ItemIds = itemIds,
                    Radius = radius,
                    ExpId = expansion,
                    ExpansionName = expansionName,
                });
            }
        }
    }
}
