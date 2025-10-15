using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Interface.Utility.Raii;
using ECommons.GameHelpers;
using ECommons.Logging;
using GatherChill.GatheringInfo;
using Lumina.Excel.Sheets;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GatherChill.Ui.DebugTabs
{
    internal class RouteEditorV2
    {
        private static uint? selectedExpansionId = null;
        private static uint selectedZoneId = 0;
        private static uint selectedRouteId = 0;
        private static int selectedNodeIndex = 0;
        private static uint nodeId = 0;
        private static float maxDistance = 25;

        private static readonly Dictionary<uint, string> ExpansionNames = new()
        {
            [0] = "A Realm Reborn",
            [1] = "Heavensward",
            [2] = "Stormblood",
            [3] = "Shadowbringers",
            [4] = "Endwalker",
            [5] = "Dawntrail"
        };

        private class EditableRoute
        {
            public RouteInfo OriginalRoute { get; set; }
            public List<NodeInfo> EditableNodes { get; set; }

            public EditableRoute(RouteInfo route)
            {
                OriginalRoute = route;
                // Create a mutable copy of the nodes
                EditableNodes = new List<NodeInfo>();
                if (route.Nodes != null)
                {
                    foreach (var node in route.Nodes)
                    {
                        EditableNodes.Add(new NodeInfo
                        {
                            NodeId = node.NodeId,
                            NodePosition = node.NodePosition,
                            LandingInfo = node.LandingInfo != null ? new LandingInfo
                            {
                                LandZone = node.LandingInfo.LandZone,
                                UseRadial = node.LandingInfo.UseRadial,
                                InnerRadius = node.LandingInfo.InnerRadius,
                                OuterRadius = node.LandingInfo.OuterRadius,
                                StartAngle = node.LandingInfo.StartAngle,
                                EndAngle = node.LandingInfo.EndAngle,
                                RotationOffset = node.LandingInfo.RotationOffset
                            } : new LandingInfo()
                        });
                    }
                }
            }
        }
        private static EditableRoute? currentEditableRoute = null;

        public static void Draw()
        {
            try
            {
                ImGui.Text("Route Editor V2");

                if (RD == null)
                {
                    ImGui.Text("ERROR: Route data is null!");
                    return;
                }

                if (RD.GatheringInfo == null)
                {
                    ImGui.Text("ERROR: GatheringInfo is null!");
                    return;
                }

                ImGui.Text($"Loaded: {RD.GatheringInfo.Count} expansions");
                ImGui.Separator();

                if (ImGui.BeginTable("RouteEditorV2Table", 2, ImGuiTableFlags.Resizable))
                {
                    ImGui.TableSetupColumn("Selector", ImGuiTableColumnFlags.WidthFixed, 300);
                    ImGui.TableSetupColumn("Editor", ImGuiTableColumnFlags.WidthStretch);
                    ImGui.TableNextRow();

                    // Left side - selector
                    ImGui.TableSetColumnIndex(0);
                    if (ImGui.BeginChild("SelectorChild", new Vector2(0, 0), true))
                    {
                        DrawExpansionDropdown();

                        if (selectedExpansionId.HasValue && RD.GatheringInfo.ContainsKey(selectedExpansionId.Value))
                        {
                            DrawZoneDropdown();
                        }

                        ImGui.Separator();

                        if (selectedZoneId > 0 &&
                            selectedExpansionId.HasValue &&
                            RD.GatheringInfo.ContainsKey(selectedExpansionId.Value) &&
                            RD.GatheringInfo[selectedExpansionId.Value].ContainsKey(selectedZoneId))
                        {
                            DrawRouteList();
                        }

                        ImGui.EndChild();
                    }

                    // Right side - editor
                    ImGui.TableSetColumnIndex(1);
                    if (ImGui.BeginChild("EditorChild", new Vector2(0, 0), true))
                    {
                        if (selectedRouteId > 0 &&
                            selectedExpansionId.HasValue &&
                            RD.GatheringInfo.ContainsKey(selectedExpansionId.Value) &&
                            RD.GatheringInfo[selectedExpansionId.Value].ContainsKey(selectedZoneId) &&
                            RD.GatheringInfo[selectedExpansionId.Value][selectedZoneId].ContainsKey(selectedRouteId))
                        {
                            var route = RD.GatheringInfo[selectedExpansionId.Value][selectedZoneId][selectedRouteId];
                            DrawRouteEditor(route);
                        }
                        else
                        {
                            ImGui.Text("Select a route to edit");
                        }

                        ImGui.EndChild();
                    }

                    ImGui.EndTable();
                }
            }
            catch (Exception ex)
            {
                ImGui.Text($"ERROR: {ex.Message}");
                PluginLog.Error($"RouteEditorV2 exception: {ex}");
            }
        }

        private static void DrawExpansionDropdown()
        {
            ImGui.Text("Expansion:");
            ImGui.SetNextItemWidth(-1);

            string currentExpansion = !selectedExpansionId.HasValue ? "Select Expansion..." :
                                     ExpansionNames.ContainsKey(selectedExpansionId.Value) ?
                                     ExpansionNames[selectedExpansionId.Value] :
                                     $"Expansion {selectedExpansionId.Value}";

            if (ImGui.BeginCombo("##ExpansionCombo", currentExpansion))
            {
                foreach (var expansionId in RD.GatheringInfo.Keys.OrderBy(x => x))
                {
                    string expansionName = ExpansionNames.ContainsKey(expansionId) ?
                                          ExpansionNames[expansionId] :
                                          $"Expansion {expansionId}";

                    bool isSelected = selectedExpansionId.HasValue && selectedExpansionId.Value == expansionId;

                    if (ImGui.Selectable(expansionName, isSelected))
                    {
                        selectedExpansionId = expansionId;
                        selectedZoneId = 0;
                        selectedRouteId = 0;
                    }

                    if (isSelected)
                        ImGui.SetItemDefaultFocus();
                }

                ImGui.EndCombo();
            }
        }

        private static void DrawZoneDropdown()
        {
            ImGui.Text("Zone:");
            ImGui.SetNextItemWidth(-1);

            string currentZone = selectedZoneId == 0 ? "Select Zone..." : $"{TerritoryName(selectedZoneId)} ({selectedZoneId})";

            if (ImGui.BeginCombo("##ZoneCombo", currentZone))
            {
                var zones = RD.GatheringInfo[selectedExpansionId.Value];

                foreach (var zoneId in zones.Keys.OrderBy(x => x))
                {
                    bool isSelected = selectedZoneId == zoneId;
                    string zoneName = $"{TerritoryName(zoneId)} ({zoneId})";

                    if (ImGui.Selectable(zoneName, isSelected))
                    {
                        selectedZoneId = zoneId;
                        selectedRouteId = 0;
                    }

                    if (isSelected)
                        ImGui.SetItemDefaultFocus();
                }

                ImGui.EndCombo();
            }
        }

        private static string TerritoryName(uint id)
        {
            try
            {
                if (Svc.Data.GetExcelSheet<TerritoryType>().TryGetRow(id, out var row))
                {
                    if (row.PlaceName.RowId != 0)
                    {
                        return Svc.Data.GetExcelSheet<PlaceName>().GetRow(row.PlaceName.RowId).Name.ToString();
                    }
                }
            }
            catch { }

            return id.ToString();
        }

        private static void DrawRouteList()
        {
            ImGui.Text("Routes:");
            ImGui.Separator();

            if (ImGui.BeginChild("RouteListChild", new Vector2(-1, -1), true))
            {
                var routes = RD.GatheringInfo[selectedExpansionId.Value][selectedZoneId];

                foreach (var (routeId, route) in routes.OrderBy(x => x.Key))
                {
                    bool isSelected = selectedRouteId == routeId;
                    string routeType = (route.GatherType == 0 || route.GatherType == 1) ? "MIN" : "BTN";
                    string displayName = $"{routeType}_{routeId}";

                    if (ImGui.Selectable(displayName, isSelected))
                    {
                        selectedRouteId = routeId;
                        selectedNodeIndex = 0;
                    }

                    if (ImGui.IsItemHovered())
                    {
                        ImGui.BeginTooltip();
                        ImGui.Text($"Route ID: {routeId}");
                        ImGui.Text($"Nodes: {route.NodeIds.Count}");
                        ImGui.Text($"Items: {route.ItemIds.Count}");
                        ImGui.EndTooltip();
                    }
                }

                ImGui.EndChild();
            }
        }

        private static void DrawRouteEditor(RouteInfo route)
        {
            // Create editable route if it's a new selection
            if (currentEditableRoute == null || currentEditableRoute.OriginalRoute.Id != route.Id)
            {
                currentEditableRoute = new EditableRoute(route);
            }

            ImGui.Text($"Editing Route: {route.Id}");
            ImGui.Separator();

            // Add export buttons here
            if (ImGui.Button("Export This Route"))
            {
                ExportSingleRoute(route, currentEditableRoute);
            }
            ImGui.SameLine();
            if (ImGui.Button("Export ALL Routes"))
            {
                ExportAllRoutes();
            }

            ImGui.Separator();

            ImGui.Text($"Expansion: {route.ExpansionId} | Zone: {route.ZoneId} | Type: {route.GatherType}");
            ImGui.Text($"Map: ({route.MapPosition.X}, {route.MapPosition.Y}) | Radius: {route.Radius}");

            ImGui.Separator();

            // Collapsible Node IDs section
            if (ImGui.CollapsingHeader($"Node IDs ({route.NodeIds.Count})"))
            {
                ImGui.Indent();
                foreach (var nodeId in route.NodeIds.OrderBy(x => x))
                {
                    ImGui.BulletText(nodeId.ToString());
                }
                ImGui.Unindent();
            }

            // Collapsible Item IDs section
            if (ImGui.CollapsingHeader($"Item IDs ({route.ItemIds.Count})"))
            {
                ImGui.Indent();
                foreach (var itemId in route.ItemIds.OrderBy(x => x))
                {
                    string itemInfo = $"[{itemId}]";

                    try
                    {
                        if (Svc.Data.GetExcelSheet<Item>().TryGetRow(itemId, out var row))
                        {
                            itemInfo += $" - {row.Name.ToString()}";
                        }
                    }
                    catch { }

                    ImGui.BulletText(itemInfo);
                }
                ImGui.Unindent();
            }

            // Node count display
            ImGui.Text($"Editable Nodes: {currentEditableRoute.EditableNodes.Count}");

            ImGui.Separator();

            // Node Editor Section
            ImGui.Text("Node Editor");

            var textLineHeight = ImGui.GetTextLineHeightWithSpacing();
            var childHeight = textLineHeight * 8 + 20;

            if (ImGui.BeginTable("NodeEditTable", 3, ImGuiTableFlags.SizingFixedFit))
            {
                ImGui.TableSetupColumn("NodeSelector", ImGuiTableColumnFlags.WidthStretch, 200);
                ImGui.TableSetupColumn("NodeViewer", ImGuiTableColumnFlags.WidthStretch, 200);
                ImGui.TableSetupColumn("Buttons", ImGuiTableColumnFlags.WidthFixed, 100f);

                ImGui.TableNextRow();

                // Column 1: Node Selector (list of existing nodes)
                ImGui.TableSetColumnIndex(0);
                ImGui.Text("Node Selector");
                if (ImGui.BeginChild("NodeSelector", new Vector2(0, childHeight), true))
                {
                    if (currentEditableRoute.EditableNodes.Count > 0)
                    {
                        for (int i = 0; i < currentEditableRoute.EditableNodes.Count; i++)
                        {
                            var node = currentEditableRoute.EditableNodes[i];

                            ImGui.PushID($"node_{i}");

                            string label = $"Node: {node.NodeId} - [{node.NodePosition.X:N2}, {node.NodePosition.Y:N2}, {node.NodePosition.Z:N2}]";
                            if (ImGui.Selectable(label, selectedNodeIndex == i))
                            {
                                selectedNodeIndex = i;
                            }

                            ImGui.PopID();
                        }
                    }
                    else
                    {
                        ImGui.Text("No nodes in route");
                    }
                    ImGui.EndChild();
                }

                // Column 2: Node Viewer (nearby gathering nodes)
                ImGui.TableSetColumnIndex(1);
                ImGui.Text("Gathering Node Viewer");
                if (ImGui.BeginChild("NodeViewer", new Vector2(0, childHeight), true))
                {
                    foreach (var x in Svc.Objects
                        .Where(x => x.ObjectKind == ObjectKind.GatheringPoint && Player.DistanceTo(x.Position) <= maxDistance)
                        .OrderBy(x => Player.DistanceTo(x.Position))
                        .ToList())
                    {
                        if (ImGui.Selectable($"Id: {x.BaseId} | Distance: {Player.DistanceTo(x.Position):N2}"))
                        {
                            nodeId = x.BaseId;
                        }
                    }
                    ImGui.EndChild();
                }

                // Column 3: Action Buttons
                ImGui.TableSetColumnIndex(2);
                ImGui.Text(" ");
                if (ImGui.BeginChild("ActionButtons", new Vector2(0, childHeight), true))
                {
                    using (ImRaii.Disabled(nodeId == 0))
                    {
                        if (ImGui.Button("Add Node", new Vector2(-1, 0)))
                        {
                            var x = Svc.Objects.Where(obj => obj.BaseId == nodeId).FirstOrDefault();
                            if (x != null)
                            {
                                var playerPos = Player.Position;

                                var addNode = new NodeInfo
                                {
                                    NodeId = x.BaseId,
                                    NodePosition = x.Position,
                                    LandingInfo = new LandingInfo
                                    {
                                        LandZone = playerPos,
                                    }
                                };

                                currentEditableRoute.EditableNodes.Add(addNode);

                                PluginLog.Information($"Added node {x.BaseId}. Total nodes: {currentEditableRoute.EditableNodes.Count}");
                            }

                            nodeId = 0;
                        }
                    }

                    using (ImRaii.Disabled(!ImGui.GetIO().KeyShift || currentEditableRoute.EditableNodes.Count == 0 || selectedNodeIndex < 0))
                    {
                        if (ImGui.Button("Remove\n(Hold Shift)", new Vector2(-1, 0)))
                        {
                            if (selectedNodeIndex >= 0 && selectedNodeIndex < currentEditableRoute.EditableNodes.Count)
                            {
                                currentEditableRoute.EditableNodes.RemoveAt(selectedNodeIndex);

                                PluginLog.Information($"Removed node at index {selectedNodeIndex}");

                                if (selectedNodeIndex > 0)
                                    selectedNodeIndex--;
                            }
                        }
                    }

                    ImGui.EndChild();
                }

                ImGui.EndTable();
            }

            // Add this new section for detailed node editing
            ImGui.Separator();

            // Node details editor
            if (currentEditableRoute.EditableNodes.Count > 0 && selectedNodeIndex >= 0 && selectedNodeIndex < currentEditableRoute.EditableNodes.Count)
            {
                ImGui.Text("Selected Node Details");
                ImGui.Separator();

                var selectedNode = currentEditableRoute.EditableNodes[selectedNodeIndex];

                ImGui.Text($"Node ID: {selectedNode.NodeId}");
                ImGui.Text($"Position: X: {selectedNode.NodePosition.X:N2}, Y: {selectedNode.NodePosition.Y:N2}, Z: {selectedNode.NodePosition.Z:N2}");

                ImGui.Separator();

                if (selectedNode.LandingInfo != null)
                {
                    ImGui.Text("Landing Position");
                    Vector3 landZone = selectedNode.LandingInfo.LandZone;
                    ImGui.SetNextItemWidth(300);
                    if (ImGui.DragFloat3($"##Land_Position_{selectedNode.NodeId}", ref landZone, 0.1f))
                    {
                        selectedNode.LandingInfo.LandZone = landZone;
                    }
                    ImGui.SameLine();
                    if (ImGui.Button("Set to Current Pos"))
                    {
                        selectedNode.LandingInfo.LandZone = Player.Position;
                    }

                    // Navigation buttons
                    if (ImGui.Button("Nav to Landing"))
                    {
                        P.navmesh.PathfindAndMoveTo(selectedNode.LandingInfo.LandZone, false);
                    }
                    ImGui.SameLine();
                    if (ImGui.Button("Nav to Node"))
                    {
                        P.navmesh.PathfindAndMoveTo(selectedNode.NodePosition, false);
                    }
                    ImGui.SameLine();
                    if (ImGui.Button("Stop Nav"))
                    {
                        P.navmesh.Stop();
                    }

                    ImGui.Separator();

                    // Radial positioning settings
                    bool useRadial = selectedNode.LandingInfo.UseRadial;
                    if (ImGui.Checkbox("Use Radial Positioning", ref useRadial))
                    {
                        selectedNode.LandingInfo.UseRadial = useRadial;
                    }

                    if (selectedNode.LandingInfo.UseRadial)
                    {
                        ImGui.Indent();

                        float innerRadius = selectedNode.LandingInfo.InnerRadius;
                        if (ImGui.DragFloat("Inner Radius", ref innerRadius, 0.1f, 0f, 50f))
                        {
                            selectedNode.LandingInfo.InnerRadius = Math.Max(0f, innerRadius);
                        }

                        float outerRadius = selectedNode.LandingInfo.OuterRadius;
                        if (ImGui.DragFloat("Outer Radius", ref outerRadius, 0.1f, innerRadius, 50f))
                        {
                            selectedNode.LandingInfo.OuterRadius = Math.Max(innerRadius, outerRadius);
                        }

                        float startAngle = selectedNode.LandingInfo.StartAngle;
                        if (ImGui.SliderFloat("Start Angle", ref startAngle, 0f, 360f))
                        {
                            selectedNode.LandingInfo.StartAngle = startAngle;
                        }

                        float endAngle = selectedNode.LandingInfo.EndAngle;
                        if (ImGui.SliderFloat("End Angle", ref endAngle, 0f, 360f))
                        {
                            selectedNode.LandingInfo.EndAngle = endAngle;
                        }

                        float rotationOffset = selectedNode.LandingInfo.RotationOffset;
                        if (ImGui.SliderFloat("Rotation Offset", ref rotationOffset, 0f, 360f))
                        {
                            selectedNode.LandingInfo.RotationOffset = rotationOffset;
                        }

                        ImGui.Unindent();
                    }
                }
                else
                {
                    ImGui.Text("Landing Info: Not set");
                    if (ImGui.Button("Initialize Landing Info"))
                    {
                        selectedNode.LandingInfo = new LandingInfo
                        {
                            LandZone = Player.Position
                        };
                    }
                }
            }
            else if (currentEditableRoute.EditableNodes.Count == 0)
            {
                ImGui.Text("No nodes to edit. Add a node first.");
            }
        }
        private static void ExportSingleRoute(RouteInfo route, EditableRoute editableRoute)
        {
            try
            {
                string baseRoutesPath = Path.Combine(Svc.PluginInterface.AssemblyLocation.Directory?.FullName ?? "", "Routes");

                string classPrefix = (route.GatherType == 0 || route.GatherType == 1) ? "MIN" : "BTN";
                string zoneName = TerritoryName(route.ZoneId);
                string expansionName = ExpansionNames.ContainsKey(route.ExpansionId) ?
                                      ExpansionNames[route.ExpansionId] :
                                      $"Expansion{route.ExpansionId}";

                string expansionFolder = Path.Combine(baseRoutesPath, SanitizeFolderName(expansionName));
                string zoneFolderName = $"{SanitizeFolderName(zoneName)}_{route.ZoneId}";
                string zoneFolder = Path.Combine(expansionFolder, zoneFolderName);

                Directory.CreateDirectory(zoneFolder);

                string className = $"{classPrefix}_{route.Id}";
                string filename = $"{className}.cs";
                string fullPath = Path.Combine(zoneFolder, filename);

                // Use the editable nodes instead of route.Nodes
                string fileContent = GenerateRouteClassFile(className, route, expansionName, zoneName, editableRoute.EditableNodes);

                File.WriteAllText(fullPath, fileContent);

                PluginLog.Information($"Exported route to: {fullPath}");
                Svc.Chat.Print($"Route {route.Id} exported successfully!");
            }
            catch (Exception ex)
            {
                PluginLog.Error($"Failed to export route {route.Id}: {ex.Message}");
                Svc.Chat.Print($"Failed to export route: {ex.Message}");
            }
        }

        private static void ExportAllRoutes()
        {
            string baseRoutesPath = Path.Combine(Svc.PluginInterface.AssemblyLocation.Directory?.FullName ?? "", "Routes");

            int successCount = 0;
            int failCount = 0;

            foreach (var (expansionId, zones) in RD.GatheringInfo)
            {
                foreach (var (zoneId, routes) in zones)
                {
                    foreach (var (routeId, route) in routes)
                    {
                        try
                        {
                            string classPrefix = (route.GatherType == 0 || route.GatherType == 1) ? "MIN" : "BTN";
                            string zoneName = TerritoryName(route.ZoneId);
                            string expansionName = ExpansionNames.ContainsKey(route.ExpansionId) ?
                                                  ExpansionNames[route.ExpansionId] :
                                                  $"Expansion{route.ExpansionId}";

                            string expansionFolder = Path.Combine(baseRoutesPath, SanitizeFolderName(expansionName));
                            string zoneFolderName = $"{SanitizeFolderName(zoneName)}_{route.ZoneId}";
                            string zoneFolder = Path.Combine(expansionFolder, zoneFolderName);

                            Directory.CreateDirectory(zoneFolder);

                            string className = $"{classPrefix}_{route.Id}";
                            string filename = $"{className}.cs";
                            string fullPath = Path.Combine(zoneFolder, filename);

                            // Use original route nodes for mass export
                            string fileContent = GenerateRouteClassFile(className, route, expansionName, zoneName, route.Nodes);
                            File.WriteAllText(fullPath, fileContent);

                            successCount++;
                        }
                        catch (Exception ex)
                        {
                            failCount++;
                            PluginLog.Error($"Failed to export route {routeId}: {ex.Message}");
                        }
                    }
                }
            }

            PluginLog.Information($"Export complete! Success: {successCount}, Failed: {failCount}");
            Svc.Chat.Print($"Exported {successCount} routes successfully! ({failCount} failed)");

            try
            {
                System.Diagnostics.Process.Start("explorer.exe", baseRoutesPath);
            }
            catch (Exception ex)
            {
                PluginLog.Error($"Failed to open folder: {ex.Message}");
            }
        }

        private static string GenerateRouteClassFile(string className, RouteInfo route, string expansionName, string zoneName, List<NodeInfo> nodes)
        {
            var sb = new StringBuilder();

            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Numerics;");
            sb.AppendLine("using GatherChill.GatheringInfo;");
            sb.AppendLine();

            string zoneFolderName = $"{SanitizeFolderName(zoneName)}_{route.ZoneId}";
            string namespaceName = $"GatherChill.Routes.{SanitizeFolderName(expansionName)}.{zoneFolderName}";
            sb.AppendLine($"namespace {namespaceName}");
            sb.AppendLine("{");
            sb.AppendLine($"\tpublic class {className} : RouteInfo");
            sb.AppendLine("\t{");

            sb.AppendLine($"\t\tpublic override uint Id => {route.Id};");
            sb.AppendLine($"\t\tpublic override uint ExpansionId => {route.ExpansionId};");
            sb.AppendLine($"\t\tpublic override uint ZoneId => {route.ZoneId};");
            sb.AppendLine($"\t\tpublic override uint GatherType => {route.GatherType};");
            sb.AppendLine($"\t\tpublic override Vector2 MapPosition => new Vector2({route.MapPosition.X}f, {route.MapPosition.Y}f);");
            sb.AppendLine($"\t\tpublic override int Radius => {route.Radius};");
            sb.AppendLine();

            sb.AppendLine("\t\tpublic override HashSet<uint> NodeIds => new()");
            sb.AppendLine("\t\t{");
            foreach (var nodeId in route.NodeIds.OrderBy(x => x))
            {
                sb.AppendLine($"\t\t\t{nodeId},");
            }
            sb.AppendLine("\t\t};");
            sb.AppendLine();

            sb.AppendLine("\t\tpublic override HashSet<uint> ItemIds => new()");
            sb.AppendLine("\t\t{");
            if (route.ItemIds != null)
            {
                foreach (var itemId in route.ItemIds.OrderBy(x => x))
                {
                    sb.AppendLine($"\t\t\t{itemId},");
                }
            }
            sb.AppendLine("\t\t};");
            sb.AppendLine();

            sb.AppendLine("\t\tpublic override List<NodeInfo> Nodes => new()");
            sb.AppendLine("\t\t{");
            if (nodes != null && nodes.Count > 0)
            {
                foreach (var nodeInfo in nodes)
                {
                    sb.AppendLine("\t\t\tnew NodeInfo");
                    sb.AppendLine("\t\t\t{");
                    sb.AppendLine($"\t\t\t\tNodeId = {nodeInfo.NodeId},");
                    sb.AppendLine($"\t\t\t\tNodePosition = new Vector3({nodeInfo.NodePosition.X}f, {nodeInfo.NodePosition.Y}f, {nodeInfo.NodePosition.Z}f),");
                    sb.AppendLine("\t\t\t\tLandingInfo = new LandingInfo");
                    sb.AppendLine("\t\t\t\t{");
                    sb.AppendLine($"\t\t\t\t\tLandZone = new Vector3({nodeInfo.LandingInfo.LandZone.X}f, {nodeInfo.LandingInfo.LandZone.Y}f, {nodeInfo.LandingInfo.LandZone.Z}f),");
                    sb.AppendLine($"\t\t\t\t\tUseRadial = {nodeInfo.LandingInfo.UseRadial.ToString().ToLower()},");
                    sb.AppendLine($"\t\t\t\t\tInnerRadius = {nodeInfo.LandingInfo.InnerRadius}f,");
                    sb.AppendLine($"\t\t\t\t\tOuterRadius = {nodeInfo.LandingInfo.OuterRadius}f,");
                    sb.AppendLine($"\t\t\t\t\tStartAngle = {nodeInfo.LandingInfo.StartAngle}f,");
                    sb.AppendLine($"\t\t\t\t\tEndAngle = {nodeInfo.LandingInfo.EndAngle}f,");
                    sb.AppendLine($"\t\t\t\t\tRotationOffset = {nodeInfo.LandingInfo.RotationOffset}f");
                    sb.AppendLine("\t\t\t\t}");
                    sb.AppendLine("\t\t\t},");
                }
            }
            sb.AppendLine("\t\t};");

            sb.AppendLine("\t}");
            sb.AppendLine("}");

            return sb.ToString();
        }

        private static string SanitizeFolderName(string name)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            string sanitized = string.Join("_", name.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));
            sanitized = sanitized.Replace(" ", "");
            return sanitized;
        }
    }
}