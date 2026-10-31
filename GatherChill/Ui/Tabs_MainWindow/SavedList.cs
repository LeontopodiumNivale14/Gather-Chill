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

        #region Gather Saved List

        private static string _searchText = "";
        private static int _renameFolderId = -1;
        private static string _renameBuffer = "";
        private static bool _openRenamePopup = false;
        private static readonly Dictionary<uint, string> _itemNameCache = new();
        private static string GetItemName(uint itemId)
        {
            if (_itemNameCache.TryGetValue(itemId, out var cached))
                return cached;

            // Swap this for whatever item-name helper you already use
            var name = Sheet_ItemInfo[itemId].Name;
            _itemNameCache[itemId] = Sheet_ItemInfo[itemId].Name;
            return name;
        }
        private static bool ListMatchesSearch(Config.Class_List list)
        {
            if (list.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase))
                return true;

            if (list.Description.Contains(_searchText, StringComparison.OrdinalIgnoreCase))
                return true;

            return list.ItemList.Any(i => GetItemName(i.ItemId).Contains(_searchText, StringComparison.OrdinalIgnoreCase));
        }
        private static IOrderedEnumerable<Config.Class_List> SortLists(IEnumerable<Config.Class_List> lists)
        {
            return C.SortMode switch
            {
                Config.ListSortMode.RecentlyModified => lists
                    .OrderByDescending(l => l.LastModified)
                    .ThenBy(l => l.Name, StringComparer.OrdinalIgnoreCase),
                Config.ListSortMode.Manual => lists
                    .OrderBy(l => l.SortOrder)
                    .ThenBy(l => l.Name, StringComparer.OrdinalIgnoreCase),
                _ => lists.OrderBy(l => l.Name, StringComparer.OrdinalIgnoreCase),
            };
        }
        private static void GatherSavedList(List<Config.Class_List> gatherList)
        {
            var scale = ImGuiHelpers.GlobalScale;
            var selectionWindow = new Vector2(200 * scale, ImGui.GetContentRegionAvail().Y);

            using var listSelect = ImRaii.Child("Gather Selection Window", selectionWindow, true);
            if (!listSelect.Success)
                return;

            // Open + draw at window level so the popup IDs match
            if (_openRenamePopup)
            {
                ImGui.OpenPopup("RenameFolder");
                _openRenamePopup = false;
            }
            DrawRenameFolderPopup();

            DrawListToolbar();
            ImGui.Separator();

            // Searching: flat, sorted results, folders ignored
            if (!string.IsNullOrWhiteSpace(_searchText))
            {
                foreach (var list in SortLists(gatherList.Where(ListMatchesSearch)))
                    DrawListEntry(list);

                return;
            }

            // Folders first
            foreach (var folder in SortFolders())
                DrawFolder(folder, gatherList);

            // Then root lists (no folder, or folder no longer exists)
            var rootLists = gatherList
                .Where(l => l.FolderId == null || C.FindFolder(l.FolderId.Value) == null);

            foreach (var list in SortLists(rootLists))
                DrawListEntry(list);
        }
        private static void DrawFolderContextMenu(Config.Class_Folder folder)
        {
            if (!ImGui.BeginPopupContextItem($"##folder_ctx_{folder.Id}"))
                return;

            if (ImGui.MenuItem("Rename"))
            {
                _renameFolderId = folder.Id;
                _renameBuffer = folder.Name;
                _openRenamePopup = true;
            }

            if (CanReorder)
            {
                ImGui.Separator();

                if (ImGui.MenuItem("Move up"))
                    C.MoveFolder(folder, -1);

                if (ImGui.MenuItem("Move down"))
                    C.MoveFolder(folder, 1);

                ImGui.Separator();
            }

            if (ImGui.MenuItem("Delete (lists move to root)"))
                C.DeleteFolder(folder.Id);

            ImGui.EndPopup();
        }
        private static void DrawListEntry(Config.Class_List list)
        {
            using var id = ImRaii.PushId($"list_{list.Id}");

            if (ImGui.Selectable(list.Name, _selectedListId == list.Id))
                _selectedListId = list.Id;

            if (!ImGui.BeginPopupContextItem($"##list_ctx_{list.Id}"))
                return;

            if (CanReorder)
            {
                if (ImGui.MenuItem("Move up"))
                    C.MoveList(list, -1);

                if (ImGui.MenuItem("Move down"))
                    C.MoveList(list, 1);

                ImGui.Separator();
            }

            if (ImGui.BeginMenu("Move to folder"))
            {
                if (ImGui.MenuItem("(None)", "", list.FolderId == null))
                    C.MoveListToFolder(list, null);

                foreach (var folder in SortFolders())
                {
                    if (ImGui.MenuItem(folder.Name, "", list.FolderId == folder.Id))
                        C.MoveListToFolder(list, folder.Id);
                }

                ImGui.EndMenu();
            }

            ImGui.EndPopup();
        }
        private static void DrawRenameFolderPopup()
        {
            if (!ImGui.BeginPopup("RenameFolder"))
                return;

            ImGui.SetNextItemWidth(180 * ImGuiHelpers.GlobalScale);
            var submitted = ImGui.InputText("##RenameFolderInput", ref _renameBuffer, 64,
                ImGuiInputTextFlags.EnterReturnsTrue);

            if (submitted && !string.IsNullOrWhiteSpace(_renameBuffer))
            {
                var folder = C.FindFolder(_renameFolderId);
                if (folder != null)
                    folder.Name = _renameBuffer.Trim();

                ImGui.CloseCurrentPopup();
            }

            ImGui.EndPopup();
        }
        private static void DrawListToolbar()
        {
            var scale = ImGuiHelpers.GlobalScale;

            ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X - 30 * scale);
            ImGui.InputTextWithHint("##ListSearch", "Search lists or items...", ref _searchText, 64);

            ImGui.SameLine();
            if (ImGui.Button("+##NewFolder"))
                C.AddFolder();

            if (ImGui.IsItemHovered())
                ImGui.SetTooltip("New folder");

            ImGui.AlignTextToFramePadding();
            ImGui.TextUnformatted("Sort:");

            ImGui.SameLine();
            ImGui.SetNextItemWidth(-1);
            using var combo = ImRaii.Combo("##ListSort", SortModeLabel(C.SortMode));

            if (!combo.Success)
                return;

            foreach (var mode in Enum.GetValues<Config.ListSortMode>())
            {
                if (ImGui.Selectable(SortModeLabel(mode), C.SortMode == mode))
                    C.SortMode = mode;
            }
        }
        private static void DrawFolder(Config.Class_Folder folder, List<Config.Class_List> gatherList)
        {
            using var id = ImRaii.PushId($"folder_{folder.Id}");

            using var node = ImRaii.TreeNode($"{folder.Name}###folder_{folder.Id}");
            DrawFolderContextMenu(folder);

            if (!node.Success)
                return;

            foreach (var list in SortLists(gatherList.Where(l => l.FolderId == folder.Id)))
                DrawListEntry(list);
        }
        private static string SortModeLabel(Config.ListSortMode mode) => mode switch
        {
            Config.ListSortMode.RecentlyModified => "Recently modified",
            Config.ListSortMode.Manual => "Manual",
            _ => "Name",
        };
        private static IEnumerable<Config.Class_Folder> SortFolders()
        {
            return C.SortMode == Config.ListSortMode.Manual
                ? C.GetSortedFolders()
                : C.Saved_Folders.OrderBy(f => f.Name, StringComparer.OrdinalIgnoreCase).ToList();
        }
        private static bool CanReorder => C.SortMode == Config.ListSortMode.Manual && string.IsNullOrWhiteSpace(_searchText);

        #endregion

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
