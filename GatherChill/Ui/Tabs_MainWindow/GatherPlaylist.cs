using Dalamud.Interface.Textures;
using Dalamud.Interface.Utility.Raii;
using GatherChill.ConfigFiles;
using GatherChill.Gui;
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
        private static Config.ItemInfo _itemToRemove = new();
        private static readonly ImGuiEx.RealtimeDragDrop<Config.ItemInfo> _gatherDragDrop = new("GatherItemOrder", id => id.ItemId.ToString());

        public static void Draw()
        {
            using (var child = ImRaii.Child("Gather Playlist: Child Container", default))
            {
                if (!child.Success)
                    return;

                var childColors = C.UseIceTheme ? ImRaii.PushColor(ImGuiCol.ChildBg, Theme_Colors.ChildBg) : default;

                var rightColumnAvail = ImGui.GetContentRegionAvail();
                float availWidth = rightColumnAvail.X;

                using (var child_Buttons = ImRaii.Child("Gather Playlist: Buttons", new(availWidth, _modeWindow), true))
                {
                    if (!child_Buttons.Success)
                        return;

                    if (ImGuiEx.IconButtonWithText(FontAwesomeIcon.Play, "Start Gathering"))
                    {

                    }
                    ImGui.SameLine();
                    if (ImGuiEx.IconButtonWithText(FontAwesomeIcon.Square, "Stop"))
                    {

                    }

                    if (ImGui.Button("Search for item"))
                        OpenItemPicker();

                    // Must be drawn in the same ID scope as the button above
                    DrawItemPicker();

                    _modeWindow = ImGui.GetCursorPosY() + ImGui.GetStyle().WindowPadding.Y;
                }

                using (var child_Table = ImRaii.Child("Gather Playlist: Table", ImGui.GetContentRegionAvail(), true))
                {
                    if (!child_Table.Success)
                        return;

                    var gatherList = C.GatherList;
                    if (gatherList.Count == 0)
                    {
                        ImGui.TextDisabled("We have no items to gather... sadge. Please add some?");
                        return;
                    }

                    _gatherDragDrop.Begin();

                    // ScrollY removed: it turns the table into its own child window,
                    // which breaks the cursor positions RealtimeDragDrop stores.
                    using (var table = ImRaii.Table("Gatherable Items", 6, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
                    {
                        if (!table.Success)
                            return;

                        ImGui.TableSetupColumn("##Order");
                        ImGui.TableSetupColumn("Item");
                        ImGui.TableSetupColumn("Gather");
                        ImGui.TableSetupColumn("Have");
                        ImGui.TableSetupColumn("Route(s)");
                        ImGui.TableSetupColumn("Remove");

                        ImGui.TableHeadersRow();

                        var timedList = NodeList(true);
                        var normalList = NodeList(false);

                        for (int i = 0; i < gatherList.Count; i++)
                        {
                            var item = gatherList[i];
                            var sheetInfo = Gather_Util.Sheet_ItemInfo[item.ItemId];
                            var uniqueId = item.ItemId.ToString();

                            ImGui.PushID($"{item.ItemId}_{sheetInfo.Name}");

                            ImGui.TableNextRow();
                            ImGui.TableSetColumnIndex(0);
                            _gatherDragDrop.NextRow();
                            _gatherDragDrop.SetRowColor(uniqueId);
                            _gatherDragDrop.DrawButtonDummy(uniqueId, gatherList, i);

                            ImGui.TableNextColumn();
                            ImGui_Ice.ImageButtonWithText(sheetInfo.IconId, sheetInfo.Name, sheetInfo.Name);

                            ImGui.TableNextColumn();
                            var amount = item.GatherAmount;
                            ImGui.SetNextItemWidth(100);
                            if (ImGui.InputUInt($"##GatherAmount_{sheetInfo.Name}_{item.ItemId}", ref amount, 1, 10))
                            {
                                item.GatherAmount = amount;
                                C.SaveDebounced();
                            }

                            ImGui.TableNextColumn();
                            ImGui.Text($"{Utils.GetItemCount(item.ItemId):N0}");

                            ImGui.TableNextColumn();
                            if (sheetInfo.NormalRoutes.Count == 1 || sheetInfo.TimedRoutes.Count == 1)
                            {
                                // IDEALLY... this will only show the one route that a user can do. 
                                var route = C.ItemRoutes[item.ItemId].EnabledRoutes.First();
                                ImGui.AlignTextToFramePadding();
                                ImGui.Text($"{route}");
                            }
                            else if (sheetInfo.NormalRoutes.Count > 1)
                            {
                                // We have multiple routes that can be selected, so we need to select which one we wanna do
                                var route = C.ItemRoutes[item.ItemId].EnabledRoutes.First();
                                if (ImGui.Button($"{route}"))
                                {
                                    RouteSelector_OpenPopup(item.ItemId);
                                }
                                if (ImGui.IsItemHovered())
                                    ImGui.SetTooltip("Select which route you would like to do");
                            }
                            else if (sheetInfo.TimedRoutes.Count > 1)
                            {
                                var firstRoute = C.ItemRoutes[item.ItemId].EnabledRoutes.First();
                                var count = C.ItemRoutes[item.ItemId].EnabledRoutes.Count();

                                string text = count > 1 ? $"{firstRoute} + {count - 1}" : $"{firstRoute}";
                                if (ImGui.Button(text))
                                {
                                    RouteSelector_OpenPopup(item.ItemId);
                                }
                                if (ImGui.IsItemHovered())
                                    ImGui.SetTooltip("Select which routes you would like to run");
                            }

                            ImGui.TableNextColumn();
                            if (ImGuiEx.IconButton(FontAwesomeIcon.Trash, $"Remove_{item.ItemId}", enabled: ImGui.IsKeyDown(ImGuiKey.LeftShift) || ImGui.IsKeyDown(ImGuiKey.RightShift)))
                            {
                                _itemToRemove = item;
                            }

                            ImGui.PopID();
                        }
                    }

                    if (C.GatherList.Contains(_itemToRemove))
                    {
                        C.GatherList.Remove(_itemToRemove);
                        C.SaveDebounced();
                        _itemToRemove = new();
                    }

                    _gatherDragDrop.End();
                }

                RouteSelector_Popup();
            }
        }

        private static uint ResolveGatherItemId(uint itemId)
        {
            var reduceInfo = Gather_Util.ReducableItems.FirstOrDefault(x =>
                   x.ResultItems.Count != 0
               && (x.ItemId == itemId || x.SublimeItemId == itemId || x.ResultItems[0].ItemId == itemId));

            return reduceInfo?.ResultItems[0].ItemId ?? itemId;
        }

        private static bool IsItemInList(uint itemId)
        {
            var targetId = ResolveGatherItemId(itemId);
            return C.GatherList.Any(x => x.ItemId == targetId);
        }

        private static void AddItem(uint itemId)
        {
            var targetId = ResolveGatherItemId(itemId);

            if (C.GatherList.Any(x => x.ItemId == targetId))
                return;

            if (!Gather_Util.Sheet_ItemInfo.TryGetValue(targetId, out var itemInfo))
                return;

            C.GatherList.Add(new Config.ItemInfo { ItemId = targetId, GatherAmount = 1 });

            // Make sure the item has usable routes enabled
            if (C.ItemRoutes.TryGetValue(targetId, out var itemConfig))
            {
                var enabled = itemConfig.EnabledRoutes;

                // If both route types exist, only normal routes should be used
                if (itemInfo.TimedRoutes.Count != 0 && itemInfo.NormalRoutes.Count != 0)
                {
                    foreach (var route in itemInfo.TimedRoutes)
                        enabled.Remove(route);
                }

                // Nothing enabled: fall back to the first normal route, otherwise all timed routes
                if (enabled.Count == 0)
                {
                    if (itemInfo.NormalRoutes.Count != 0)
                        enabled.Add(itemInfo.NormalRoutes[0]);
                    else
                        foreach (var route in itemInfo.TimedRoutes)
                            enabled.Add(route);
                }
            }

            C.SaveDebounced();
        }

        private static List<Config.ItemInfo> NodeList(bool timed)
        {
            var result = new List<Config.ItemInfo>();
            foreach (var item in C.GatherList)
            {
                if (!Gather_Util.Sheet_ItemInfo.TryGetValue(item.ItemId, out var itemInfo))
                    continue;

                var routeCount = timed ? itemInfo.TimedRoutes.Count : itemInfo.NormalRoutes.Count;
                if (routeCount == 0)
                    continue;

                result.Add(item);
            }
            return result;
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
                        else
                        {
                            var isEnabled = config.Contains(route);
                            if (ImGui.Checkbox($"##{route}", ref isEnabled))
                            {
                                if (isEnabled)
                                    config.Add(route);
                                else
                                    config.Remove(route);

                                C.Save();
                            }
                        }

                        ImGui.TableNextColumn();
                        ImGui.Text($"{route}");

                        if (!Gather_Util.Sheet_RouteInfo.TryGetValue(route, out var routeInfo))
                            continue;

                        ImGui.TableNextColumn();
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
                var added = IsItemInList(itemId);
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

                AddItem(itemId);
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
    }
}