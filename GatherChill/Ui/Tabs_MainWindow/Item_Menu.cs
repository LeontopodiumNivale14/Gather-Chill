using Dalamud.Interface.Utility.Raii;
using GatherChill.Gui;
using GatherChill.Ui.Tables;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Tools;
using System.Collections.Generic;

namespace GatherChill.Ui.Tabs_MainWindow
{
    internal class Item_Menu
    {
        private static Table_Items.ItemTable? ItemTable = null;
        private static List<Table_Items.GatherItems> TableItems = [];
        private static int ItemCount = 0;

        public static void Draw()
        {
            var childColors = C.UseIceTheme ? ImRaii.PushColor(ImGuiCol.ChildBg, Theme_Colors.ChildBg) : default;

            using (var child = ImRaii.Child("Item Menu", default, true))
            {
                if (!child.Success)
                    return;

                try
                {
                    if (ItemTable == null && Gather_Util.Sheet_ItemInfo.Count > 0)
                    {
                        foreach (var item in Gather_Util.Sheet_ItemInfo)
                        {
                            Table_Items.GatherItems gatherItem = new() { ItemId = item.Key };
                            TableItems.Add(gatherItem);
                        }
                        ItemCount = TableItems.Count();
                        ItemTable = new(TableItems);
                    }
                    ImGui.Text($"Count: {ItemCount:N0} | Dictionary Count: {Gather_Util.Sheet_ItemInfo.Count()}");
                    ItemTable?.Draw();
                }
                catch (Exception ex)
                {
                    IceLogging.Error(ex.Message, "Drawing Item Table");
                }
            }
        }
    }
}
