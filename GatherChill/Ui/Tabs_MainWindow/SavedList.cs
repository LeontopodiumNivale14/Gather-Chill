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
using static GatherChill.ConfigFiles.Config;

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

        #region State

        private static string _searchText = "";
        private static string _renameBuffer = "";
        private static int _renameFolderId = -1;
        private static int _renameListId = -1;
        private static bool _openRenamePopup = false;

        private const string ListPayload = "GC_LIST";
        private const string FolderPayload = "GC_FOLDER";
        private static int _draggedListId = -1;
        private static int _draggedFolderId = -1;

        private const ImGuiDragDropFlags LineFlags = ImGuiDragDropFlags.AcceptBeforeDelivery | ImGuiDragDropFlags.AcceptNoDrawDefaultRect;

        private static readonly Dictionary<uint, string> _itemNameCache = new();

        // Reordering only makes sense in Manual mode, and not while search flattens everything
        private static bool CanReorder => C.SortMode == Config.ListSortMode.Manual && string.IsNullOrWhiteSpace(_searchText);

        #endregion

        #region Main Draw

        private static void GatherSavedList(List<Config.Class_List> gatherList)
        {
            var scale = ImGuiHelpers.GlobalScale;
            var style = ImGui.GetStyle();

            var width = 200 * scale;
            var padding = style.WindowPadding.Y * 2;

            // Toolbar is two rows (search + folder button, then sort), footer is one row
            var topHeight = ImGui.GetFrameHeight() * 2 + style.ItemSpacing.Y + padding;
            var bottomHeight = ImGui.GetFrameHeight() + padding;

            // Whatever is left, minus the gaps between the three children
            var middleHeight = Math.Max(
                ImGui.GetContentRegionAvail().Y - topHeight - bottomHeight - style.ItemSpacing.Y * 2,
                50 * scale);

            // Open + draw at the parent level so the popup IDs match
            if (_openRenamePopup)
            {
                ImGui.OpenPopup("RenameEntry");
                _openRenamePopup = false;
            }
            DrawRenamePopup();

            // Group makes the three children behave as one block, so a SameLine()
            // for whatever draws next to this panel lines up with the top of it
            using var group = ImRaii.Group();

            // Each child has to end before the next one begins, so these are scoped blocks
            using (var top = ImRaii.Child("##GatherToolbar", new Vector2(width, topHeight), true))
            {
                if (top.Success)
                    DrawListToolbar();
            }

            using (var middle = ImRaii.Child("##GatherListScroll", new Vector2(width, middleHeight), true))
            {
                if (middle.Success)
                    DrawListContents(gatherList);
            }

            using (var bottom = ImRaii.Child("##GatherFooter", new Vector2(width, bottomHeight), true))
            {
                if (bottom.Success)
                    DrawImportExportFooter();
            }
        }
        private static void DrawListContents(List<Config.Class_List> gatherList)
        {
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

            DrawRootDropZone();
        }

        private static void DrawListToolbar()
        {
            var scale = ImGuiHelpers.GlobalScale;

            ImGui.SetNextItemWidth(ImGui.GetContentRegionAvail().X - 30 * scale);
            ImGui.InputTextWithHint("##ListSearch", "Search lists or items...", ref _searchText, 64);

            ImGui.SameLine();
            if (ImGuiEx.IconButton(FontAwesomeIcon.Folder, "##NewFolder"))
            {
                C.AddFolder();
                C.Save();
            }

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
                if (!ImGui.Selectable(SortModeLabel(mode), C.SortMode == mode))
                    continue;

                C.SortMode = mode;
                C.Save();
            }
        }

        private static void DrawFolder(Config.Class_Folder folder, List<Config.Class_List> gatherList)
        {
            using var id = ImRaii.PushId($"folder_{folder.Id}");

            using var node = ImRaii.TreeNode($"{folder.Name}###folder_{folder.Id}");

            // All of these attach to the tree node, so they must come right after it
            FolderDragSource(folder);
            FolderDropTarget(folder);
            DrawFolderContextMenu(folder);

            if (!node.Success)
                return;

            foreach (var list in SortLists(gatherList.Where(l => l.FolderId == folder.Id)))
                DrawListEntry(list);
        }

        private static void DrawListEntry(Config.Class_List list)
        {
            using var id = ImRaii.PushId($"list_{list.Id}");

            if (ImGui.Selectable(list.Name, _selectedListId == list.Id))
                _selectedListId = list.Id;

            // Attach to the Selectable, so they must come right after it
            ListDragSource(list);
            ListDropTarget(list);

            if (!ImGui.BeginPopupContextItem($"##list_ctx_{list.Id}"))
                return;

            if (ImGui.MenuItem("Rename"))
            {
                _renameListId = list.Id;
                _renameFolderId = -1;
                _renameBuffer = list.Name;
                _openRenamePopup = true;
            }

            if (ImGui.MenuItem("Export"))
                ExportList(list);

            ImGui.Separator();

            if (CanReorder)
            {
                if (ImGui.MenuItem("Move up"))
                {
                    C.MoveList(list, -1);
                    C.Save();
                }

                if (ImGui.MenuItem("Move down"))
                {
                    C.MoveList(list, 1);
                    C.Save();
                }

                ImGui.Separator();
            }

            if (ImGui.BeginMenu("Move to folder"))
            {
                if (ImGui.MenuItem("(None)", "", list.FolderId == null))
                {
                    C.MoveListToFolder(list, null);
                    C.Save();
                }

                foreach (var folder in SortFolders())
                {
                    if (!ImGui.MenuItem(folder.Name, "", list.FolderId == folder.Id))
                        continue;

                    C.MoveListToFolder(list, folder.Id);
                    C.Save();
                }

                ImGui.EndMenu();
            }

            ImGui.EndPopup();
        }

        #endregion

        #region Menus And Popups

        private static void DrawFolderContextMenu(Config.Class_Folder folder)
        {
            if (!ImGui.BeginPopupContextItem($"##folder_ctx_{folder.Id}"))
                return;

            if (ImGui.MenuItem("Rename"))
            {
                _renameFolderId = folder.Id;
                _renameListId = -1;
                _renameBuffer = folder.Name;
                _openRenamePopup = true;
            }

            if (ImGui.MenuItem("Export folder"))
                ExportFolder(folder);

            if (CanReorder)
            {
                ImGui.Separator();

                if (ImGui.MenuItem("Move up"))
                {
                    C.MoveFolder(folder, -1);
                    C.Save();
                }

                if (ImGui.MenuItem("Move down"))
                {
                    C.MoveFolder(folder, 1);
                    C.Save();
                }
            }

            ImGui.Separator();

            if (ImGui.MenuItem("Delete (lists move to root)"))
            {
                C.DeleteFolder(folder.Id);
                C.Save();
            }

            ImGui.EndPopup();
        }

        private static void DrawRenamePopup()
        {
            if (!ImGui.BeginPopup("RenameEntry"))
                return;

            ImGui.SetNextItemWidth(180 * ImGuiHelpers.GlobalScale);

            // Start typing right away
            if (ImGui.IsWindowAppearing())
                ImGui.SetKeyboardFocusHere();

            var submitted = ImGui.InputText("##RenameInput", ref _renameBuffer, 64,
                ImGuiInputTextFlags.EnterReturnsTrue);

            if (submitted && !string.IsNullOrWhiteSpace(_renameBuffer))
            {
                var name = _renameBuffer.Trim();

                if (_renameListId != -1)
                {
                    if (C.FindPlaylist(_renameListId) is { } list)
                        C.RenameList(list, name);
                }
                else if (C.FindFolder(_renameFolderId) is { } folder)
                {
                    folder.Name = name;
                }

                C.Save();
                ImGui.CloseCurrentPopup();
            }

            ImGui.EndPopup();
        }

        #endregion

        #region Drag And Drop

        private static void ListDragSource(Config.Class_List list)
        {
            if (!CanReorder)
                return;

            if (!ImGui.BeginDragDropSource())
                return;

            _draggedListId = list.Id;
            ImGui.SetDragDropPayload(ListPayload, BitConverter.GetBytes(list.Id));
            ImGui.TextUnformatted(list.Name);
            ImGui.EndDragDropSource();
        }

        private static void ListDropTarget(Config.Class_List target)
        {
            if (!CanReorder)
                return;

            var after = IsMouseInLowerHalf();

            if (!ImGui.BeginDragDropTarget())
                return;

            var payload = ImGui.AcceptDragDropPayload(ListPayload, LineFlags);

            // No line when hovering the list you're dragging
            if (!payload.IsNull && _draggedListId != target.Id)
            {
                DrawInsertionLine(after);

                if (payload.IsDelivery() && C.FindPlaylist(_draggedListId) is { } dragged)
                {
                    C.MoveListNextTo(dragged, target, after);
                    C.Save();
                }
            }

            ImGui.EndDragDropTarget();
        }

        private static void FolderDragSource(Config.Class_Folder folder)
        {
            if (!CanReorder)
                return;

            if (!ImGui.BeginDragDropSource())
                return;

            _draggedFolderId = folder.Id;
            ImGui.SetDragDropPayload(FolderPayload, BitConverter.GetBytes(folder.Id));
            ImGui.TextUnformatted(folder.Name);
            ImGui.EndDragDropSource();
        }

        // Folders accept lists (move inside, default highlight) and other folders (reorder, line)
        private static void FolderDropTarget(Config.Class_Folder target)
        {
            if (!CanReorder)
                return;

            var after = IsMouseInLowerHalf();

            if (!ImGui.BeginDragDropTarget())
                return;

            var listPayload = ImGui.AcceptDragDropPayload(ListPayload);
            if (!listPayload.IsNull && C.FindPlaylist(_draggedListId) is { } list)
            {
                C.MoveListToFolder(list, target.Id);
                C.Save();
            }

            var folderPayload = ImGui.AcceptDragDropPayload(FolderPayload, LineFlags);
            if (!folderPayload.IsNull && _draggedFolderId != target.Id)
            {
                DrawInsertionLine(after);

                if (folderPayload.IsDelivery() && C.FindFolder(_draggedFolderId) is { } folder)
                {
                    C.MoveFolderNextTo(folder, target, after);
                    C.Save();
                }
            }

            ImGui.EndDragDropTarget();
        }

        // Empty space at the bottom of the panel: drop a list here to move it to the root
        private static void DrawRootDropZone()
        {
            if (!CanReorder)
                return;

            var height = Math.Max(24 * ImGuiHelpers.GlobalScale, ImGui.GetContentRegionAvail().Y);
            ImGui.InvisibleButton("##RootDrop", new Vector2(-1, height));

            if (!ImGui.BeginDragDropTarget())
                return;

            var payload = ImGui.AcceptDragDropPayload(ListPayload);
            if (!payload.IsNull && C.FindPlaylist(_draggedListId) is { } list)
            {
                C.MoveListToFolder(list, null);
                C.Save();
            }

            ImGui.EndDragDropTarget();
        }

        // Draws the line between items: top edge for "before", bottom edge for "after".
        // Offsetting by half the item spacing puts it in the gap, so the "after" line of
        // one item and the "before" line of the next are in the same spot.
        // Fucking drag drop is black magic is2g
        private static void DrawInsertionLine(bool after)
        {
            var min = ImGui.GetItemRectMin();
            var max = ImGui.GetItemRectMax();
            var halfGap = ImGui.GetStyle().ItemSpacing.Y * 0.5f;
            var y = after ? max.Y + halfGap : min.Y - halfGap;

            ImGui.GetWindowDrawList().AddLine(
                new Vector2(min.X, y),
                new Vector2(max.X, y),
                ImGui.GetColorU32(ImGuiCol.DragDropTarget),
                2f * ImGuiHelpers.GlobalScale);
        }

        // Screen Y grows downward, so a bigger Y means lower on screen.
        // "Mouse Y > midpoint" = the mouse is in the bottom half = drop AFTER the item.
        private static bool IsMouseInLowerHalf()
        {
            var min = ImGui.GetItemRectMin();
            var max = ImGui.GetItemRectMax();
            return ImGui.GetMousePos().Y > (min.Y + max.Y) * 0.5f;
        }

        #endregion

        #region Sorting And Search

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

        private static IEnumerable<Config.Class_Folder> SortFolders()
        {
            return C.SortMode == Config.ListSortMode.Manual
                ? C.GetSortedFolders()
                : C.Saved_Folders.OrderBy(f => f.Name, StringComparer.OrdinalIgnoreCase).ToList();
        }

        private static string SortModeLabel(Config.ListSortMode mode) => mode switch
        {
            Config.ListSortMode.RecentlyModified => "Recently modified",
            Config.ListSortMode.Manual => "Manual",
            _ => "Name",
        };

        private static bool ListMatchesSearch(Config.Class_List list)
        {
            if (list.Name.Contains(_searchText, StringComparison.OrdinalIgnoreCase))
                return true;

            if (list.Description.Contains(_searchText, StringComparison.OrdinalIgnoreCase))
                return true;

            return list.ItemList.Any(i => GetItemName(i.ItemId).Contains(_searchText, StringComparison.OrdinalIgnoreCase));
        }

        private static string GetItemName(uint itemId)
        {
            if (_itemNameCache.TryGetValue(itemId, out var cached))
                return cached;

            var name = Sheet_ItemInfo[itemId].Name;
            _itemNameCache[itemId] = name;
            return name;
        }

        #endregion

        #region Import / Export

        private static void DrawImportExportFooter()
        {
            var spacing = ImGui.GetStyle().ItemSpacing.X;
            var width = (ImGui.GetContentRegionAvail().X - spacing) * 0.5f;

            var selected = C.FindPlaylist(_selectedListId);
            var exportLabel = selected != null ? "Export" : "Export All";

            if (ImGui.Button($"{exportLabel}##ExportButton", new Vector2(width, 0)))
            {
                if (selected != null)
                    ExportList(selected);
                else
                    ExportAll();
            }

            if (ImGui.IsItemHovered())
            {
                ImGui.SetTooltip(selected != null
                    ? $"Copy \"{selected.Name}\" to the clipboard"
                    : "Copy every folder and list to the clipboard");
            }

            ImGui.SameLine();
            if (ImGui.Button("Import", new Vector2(width, 0)))
                ImportFromClipboard();

            if (ImGui.IsItemHovered())
                ImGui.SetTooltip("Add the folders and lists copied to the clipboard");
        }
        private static void ExportAll()
        {
            if (C.Saved_List.Count == 0 && C.Saved_Folders.Count == 0)
            {
                ShowResult(false, "Nothing to export yet.");
                return;
            }

            ImGui.SetClipboardText(C.ExportAll());
            ShowResult(true, "Copied all folders and lists to the clipboard.");
        }
        private static void ExportList(Config.Class_List list)
        {
            ImGui.SetClipboardText(C.Export(Array.Empty<Config.Class_Folder>(), new[] { list }));
            ShowResult(true, $"Copied \"{list.Name}\" to the clipboard.");
        }
        private static void ExportFolder(Config.Class_Folder folder)
        {
            var lists = C.Saved_List.Where(l => l.FolderId == folder.Id).ToList();

            ImGui.SetClipboardText(C.Export(new[] { folder }, lists));
            ShowResult(true, $"Copied folder \"{folder.Name}\" and its {lists.Count} list(s) to the clipboard.");
        }
        private static void ImportFromClipboard()
        {
            var result = C.Import(ImGui.GetClipboardText());
            if (result.Success)
                C.Save();

            ShowResult(result.Success, result.Message);
        }
        private static void ShowResult(bool success, string message)
        {
            if (success)
                Notify.Success(message);
            else
                Notify.Error(message);
        }

        #endregion

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
