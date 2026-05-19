using ECommons.Logging;
using GatherChill.Enums;
using GatherChill.Items;
using System.Collections.Generic;

namespace GatherChill.Ui.Tabs_MainWindow
{
    internal class Item_Menu
    {
        public Item_Table? ItemTable;
        public List<GatherableItem> TableItems;
        public int ItemCount = 0;

        public void Draw()
        {
            var bottomSpace = ImGui.GetTextLineHeight() + 6f;
            bottomSpace += 12f; // prevent the tabs from creating a scrollbar
            if (ImGui.BeginChild("###ItemTable", new Vector2(ImGui.GetContentRegionAvail().X, ImGui.GetContentRegionAvail().Y - bottomSpace), false))
            {
                try
                {
                    // don't rebuild everything every frame
                    if (ItemTable == null && P.allItems.Count > 0)
                    {
                        var allItems = P.allItems.Values
                            .Where(gi => gi.Type != CombinedGatherType.UNKNOWN) // items that aren't busted somehow
                            .Where(gi => gi.PlaceNames.Count > 0) // non-quest items
                            //.Where(gi => gi.RouteIds.Count > 0) // items that appear on at least one route
                            ;
                        ItemCount = allItems.Count();
                        TableItems = [.. allItems];
                        ItemTable = new(TableItems);
                    }
                    ItemTable.Draw(ITEM_ICON_SIZE + 4f);
                }
                catch (Exception ex)
                {
                    PluginLog.Error(ex.Message);
                }
            }
            ImGui.EndChild();

            var filterActive = ItemTable.FilteredItems.Count != 0 && ItemTable.FilteredItems.Count != ItemCount;
            var filterCount = filterActive ? $" (of {ItemCount})" : "";

            ImGui.Dummy(new Vector2(0, 3f));
            ImGui.Text($"Items: {ItemTable.FilteredItems.Count}{filterCount}");
            ImGui.Dummy(new Vector2(0, 3f));
        }
    }
}
