using Dalamud.Bindings.ImPlot;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Interface.Utility.Raii;
using ECommons.GameHelpers;
using ECommons.Logging;
using FFXIVClientStructs.FFXIV.Client.Game;
using GatherChill.GatheringInfo;
using GatherChill.Scheduler;
using GatherChill.Scheduler.Tasks;
using GatherChill.Utilities;
using GatherChill.Utilities.GatheringHelpers;
using Lumina.Excel.Sheets;
using Pictomancy;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace GatherChill.Ui.RouteWindowTabs
{
    internal class EditRouteV2
    {
        private readonly string[] expansionNames = { "ARR", "HW", "Stb", "ShB", "EW", "DT" };
        private readonly string[] gatherJobs = { "MIN [16]", "BTN [17]", "FSH [18]" };

        private string searchFilter = "";
        private int selectedGroupIndex = 0;
        private int selectedNodeIndex = -1;
        private int selectedLocationIndex = -1;

        // Drag->Drop Bullshit... honestly might just make it a right click -> "Move into this group" to save a ton of code. . . 
        private int _draggedNodeGroupIndex = -1;
        private int _draggedNodeIndex = -1;
        private int _draggedLocationIndex = -1;
        private bool _isDragging = false;

        private float navRadius = 0.1f;

        private readonly Random _random = new Random();

        public void Draw(uint selectedRoute)
        {
            ImGui.AlignTextToFramePadding();
            ImGui.Text($"Viewing Route: {selectedRoute}");
            if (!P.routeLoader.Routes.TryGetValue(selectedRoute, out var routeInfo))
            {
                ImGui.Text("Route is not in the database... that shouldn't be right");
                return;
            }

            ImGui.SameLine();
            if (ImGui.Button("Save Changes"))
            {
                SaveRouteChanges(routeInfo);
            }
            ImGui.SameLine();
            if (ImGui.Button("Stop current task"))
            {
                P.taskManager.Tasks.Clear();
                P.taskManager.Abort();
                if (P.navmesh.IsRunning())
                {
                    P.navmesh.Stop();
                }
                SchedulerMain.State = Enums.IceState.Idle;
                SchedulerMain.ItemId = null;
                SchedulerMain.RouteId = null;
            }
            if (P.taskManager.CurrentTask != null)
            {
                ImGui.SameLine();
                ImGui.Text($"Current Task: {P.taskManager.CurrentTask.Name}");
            }

            if (ImGui.BeginTabBar("RouteEditorTabs"))
            {
                if (ImGui.BeginTabItem("Route Info"))
                {
                    RouteInfoWindow(routeInfo);
                    ImGui.EndTabItem();
                }

                if (ImGui.BeginTabItem("All Items"))
                {
                    RouteItemWindow(selectedRoute);
                    ImGui.EndTabItem();
                }

                if (ImGui.BeginTabItem("Route Node Info"))
                {
                    Route_NodeInfo(routeInfo);
                    ImGui.EndTabItem();
                }

                if (ImGui.BeginTabItem("Node Viewer"))
                {
                    NodeViewer(routeInfo);
                    ImGui.EndTabItem();
                }

                ImGui.EndTabBar();
            }
        }

        private void RouteInfoWindow(GatheringRoute route)
        {
            ImGui.Text("Route Info");
            ImGui.Separator();

            var zoneName = route.ZoneName;
            ImGui.SetNextItemWidth(200);
            if (ImGui.InputText("Zone Name", ref zoneName))
            {
                route.ZoneName = zoneName;
            }

            ImGui.SetNextItemWidth(200);
            var placeName = route.PlaceName;
            if (ImGui.InputText("Place Name", ref placeName))
            {
                route.PlaceName = placeName;
            }

            ImGui.SetNextItemWidth(200);
            var expansion = (int)route.ExpansionId;
            if (ImGui.Combo("Expansion", ref expansion, expansionNames, expansionNames.Length))
            {
                route.ExpansionId = (uint)expansion;
            }

            ImGui.SetNextItemWidth(200);
            var jobId = (int)route.GatheringJobId - 16;
            if (ImGui.Combo("Gathering Job", ref jobId, gatherJobs, gatherJobs.Length))
            {
                var adjustedId = jobId + 16;
                route.GatheringJobId = (uint)adjustedId;
            }

            ImGui.SetNextItemWidth(150);
            var level = route.LevelRequirement;
            if (ImGui.InputInt("Level Requirement", ref level))
            {
                level = Math.Clamp(level, 1, 100);
                route.LevelRequirement = level;
            }

            var requiresFolklore = route.RequiresFolklore;
            if (ImGui.Checkbox("Requires Folklore", ref requiresFolklore))
            {
                route.RequiresFolklore = requiresFolklore;
            }

            if (requiresFolklore)
            {
                ImGui.SetNextItemWidth(300);
                var folkloreBook = route.FolkloreBook;
                if (ImGui.InputText("Folklore Book", ref folkloreBook))
                {
                    route.FolkloreBook = folkloreBook;
                }
            }

            bool timedNode = route.TimedNode;
            if (ImGui.Checkbox("Timed Node?", ref timedNode))
            {
                route.TimedNode = timedNode;
            }

            ImGui.SetNextItemWidth(300);
            var authors = route.Author;
            if (ImGui.InputText("Author", ref authors))
            {
                route.Author = authors;
            }

            ImGui.Dummy(new Vector2(0, 5));
            ImGui.Separator();
            ImGui.AlignTextToFramePadding();
            ImGui.Text($"Last Updated: {route.LastUpdated}");
            ImGui.SameLine();
            if (ImGui.Button("Update Timestamp"))
            {
                route.LastUpdated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        private void RouteItemWindow(uint selectedRoute)
        {
            if (!Utils.SheetInfo.TryGetValue(selectedRoute, out var sheetInfo))
            {
                ImGui.Text("Hmm... No items seem to exist here. Please report this");
                return;
            }

            ImGui.SetNextItemWidth(300);
            ImGui.InputTextWithHint("##RouteItem_Itemsearch", "Search items...", ref searchFilter, 300);


            if (ImGui.BeginTable("ItemInfo_RouteEditorV2", 4, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.ScrollY))
            {
                ImGui.TableSetupColumn("Icon");
                ImGui.TableSetupColumn("Item Name");
                ImGui.TableSetupColumn("Item ID");
                ImGui.TableSetupScrollFreeze(0, 1);
                ImGui.TableHeadersRow();

                var imageSize = new Vector2(24, 24);

                foreach (var item in sheetInfo.ItemIds)
                {
                    if (Svc.Data.GetExcelSheet<Item>().TryGetRow(item, out var itemInfo))
                    {
                        var itemName = itemInfo.Name.ToString();
                        if (!string.IsNullOrEmpty(searchFilter) &&
                            !itemName.Contains(searchFilter, StringComparison.OrdinalIgnoreCase))
                            continue;

                        ImGui.TableNextRow();
                        ImGui.TableSetColumnIndex(0);

                        ImGui.PushID($"Item_{itemInfo.RowId}");

                        if (Svc.Texture.TryGetFromGameIcon((int)itemInfo.Icon, out var icon))
                        {
                            ImGui.Image(icon.GetWrapOrEmpty().Handle, imageSize);
                        }

                        ImGui.TableNextColumn();
                        ImGui_Util.Table_VertCenterText(itemName);

                        ImGui.TableNextColumn();
                        ImGui_Util.Table_VertCenterText($"{item}");

                        ImGui.TableNextColumn();
                        if (ImGui.Button("Gather Item"))
                        {
                            SchedulerMain.ItemId = itemInfo.RowId;
                            SchedulerMain.RouteId = selectedRoute;
                            SchedulerMain.State = Enums.IceState.Start;
                        }

                        ImGui.PopID();
                    }
                }

                ImGui.EndTable();
            }
        }

        private void Route_NodeInfo(GatheringRoute route)
        {
            using (var routeGroups = ImRaii.Child("Route_RouteGroups", new Vector2(300, 200), true))
            {
                if (!routeGroups.Success)
                {

                }

                ImGui.Text("Node Groups");
                if (ImGui.Button("Add New Group"))
                {
                    var newGroupId = route.NodeGroups.Count > 0 ? route.NodeGroups.Max(g => g.GroupId) + 1
                                                                : 1;
                    route.NodeGroups.Add(new NodeGroup { GroupId = newGroupId });
                }
                if (ImGui.Button("Add Missing Nodes"))
                {
                    foreach (var obj in Svc.Objects.Where(x => x.ObjectKind == ObjectKind.GatheringPoint))
                    {
                        P.routeLoader.AddNodeLocationIfMissing(route, obj.BaseId, obj.Position);
                    }
                }

                ImGui.Separator();
                for (int i = 0; i < route.NodeGroups.Count; i++)
                {
                    var group = route.NodeGroups[i];
                    bool isSelected = selectedGroupIndex == i;

                    if (ImGui.Selectable($"Group {group.GroupId} ({group.Nodes.Count} nodes)##group{i}", isSelected))
                    {
                        selectedGroupIndex = i;
                        selectedNodeIndex = -1;
                        selectedLocationIndex = -1;
                    }

                    if (ImGui.BeginPopupContextItem($"groupcontext{i}"))
                    {
                        using (ImRaii.Disabled(group.GroupId == 0))
                        {
                            if (ImGui.MenuItem("Delete Group"))
                            {

                                route.NodeGroups.RemoveAt(i);
                                if (selectedGroupIndex == i)
                                    selectedGroupIndex = 0;
                            }
                        }

                        if (i > 0 && ImGui.MenuItem("Move Up"))
                        {
                            (route.NodeGroups[i], route.NodeGroups[i - 1]) = (route.NodeGroups[i - 1], route.NodeGroups[i]);
                        }

                        if (i < route.NodeGroups.Count - 1 && ImGui.MenuItem("Move Down"))
                        {
                            (route.NodeGroups[i], route.NodeGroups[i + 1]) = (route.NodeGroups[i + 1], route.NodeGroups[i]);
                        }

                        ImGui.EndPopup();
                    }
                }
            }

            ImGui.SameLine(0, 5);

            using (var nodeGroups = ImRaii.Child("Route_NodeGroups", new Vector2(0, 200), true))
            {
                if (selectedGroupIndex > route.NodeGroups.Count)
                    selectedGroupIndex = 0;

                ImGui.Text("Drag nodes between groups below:");

                // Calculate the maximum height needed across all groups
                float maxGroupHeight = 0;
                float itemHeight = ImGui.GetTextLineHeightWithSpacing();
                float dropZoneHeight = 30;
                float windowPadding = ImGui.GetStyle().WindowPadding.Y * 2;

                foreach (var group in route.NodeGroups)
                {
                    float nodeContentHeight = 4 * itemHeight;
                    float totalHeight = nodeContentHeight + dropZoneHeight + windowPadding;
                    maxGroupHeight = Math.Max(maxGroupHeight, totalHeight);
                }

                // Draw all groups with their nodes for drag-drop
                if (ImGui.BeginTable("NodeGroupsTable", route.NodeGroups.Count, ImGuiTableFlags.Resizable | ImGuiTableFlags.BordersInnerV))
                {
                    // Setup columns for each group
                    for (int g = 0; g < route.NodeGroups.Count; g++)
                    {
                        ImGui.TableSetupColumn($"Group {route.NodeGroups[g].GroupId}", ImGuiTableColumnFlags.WidthStretch);
                    }
                    ImGui.TableHeadersRow();

                    ImGui.TableNextRow();

                    // Draw each group's nodes
                    for (int groupIdx = 0; groupIdx < route.NodeGroups.Count; groupIdx++)
                    {
                        ImGui.TableSetColumnIndex(groupIdx);
                        var group = route.NodeGroups[groupIdx];

                        // Use the max height so all columns are aligned
                        using (var groupChild = ImRaii.Child($"GroupNodes{groupIdx}", new Vector2(-1, maxGroupHeight), true))
                        {
                            if (!groupChild.Success)
                            {

                            }

                            // Draw nodes in this group
                            for (int nodeIdx = 0; nodeIdx < group.Nodes.Count; nodeIdx++)
                            {
                                var node = group.Nodes[nodeIdx];

                                ImGui.PushID($"node_{groupIdx}_{nodeIdx}");

                                bool isSelected = selectedGroupIndex == groupIdx && selectedNodeIndex == nodeIdx;

                                if (ImGui.Selectable($"Node {node.NodeId} ({node.Locations.Count} locs)", isSelected, ImGuiSelectableFlags.AllowDoubleClick))
                                {
                                    selectedGroupIndex = groupIdx;
                                    selectedNodeIndex = nodeIdx;
                                    selectedLocationIndex = -1;
                                }

                                // Drag source
                                if (ImGui.BeginDragDropSource())
                                {
                                    _draggedNodeGroupIndex = groupIdx;
                                    _draggedNodeIndex = nodeIdx;
                                    _isDragging = true;

                                    // Set payload to carry indices
                                    unsafe
                                    {
                                        int* data = stackalloc int[2];
                                        data[0] = groupIdx;
                                        data[1] = nodeIdx;
                                        byte* dataPtr = (byte*)data;
                                        ReadOnlySpan<byte> spanData = new ReadOnlySpan<byte>(dataPtr, sizeof(int) * 2);
                                        ImGui.SetDragDropPayload("NODE_MOVE", spanData);
                                    }

                                    ImGui.Text($"Moving Node {node.NodeId}");
                                    ImGui.EndDragDropSource();
                                }

                                // Drop target ON THE NODE ITSELF
                                if (ImGui.BeginDragDropTarget())
                                {
                                    unsafe
                                    {
                                        var payload = ImGui.AcceptDragDropPayload("NODE_MOVE");
                                        if (!payload.IsNull)
                                        {
                                            int* data = (int*)payload.Data;
                                            int sourceGroupIdx = data[0];
                                            int sourceNodeIdx = data[1];

                                            if (sourceGroupIdx >= 0 && sourceGroupIdx < route.NodeGroups.Count &&
                                                sourceNodeIdx >= 0 && sourceNodeIdx < route.NodeGroups[sourceGroupIdx].Nodes.Count)
                                            {
                                                // Don't drop on yourself
                                                if (sourceGroupIdx != groupIdx || sourceNodeIdx != nodeIdx)
                                                {
                                                    var nodeToMove = route.NodeGroups[sourceGroupIdx].Nodes[sourceNodeIdx];

                                                    // Remove from source
                                                    route.NodeGroups[sourceGroupIdx].Nodes.RemoveAt(sourceNodeIdx);

                                                    // Add to target group
                                                    route.NodeGroups[groupIdx].Nodes.Add(nodeToMove);

                                                    // Update selection
                                                    selectedGroupIndex = groupIdx;
                                                    selectedNodeIndex = route.NodeGroups[groupIdx].Nodes.Count - 1;

                                                    _isDragging = false;
                                                }
                                            }
                                        }
                                    }
                                    ImGui.EndDragDropTarget();
                                }

                                // Context menu for delete
                                if (ImGui.BeginPopupContextItem($"nodecontext_{groupIdx}_{nodeIdx}"))
                                {
                                    if (ImGui.MenuItem("Delete Node"))
                                    {
                                        group.Nodes.RemoveAt(nodeIdx);
                                        if (selectedNodeIndex == nodeIdx && selectedGroupIndex == groupIdx)
                                            selectedNodeIndex = -1;
                                    }
                                    ImGui.EndPopup();
                                }

                                ImGui.PopID();
                            }

                            // Add empty space at the bottom that acts as a drop target
                            var availHeight = ImGui.GetContentRegionAvail().Y;
                            if (availHeight > 0)
                            {
                                ImGui.InvisibleButton($"##dropzone_{groupIdx}", new Vector2(-1, availHeight));

                                // Drop target for the empty space
                                if (ImGui.BeginDragDropTarget())
                                {
                                    unsafe
                                    {
                                        var payload = ImGui.AcceptDragDropPayload("NODE_MOVE");
                                        if (!payload.IsNull)
                                        {
                                            int* data = (int*)payload.Data;
                                            int sourceGroupIdx = data[0];
                                            int sourceNodeIdx = data[1];

                                            if (sourceGroupIdx >= 0 && sourceGroupIdx < route.NodeGroups.Count &&
                                                sourceNodeIdx >= 0 && sourceNodeIdx < route.NodeGroups[sourceGroupIdx].Nodes.Count)
                                            {
                                                var nodeToMove = route.NodeGroups[sourceGroupIdx].Nodes[sourceNodeIdx];

                                                // Remove from source
                                                route.NodeGroups[sourceGroupIdx].Nodes.RemoveAt(sourceNodeIdx);

                                                // Add to target
                                                route.NodeGroups[groupIdx].Nodes.Add(nodeToMove);

                                                // Update selection
                                                selectedGroupIndex = groupIdx;
                                                selectedNodeIndex = route.NodeGroups[groupIdx].Nodes.Count - 1;

                                                _isDragging = false;
                                            }
                                        }
                                    }
                                    ImGui.EndDragDropTarget();
                                }
                            }
                        }
                    }

                    ImGui.EndTable();
                }
            }

            using (var nodeList = ImRaii.Child("Route_NodeList", new(200, 0), true))
            {
                if (!nodeList.Success)
                {
                    // Don't return - let drawing code execute
                }
                else
                {
                    var selectedGroup = route.NodeGroups[selectedGroupIndex];

                    if (selectedNodeIndex < 0 || selectedNodeIndex >= selectedGroup.Nodes.Count)
                    {
                        ImGui.TextWrapped("We seem to have selected an invalid node, please select a proper one");
                        if (selectedGroup.Nodes.Count > 0)
                            selectedNodeIndex = 0;
                        // Don't return - let drawing code execute
                    }
                    else
                    {
                        var selectedNode = selectedGroup.Nodes[selectedNodeIndex];
                        ImGui.Text("Select Node");
                        if (ImGui.Button("Add Nearby Nodes"))
                        {
                            //TODO: Need to add logic for adding all nearby nodes with the ID here
                        }

                        for (int i = 0; i < selectedNode.Locations.Count; i++)
                        {
                            var loc = selectedNode.Locations[i];
                            bool isSelected = selectedLocationIndex == i;

                            ImGui.PushID($"loc_{i}");

                            if (ImGui.Selectable($"Loc {i + 1}: [{loc.Position.X:F1}, {loc.Position.Z:F1}]", isSelected))
                            {
                                selectedLocationIndex = i;
                            }

                            // Drag source for reordering
                            if (ImGui.BeginDragDropSource())
                            {
                                _draggedLocationIndex = i;

                                unsafe
                                {
                                    int dragIndex = i;
                                    byte* dataPtr = (byte*)&dragIndex;
                                    ReadOnlySpan<byte> data = new ReadOnlySpan<byte>(dataPtr, sizeof(int));
                                    ImGui.SetDragDropPayload("LOCATION_REORDER", data);
                                }

                                ImGui.Text($"Moving Location {i + 1}");
                                ImGui.EndDragDropSource();
                            }

                            // Drop target for reordering
                            if (ImGui.BeginDragDropTarget())
                            {
                                unsafe
                                {
                                    var payload = ImGui.AcceptDragDropPayload("LOCATION_REORDER");
                                    if (!payload.IsNull)
                                    {
                                        int sourceIdx = *(int*)payload.Data;

                                        if (sourceIdx != i && sourceIdx >= 0 && sourceIdx < selectedNode.Locations.Count)
                                        {
                                            // Reorder: move source to target position
                                            var locationToMove = selectedNode.Locations[sourceIdx];
                                            selectedNode.Locations.RemoveAt(sourceIdx);

                                            // Adjust insert index if needed
                                            int insertIdx = i;
                                            if (sourceIdx < i)
                                                insertIdx--;

                                            selectedNode.Locations.Insert(insertIdx, locationToMove);
                                            selectedLocationIndex = insertIdx;
                                        }
                                    }
                                }
                                ImGui.EndDragDropTarget();
                            }

                            if (ImGui.BeginPopupContextItem($"loccontext_{i}"))
                            {
                                if (ImGui.MenuItem("Delete Location"))
                                {
                                    selectedNode.Locations.RemoveAt(i);
                                    if (selectedLocationIndex == i)
                                        selectedLocationIndex = -1;
                                }

                                if (ImGui.MenuItem("Duplicate"))
                                {
                                    var newLoc = new NodeLocation
                                    {
                                        Position = new Position { X = loc.Position.X, Y = loc.Position.Y, Z = loc.Position.Z },
                                        FlightAngle_Min = loc.FlightAngle_Min,
                                        FlightAngle_Max = loc.FlightAngle_Max,
                                        FlightDistance_Min = loc.FlightDistance_Min,
                                        FlightDistance_Max = loc.FlightDistance_Max,
                                        AllowFlying = loc.AllowFlying
                                    };
                                    selectedNode.Locations.Insert(i + 1, newLoc);
                                }

                                ImGui.EndPopup();
                            }

                            ImGui.PopID();
                        }
                    }
                }
            }

            ImGui.SameLine(0, 5);
            bool isHoveringMinAngle = false;
            bool isHoveringMaxAngle = false;
            Vector3? hoveredDotLoc = null;

            using (var nodeDetails = ImRaii.Child("Route_SpecificNode", new(0, 0), true))
            {
                if (!nodeDetails.Success)
                {
                    // Don't return - let drawing code execute
                }
                else if (selectedGroupIndex >= 0 && selectedGroupIndex < route.NodeGroups.Count && selectedNodeIndex >= 0 && selectedNodeIndex < route.NodeGroups[selectedGroupIndex].Nodes.Count)
                {
                    var selectedGroup = route.NodeGroups[selectedGroupIndex];
                    var selectedNode = selectedGroup.Nodes[selectedNodeIndex];

                    if (ImGui.BeginTabBar("Detailed Info"))
                    {
                        if (ImGui.BeginTabItem("Node Details"))
                        {
                            if (selectedLocationIndex < 0 || selectedLocationIndex >= selectedNode.Locations.Count)
                            {
                                ImGui.Text($"Select a location to edit | current index: {selectedLocationIndex} | Current count: {selectedNode.Locations.Count}");
                                if (selectedNode.Locations.Count > 0)
                                {
                                    selectedLocationIndex = 0;
                                }
                            }
                            else
                            {

                                var locationInfo = selectedNode.Locations[selectedLocationIndex];
                                bool allowFlying = locationInfo.AllowFlying;
                                if (ImGui.Checkbox("Allow Flying##editflying", ref allowFlying))
                                    locationInfo.AllowFlying = allowFlying;

                                ImGui.Dummy(new Vector2(0, 5));

                                ImGui.Text($"Node Location: {locationInfo.Position.X:N2}, {locationInfo.Position.Y:N2}, {locationInfo.Position.Z:N2}");
                                ImGui.Separator();
                                ImGui.Text("Flight Ring Info");
                                var minAngle = locationInfo.FlightAngle_Min;
                                var maxAngle = locationInfo.FlightAngle_Max;

                                float CalculateArcSpan(float min, float max)
                                {
                                    if (min <= max)
                                        return max - min;
                                    else
                                        return (360f - min) + max;
                                }

                                ImGui.SetNextItemWidth(150);
                                if (ImGui.DragFloat("Min Angle##editminangle", ref minAngle, 1f, 0f, 360f))
                                {
                                    // Normalize to 0-360 range
                                    minAngle = minAngle % 360f;
                                    if (minAngle < 0f) minAngle += 360f;

                                    locationInfo.FlightAngle_Min = minAngle;

                                    // Optional: Show warning if range is suspicious
                                    float span = CalculateArcSpan(minAngle, maxAngle);
                                    if (span > 359f)
                                    {
                                        ImGui.SameLine();
                                        ImGui.TextColored(new Vector4(1f, 0.5f, 0f, 1f), "⚠ Full circle");
                                    }
                                }
                                isHoveringMinAngle = ImGui.IsItemHovered() || ImGui.IsItemActive();

                                ImGui.SetNextItemWidth(150);
                                if (ImGui.DragFloat("Max Angle##editmaxangle", ref maxAngle, 1f, 0f, 360f))
                                {
                                    // Normalize to 0-360 range
                                    maxAngle = maxAngle % 360f;
                                    if (maxAngle < 0f) maxAngle += 360f;

                                    locationInfo.FlightAngle_Max = maxAngle;

                                    // Optional: Show warning if range is suspicious
                                    float span = CalculateArcSpan(minAngle, maxAngle);
                                    if (span > 359f)
                                    {
                                        ImGui.SameLine();
                                        ImGui.TextColored(new Vector4(1f, 0.5f, 0f, 1f), "⚠ Full circle");
                                    }
                                }
                                isHoveringMaxAngle = ImGui.IsItemHovered() || ImGui.IsItemActive();

                                var fanHeight = locationInfo.FlightFan_Height;
                                ImGui.SetNextItemWidth(150);
                                if (ImGui.DragFloat("Fan Height Set", ref fanHeight, 0.1f))
                                {
                                    locationInfo.FlightFan_Height = fanHeight;
                                }

                                ImGui.Dummy(new(0, 5));
                                float minDistance = locationInfo.FlightDistance_Min;
                                float maxDistance = locationInfo.FlightDistance_Max;

                                ImGui.SetNextItemWidth(150);
                                if (ImGui.DragFloatRange2("Distance", ref minDistance, ref maxDistance, 0.1f, 1f, 8f))
                                {
                                    locationInfo.FlightDistance_Min = minDistance;
                                    locationInfo.FlightDistance_Max = maxDistance;
                                }
                                ImGui.SameLine();
                                if (ImGui.Button("Default dist##landFanEdit"))
                                {
                                    locationInfo.FlightDistance_Min = 3;
                                    locationInfo.FlightDistance_Max = 5;
                                }

                                ImGui.Separator();
                                bool useLandingSpots = locationInfo.UseSpecificWalkingSpots;
                                if (ImGui.Checkbox("Use Specific Landing Spots", ref useLandingSpots))
                                {
                                    locationInfo.UseSpecificWalkingSpots = useLandingSpots;
                                }

                                ImGui.Dummy(new(0, 5));

                                if (useLandingSpots)
                                {
                                    if (ImGui.Button("Add position as walk to"))
                                    {
                                        locationInfo.WalkablePositions.Add(new Position()
                                        {
                                            X = Player.Position.X,
                                            Y = Player.Position.Y,
                                            Z = Player.Position.Z
                                        });
                                    }
                                    ImGui.SetNextItemWidth(200);
                                    if (ImGui.CollapsingHeader("Move to points"))
                                    {
                                        if (locationInfo.WalkablePositions.Count > 0)
                                        {
                                            if (ImGui.BeginTable("Landable Locations Table", 5, ImGuiTableFlags.Borders | ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.RowBg))
                                            {
                                                ImGui.TableSetupColumn("X");
                                                ImGui.TableSetupColumn("Y");
                                                ImGui.TableSetupColumn("Z");
                                                ImGui.TableSetupColumn("Move To");
                                                ImGui.TableSetupColumn("Remove");

                                                ImGui.TableHeadersRow();

                                                for (int i = 0; i < locationInfo.WalkablePositions.Count; i++)
                                                {
                                                    var position = locationInfo.WalkablePositions[i];
                                                    ImGui.PushID($"{position}{i}");

                                                    ImGui.TableNextRow();
                                                    ImGui.TableSetColumnIndex(0);
                                                    ImGui.Text($"{position.X:N2}");

                                                    ImGui.TableNextColumn();
                                                    ImGui.Text($"{position.Y:N2}");

                                                    ImGui.TableNextColumn();
                                                    ImGui.Text($"{position.Z:N2}");

                                                    ImGui.TableNextColumn();
                                                    if (ImGui.Button("Move to"))
                                                    {
                                                        P.navmesh.PathfindAndMoveTo(position.ToVector3(), false);
                                                    }
                                                    if (ImGui.IsItemHovered())
                                                    {
                                                        hoveredDotLoc = position.ToVector3();
                                                    }

                                                    ImGui.TableNextColumn();
                                                    if (ImGui.Button("Remove"))
                                                    {
                                                        locationInfo.WalkablePositions.Remove(position);
                                                        break;
                                                    }
                                                    if (ImGui.IsItemHovered())
                                                    {
                                                        hoveredDotLoc = position.ToVector3();
                                                    }

                                                    ImGui.PopID();
                                                }

                                                ImGui.EndTable();

                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    var gatheringAngle_Min = locationInfo.GatherAngle_Min;
                                    var gatheringAngle_Max = locationInfo.GatherAngle_Max;
                                    var gatheringDist_Min = locationInfo.GatherDist_Min;
                                    var gatheringDist_Max = locationInfo.GatherDist_Max;
                                    var gatherFan_height = locationInfo.GatherFan_Height;

                                    ImGui.SetNextItemWidth(150);
                                    if (ImGui.DragFloat("Min Angle##editgatheringAngle_Min", ref gatheringAngle_Min, 1f, 0f, 360f))
                                    {
                                        // Normalize to 0-360 range
                                        gatheringAngle_Min = gatheringAngle_Min % 360f;
                                        if (gatheringAngle_Min < 0f) gatheringAngle_Min += 360f;

                                        locationInfo.GatherAngle_Min = gatheringAngle_Min;

                                        // Optional: Show warning if range is suspicious
                                        float span = CalculateArcSpan(gatheringAngle_Min, gatheringAngle_Max);
                                        if (span > 359f)
                                        {
                                            ImGui.SameLine();
                                            ImGui.TextColored(new Vector4(1f, 0.5f, 0f, 1f), "⚠ Full circle");
                                        }
                                    }

                                    ImGui.SetNextItemWidth(150);
                                    if (ImGui.DragFloat("Max Angle##editgatheringAngle_Max", ref gatheringAngle_Max, 1f, 0f, 360f))
                                    {
                                        // Normalize to 0-360 range
                                        gatheringAngle_Max = gatheringAngle_Max % 360f;
                                        if (gatheringAngle_Max < 0f) gatheringAngle_Max += 360f;

                                        locationInfo.GatherAngle_Max = gatheringAngle_Max;

                                        // Optional: Show warning if range is suspicious
                                        float span = CalculateArcSpan(gatheringAngle_Min, gatheringAngle_Max);
                                        if (span > 359f)
                                        {
                                            ImGui.SameLine();
                                            ImGui.TextColored(new Vector4(1f, 0.5f, 0f, 1f), "⚠ Full circle");
                                        }
                                    }

                                    ImGui.SetNextItemWidth(150);
                                    if (ImGui.DragFloat("Fan Height", ref gatherFan_height, 0.1f))
                                    {
                                        locationInfo.GatherFan_Height = gatherFan_height;
                                    }
                                    ImGui.SameLine();
                                    if (ImGui.Button("Match to land fan height"))
                                    {
                                        locationInfo.GatherFan_Height = locationInfo.FlightFan_Height;
                                    }

                                    ImGui.SetNextItemWidth(150);
                                    if (ImGui.DragFloatRange2("Gather Distance", ref gatheringDist_Min, ref gatheringDist_Max, 0.1f, 1, 3))
                                    {
                                        locationInfo.GatherDist_Min = gatheringDist_Min;
                                        locationInfo.GatherDist_Max = gatheringDist_Max;
                                    }
                                    ImGui.SameLine();
                                    if (ImGui.Button("Default dist##gatherEditVersion"))
                                    {
                                        locationInfo.GatherDist_Min = 1;
                                        locationInfo.GatherDist_Max = 2.4f;
                                    }
                                    ImGui.SameLine();
                                    if (ImGui.Button("Match to land fan"))
                                    {
                                        locationInfo.GatherAngle_Min = locationInfo.FlightAngle_Min;
                                        locationInfo.GatherAngle_Max = locationInfo.FlightAngle_Max;
                                        locationInfo.GatherDist_Min = locationInfo.FlightDistance_Min;
                                        locationInfo.GatherDist_Max = locationInfo.FlightDistance_Max;
                                        locationInfo.GatherFan_Height = locationInfo.FlightFan_Height;
                                    }

                                }

                                ImGui.Separator();

                                if (ImGui.Button("Fly to destination"))
                                {
                                    var destination = NodeLocationExtensions.GetRandomFlightPosition(locationInfo, Player.Position);
                                    var closestPoint = Vector3.Zero;
                                    if (locationInfo.UseSpecificWalkingSpots)
                                    {
                                        var walkPointSpecific = locationInfo.WalkablePositions.OrderBy(x => Vector3.Distance(destination, x.ToVector3())).FirstOrDefault();
                                        closestPoint = walkPointSpecific.ToVector3();
                                    }
                                    else
                                    {
                                        closestPoint = NodeLocationExtensions.GetRandomGatherPosition(locationInfo, Player.Position);
                                    }
                                    P.taskManager.Enqueue(() => Task_NavmeshMove.Task_FlyTo(destination, distance: 1, stayMounted: false), TaskConfig);
                                    P.taskManager.Enqueue(() => Task_NavmeshMove.Task_GroundTo(closestPoint, true, 1, true), TaskConfig);
                                }
                                ImGui.SameLine();
                                if (ImGui.Button("Land?"))
                                {
                                    LandFunction();
                                }
                            }

                            ImGui.EndTabItem();
                        }

                        if (ImGui.BeginTabItem("Picto Settings"))
                        {
                            bool useVfx = C.UseVfx;
                            if (ImGui.Checkbox("Use FFXIV Vfx for circle", ref useVfx))
                            {
                                C.UseVfx = useVfx;
                                C.Save();
                            }

                            var selectedColor = C.Picto_SelectedColor;
                            if (ImGui.ColorEdit4("Selected Color", ref selectedColor))
                            {
                                C.Picto_SelectedColor = selectedColor;
                                C.Save();
                            }

                            var color1 = C.Picto_GroupColor1;
                            if (ImGui.ColorEdit4("Group Color 1", ref color1))
                            {
                                C.Picto_GroupColor1 = color1;
                                C.Save();
                            }

                            var color2 = C.Picto_GroupColor2;
                            if (ImGui.ColorEdit4("Group Color 2", ref color2))
                            {
                                C.Picto_GroupColor2 = color2;
                                C.Save();
                            }

                            var color3 = C.Picto_GroupColor3;
                            if (ImGui.ColorEdit4("Group Color 3", ref color3))
                            {
                                C.Picto_GroupColor3 = color3;
                                C.Save();
                            }

                            var color4 = C.Picto_GroupColor4;
                            if (ImGui.ColorEdit4("Group Color 4", ref color4))
                            {
                                C.Picto_GroupColor4 = color4;
                                C.Save();
                            }

                            var color5 = C.Picto_GroupColor5;
                            if (ImGui.ColorEdit4("Group Color 5", ref color5))
                            {
                                C.Picto_GroupColor5 = color5;
                                C.Save();
                            }

                            var color6 = C.Picto_GroupColor6;
                            if (ImGui.ColorEdit4("Group Color 6", ref color6))
                            {
                                C.Picto_GroupColor6 = color6;
                                C.Save();
                            }

                            var color7 = C.Picto_GroupColor7;
                            if (ImGui.ColorEdit4("Group Color 7", ref color7))
                            {
                                C.Picto_GroupColor7 = color7;
                                C.Save();
                            }

                            var color8 = C.Picto_GroupColor8;
                            if (ImGui.ColorEdit4("Group Color 8", ref color8))
                            {
                                C.Picto_GroupColor8 = color8;
                                C.Save();
                            }

                            var radiusColor = C.Picto_RadiusColor;
                            if (ImGui.ColorEdit4("Radius Color", ref radiusColor))
                            {
                                C.Picto_RadiusColor = radiusColor;
                                C.Save();
                            }

                            var textColor = C.Picto_TextColor;
                            if (ImGui.ColorEdit4("Text Color", ref textColor))
                            {
                                C.Picto_TextColor = textColor;
                                C.Save();
                            }
                            var scale = C.Picto_TextScale;
                            if (ImGui.DragFloat("Text Scale", ref scale, 0.1f, 1, 3))
                            {
                                C.Picto_TextScale = scale;
                            }


                            ImGui.EndTabItem();
                        }

                        ImGui.EndTabBar();
                    }
                }
            }

            using (var pictoDraw = PictoService.Draw())
            {
                if (pictoDraw == null)
                    return;

                // Define color palette: 4 groups with 2 colors each (primary and secondary)
                // Format: ABGR (Alpha, Blue, Green, Red)
                var groupColors = new uint[,]
                {
                    { ToUintABGR(C.Picto_GroupColor1), ToUintABGR(C.Picto_GroupColor2) },
                    { ToUintABGR(C.Picto_GroupColor3), ToUintABGR(C.Picto_GroupColor4) },
                    { ToUintABGR(C.Picto_GroupColor5), ToUintABGR(C.Picto_GroupColor6) },
                    { ToUintABGR(C.Picto_GroupColor7), ToUintABGR(C.Picto_GroupColor8) }
                };

                uint selectedColor = ToUintABGR(C.Picto_SelectedColor);

                // Get the currently selected location for comparison (if valid)
                NodeLocation selectedLocation = null;
                if (selectedGroupIndex >= 0 && selectedGroupIndex < route.NodeGroups.Count &&
                    selectedNodeIndex >= 0 && selectedNodeIndex < route.NodeGroups[selectedGroupIndex].Nodes.Count &&
                    selectedLocationIndex >= 0 && selectedLocationIndex < route.NodeGroups[selectedGroupIndex].Nodes[selectedNodeIndex].Locations.Count)
                {
                    selectedLocation = route.NodeGroups[selectedGroupIndex].Nodes[selectedNodeIndex].Locations[selectedLocationIndex];
                }

                // Draw all node groups
                for (int groupIdx = 0; groupIdx < route.NodeGroups.Count; groupIdx++)
                {
                    var nodeGroup = route.NodeGroups[groupIdx];
                    int colorGroupIdx = groupIdx % 4; // Cycle through 4 color groups

                    for (int nodeIdx = 0; nodeIdx < nodeGroup.Nodes.Count; nodeIdx++)
                    {
                        var node = nodeGroup.Nodes[nodeIdx];
                        // Alternate between primary (0) and secondary (1) color within the group
                        int colorVariant = nodeIdx % 2;
                        uint nodeColor = groupColors[colorGroupIdx, colorVariant];

                        foreach (var nodeLoc in node.Locations)
                        {
                            // Check if this is the selected location
                            bool isSelected = selectedLocation != null &&
                                            selectedLocation.Position.X == nodeLoc.Position.X &&
                                            selectedLocation.Position.Y == nodeLoc.Position.Y &&
                                            selectedLocation.Position.Z == nodeLoc.Position.Z;

                            uint drawColor = isSelected ? selectedColor : nodeColor;
                            if (C.UseVfx)
                            {
                                PictoService.VfxRenderer.AddCircle($"{nodeLoc.Position.X} {nodeLoc.Position.Z}", nodeLoc.Position.ToVector3(), 3, Utils.FromUintABGR(drawColor));
                            }
                            else
                            {
                                Vector3 lowerPos = new Vector3(nodeLoc.Position.X, nodeLoc.Position.Y - 1, nodeLoc.Position.Z);
                                pictoDraw.AddCircleFilled(lowerPos, 3, drawColor);
                            }
                            
                            // nodeText above the node 
                            var textLoc = new Vector3(nodeLoc.Position.X, nodeLoc.Position.Y + 2, nodeLoc.Position.Z);
                            if (Player.DistanceTo(textLoc) < 100)
                            {
                                pictoDraw.AddText(textLoc, ToUintABGR(C.Picto_TextColor), $"{node.NodeId} | {nodeLoc.Position.ToVector3():N2} | Distance: {Player.DistanceTo(textLoc):N2}", C.Picto_TextScale);
                            }
                            else
                            {
                                pictoDraw.AddText(textLoc, ToUintABGR(C.Picto_TextColor), $"{node.NodeId} | {nodeLoc.Position.ToVector3():N2}", C.Picto_TextScale);
                            }

                            Vector3 fanLoc = new Vector3(nodeLoc.Position.X, nodeLoc.Position.Y + nodeLoc.FlightFan_Height, nodeLoc.Position.Z);
                            var fanColor = isSelected ? selectedColor : ToUintABGR(C.Picto_RadiusColor);
                            var editColor = ToUintABGR(C.Picto_GroupColor1);

                            if (isSelected)
                            {
                                if (isHoveringMinAngle)
                                {
                                    // Draw a thin fan slice at the min angle edge
                                    pictoDraw.AddFanFilled(
                                        fanLoc,
                                        nodeLoc.FlightDistance_Min,
                                        nodeLoc.FlightDistance_Max,
                                        DegreesToRadians(nodeLoc.FlightAngle_Min - 2), // 2 degree width on each side
                                        DegreesToRadians(nodeLoc.FlightAngle_Min + 2),
                                        editColor
                                    );
                                }

                                if (isHoveringMaxAngle)
                                {
                                    // Draw a thin fan slice at the max angle edge
                                    pictoDraw.AddFanFilled(
                                        fanLoc,
                                        nodeLoc.FlightDistance_Min,
                                        nodeLoc.FlightDistance_Max,
                                        DegreesToRadians(nodeLoc.FlightAngle_Max - 2), // 2 degree width on each side
                                        DegreesToRadians(nodeLoc.FlightAngle_Max + 2),
                                        editColor
                                    );
                                }
                            }

                            pictoDraw.AddFanFilled(
                                fanLoc,
                                nodeLoc.FlightDistance_Min,
                                nodeLoc.FlightDistance_Max,
                                DegreesToRadians(nodeLoc.FlightAngle_Min),
                                DegreesToRadians(nodeLoc.FlightAngle_Max),
                                fanColor
                            );

                            if (nodeLoc.UseSpecificWalkingSpots)
                            {
                                foreach (var walkPosition in nodeLoc.WalkablePositions)
                                {
                                    var normalColor = ToUintABGR(C.Picto_DotColor);
                                    if (hoveredDotLoc != null && hoveredDotLoc == walkPosition.ToVector3())
                                    {
                                        normalColor = editColor;
                                    }
                                    pictoDraw.AddDot(walkPosition.ToVector3(), 5, normalColor);
                                }
                            }
                            else
                            {
                                Vector3 landFanLoc = new Vector3(nodeLoc.Position.X, nodeLoc.Position.Y + nodeLoc.GatherFan_Height, nodeLoc.Position.Z);
                                pictoDraw.AddFanFilled
                                (
                                    landFanLoc,
                                    nodeLoc.GatherDist_Min,
                                    nodeLoc.GatherDist_Max,
                                    DegreesToRadians(nodeLoc.GatherAngle_Min),
                                    DegreesToRadians(nodeLoc.GatherAngle_Max),
                                    fanColor
                                );
                            }
                        }
                    }
                }
            }
        }

        private void SaveRouteChanges(GatheringRoute route)
        {
            route.LastUpdated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // Save to disk using the route loader
            try
            {
                P.routeLoader.SaveRoute(route, route.PlaceName);
                Svc.Log.Info($"Successfully saved route {route.RouteId}");
            }
            catch (Exception ex)
            {
                Svc.Log.Error($"Failed to save route {route.RouteId}: {ex.Message}");
            }
        }

        private Vector3 GetRandomPointInFan(NodeLocation nodeLoc)
        {
            // Get random distance between min and max
            float distance = (float)(_random.NextDouble() * (nodeLoc.FlightDistance_Max - nodeLoc.FlightDistance_Min) + nodeLoc.FlightDistance_Min);

            // Get random angle between min and max (in degrees)
            float angleDegrees = (float)(_random.NextDouble() * (nodeLoc.FlightAngle_Max - nodeLoc.FlightAngle_Min) + nodeLoc.FlightAngle_Min);

            // Convert to radians
            float angleRadians = angleDegrees * MathF.PI / 180f;

            float offsetX = distance * -MathF.Sin(angleRadians);
            float offsetZ = distance * MathF.Cos(angleRadians);

            return new Vector3(
                nodeLoc.Position.X + offsetX,
                nodeLoc.Position.Y + nodeLoc.FlightFan_Height,
                nodeLoc.Position.Z + offsetZ
            );
        }

        private bool showOnlyRouteNodes = false;

        private void NodeViewer(GatheringRoute route)
        {
            ImGui.Checkbox("Show only route nodes", ref showOnlyRouteNodes);
            if (ImGui.BeginTable("Node Viewer", 5, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
            {
                ImGui.TableSetupColumn("NodeID");
                ImGui.TableSetupColumn("Position");
                ImGui.TableSetupColumn("Targetable?");
                ImGui.TableSetupColumn("V.3 Dist");
                ImGui.TableSetupColumn("V.2 Dist");

                ImGui.TableHeadersRow();
                foreach (var obj in Svc.Objects.Where(x => x.ObjectKind == ObjectKind.GatheringPoint)
                                               .OrderBy(x => Player.DistanceTo(x.Position)))
                {
                    if (showOnlyRouteNodes)
                    {
                        if (!route.NodeIds.Contains(obj.BaseId))
                            continue;
                    }

                    ImGui.TableNextRow();
                    ImGui.TableSetColumnIndex(0);
                    ImGui.Text($"{obj.BaseId}");

                    ImGui.TableNextColumn();
                    ImGui.Text($"X:{obj.Position.X:N2}, Y:{obj.Position.Y:N2}, Z:{obj.Position.Z:N2}");

                    ImGui.TableNextColumn();
                    ImGui.Text($"{obj.IsTargetable}");

                    ImGui.TableNextColumn();
                    ImGui.Text($"{Player.DistanceTo(obj.Position):N2}");

                    ImGui.TableNextColumn();
                    ImGui.Text($"{Player.DistanceTo(new Vector2(obj.Position.X, obj.Position.Z)):N2}");
                }


                ImGui.EndTable();
            }
        }

        private static unsafe void LandFunction()
        {
            ActionManager.Instance()->UseAction(ActionType.GeneralAction, 23);
        }
    }
}