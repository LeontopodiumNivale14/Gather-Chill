using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Interface.Utility.Raii;
using ECommons.GameHelpers;
using GatherChill.GatheringInfo;
using GatherChill.Scheduler.Handlers;
using GatherChill.Utilities;
using GatherChill.Utilities.GatheringHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GatherChill.Ui.RouteWindowTabs
{
    internal class Route_Editor
    {
        public static uint SelectedRoute = 0;

        private static uint selectedNodeId = 0;
        private static int selectedLocationIndex = 0;
        private static Vector3 selectedNodePos = Vector3.Zero;

        public static void Draw()
        {
            if (P.routeEditor.Routes.TryGetValue(SelectedRoute, out var routeInfo))
            {
                ImGui.Text($"Route: {SelectedRoute}");
                if (ImGui.Button("Save Route"))
                {
                    P.routeEditor.SaveRoute(routeInfo, C.SaveLocation);
                }

                if (ImGui.BeginTabBar("Route Editor: Route Selection"))
                {
                    if (ImGui.BeginTabItem("Details"))
                    {
                        RouteDetails(routeInfo);
                        ImGui.EndTabItem();
                    }

                    if (ImGui.BeginTabItem("Item Info"))
                    {
                        ItemDetails(routeInfo);
                        ImGui.EndTabItem();
                    }

                    if (ImGui.BeginTabItem("Node Details"))
                    {
                        NodeDetails(routeInfo);
                        ImGui.EndTabItem();
                    }

                    ImGui.EndTabBar();
                }
            }
            else
            {
                ImGui.Text("Tehe not loaded");
            }
        }

        private static void RouteDetails(GatheringRoute routeInfo)
        {
            if (ImGui.BeginTable($"Route Details: {routeInfo.RouteId}", 2, ImGuiTableFlags.RowBg | ImGuiTableFlags.Borders | ImGuiTableFlags.SizingFixedFit))
            {
                #region ID

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Text($"ID");

                ImGui.TableNextColumn();
                ImGui.Text($"{routeInfo.RouteId}");

                #endregion

                #region Territory

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Text($"Territory");

                ImGui.TableNextColumn();
                ImGui.Text($"[{routeInfo.TerritoryId}] {routeInfo.ZoneName}");

                #endregion

                #region Expansion

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Text("Expansion");

                ImGui.TableNextColumn();
                string expansion = routeInfo.ExpansionId switch
                {
                    0 => "ARR",
                    1 => "Heavensword",
                    2 => "Stormblood",
                    3 => "Shadowbringers",
                    4 => "Endwalker",
                    5 => "Dawntrail",
                    _ => "New/???"
                };
                ImGui.Text(expansion);

                #endregion

                #region Gathering Job

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Text($"Job");

                ImGui.TableNextColumn();
                string Job = routeInfo.GatheringJobId switch
                {
                    16 => "Miner",
                    17 => "Botanist",
                    18 => "Fisher",
                    _ => $"{routeInfo.GatheringJobId}"
                };
                ImGui.Text($"{Job}");

                #endregion

                #region Lv

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui_Util.Table_VertCenterText($"Lv.");

                ImGui.TableNextColumn();
                var level = routeInfo.LevelRequirement;
                if (ImGui.InputInt("##RequiredLv", ref level))
                    routeInfo.LevelRequirement = level;

                #endregion

                #region Node Count

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Text($"Node[s]");

                ImGui.TableNextColumn();
                ImGui.Text($"{routeInfo.NodeIds.Count()}");
                ImGui.SameLine();
                ImGui.TextDisabled("?");
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    foreach (var nodeId in routeInfo.NodeIds)
                    {
                        ImGui.Text($"{nodeId}");
                    }
                    ImGui.EndTooltip();
                }


                #endregion

                #region Settings

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Text($"Folklore Required");

                ImGui.TableNextColumn();
                bool folklore = routeInfo.RequiresFolklore;
                if (ImGui.Checkbox($"##FolkloreReq", ref folklore))
                    routeInfo.RequiresFolklore = folklore;

                if (folklore)
                {
                    string reqBook = routeInfo.FolkloreBook;
                    if (ImGui.InputText("Name", ref reqBook))
                        routeInfo.FolkloreBook = reqBook;
                }

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Text($"Timed Node?");

                ImGui.TableNextColumn();
                bool timedNode = routeInfo.TimedNode;
                if (ImGui.Checkbox("##timedNode", ref timedNode))
                    routeInfo.TimedNode = timedNode;

                #endregion

                #region Details

                string authors = routeInfo.Author;
                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui_Util.Table_VertCenterText("Author(s)");

                ImGui.TableNextColumn();
                ImGui.SetNextItemWidth(200);
                if (ImGui.InputText("##authors", ref authors))
                    routeInfo.Author = authors;

                #endregion

                ImGui.EndTable();
            }
        }

        private static void ItemDetails(GatheringRoute routeInfo)
        {

        }

        private static bool _isGeneratingFan = false;
        private static string _fanGenStatus = string.Empty;

        private static uint _draggedNodeId = 0;
        private static int _dragTargetGroupId = -1;

        private static bool AutoUpdateMissing = true;

        private static unsafe void NodeDetails(GatheringRoute routeInfo)
        {
            if (ImGui.BeginTable("Node Details", 2, ImGuiTableFlags.Borders | ImGuiTableFlags.SizingFixedFit))
            {
                ImGui.TableSetupColumn("Selectors");
                ImGui.TableSetupColumn("Details", ImGuiTableColumnFlags.WidthStretch);

                #region Node Group Selector

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                if (ImGui.BeginChild("Node Details", new Vector2(200, 250)))
                {
                    if (ImGui.Button("Add Missing Nodes"))
                    {
                        var gatheringNodes = Svc.Objects.Where(x => routeInfo.NodeIds.Contains(x.BaseId))
                                             .Where(x => x.ObjectKind == ObjectKind.GatheringPoint);

                        foreach (var node in gatheringNodes)
                            P.routeEditor.AddNodeLocationIfMissing(routeInfo, node.BaseId, node.Position);
                    }

                    ImGui.Checkbox("Auto Update Missing", ref AutoUpdateMissing);
                    if (AutoUpdateMissing)
                    {
                        var gatheringNodes = Svc.Objects.Where(x => routeInfo.NodeIds.Contains(x.BaseId))
                     .Where(x => x.ObjectKind == ObjectKind.GatheringPoint);

                        foreach (var node in gatheringNodes)
                            P.routeEditor.AddNodeLocationIfMissing(routeInfo, node.BaseId, node.Position);
                    }

                    if (ImGui.Button("+Group"))
                        routeInfo.GroupCount++;

                    ImGui.SameLine();

                    using (var disabled = ImRaii.Disabled(routeInfo.GroupCount <= 1))
                    {
                        if (ImGui.Button("-Group"))
                        {
                            foreach (var node in routeInfo.NodeInfo.Where(n => n.GroupId == routeInfo.GroupCount - 1))
                                node.GroupId = routeInfo.GroupCount - 2;
                            routeInfo.GroupCount--;
                        }
                    }

                    foreach (var nodeId in routeInfo.NodeIds)
                        ImGui.Text($"{nodeId}");
                }
                ImGui.EndChild();

                ImGui.TableNextColumn();
                if (ImGui.BeginTable("Node Info | Groups", 3, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingFixedFit))
                {
                    ImGui.TableSetupColumn("Node Id");
                    ImGui.TableSetupColumn("Count");
                    ImGui.TableSetupColumn("Group");
                    ImGui.TableHeadersRow();

                    // Validate selectedNodeId — fall back to first if missing
                    if (selectedNodeId == 0 || routeInfo.NodeInfo.All(x => x.NodeId != selectedNodeId))
                    {
                        var first = routeInfo.NodeInfo.FirstOrDefault();
                        selectedNodeId = first?.NodeId ?? 0;
                        selectedLocationIndex = 0;
                    }

                    // Group the nodes by GroupId first so we know the position within each group
                    var groupedNodes = Enumerable.Range(0, routeInfo.GroupCount).Select(gId => (gId, nodes: routeInfo.NodeInfo.Where(n => n.GroupId == gId).ToList()));

                    foreach (var (groupId, locationsList) in groupedNodes)
                    {
                        var colors = C.Picto_GroupColors.TryGetValue(groupId, out var c) ? c : C.Picto_GroupColors[0];

                        // Group header row as drop target
                        ImGui.TableNextRow();
                        ImGui.TableSetColumnIndex(0);
                        ImGui.TextDisabled($"── Group {groupId} ──");

                        if (ImGui.BeginDragDropTarget())
                        {
                            try
                            {
                                var payload = ImGui.AcceptDragDropPayload("GATHERING_NODE");
                                if (payload.DataSize > 0 && _draggedNodeId != 0)
                                {
                                    var node = routeInfo.NodeInfo.FirstOrDefault(n => n.NodeId == _draggedNodeId);
                                    if (node != null)
                                        node.GroupId = groupId;  // was group.Key
                                    _draggedNodeId = 0;
                                }
                            }
                            catch { }
                            ImGui.EndDragDropTarget();
                        }

                        // Node rows
                        for (int i = 0; i < locationsList.Count; i++)
                        {
                            var nodeGroup = locationsList[i];
                            var pictoColor = i == 0 ? colors.Primary : colors.Secondary;

                            foreach (var location in nodeGroup.Locations)
                            {
                                PictoManager.DrawVfxCircle($"{location.Position}", location.Position, pictoColor);
                                PictoManager.DrawGatheringFan(location, selectedNodePos);
                            }

                            ImGui.PushID($"{nodeGroup.NodeId}");

                            ImGui.TableNextRow();
                            ImGui.TableSetColumnIndex(0);
                            if (selectedNodeId == nodeGroup.NodeId)
                                ImGui.TableSetBgColor(ImGuiTableBgTarget.RowBg0, ImGui.GetColorU32(new Vector4(0.0f, 1.0f, 0.2f, 0.25f)));

                            // Button is both the select trigger and drag source
                            ImGui.Button($"{nodeGroup.NodeId}");

                            if (ImGui.IsItemClicked() && selectedNodeId != nodeGroup.NodeId)
                            {
                                selectedNodeId = nodeGroup.NodeId;
                                selectedLocationIndex = 0;
                            }

                            if (ImGui.BeginDragDropSource())
                            {
                                byte[] dummyData = new byte[1];
                                ImGui.SetDragDropPayload("GATHERING_NODE", dummyData, (ImGuiCond)0);
                                _draggedNodeId = nodeGroup.NodeId;
                                ImGui.Text($"Node {nodeGroup.NodeId} → Group ?");
                                ImGui.EndDragDropSource();
                            }

                            // After the node button, before TableNextColumn
                            ImGui.SameLine();
                            if (ImGui.ArrowButton("##up", ImGuiDir.Up))
                            {
                                var idx = routeInfo.NodeInfo.IndexOf(nodeGroup);
                                // Find the previous node in the same group
                                var prevIdx = idx - 1;
                                while (prevIdx >= 0 && routeInfo.NodeInfo[prevIdx].GroupId != nodeGroup.GroupId)
                                    prevIdx--;

                                if (prevIdx >= 0)
                                    (routeInfo.NodeInfo[prevIdx], routeInfo.NodeInfo[idx]) = (routeInfo.NodeInfo[idx], routeInfo.NodeInfo[prevIdx]);
                            }
                            ImGui.SameLine();
                            if (ImGui.ArrowButton("##down", ImGuiDir.Down))
                            {
                                var idx = routeInfo.NodeInfo.IndexOf(nodeGroup);
                                // Find the next node in the same group
                                var nextIdx = idx + 1;
                                while (nextIdx < routeInfo.NodeInfo.Count && routeInfo.NodeInfo[nextIdx].GroupId != nodeGroup.GroupId)
                                    nextIdx++;

                                if (nextIdx < routeInfo.NodeInfo.Count)
                                    (routeInfo.NodeInfo[nextIdx], routeInfo.NodeInfo[idx]) = (routeInfo.NodeInfo[idx], routeInfo.NodeInfo[nextIdx]);
                            }

                            ImGui.TableNextColumn();
                            ImGui_Util.Table_VertCenterText($"{nodeGroup.Locations.Count}");

                            ImGui.PopID();
                        }
                    }

                    ImGui.EndTable();
                }

                #endregion

                #region Location Editor

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);

                if (ImGui.Button("Stop Navmesh"))
                {
                    P.navmesh.Stop();
                }

                // Validate selectedNodeId — fall back to first if missing
                if (routeInfo.NodeInfo.Count == 0)
                {
                    ImGui.TextDisabled("No nodes added yet.");
                    ImGui.EndTable();
                    return;
                }

                if (selectedNodeId == 0 || routeInfo.NodeInfo.All(x => x.NodeId != selectedNodeId))
                {
                    selectedNodeId = routeInfo.NodeInfo[0].NodeId;
                    selectedLocationIndex = 0;
                }

                // Single lookup — no nested foreach
                var selectedNode = routeInfo.NodeInfo.FirstOrDefault(x => x.NodeId == selectedNodeId);
                if (selectedNode != null)
                {


                    // Clamp index in case locations changed
                    if (selectedLocationIndex >= selectedNode.Locations.Count)
                        selectedLocationIndex = Math.Max(0, selectedNode.Locations.Count - 1);

                    ImGui.Text($"Node: {selectedNodeId}");

                    for (int i = 0; i < selectedNode.Locations.Count; i++)
                    {
                        var loc = selectedNode.Locations[i];
                        bool isSelected = selectedLocationIndex == i;

                        if (ImGui.Selectable($"{loc.Position.X:N2} {loc.Position.Y:N2} {loc.Position.Z:N2}##loc{i}", isSelected))
                        {
                            selectedLocationIndex = i;
                        }
                    }
                }

                #endregion

                #region Node Editor Itself

                ImGui.TableNextColumn();
                var editorNode = selectedNode.Locations[selectedLocationIndex];
                if (editorNode != null)
                {
                    selectedNodePos = editorNode.Position;

                    ImGui.Text($"Node Position: {editorNode.Position.X:N2}, {editorNode.Position.Y:N2}, {editorNode.Position.Z:N2}");
                    bool fly = editorNode.AllowFlying;
                    if (ImGui.Checkbox("Flying Allowed", ref fly))
                    {
                        editorNode.AllowFlying = fly;
                    }

                    #region Gathering Fan Info

                    ImGui.Separator();
                    ImGui.Text($"Gathering Location");
                    var gatherInfo = editorNode.Gathering_FanInfo;

                    var gather_FanStart = gatherInfo.Fan_StartAngle;
                    var gather_FanEnd = gatherInfo.Fan_EndAngle;
                    var gather_FanMin = gatherInfo.Fan_DistanceMin;
                    var gather_FanMax = gatherInfo.Fan_DistanceMax;
                    var gather_Height = gatherInfo.Fan_Height;

                    ImGui.PushID("Fan: Gathering");

                    ImGui.SetNextItemWidth(100);
                    if (ImGui.DragFloat("Start", ref gather_FanStart, 1, 0, 360))
                    {
                        gatherInfo.Fan_StartAngle = gather_FanStart;
                    }

                    ImGui.SameLine();
                    ImGui.SetNextItemWidth(100);
                    if (ImGui.DragFloat("End", ref gather_FanEnd, 1, 0, 360))
                    {
                        gatherInfo.Fan_EndAngle = gather_FanEnd;
                    }

                    ImGui.SetNextItemWidth(100);
                    if (ImGui.DragFloat("Min Distance", ref gather_FanMin, 0.2f, 0, 4))
                    {
                        gatherInfo.Fan_DistanceMin = gather_FanMin;
                    }

                    ImGui.SameLine();
                    ImGui.SetNextItemWidth(100);
                    if (ImGui.DragFloat("Max Distance", ref gather_FanMax, 0.2f, 0, 4))
                    {
                        gatherInfo.Fan_DistanceMax = gather_FanMax;
                    }

                    ImGui.SetNextItemWidth(100);
                    if (ImGui.DragFloat("Height", ref gather_Height, 0.1f, 0, 5))
                    {
                        gatherInfo.Fan_Height = gather_Height;
                    }

                    using (var disabled = ImRaii.Disabled(_isGeneratingFan || !ImGui.IsKeyDown(ImGuiKey.LeftShift)))
                    {
                        if (ImGui.Button("Generate Fan from Navmesh"))
                        {
                            _ = GenerateFanForNode(editorNode);
                        }
                    }

                    ImGui.SameLine();
                    if (ImGui.Button("Pathfind to fan"))
                    {
                        var randomPos = NodeLocationExtensions.GetRandomGatherPosition(editorNode, Player.Position);
                        P.navmesh.PathfindAndMoveTo(randomPos, true);
                    }

                    ImGui.PopID();

                    #endregion

                    #region Flight Fan Info

                    ImGui.Separator();
                    var flightInfo = editorNode.Flight_FanInfo;

                    var flight_FanStart = flightInfo.Fan_StartAngle;
                    var flight_FanEnd = flightInfo.Fan_EndAngle;
                    var flight_FanMin = flightInfo.Fan_DistanceMin;
                    var flight_FanMax = flightInfo.Fan_DistanceMax;
                    var flight_Height = flightInfo.Fan_Height;

                    ImGui.PushID($"Fan: Flight");

                    ImGui.SetNextItemWidth(100);
                    if (ImGui.DragFloat("Start", ref flight_FanStart, 1, 0, 360))
                    {
                        flightInfo.Fan_StartAngle = flight_FanStart;
                    }

                    ImGui.SameLine();
                    ImGui.SetNextItemWidth(100);
                    if (ImGui.DragFloat("End", ref flight_FanEnd, 1, 0, 360))
                    {
                        flightInfo.Fan_EndAngle = flight_FanEnd;
                    }

                    ImGui.SetNextItemWidth(100);
                    if (ImGui.DragFloat("Min Distance", ref flight_FanMin, 0.2f, 0, 4))
                    {
                        flightInfo.Fan_DistanceMin = flight_FanMin;
                    }

                    ImGui.SameLine();
                    ImGui.SetNextItemWidth(100);
                    if (ImGui.DragFloat("Max Distance", ref flight_FanMax, 0.2f, 0, 4))
                    {
                        flightInfo.Fan_DistanceMax = flight_FanMax;
                    }

                    ImGui.SetNextItemWidth(100);
                    if (ImGui.DragFloat("Height", ref flight_Height, 0.1f, 0, 5))
                    {
                        flightInfo.Fan_Height = flight_Height;
                    }

                    using (var disabled = ImRaii.Disabled(_isGeneratingFan || !ImGui.IsKeyDown(ImGuiKey.LeftShift)))
                    {
                        if (ImGui.Button("Generate Fan from Navmesh"))
                        {
                            _ = GenerateFlightFanForNode(editorNode);
                        }
                    }

                    ImGui.SameLine();
                    if (ImGui.Button("Pathfind to fan"))
                    {
                        var randomPos = NodeLocationExtensions.GetRandomFlightPosition(editorNode, Player.Position);
                        P.navmesh.PathfindAndMoveTo(randomPos, true);
                    }

                    ImGui.PopID();


                    #endregion

                }

                #endregion

                #region Picto Editor

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);

                if (ImGui.CollapsingHeader("Gather Point Color Editor"))
                {
                    foreach (var group in C.Picto_GroupColors)
                    {
                        ImGui.Text($"Group {group.Key}");
                        var primary = group.Value.Primary;
                        var secondary = group.Value.Secondary;

                        ImGui.PushID($"Picto Group Colors {group.Key}");

                        if (ImGui.ColorEdit4("Primary Color", ref primary, ImGuiColorEditFlags.PickerHueWheel))
                        {
                            C.Picto_GroupColors[group.Key].Primary = primary;
                            C.SaveDebounced();
                        }

                        if (ImGui.ColorEdit4("Secondary Color", ref secondary, ImGuiColorEditFlags.PickerHueWheel))
                        {
                            C.Picto_GroupColors[group.Key].Secondary = secondary;
                            C.SaveDebounced();
                        }

                        ImGui.PopID();
                    }
                }

                ImGui.TableNextColumn();

                if (ImGui.CollapsingHeader("Picto Editor Pt. 2"))
                {
                    var color_GatherFan = C.Picto_GatherFanColor;
                    if (ImGui.ColorEdit4("Gathering Fan", ref color_GatherFan, ImGuiColorEditFlags.PickerHueWheel))
                    {
                        C.Picto_GatherFanColor = color_GatherFan;
                        C.SaveDebounced();
                    }

                    var color_FlightFan = C.Picto_FlightFanColor;
                    if (ImGui.ColorEdit4("Landing Fan", ref color_FlightFan, ImGuiColorEditFlags.PickerHueWheel))
                    {
                        C.Picto_FlightFanColor = color_FlightFan;
                        C.SaveDebounced();
                    }

                    var color_SelectedFan = C.Picto_SelectedFan;
                    if (ImGui.ColorEdit4("Selected Fan", ref color_SelectedFan, ImGuiColorEditFlags.PickerHueWheel))
                    {
                        C.Picto_SelectedFan = color_SelectedFan;
                        C.SaveDebounced();
                    }
                }

                #endregion

                ImGui.EndTable();
            }
        }

        private static async Task GenerateFanForNode(NodeLocation route)
        {
            _isGeneratingFan = true;
            _fanGenStatus = string.Empty;

            try
            {
                Vector3 nodePos = route.Position;

                const float snapToleranceXZ = 0.5f;
                const float snapToleranceY = 5f;
                const float testDistanceMin = 1.0f;
                const float testDistanceMax = 2.4f;
                const float distanceStep = 0.5f;
                const int angleSamples = 360;

                var validDistances = new Dictionary<int, List<float>>();
                var validYHeights = new Dictionary<int, float>();

                await Task.Run(() =>
                {
                    for (int angleDeg = 0; angleDeg < angleSamples; angleDeg++)
                    {
                        bool allDistancesValid = true;
                        var distancesForAngle = new List<float>();
                        float highestY = float.MinValue;

                        for (float dist = testDistanceMin; dist <= testDistanceMax; dist += distanceStep)
                        {
                            float standardAngle = 180f - angleDeg;
                            float rad = standardAngle * (MathF.PI / 180f);
                            Vector3 candidate = new Vector3(
                                nodePos.X + dist * MathF.Sin(rad),
                                nodePos.Y,
                                nodePos.Z + dist * MathF.Cos(rad)
                            );

                            var nearest = P.navmesh.NearestPointReachable(candidate, snapToleranceXZ, snapToleranceY);
                            if (nearest.HasValue)
                            {
                                float xzDist = MathF.Sqrt(
                                    MathF.Pow(nearest.Value.X - candidate.X, 2) +
                                    MathF.Pow(nearest.Value.Z - candidate.Z, 2)
                                );
                                float yDist = MathF.Abs(nearest.Value.Y - candidate.Y);

                                if (xzDist <= snapToleranceXZ && yDist <= snapToleranceY)
                                {
                                    distancesForAngle.Add(dist);
                                    if (nearest.Value.Y > highestY)
                                        highestY = nearest.Value.Y;
                                }
                                else
                                {
                                    allDistancesValid = false;
                                    break;
                                }
                            }
                            else
                            {
                                allDistancesValid = false;
                                break;
                            }
                        }

                        if (allDistancesValid && distancesForAngle.Count > 0)
                        {
                            validDistances[angleDeg] = distancesForAngle;
                            validYHeights[angleDeg] = highestY;
                        }
                    }
                });

                if (validDistances.Count == 0)
                {
                    _fanGenStatus = "No reachable points found around this node.";
                    return;
                }

                bool[] valid = new bool[360];
                foreach (var kvp in validDistances)
                    valid[kvp.Key] = true;

                int bestStart = 0, bestLen = 0;
                int currentStart = 0, currentLen = 0;

                for (int i = 0; i < 720; i++)
                {
                    if (valid[i % 360])
                    {
                        if (currentLen == 0)
                            currentStart = i;
                        currentLen++;

                        if (currentLen > bestLen)
                        {
                            bestLen = currentLen;
                            bestStart = currentStart;
                        }
                    }
                    else
                    {
                        currentLen = 0;
                    }

                    if (currentLen >= 360)
                        break;
                }

                if (bestLen == 0)
                {
                    _fanGenStatus = "Could not find a contiguous arc of reachable angles.";
                    return;
                }

                int ffxivStart = bestStart % 360;
                int ffxivEnd = (bestStart + bestLen - 1) % 360;

                float allMin = float.MaxValue, allMax = float.MinValue;
                float arcMaxY = float.MinValue;

                foreach (var kvp in validDistances)
                {
                    int normalizedAngle = ((kvp.Key - ffxivStart) % 360 + 360) % 360;
                    if (normalizedAngle < bestLen)
                    {
                        foreach (var d in kvp.Value)
                        {
                            if (d < allMin) allMin = d;
                            if (d > allMax) allMax = d;
                        }
                    }
                }

                foreach (var kvp in validYHeights)
                {
                    int normalizedAngle = ((kvp.Key - ffxivStart) % 360 + 360) % 360;
                    if (normalizedAngle < bestLen && kvp.Value > arcMaxY)
                        arcMaxY = kvp.Value;
                }

                float fanHeight = 0f;
                if (arcMaxY != float.MinValue && arcMaxY > nodePos.Y)
                    fanHeight = MathF.Round((arcMaxY - nodePos.Y) + 0.2f, 2);

                route.Gathering_FanInfo.Fan_StartAngle = ffxivStart;
                route.Gathering_FanInfo.Fan_EndAngle = ffxivEnd;
                route.Gathering_FanInfo.Fan_DistanceMin = MathF.Round(allMin, 1);
                route.Gathering_FanInfo.Fan_DistanceMax = MathF.Round(allMax, 1);
                route.Gathering_FanInfo.Fan_Height = fanHeight;

                _fanGenStatus = $"Generated! Angles: {ffxivStart}→{ffxivEnd} (arc {bestLen}°), Distance: {allMin:F1}→{allMax:F1}, Height: {fanHeight:F2}";
                IceLogging.Info($"[FanGen] Node {route.Position}: FFXIV {ffxivStart}→{ffxivEnd}, dist {allMin:F1}→{allMax:F1}, height {fanHeight:F2}");
            }
            catch (Exception ex)
            {
                _fanGenStatus = $"Error: {ex.Message}";
                IceLogging.Error($"[FanGen] Failed: {ex.Message}");
            }
            finally
            {
                _isGeneratingFan = false;
            }
        }

        private static async Task GenerateFlightFanForNode(NodeLocation route)
        {
            _isGeneratingFan = true;
            _fanGenStatus = string.Empty;

            try
            {
                // Guard: gathering fan must exist first since we constrain to its arc
                int gatherStart = (int)route.Gathering_FanInfo.Fan_StartAngle;
                int gatherEnd = (int)route.Gathering_FanInfo.Fan_EndAngle;
                int gatherLen = ((gatherEnd - gatherStart) % 360 + 360) % 360 + 1;

                if (gatherLen == 0)
                {
                    _fanGenStatus = "Generate the gathering fan first.";
                    return;
                }

                Vector3 nodePos = route.Position;

                const float snapToleranceXZ = 0.5f;
                const float snapToleranceY = 5f;
                const float landDistanceMin = 2.5f;
                const float landDistanceMax = 4.0f;
                const float distanceStep = 0.5f;

                var validDistances = new Dictionary<int, List<float>>();
                var validYHeights = new Dictionary<int, float>();

                await Task.Run(() =>
                {
                    for (int i = 0; i < gatherLen; i++)
                    {
                        int angleDeg = (gatherStart + i) % 360;
                        bool allDistancesValid = true;
                        var distancesForAngle = new List<float>();
                        float highestY = float.MinValue;

                        for (float dist = landDistanceMin; dist <= landDistanceMax; dist += distanceStep)
                        {
                            float standardAngle = 180f - angleDeg;
                            float rad = standardAngle * (MathF.PI / 180f);
                            Vector3 candidate = new Vector3(
                                nodePos.X + dist * MathF.Sin(rad),
                                nodePos.Y,
                                nodePos.Z + dist * MathF.Cos(rad)
                            );

                            var nearest = P.navmesh.NearestPointReachable(candidate, snapToleranceXZ, snapToleranceY);
                            if (nearest.HasValue)
                            {
                                float xzDist = MathF.Sqrt(
                                    MathF.Pow(nearest.Value.X - candidate.X, 2) +
                                    MathF.Pow(nearest.Value.Z - candidate.Z, 2)
                                );
                                float yDist = MathF.Abs(nearest.Value.Y - candidate.Y);

                                if (xzDist <= snapToleranceXZ && yDist <= snapToleranceY)
                                {
                                    distancesForAngle.Add(dist);
                                    if (nearest.Value.Y > highestY)
                                        highestY = nearest.Value.Y;
                                }
                                else
                                {
                                    allDistancesValid = false;
                                    break;
                                }
                            }
                            else
                            {
                                allDistancesValid = false;
                                break;
                            }
                        }

                        if (allDistancesValid && distancesForAngle.Count > 0)
                        {
                            validDistances[angleDeg] = distancesForAngle;
                            validYHeights[angleDeg] = highestY;
                        }
                    }
                });

                if (validDistances.Count == 0)
                {
                    _fanGenStatus = "No reachable flight points found within gathering arc.";
                    return;
                }

                // Find largest contiguous sub-arc within the gathering arc bounds
                // Indexed locally (0..gatherLen-1) so no wraparound needed
                bool[] valid = new bool[gatherLen];
                foreach (var kvp in validDistances)
                {
                    int localIdx = ((kvp.Key - gatherStart) % 360 + 360) % 360;
                    if (localIdx < gatherLen)
                        valid[localIdx] = true;
                }

                int bestStart = 0, bestLen = 0;
                int currentStart = 0, currentLen = 0;

                for (int i = 0; i < gatherLen; i++)
                {
                    if (valid[i])
                    {
                        if (currentLen == 0) currentStart = i;
                        currentLen++;
                        if (currentLen > bestLen) { bestLen = currentLen; bestStart = currentStart; }
                    }
                    else
                    {
                        currentLen = 0;
                    }
                }

                if (bestLen == 0)
                {
                    _fanGenStatus = "Could not find a contiguous flight arc within gathering bounds.";
                    return;
                }

                int ffxivStart = (gatherStart + bestStart) % 360;
                int ffxivEnd = (gatherStart + bestStart + bestLen - 1) % 360;

                float allMin = float.MaxValue, allMax = float.MinValue;
                float arcMaxY = float.MinValue;

                foreach (var kvp in validDistances)
                {
                    int localIdx = ((kvp.Key - gatherStart) % 360 + 360) % 360;
                    if (localIdx >= bestStart && localIdx < bestStart + bestLen)
                    {
                        foreach (var d in kvp.Value)
                        {
                            if (d < allMin) allMin = d;
                            if (d > allMax) allMax = d;
                        }
                    }
                }

                foreach (var kvp in validYHeights)
                {
                    int localIdx = ((kvp.Key - gatherStart) % 360 + 360) % 360;
                    if (localIdx >= bestStart && localIdx < bestStart + bestLen && kvp.Value > arcMaxY)
                        arcMaxY = kvp.Value;
                }

                float fanHeight = 0f;
                if (arcMaxY != float.MinValue && arcMaxY > nodePos.Y)
                    fanHeight = MathF.Round((arcMaxY - nodePos.Y) + 0.2f, 2);

                route.Flight_FanInfo.Fan_StartAngle = ffxivStart;
                route.Flight_FanInfo.Fan_EndAngle = ffxivEnd;
                route.Flight_FanInfo.Fan_DistanceMin = MathF.Round(allMin, 1);
                route.Flight_FanInfo.Fan_DistanceMax = MathF.Round(allMax, 1);
                route.Flight_FanInfo.Fan_Height = fanHeight;

                _fanGenStatus = $"Flight fan generated! Angles: {ffxivStart}→{ffxivEnd} (arc {bestLen}°), Distance: {allMin:F1}→{allMax:F1}, Height: {fanHeight:F2}";
                IceLogging.Info($"[FanGen] Flight {route.Position}: FFXIV {ffxivStart}→{ffxivEnd}, dist {allMin:F1}→{allMax:F1}, height {fanHeight:F2}");
            }
            catch (Exception ex)
            {
                _fanGenStatus = $"Error: {ex.Message}";
                IceLogging.Error($"[FanGen] Flight failed: {ex.Message}");
            }
            finally
            {
                _isGeneratingFan = false;
            }
        }
    }
}