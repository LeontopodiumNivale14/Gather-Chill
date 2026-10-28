using Dalamud.Interface.Textures;
using Dalamud.Interface.Utility.Raii;
using GatherChill.ConfigFiles;
using GatherChill.Enums;
using GatherChill.Gui;
using GatherChill.Scheduler;
using GatherChill.Ui.Tables;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace GatherChill.Ui.Tabs_MainWindow
{
    internal class GatherPlaylist
    {
        private static float _modeWindow = 100f;

        private static readonly List<Config.ItemInfo> _normalItems = new();
        private static readonly List<Config.ItemInfo> _timedItems = new();

        private static Table_GatherPlaylist.GatherPlaylistTable? _normalTable;
        private static Table_GatherPlaylist.GatherPlaylistTable? _timedTable;

        // Keeping these separate. Partially because I want to still let users set a priority for timed nodes. And don't wanna fuck with timed vs not 
        private static readonly ImGuiEx.RealtimeDragDrop<Config.ItemInfo> _normalDragDrop = new("GatherItemOrder_Normal", id => id.ItemId.ToString());
        private static readonly ImGuiEx.RealtimeDragDrop<Config.ItemInfo> _timedDragDrop = new("GatherItemOrder_Timed", id => id.ItemId.ToString());

        public static void Draw()
        {
            using (var child = ImRaii.Child("Gather Playlist: Child Container", default))
            {
                if (!child.Success)
                    return;

                using var colors = ImGui_Ice.PushChildBG();

                var rightColumnAvail = ImGui.GetContentRegionAvail();
                float availWidth = rightColumnAvail.X;

                using (var child_Buttons = ImRaii.Child("Gather Playlist: Buttons", new(availWidth, _modeWindow), true))
                {
                    if (!child_Buttons.Success)
                        return;

                    if (ImGuiEx.IconButtonWithText(FontAwesomeIcon.Play, "Start Gathering", Gather_Helper.State == IceState.Idle))
                    {
                        Gather_Helper.State = IceState.Start;
                    }
                    ImGui.SameLine();
                    if (ImGuiEx.IconButtonWithText(FontAwesomeIcon.Square, "Stop", Gather_Helper.State != IceState.Idle))
                    {
                        SchedulerMain.DisablePlugin();
                    }

                    if (ImGui.Button("Search for item"))
                        OpenItemPicker();

                    ImGui.SameLine();
                    var canClear = Gather_Helper.State == IceState.Idle && C.GatherList.Count > 0;
                    if (ImGuiEx.IconButtonWithText(FontAwesomeIcon.Trash, "Clear All", canClear))
                        ImGui.OpenPopup(ClearAllPopupId);

                    ImGui.SameLine();
                    if (ImGuiEx.IconButtonWithText(FontAwesomeIcon.Save, "Save List"))
                    {
                        _listName = string.Empty;
                        _listDescription = string.Empty;
                        ImGui.OpenPopup(SavePopupID);
                    }

                    DrawItemPicker();
                    DrawClearAllPopup();
                    SavePopup();

                    _modeWindow = ImGui.GetCursorPosY() + ImGui.GetStyle().WindowPadding.Y;
                }

                using (var child_Table = ImRaii.Child("Gather Playlist: Table", ImGui.GetContentRegionAvail(), true))
                {
                    if (!child_Table.Success)
                        return;

                    if (C.GatherList.Count == 0)
                    {
                        ImGui.TextDisabled("We have no items to gather... sadge. Please add some?");
                        return;
                    }

                    RebuildPartitions();

                    DrawItemTable("Gather in order", _normalItems, ref _normalTable, _normalDragDrop, timed: false);
                    ImGui.Spacing();
                    DrawItemTable("Timed nodes (gathered when up)", _timedItems, ref _timedTable, _timedDragDrop, timed: true);

                    SyncOrder();
                }

                RouteSelector_Popup();
            }
        }

        // Matches the route popup: normal routes win if both exist
        private static bool IsTimedItem(uint itemId)
        {
            if (!Gather_Util.Sheet_ItemInfo.TryGetValue(itemId, out var info))
                return false;

            return info.NormalRoutes.Count == 0 && info.TimedRoutes.Count > 0;
        }

        private static void RebuildPartitions()
        {
            _normalItems.Clear();
            _timedItems.Clear();

            foreach (var item in C.GatherList)
            {
                if (IsTimedItem(item.ItemId))
                    _timedItems.Add(item);
                else
                    _normalItems.Add(item);
            }
        }

        // Doing this so I can keep the normal items -> timed items in order
        // If I don't, then it's going to get messy with the list...
        private static void SyncOrder()
        {
            var merged = _normalItems.Concat(_timedItems).ToList();
            if (merged.SequenceEqual(C.GatherList))
                return;

            C.GatherList.Clear();
            C.GatherList.AddRange(merged);
            C.SaveDebounced();
        }

        private static void DrawItemTable(string header, List<Config.ItemInfo> items, ref Table_GatherPlaylist.GatherPlaylistTable? table, ImGuiEx.RealtimeDragDrop<Config.ItemInfo> dragDrop, bool timed)
        {
            ImGui.TextUnformatted(header);

            if (items.Count == 0)
            {
                ImGui.TextDisabled("Nothing here.");
                return;
            }

            table ??= new(items, dragDrop, RouteSelector_OpenPopup, timed);
            table.Refresh();

            dragDrop.Begin();
            table.Draw();

            if (table.ItemToRemove is { } itemToRemove)
            {
                // Remove from the sub-list too, otherwise SyncOrder would write it back this frame
                items.Remove(itemToRemove);
                C.GatherList.Remove(itemToRemove);
                C.SaveDebounced();
                table.ItemToRemove = null;
            }

            dragDrop.End();
        }

        #region Route Selector Popup

        private static uint _routeSelector_itemId = 0;
        private static bool _routeSelectorPopup;

        public static void RouteSelector_OpenPopup(uint itemId)
        {
            _routeSelector_itemId = itemId;
            _routeSelectorPopup = true;
        }
        private const int RoutesPerPage = 10;
        private static int _routePage;

        public static void RouteSelector_Popup()
        {
            string popupName = "Route Selector: Popup";

            if (_routeSelectorPopup)
            {
                _routePage = 0;
                ImGui.OpenPopup(popupName);
                _routeSelectorPopup = false;
            }

            using (var popup = ImRaii.Popup(popupName))
            {
                if (!popup.Success)
                    return;

                if (!Gather_Util.Sheet_ItemInfo.TryGetValue(_routeSelector_itemId, out var itemInfo))
                    return;

                ImGui_Ice.ImageButtonWithText(itemInfo.IconId, itemInfo.Name, itemInfo.Name);
                var list = itemInfo.NormalRoutes.Count != 0 ? itemInfo.NormalRoutes : itemInfo.TimedRoutes;
                list = list.OrderBy(x => Gather_Util.Sheet_RouteInfo[x].TerritoryId)
                           .ThenBy(x => Gather_Util.Sheet_RouteInfo[x].Job)
                           .ThenBy(x => x).ToList();

                int totalPages = Math.Max(1, (list.Count + RoutesPerPage - 1) / RoutesPerPage);
                _routePage = Math.Clamp(_routePage, 0, totalPages - 1);

                var pageRoutes = list.Skip(_routePage * RoutesPerPage).Take(RoutesPerPage).ToList();
                bool needsPaging = totalPages > 1;

                var flags = ImGuiTableFlags.Borders | ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.RowBg;
                using (var table = ImRaii.Table("Selectable Table", 5, flags))
                {
                    if (!table.Success)
                        return;

                    ImGui.TableSetupColumn("Select");
                    ImGui.TableSetupColumn("RouteId");
                    ImGui.TableSetupColumn("Location");
                    ImGui.TableSetupColumn("Type");
                    ImGui.TableSetupColumn("Flag");
                    ImGui.TableHeadersRow();

                    var config = C.ItemRoutes[_routeSelector_itemId].EnabledRoutes;
                    bool isNormal = itemInfo.NormalRoutes.Count != 0;
                    bool timedMulti = itemInfo.TimedRoutes.Count > 1;

                    foreach (var route in pageRoutes)
                    {
                        using var push = ImRaii.PushId($"{route}");

                        ImGui.TableNextRow();
                        ImGui.TableSetColumnIndex(0);

                        if (isNormal)
                        {
                            if (ImGui.RadioButton($"##{route}", config.Contains(route)))
                            {
                                config.Clear();
                                config.Add(route);

                                C.Save();
                                ImGui.CloseCurrentPopup();
                            }
                        }
                        else if (timedMulti)
                        {
                            var isEnabled = config.Contains(route);
                            if (ImGui.Checkbox($"##{route}", ref isEnabled))
                            {
                                bool multiEnabled = config.Count > 1;

                                if (isEnabled)
                                    config.Add(route);
                                else if (multiEnabled)
                                    config.Remove(route);

                                C.Save();
                            }
                        }

                        ImGui.TableNextColumn();
                        ImGui.AlignTextToFramePadding();
                        ImGui.Text($"{route}");
                        var routeDetails = P.routeEditor.GetRoute(route);
                        if (routeDetails.AetheryteId == 0)
                        {
                            ImGui.TableSetBgColor(ImGuiTableBgTarget.RowBg0, ImGui.GetColorU32(EColor.Red));
                            ImGui.SameLine();
                            ImGui_Ice.Icon(FontAwesomeIcon.ExclamationTriangle);
                            if (ImGui.IsItemHovered())
                            {
                                ImGui.BeginTooltip();
                                ImGui.Text("We are missing details for this (aetheryte probably) so this might not work");
                                ImGui.EndTooltip();
                            }
                        }

                        if (!Gather_Util.Sheet_RouteInfo.TryGetValue(route, out var routeInfo))
                            continue;

                        ImGui.TableNextColumn();
                        ImGui.AlignTextToFramePadding();
                        ImGui.Text($"{routeInfo.ZoneName}");

                        ImGui.TableNextColumn();
                        GameIcons.DrawInline(Gather_Util.Job_IconIds[routeInfo.Job].IconId, false);

                        ImGui.TableNextColumn();
                        if (ImGuiEx.IconButton(FontAwesomeIcon.Flag, "Location"))
                            routeInfo.Map.OpenMap($"Route {route}");
                    }

                    // Pad the last page so the popup height stays constant between pages
                    if (needsPaging)
                    {
                        float rowHeight = ImGui.GetFrameHeight() + ImGui.GetStyle().CellPadding.Y * 2;
                        for (int i = pageRoutes.Count; i < RoutesPerPage; i++)
                        {
                            ImGui.TableNextRow(ImGuiTableRowFlags.None, rowHeight);
                            ImGui.TableNextColumn();
                        }
                    }
                }

                if (!needsPaging)
                    return;

                using (ImRaii.Disabled(_routePage == 0))
                {
                    if (ImGuiEx.IconButton(FontAwesomeIcon.ChevronLeft, "Prev Page"))
                        _routePage--;
                }

                ImGui.SameLine();
                ImGui.Text($"Page {_routePage + 1} / {totalPages}");
                ImGui.SameLine();

                using (ImRaii.Disabled(_routePage >= totalPages - 1))
                {
                    if (ImGuiEx.IconButton(FontAwesomeIcon.ChevronRight, "Next Page"))
                        _routePage++;
                }
            }
        }

        #endregion

        #region Item Picker Popup

        private const string PickerPopupId = "GatherChill_ItemPickerPopup";
        private const int PickerPageSize = 15;

        /// <summary>false = sort by name, true = sort by itemId</summary>
        private static bool PickerSortById = false;

        /// <summary>
        /// Items found in any of these territories never show up in the picker.
        /// Add territory ids here (TerritoryType row ids).
        /// </summary>
        private static readonly HashSet<uint> TerritoryBlacklist = new()
        {
            512, 514, 515, 624, 625, 656, 901, 929, 939 // Diadem Territories
        };

        private static string _pickerSearch = "";
        private static string _pickerLastSearch = null;
        private static int _pickerLastSheetCount = -1;
        private static bool _pickerLastSortById = false;
        private static int _pickerPage = 0;
        private static bool _pickerOpenRequested;
        private static List<KeyValuePair<uint, Gather_Util.ItemClass>> _pickerFiltered = new();

        private static int PickerMaxPage => Math.Max(0, (_pickerFiltered.Count - 1) / PickerPageSize);

        private static bool IsItemBlacklisted(Gather_Util.ItemClass item)
        {
            if (TerritoryBlacklist.Count == 0)
                return false;

            if (item.Territories == null)
                return false;

            foreach (var territoryId in item.Territories())
            {
                if (TerritoryBlacklist.Contains(territoryId))
                    return true;
            }

            return false;
        }

        private static void OpenItemPicker()
        {
            _pickerSearch = "";
            _pickerLastSearch = null; // forces a rebuild, which also resets the page
            _pickerOpenRequested = true;
        }

        private static void DrawItemPicker()
        {
            if (_pickerOpenRequested)
            {
                ImGui.OpenPopup(PickerPopupId);
                _pickerOpenRequested = false;
            }

            ImGui.SetNextWindowSizeConstraints(new Vector2(420, 0), new Vector2(600, 700));
            if (!ImGui.BeginPopup(PickerPopupId))
                return;

            RebuildPickerFilterIfNeeded();

            ImGui.SetNextItemWidth(-1);
            if (ImGui.IsWindowAppearing())
                ImGui.SetKeyboardFocusHere();
            ImGui.InputTextWithHint("##itemSearch", "Search by name or id...", ref _pickerSearch, 64);

            ImGui.Separator();
            DrawPickerRows();
            ImGui.Separator();
            DrawPickerPageControls();

            ImGui.EndPopup();
        }

        private static void DrawPickerRows()
        {
            if (_pickerFiltered.Count == 0)
            {
                ImGui.TextDisabled("No items found.");
                return;
            }

            var rowHeight = ImGui.GetFrameHeight();
            var pageItems = _pickerFiltered.Skip(_pickerPage * PickerPageSize).Take(PickerPageSize);

            foreach (var (itemId, item) in pageItems)
            {
                var added = GatherList_Util.IsInList(itemId);
                var rowStart = ImGui.GetCursorScreenPos();

                // Full-width selectable owns hover + click, icon and name are drawn on top
                var clicked = ImGui.Selectable($"##row{itemId}", false, ImGuiSelectableFlags.None, new Vector2(0, rowHeight));

                ImGui.SetCursorScreenPos(rowStart);
                if (GameIcons.TryGetScaledIcon(item.IconId, (int)MathF.Round(rowHeight), out var texture))
                    ImGui.Image(texture.Handle, new Vector2(rowHeight));
                else
                    ImGui.Dummy(new Vector2(rowHeight));

                ImGui.SameLine();
                ImGui.AlignTextToFramePadding();
                if (added)
                    ImGui.TextDisabled($"{item.Name} (added)");
                else
                    ImGui.TextUnformatted(item.Name);

                if (!clicked || added)
                    continue;

                if (GatherList_Util.TryAdd(itemId, 1))
                    C.SaveDebounced();
                ImGui.CloseCurrentPopup();
                return;
            }
        }

        private static void DrawPickerPageControls()
        {
            var maxPage = PickerMaxPage;

            // page <= 0 : at (or before) the first page -> can't go back
            ImGui.BeginDisabled(_pickerPage <= 0);
            if (ImGui.Button("<<")) _pickerPage = 0;
            ImGui.SameLine();
            if (ImGui.Button("<")) _pickerPage--;
            ImGui.EndDisabled();

            ImGui.SameLine();
            ImGui.Text($"Page {_pickerPage + 1} / {maxPage + 1}  ({_pickerFiltered.Count} items)");
            ImGui.SameLine();

            // page >= maxPage : at (or past) the last page -> can't go forward
            ImGui.BeginDisabled(_pickerPage >= maxPage);
            if (ImGui.Button(">")) _pickerPage++;
            ImGui.SameLine();
            if (ImGui.Button(">>")) _pickerPage = maxPage;
            ImGui.EndDisabled();
        }

        private static void RebuildPickerFilterIfNeeded()
        {
            var sheet = Gather_Util.Sheet_ItemInfo;
            if (_pickerSearch == _pickerLastSearch
                && sheet.Count == _pickerLastSheetCount
                && PickerSortById == _pickerLastSortById)
                return;

            _pickerLastSearch = _pickerSearch;
            _pickerLastSheetCount = sheet.Count;
            _pickerLastSortById = PickerSortById;
            _pickerPage = 0; // any change to the results starts back on page 1

            var query = _pickerSearch.Trim();
            var result = sheet.AsEnumerable()
                .Where(x => !IsItemBlacklisted(x.Value));

            if (query.Length > 0)
            {
                result = result.Where(x =>
                    x.Value.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    x.Key.ToString().Contains(query));
            }

            _pickerFiltered = PickerSortById
                ? result.OrderBy(x => x.Key).ToList()
                : result.OrderBy(x => x.Value.Name).ToList();
        }

        #endregion

        #region Clear All Popup

        private const string ClearAllPopupId = "GatherChill_ClearAllPopup";

        private static void DrawClearAllPopup()
        {
            using var popup = ImRaii.Popup(ClearAllPopupId);
            if (!popup.Success)
                return;

            ImGui.Text($"Remove all {C.GatherList.Count} items from the playlist?");
            ImGui.TextDisabled("This can't be undone.");
            ImGui.Spacing();

            if (ImGui.Button("Yes, clear all"))
            {
                C.GatherList.Clear();
                C.Save();
                ImGui.CloseCurrentPopup();
                return;
            }

            ImGui.SameLine();
            if (ImGui.Button("Cancel"))
                ImGui.CloseCurrentPopup();
        }

        #endregion

        #region Save Gather List

        private static string _listName = string.Empty;
        private static string _listDescription = string.Empty;
        private static readonly string SavePopupID = "Save new gather list popup";

        private static void SavePopup()
        {
            using (var popup = ImRaii.Popup(SavePopupID))
            {
                if (!popup.Success)
                    return;

                ImGui.InputText("List Name", ref _listName);
                ImGui.InputTextMultiline("Description", ref _listDescription, 3000);
                using (ImRaii.Disabled(_listName == string.Empty))
                {
                    if (ImGui.Button("Save new Gather List"))
                    {
                        C.SaveNewGatherList(_listName, _listDescription);
                        ImGui.CloseCurrentPopup();
                    }
                }
            }
        }

        #endregion
    }
}