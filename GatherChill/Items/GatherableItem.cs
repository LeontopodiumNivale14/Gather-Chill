using ECommons.Logging;
using GatherChill.Enums;
using Lumina;
using Lumina.Excel.Sheets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace GatherChill.Items
{
    internal class GatherableItem
    {
        public Item Item;
        public GatheringItem GatheringItem;
        public string Name;
        public CombinedGatherType Type = CombinedGatherType.UNKNOWN;
        public int Stars = 0;
        public string ZoneName = "";
        public List<string> PlaceNames = [];

        public List<uint> RouteIds = [];

        public GatherableItem(GatheringItem gi)
        {
            GatheringItem = gi;
            Item = Svc.Data.GetExcelSheet<Item>().GetRowOrDefault(gi.Item.RowId) ?? new Item();
            if (Item.RowId == 0)
            {
                PluginLog.Error($"Invalid item from GatheringItem with RowId {gi.RowId}");
                return;
            }

            var giLevel = gi.GatheringItemLevel.ValueNullable;
            Stars = giLevel == null ? 0 : (giLevel.Value.GatheringItemLevel << 3) + giLevel.Value.Stars;
            Name = Item.Name.ToString();
        }
    }
}
