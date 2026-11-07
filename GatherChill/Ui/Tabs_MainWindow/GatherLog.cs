using Dalamud.Interface.Utility.Raii;
using ECommons.GameHelpers;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game;
using GatherChill.Gui;
using GatherChill.IPC;
using GatherChill.Ui.Tables;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.GatheringHelpers.GatherListUtils;
using GatherChill.Utilities.Tools;
using System.Collections.Generic;

namespace GatherChill.Ui.Tabs_MainWindow
{
    internal class GatherLog
    {
        private static Table_GatherLog.GatherLogTable? ItemTable = null;
        private static List<Table_GatherLog.Gatherables> TableItems = [];
        private static int ItemCount = 0;
        private static int RecordCount = 0;

        private static int CurrentlyUnlocked = 0;
        private static int TotalAmount = 1000;


        public static void Draw()
        {
            using (var child = ImRaii.Child("Item Menu", default, false))
            {
                if (!child.Success)
                    return;

                using var colors = ImGui_Ice.PushChildBG();
                ItemProgressWindow();
                GatherableItemTable();
            }
        }

        private static float _modeWindow = 100f;

        private static void ItemProgressWindow()
        {
            var rightColumnAvail = ImGui.GetContentRegionAvail();
            float availWidth = rightColumnAvail.X;

            using (var child_Buttons = ImRaii.Child("Gatherable Items: Progress", new(availWidth, _modeWindow), true))
            {
                if (!child_Buttons.Success)
                    return;

                UpdateItemCount();

                if (ImGui.Button("Add Visible Items to list..."))
                {
                    ImGui.OpenPopup("Item Filter List");
                }
                ImGui_Ice.Draw_XPBar(CurrentlyUnlocked, TotalAmount, $"Total Gathered: {CurrentlyUnlocked:N0}");

                ItemList_Popup();


                _modeWindow = ImGui.GetCursorPosY() + ImGui.GetStyle().WindowPadding.Y;
            }
        }
        private static unsafe void UpdateItemCount()
        {
            if (!EzThrottler.Throttle("Update Count", 1000))
                return;

            var questManager = QuestManager.Instance();
            if (questManager == null)
                return;

            if (!Player.Available)
                return;

            var unlockables = Gather_Util.Sheet_ItemInfo.Where(x => x.Value.GatheringRow < 10000 && x.Value.GatheringRow != 0);
            CurrentlyUnlocked = unlockables.Where(x => QuestManager.IsGatheringItemGathered(x.Value.GatheringRow)).ToList().Count;
            TotalAmount = unlockables.Count();
        }
        private static void GatherableItemTable()
        {
            using (var child = ImRaii.Child("Item Table Viewer", default, true))
            {
                if (!child.Success)
                    return;

                try
                {
                    if (ItemTable == null && Gather_Util.Sheet_ItemInfo.Count > 0)
                    {
                        var filteredItems = Gather_Util.Sheet_ItemInfo
                            .Where(x => x.Value.GatheringRow < 10000 && x.Value.GatheringRow != 0);

                        foreach (var item in filteredItems)
                        {
                            Table_GatherLog.Gatherables gatherItem = new() { ItemId = item.Key };
                            TableItems.Add(gatherItem);
                        }
                        ItemCount = TableItems.Count();
                        RecordCount = filteredItems.Count();
                        ItemTable = new(TableItems);
                    }



                    ItemTable?.Draw();
                }
                catch (Exception ex)
                {
                    IceLogging.Error(ex.Message, "Drawing Item Table");
                }
            }
        }

        private static bool IncludeTimedNodes = false;
        private static bool IncludeCollectables = false;

        private static void ItemList_Popup()
        {
            using (var popup = ImRaii.Popup("Item Filter List"))
            {
                if (!popup.Success)
                    return;

                ImGui.Checkbox("Include Timed Nodes", ref IncludeTimedNodes);
                ImGui.Checkbox("Include Collectable Items", ref IncludeCollectables);
                if (ImGui.Button("Add Visible Items"))
                {
                    List<uint> itemList = new();

                    foreach (var item in ItemTable.GetVisibleItems())
                    {
                        var info = item.ItemInfo;

                        bool hasNormalRoute = info.NormalRoutes.Count != 0;
                        bool hasTimedRoute = IncludeTimedNodes && info.TimedRoutes.Count != 0;
                        if (!hasNormalRoute && !hasTimedRoute)
                            continue;

                        bool questAllowed = !info.IsCollectable || IncludeCollectables;
                        if (!questAllowed)
                            continue;

                        itemList.Add(item.ItemId);
                    }

                    foreach (var item in itemList)
                    {
                        GatherList_Util.TryAdd(item, 1);
                    }
                    if (itemList.Count != 0)
                        C.Save();

                    ImGui.CloseCurrentPopup();
                }
            }
        }
    }
}
