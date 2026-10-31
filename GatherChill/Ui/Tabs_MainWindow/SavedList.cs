using Dalamud.Interface.Utility;
using Dalamud.Interface.Utility.Raii;
using GatherChill.ConfigFiles;
using GatherChill.Enums;
using GatherChill.Gui;
using GatherChill.Scheduler;
using GatherChill.Utilities.GatheringHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui.Tabs_MainWindow
{
    internal class SavedList
    {
        private static int _selectedListId = 0; 

        public static void Draw()
        {
            ImGui.SameLine();
            GatherSavedList(C.Saved_List);
            ImGui.SameLine();
            Gather_ListDetails(C.Saved_List, true);

        }

        private static void GatherSavedList(List<Config.Class_List> gatherList)
        {
            var scale = ImGuiHelpers.GlobalScale;
            var selectionWindow = new Vector2(200 * scale, ImGui.GetContentRegionAvail().Y);

            using (var listSelect = ImRaii.Child("Gather Selection Window", selectionWindow, true))
            {
                if (!listSelect.Success)
                    return;

                foreach (var list in gatherList)
                {
                    bool isSelected = _selectedListId == list.Id;
                    ImGui.PushID($"{list.Name}_{list.Id}");

                    if (ImGui.Selectable($"{list.Name}", isSelected))
                    {
                        _selectedListId = list.Id;
                    }
                }
            }
        }

        private static bool _editName = false;
        private static string _newName = string.Empty;

        private static bool _editDescription = false;
        private static string _descriptionInfo = string.Empty;

        private static void Gather_ListDetails(List<Config.Class_List> list, bool allowEdit)
        {
            using (var listInfo = ImRaii.Child("Leve Playlist: Details", ImGui.GetContentRegionAvail(), true))
            {
                if (!listInfo.Success)
                    return;

                var savedList = list.Find(c => c.Id == _selectedListId);
                if (savedList == null)
                    return;

                if (ImGui.Button("Overwrite current list"))
                {
                    OverwriteList(savedList.ItemList);
                }
                ImGui.SameLine();
                if (ImGui.Button("Add to current list"))
                {
                    AddToCurrent(savedList.ItemList);
                }

                if (allowEdit)
                {
                    ImGui.SameLine();
                    var keysHeld = ImGui.IsKeyDown(ImGuiKey.LeftShift) || ImGui.IsKeyDown(ImGuiKey.RightShift);

                    using (ImRaii.Disabled(!keysHeld))
                    {
                        if (ImGuiEx.IconButton(FontAwesomeIcon.Trash, "Delete List"))
                        {
                            C.Saved_List.Remove(savedList);
                            C.SaveDebounced();

                            _selectedListId = 0;
                        }
                    }
                    if (ImGui.IsItemHovered(ImGuiHoveredFlags.AllowWhenDisabled))
                    {
                        ImGui.SetTooltip("Remove the playlist.\n" +
                            "Hold shift to allow");
                    }
                }

                if (_editName)
                {
                    if (ImGui.Button("Save New Name##Name"))
                    {
                        savedList.Name = _newName;
                        C.Save();

                        _newName = string.Empty;
                        _editName = false;
                    }
                    ImGui.SameLine();
                }
                else if (allowEdit)
                {
                    if (ImGuiEx.IconButtonWithText(FontAwesomeIcon.PencilAlt, "Edit List Name"))
                    {
                        _newName = savedList.Name;
                        _editName = true;
                    }
                    ImGui.SameLine();
                }

                if (_editDescription)
                {
                    if (ImGui.Button("Save##Description"))
                    {
                        savedList.Description = _descriptionInfo;
                        C.Save();

                        _descriptionInfo = string.Empty;
                        _editDescription = false;
                    }
                }
                else if (allowEdit)
                {
                    if (ImGuiEx.IconButtonWithText(FontAwesomeIcon.PencilAlt, "Edit Description"))
                    {
                        _descriptionInfo = savedList.Description;
                        _editDescription = true;
                    }
                }


                if (_editName)
                {
                    ImGui.InputText("Name", ref _newName, 100);
                }
                else
                {
                    ImGui.Text($"List: {savedList.Name}");
                }

                if (_editDescription)
                {
                    ImGui.InputTextMultiline("Description", ref _descriptionInfo, 3000, new Vector2(-1, 100));
                }
                else
                {
                    ImGui.Text($"Description:");
                    ImGui.Text($"{savedList.Description}");
                }

                Gather_ListTable(savedList.ItemList);
            }
        }

        private static void Gather_ListTable(List<Config.Class_ItemList> gatherList)
        {
            using (var table = ImRaii.Table("Gather List: Current Table", 6, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.ScrollY | ImGuiTableFlags.RowBg))
            {
                if (!table.Success)
                    return;

                ImGui.TableSetupColumn("Icon");
                ImGui.TableSetupColumn("Item");
                ImGui.TableSetupColumn("Mode");
                ImGui.TableSetupColumn("Amount");
                ImGui.TableSetupColumn("Uptime");
                ImGui.TableSetupColumn("Route(s)");

                foreach (var item in gatherList)
                {
                    if (Gather_Util.Sheet_ItemInfo.TryGetValue(item.ItemId, out var itemInfo))
                    {
                        ImGui.TableNextRow();
                        ImGui.TableSetColumnIndex(0);
                        var size = ImGui.GetTextLineHeight();
                        ImGui_Ice.ImageButton(itemInfo.IconId, $"{itemInfo.Name}_{itemInfo.IconId}");

                        ImGui.TableNextColumn();
                        ImGui.AlignTextToFramePadding();
                        ImGui.Text($"{itemInfo.Name}");

                        ImGui.TableNextColumn();
                        ImGui.AlignTextToFramePadding();
                        ImGui.Text($"{item.Mode}");

                        ImGui.TableNextColumn();
                        var amount = item.Mode == Enums.GatherMode.Gather ? item.GatherAmount : item.KeepAmount;
                        ImGui.AlignTextToFramePadding();
                        ImGui.Text($"{amount}");

                        ImGui.TableNextColumn();
                        var windows = Gather_Util.UptimeHelper.GetWindows(item.EnabledRoutes);
                        Gather_Util.UptimeHelper.Draw(windows, "No timed");

                        ImGui.TableNextColumn();
                        var enabledRoutes = item.EnabledRoutes;
                        var count = enabledRoutes.Count();
                        if (count == 0)
                        {
                            ImGui.AlignTextToFramePadding();
                            ImGui.TextDisabled("None");
                        }
                        else
                        {
                            var firstRoute = item.EnabledRoutes.First();
                            var routeText = count > 1 ? $"{firstRoute} + {count - 1}" : $"{firstRoute}";

                            ImGui.Button(routeText);
                            if (ImGui.IsItemHovered() && count > 1)
                            {
                                ImGui.BeginTooltip();
                                var allRoutes = string.Join(", ", enabledRoutes);
                                ImGui.Text($"{allRoutes}");
                                ImGui.EndTooltip();
                            }
                        }
                    }
                }
            }
        }

        private static void OverwriteList(List<Config.Class_ItemList> savedItems)
        {
            if (Gather_Helper.State != IceState.Idle)
                return;

            C.GatherList.Clear();

            foreach (var saved in savedItems)
            {
                if (!Gather_Util.Sheet_ItemInfo.ContainsKey(saved.ItemId))
                    continue;

                // Guards against a saved list that contains the same item twice
                if (C.GatherList.Any(x => x.ItemId == saved.ItemId))
                    continue;

                ApplyRoutes(saved);

                var entry = new Config.ItemInfo { ItemId = saved.ItemId };
                CopyAmounts(entry, saved);
                C.GatherList.Add(entry);
            }

            C.Save();
        }

        private static void AddToCurrent(List<Config.Class_ItemList> savedItems)
        {
            if (Gather_Helper.State != IceState.Idle)
                return;

            foreach (var saved in savedItems)
            {
                if (!Gather_Util.Sheet_ItemInfo.ContainsKey(saved.ItemId))
                    continue;

                ApplyRoutes(saved);

                var entry = C.GatherList.FirstOrDefault(x => x.ItemId == saved.ItemId);
                if (entry == null)
                {
                    entry = new Config.ItemInfo { ItemId = saved.ItemId };
                    C.GatherList.Add(entry);
                }

                CopyAmounts(entry, saved);
            }

            C.Save();
        }

        private static void ApplyRoutes(Config.Class_ItemList saved)
        {
            if (!Gather_Util.Sheet_ItemInfo.TryGetValue(saved.ItemId, out var itemInfo))
                return;

            var valid = saved.EnabledRoutes
                .Where(route => Gather_Util.Sheet_RouteInfo.ContainsKey(route))
                .ToList();

            if (valid.Count == 0)
                return;

            // Normal items are single-route (radio button in the popup)
            if (itemInfo.NormalRoutes.Count != 0)
                valid = valid.Take(1).ToList();

            var live = C.ItemRoutes[saved.ItemId].EnabledRoutes;
            live.Clear();
            foreach (var route in valid)
                live.Add(route);
        }

        private static void CopyAmounts(Config.ItemInfo target, Config.Class_ItemList saved)
        {
            target.GatherAmount = saved.GatherAmount;
            target.KeepAmount = saved.KeepAmount;
            target.Mode = saved.Mode;
        }
    }
}
