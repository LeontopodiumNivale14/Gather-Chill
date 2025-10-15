using Dalamud.Interface.Colors;
using Dalamud.Interface.Utility.Raii;
using ECommons.Logging;
using GatherChill.Yaml;
using GatherChill.Yaml.ConfigFiles;
using GatherChill.Utilities.GatheringHelpers;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui;

internal class RouteEditorWindow : Window
{
    private RouteConfig? selectedRoute;
    private RouteConfig? editingRoute; // Copy for editing
    private List<(string Display, RouteConfig Config)> availableRoutes = new();
    private string routeFilter = "";
    private bool showDeleteConfirmation = false;

    // Backup values for detecting changes that require file moves
    private string originalZoneName = "";
    private uint originalZoneId = 0;
    private string originalExpansionName = "";

    // UI state
    private string newNodeIdInput = "";
    private string newItemIdInput = "";
    private string newItemNameInput = "";

    // NodeInfo UI state
    private int selectedNodeInfoIndex = -1;
    private GatherClasses.RouteData? editingNodeInfo = null;

    public RouteEditorWindow() :
        base($"Route Editor {P.GetType().Assembly.GetName().Version} ###RouteEditor")
    {
        Flags = ImGuiWindowFlags.None;
        SizeConstraints = new()
        {
            MinimumSize = new Vector2(900, 700),
            MaximumSize = new Vector2(1400, 1200),
        };
        P.windowSystem.AddWindow(this);
        AllowPinning = false;

        RefreshRouteList();
    }

    public void Dispose() { }

    private void RefreshRouteList()
    {
        availableRoutes = RouteConfigManager.GetAllRoutesForSelection();
    }

    public override void Draw()
    {
        if (ImGui.BeginTable("RouteEditorLayout", 2, ImGuiTableFlags.Resizable))
        {
            ImGui.TableSetupColumn("Selection", ImGuiTableColumnFlags.WidthFixed, 300);
            ImGui.TableSetupColumn("Editor", ImGuiTableColumnFlags.WidthStretch);

            ImGui.TableNextRow();

            // Left column - Route selection
            ImGui.TableSetColumnIndex(0);
            DrawRouteSelection();

            // Right column - Route editor
            ImGui.TableSetColumnIndex(1);
            DrawRouteEditor();

            ImGui.EndTable();
        }

        // Delete confirmation popup
        if (showDeleteConfirmation)
        {
            ImGui.OpenPopup("Delete Route?");
        }

        if (ImGui.BeginPopupModal("Delete Route?", ref showDeleteConfirmation, ImGuiWindowFlags.AlwaysAutoResize))
        {
            ImGui.Text($"Are you sure you want to delete Route {selectedRoute?.RouteId}?");
            ImGui.Text($"Zone: {selectedRoute?.ZoneName} ({selectedRoute?.ZoneId})");
            ImGui.Text("This action cannot be undone!");

            ImGui.Separator();

            if (ImGui.Button("Delete", new Vector2(100, 0)))
            {
                if (selectedRoute != null)
                {
                    RouteConfigManager.DeleteRouteConfig(selectedRoute);
                    RefreshRouteList();
                    selectedRoute = null;
                    editingRoute = null;
                }
                showDeleteConfirmation = false;
                ImGui.CloseCurrentPopup();
            }

            ImGui.SameLine();

            if (ImGui.Button("Cancel", new Vector2(100, 0)))
            {
                showDeleteConfirmation = false;
                ImGui.CloseCurrentPopup();
            }

            ImGui.EndPopup();
        }
    }

    private void DrawRouteSelection()
    {
        ImGui.Text("Route Selection");
        ImGui.Separator();

        if (ImGui.Button("Refresh List"))
        {
            RefreshRouteList();
        }

        ImGui.SameLine();

        if (ImGui.Button("New Route"))
        {
            CreateNewRoute();
        }

        // Filter input
        ImGui.SetNextItemWidth(-1);
        if (ImGui.InputTextWithHint("##RouteFilter", "Filter routes...", ref routeFilter, 256))
        {
            // Filter is applied in the list below
        }

        ImGui.Separator();

        // Route list
        if (ImGui.BeginChild("RouteList", new Vector2(-1, -1), true))
        {
            var filteredRoutes = string.IsNullOrEmpty(routeFilter)
                ? availableRoutes
                : availableRoutes.Where(r => r.Display.Contains(routeFilter, StringComparison.OrdinalIgnoreCase)).ToList();

            foreach (var (display, config) in filteredRoutes)
            {
                bool isSelected = selectedRoute?.RouteId == config.RouteId;

                if (ImGui.Selectable(display, isSelected))
                {
                    SelectRoute(config);
                }

                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.Text($"Route ID: {config.RouteId}");
                    ImGui.Text($"Zone: {config.ZoneName} ({config.ZoneId})");
                    ImGui.Text($"Expansion: {config.ExpansionName}");
                    ImGui.Text($"Gathering Type: {config.GatheringType}");
                    ImGui.Text($"Nodes: {config.NodeIds.Count}");
                    ImGui.Text($"Items: {config.Items.Count}");
                    ImGui.Text($"Node Info: {config.NodeInfo.Count}");
                    ImGui.EndTooltip();
                }
            }

            ImGui.EndChild();
        }
    }

    private void DrawRouteEditor()
    {
        if (editingRoute == null)
        {
            ImGui.Text("Select a route to edit");
            return;
        }

        ImGui.Text($"Editing Route {editingRoute.RouteId}");
        ImGui.Separator();

        // Save/Cancel buttons
        if (ImGui.Button("Save Changes"))
        {
            SaveCurrentRoute();
        }

        ImGui.SameLine();

        if (ImGui.Button("Cancel Changes"))
        {
            CancelChanges();
        }

        ImGui.SameLine();

        using (ImRaii.PushColor(ImGuiCol.Button, ImGuiColors.DalamudRed))
        {
            if (ImGui.Button("Delete Route"))
            {
                showDeleteConfirmation = true;
            }
        }

        if (ImGui.Button("Copy to new format"))
        {
            string exportedCode = ExportRouteInfo();
            ImGui.SetClipboardText(exportedCode);
            PluginLog.Information("Route exported to clipboard!");
        }

        ImGui.Separator();

        // Tabbed interface for better organization
        if (ImGui.BeginTabBar("RouteEditorTabs"))
        {
            if (ImGui.BeginTabItem("Basic Info"))
            {
                DrawBasicRouteInfo();
                DrawMapInfo();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("Node IDs"))
            {
                DrawNodeIdsSection();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("Items"))
            {
                DrawItemsSection();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("Node Details"))
            {
                DrawNodeInfoSection();
                ImGui.EndTabItem();
            }

            ImGui.EndTabBar();
        }
    }

    private void DrawBasicRouteInfo()
    {
        if (ImGui.CollapsingHeader("Basic Information", ImGuiTreeNodeFlags.DefaultOpen))
        {
            // Route ID
            int routeId = (int)editingRoute!.RouteId;
            if (ImGui.InputInt("Route ID", ref routeId))
            {
                if (routeId >= 0)
                    editingRoute.RouteId = (uint)routeId;
            }

            // Zone Name
            string zoneName = editingRoute.ZoneName;
            if (ImGui.InputText("Zone Name", ref zoneName, 256))
            {
                editingRoute.ZoneName = zoneName;
            }

            // Zone ID
            int zoneId = (int)editingRoute.ZoneId;
            if (ImGui.InputInt("Zone ID", ref zoneId))
            {
                if (zoneId >= 0)
                    editingRoute.ZoneId = (uint)zoneId;
            }

            // Expansion Name
            string expansionName = editingRoute.ExpansionName;
            if (ImGui.InputText("Expansion Name", ref expansionName, 256))
            {
                editingRoute.ExpansionName = expansionName;
            }

            // Expansion ID
            int expansionId = (int)editingRoute.ExpansionId;
            if (ImGui.InputInt("Expansion ID", ref expansionId))
            {
                if (expansionId >= 0)
                    editingRoute.ExpansionId = (uint)expansionId;
            }

            // Gathering Type
            int gatheringType = (int)editingRoute.GatheringType;
            if (ImGui.InputInt("Gathering Type", ref gatheringType))
            {
                if (gatheringType >= 0)
                    editingRoute.GatheringType = (uint)gatheringType;
            }
        }
    }

    private void DrawMapInfo()
    {
        if (ImGui.CollapsingHeader("Map Information"))
        {
            // Map Center
            Vector2 mapCenter = editingRoute!.MapCenter;
            if (ImGui.InputFloat2("Map Center", ref mapCenter))
            {
                editingRoute.MapCenter = mapCenter;
            }

            // Map Radius
            int mapRadius = (int)editingRoute.MapRadius;
            if (ImGui.InputInt("Map Radius", ref mapRadius))
            {
                if (mapRadius >= 0)
                    editingRoute.MapRadius = (uint)mapRadius;
            }
        }
    }

    private void DrawNodeIdsSection()
    {
        // Add new node ID
        ImGui.Text("Add Node ID:");
        ImGui.SetNextItemWidth(200);
        ImGui.InputText("##NewNodeId", ref newNodeIdInput, 32);
        ImGui.SameLine();
        if (ImGui.Button("Add##NodeId"))
        {
            if (uint.TryParse(newNodeIdInput, out uint nodeId) && !editingRoute!.NodeIds.Contains(nodeId))
            {
                editingRoute.NodeIds.Add(nodeId);
                newNodeIdInput = "";
            }
        }

        ImGui.Separator();

        // List existing node IDs
        var nodeIds = editingRoute!.NodeIds.ToList();
        for (int i = 0; i < nodeIds.Count; i++)
        {
            var nodeId = nodeIds[i];
            ImGui.Text($"{nodeId}");
            ImGui.SameLine();
            if (ImGui.Button($"Remove##NodeId{i}"))
            {
                editingRoute.NodeIds.Remove(nodeId);
            }
        }
    }

    private void DrawItemsSection()
    {
        // Add new item
        ImGui.Text("Add Item:");
        ImGui.SetNextItemWidth(100);
        ImGui.InputText("Item ID##NewItem", ref newItemIdInput, 32);
        ImGui.SameLine();
        ImGui.SetNextItemWidth(200);
        ImGui.InputText("Item Name##NewItem", ref newItemNameInput, 256);
        ImGui.SameLine();
        if (ImGui.Button("Add##Item"))
        {
            if (uint.TryParse(newItemIdInput, out uint itemId) && !string.IsNullOrEmpty(newItemNameInput))
            {
                if (!editingRoute!.Items.ContainsKey(itemId))
                {
                    editingRoute.Items[itemId] = newItemNameInput;
                    editingRoute.ItemIds.Add(itemId);
                    newItemIdInput = "";
                    newItemNameInput = "";
                }
            }
        }

        ImGui.Separator();

        // List existing items
        var itemsToRemove = new List<uint>();
        foreach (var (itemId, itemName) in editingRoute!.Items.ToList())
        {
            string editableName = itemName;
            ImGui.SetNextItemWidth(80);
            ImGui.Text($"{itemId}");
            ImGui.SameLine();
            ImGui.SetNextItemWidth(200);
            if (ImGui.InputText($"##ItemName{itemId}", ref editableName, 256))
            {
                editingRoute.Items[itemId] = editableName;
            }
            ImGui.SameLine();
            if (ImGui.Button($"Remove##Item{itemId}"))
            {
                itemsToRemove.Add(itemId);
            }
        }

        // Remove items marked for removal
        foreach (var itemId in itemsToRemove)
        {
            editingRoute.Items.Remove(itemId);
            editingRoute.ItemIds.Remove(itemId);
        }
    }

    private void DrawNodeInfoSection()
    {
        ImGui.Text("Node Details Configuration");
        ImGui.Separator();

        // Add new node info button
        if (ImGui.Button("Add New Node Detail"))
        {
            var newNodeInfo = new GatherClasses.RouteData
            {
                NodeId = 0,
                NodePosition = Vector3.Zero,
                LandZone = Vector3.Zero,
                RadialPositioning = false,
                InnerRadius = 1.0f,
                OuterRadius = 3.0f,
                StartAngle = 0.0f,
                EndAngle = 360.0f,
                RotationOffset = 0.0f
            };
            editingRoute!.NodeInfo.Add(newNodeInfo);
        }

        ImGui.Separator();

        // Two-column layout: List on left, editor on right
        if (ImGui.BeginTable("NodeInfoLayout", 2, ImGuiTableFlags.Resizable))
        {
            ImGui.TableSetupColumn("List", ImGuiTableColumnFlags.WidthFixed, 200);
            ImGui.TableSetupColumn("Editor", ImGuiTableColumnFlags.WidthStretch);

            ImGui.TableNextRow();

            // Left column - Node list
            ImGui.TableSetColumnIndex(0);
            DrawNodeInfoList();

            // Right column - Node editor
            ImGui.TableSetColumnIndex(1);
            DrawNodeInfoEditor();

            ImGui.EndTable();
        }
    }

    private void DrawNodeInfoList()
    {
        ImGui.Text("Node List");
        ImGui.Separator();

        if (ImGui.BeginChild("NodeInfoList", new Vector2(-1, -1), true))
        {
            for (int i = 0; i < editingRoute!.NodeInfo.Count; i++)
            {
                var nodeInfo = editingRoute.NodeInfo[i];
                bool isSelected = selectedNodeInfoIndex == i;

                string displayText = nodeInfo.NodeId == 0 ? $"Node {i + 1} (New)" : $"Node {nodeInfo.NodeId}";

                if (ImGui.Selectable($"{displayText}##Node{i}", isSelected))
                {
                    selectedNodeInfoIndex = i;
                    editingNodeInfo = CloneNodeInfo(nodeInfo);
                }

                // Context menu for deletion
                if (ImGui.BeginPopupContextItem($"NodeContext{i}"))
                {
                    if (ImGui.MenuItem("Delete"))
                    {
                        editingRoute.NodeInfo.RemoveAt(i);
                        if (selectedNodeInfoIndex == i)
                        {
                            selectedNodeInfoIndex = -1;
                            editingNodeInfo = null;
                        }
                        else if (selectedNodeInfoIndex > i)
                        {
                            selectedNodeInfoIndex--;
                        }
                    }
                    ImGui.EndPopup();
                }
            }

            ImGui.EndChild();
        }
    }

    private void DrawNodeInfoEditor()
    {
        if (selectedNodeInfoIndex == -1 || editingNodeInfo == null)
        {
            ImGui.Text("Select a node to edit its details");
            return;
        }

        ImGui.Text($"Editing Node Details");
        ImGui.Separator();

        // Save/Cancel buttons for node editing
        if (ImGui.Button("Save Node"))
        {
            editingRoute!.NodeInfo[selectedNodeInfoIndex] = CloneNodeInfo(editingNodeInfo);
        }

        ImGui.SameLine();

        if (ImGui.Button("Cancel"))
        {
            editingNodeInfo = CloneNodeInfo(editingRoute!.NodeInfo[selectedNodeInfoIndex]);
        }

        ImGui.Separator();

        // Node ID
        int nodeId = (int)editingNodeInfo.NodeId;
        if (ImGui.InputInt("Node ID", ref nodeId))
        {
            if (nodeId >= 0)
                editingNodeInfo.NodeId = (uint)nodeId;
        }

        // Node Position
        Vector3 nodePosition = editingNodeInfo.NodePosition;
        if (ImGui.InputFloat3("Node Position", ref nodePosition))
        {
            editingNodeInfo.NodePosition = nodePosition;
        }

        // Land Zone
        Vector3 landZone = editingNodeInfo.LandZone;
        if (ImGui.InputFloat3("Land Zone", ref landZone))
        {
            editingNodeInfo.LandZone = landZone;
        }

        // Radial Positioning
        bool radialPositioning = editingNodeInfo.RadialPositioning;
        if (ImGui.Checkbox("Radial Positioning", ref radialPositioning))
        {
            editingNodeInfo.RadialPositioning = radialPositioning;
        }

        // Show radius and angle controls only if radial positioning is enabled
        if (editingNodeInfo.RadialPositioning)
        {
            // Inner Radius
            float innerRadius = editingNodeInfo.InnerRadius;
            if (ImGui.InputFloat("Inner Radius", ref innerRadius))
            {
                editingNodeInfo.InnerRadius = Math.Max(0.0f, innerRadius);
            }

            // Outer Radius
            float outerRadius = editingNodeInfo.OuterRadius;
            if (ImGui.InputFloat("Outer Radius", ref outerRadius))
            {
                editingNodeInfo.OuterRadius = Math.Max(editingNodeInfo.InnerRadius, outerRadius);
            }

            // Start Angle
            float startAngle = editingNodeInfo.StartAngle;
            if (ImGui.SliderFloat("Start Angle", ref startAngle, 0.0f, 360.0f))
            {
                editingNodeInfo.StartAngle = startAngle;
            }

            // End Angle
            float endAngle = editingNodeInfo.EndAngle;
            if (ImGui.SliderFloat("End Angle", ref endAngle, 0.0f, 360.0f))
            {
                editingNodeInfo.EndAngle = endAngle;
            }

            // Rotation Offset
            float rotationOffset = editingNodeInfo.RotationOffset;
            if (ImGui.SliderFloat("Rotation Offset", ref rotationOffset, 0.0f, 360.0f))
            {
                editingNodeInfo.RotationOffset = rotationOffset;
            }
        }
    }

    private GatherClasses.RouteData CloneNodeInfo(GatherClasses.RouteData original)
    {
        return new GatherClasses.RouteData
        {
            NodeId = original.NodeId,
            NodePosition = original.NodePosition,
            LandZone = original.LandZone,
            RadialPositioning = original.RadialPositioning,
            InnerRadius = original.InnerRadius,
            OuterRadius = original.OuterRadius,
            StartAngle = original.StartAngle,
            EndAngle = original.EndAngle,
            RotationOffset = original.RotationOffset
        };
    }

    private void SelectRoute(RouteConfig config)
    {
        selectedRoute = config;

        // Create a copy for editing
        editingRoute = new RouteConfig
        {
            RouteId = config.RouteId,
            ZoneName = config.ZoneName,
            ZoneId = config.ZoneId,
            ExpansionName = config.ExpansionName,
            ExpansionId = config.ExpansionId,
            MapCenter = config.MapCenter,
            MapRadius = config.MapRadius,
            GatheringType = config.GatheringType,
            NodeIds = new HashSet<uint>(config.NodeIds),
            Items = new Dictionary<uint, string>(config.Items),
            ItemIds = new HashSet<uint>(config.ItemIds),
            NodeInfo = config.NodeInfo.Select(n => CloneNodeInfo(n)).ToList()
        };

        // Store original values for move detection
        originalZoneName = config.ZoneName;
        originalZoneId = config.ZoneId;
        originalExpansionName = config.ExpansionName;

        // Reset node info editing state
        selectedNodeInfoIndex = -1;
        editingNodeInfo = null;
    }

    private void CreateNewRoute()
    {
        var newRoute = new RouteConfig
        {
            RouteId = GetNextAvailableRouteId(),
            ZoneName = "New Zone",
            ZoneId = 0,
            ExpansionName = "Unknown",
            ExpansionId = 0,
            MapCenter = Vector2.Zero,
            MapRadius = 0,
            GatheringType = 0,
            NodeIds = new HashSet<uint>(),
            Items = new Dictionary<uint, string>(),
            ItemIds = new HashSet<uint>(),
            NodeInfo = new List<GatherClasses.RouteData>()
        };

        SelectRoute(newRoute);
    }

    private uint GetNextAvailableRouteId()
    {
        if (availableRoutes.Count == 0) return 1;
        return availableRoutes.Max(r => r.Config.RouteId) + 1;
    }

    private void SaveCurrentRoute()
    {
        if (editingRoute == null) return;

        try
        {
            // Check if we need to move the file (zone/expansion changed)
            bool needsMove = originalZoneName != editingRoute.ZoneName ||
                           originalZoneId != editingRoute.ZoneId ||
                           originalExpansionName != editingRoute.ExpansionName;

            if (needsMove)
            {
                RouteConfigManager.MoveRouteConfig(editingRoute, originalZoneName, originalZoneId, originalExpansionName);
            }
            else
            {
                RouteConfigManager.SaveRouteConfig(editingRoute);
            }

            // Update the selected route with our changes
            if (selectedRoute != null)
            {
                selectedRoute.RouteId = editingRoute.RouteId;
                selectedRoute.ZoneName = editingRoute.ZoneName;
                selectedRoute.ZoneId = editingRoute.ZoneId;
                selectedRoute.ExpansionName = editingRoute.ExpansionName;
                selectedRoute.ExpansionId = editingRoute.ExpansionId;
                selectedRoute.MapCenter = editingRoute.MapCenter;
                selectedRoute.MapRadius = editingRoute.MapRadius;
                selectedRoute.GatheringType = editingRoute.GatheringType;
                selectedRoute.NodeIds = new HashSet<uint>(editingRoute.NodeIds);
                selectedRoute.Items = new Dictionary<uint, string>(editingRoute.Items);
                selectedRoute.ItemIds = new HashSet<uint>(editingRoute.ItemIds);
                selectedRoute.NodeInfo = editingRoute.NodeInfo.Select(n => CloneNodeInfo(n)).ToList();
            }

            // Update original values
            originalZoneName = editingRoute.ZoneName;
            originalZoneId = editingRoute.ZoneId;
            originalExpansionName = editingRoute.ExpansionName;

            RefreshRouteList();
            PluginLog.Information("Route saved successfully!");
        }
        catch (Exception ex)
        {
            PluginLog.Error($"Failed to save route: {ex.Message}");
        }
    }

    private void CancelChanges()
    {
        if (selectedRoute != null)
        {
            SelectRoute(selectedRoute); // This will reload the editing copy
        }
    }

    private string ExportRouteInfo()
    {
        if (editingRoute == null) return "";

        var sb = new StringBuilder();

        // Basic properties
        sb.AppendLine($"\t\t public override string Id => \"{editingRoute.RouteId}\";");
        sb.AppendLine($"\t\t public override uint ExpansionId => {editingRoute.ExpansionId};");
        sb.AppendLine($"\t\t public override uint ZoneId => {editingRoute.ZoneId};");
        sb.AppendLine($"\t\t public override uint GatheringType => {editingRoute.GatheringType};");
        sb.AppendLine($"\t\t public override Vector2 MapPosition => new Vector2({editingRoute.MapCenter.X}f, {editingRoute.MapCenter.Y}f);");
        sb.AppendLine($"\t\t public override int Radius => {editingRoute.MapRadius};");
        sb.AppendLine($"\t\t public ovveride uint GatherType => {editingRoute.GatheringType};");
        sb.AppendLine();

        // NodeIds
        sb.AppendLine("\t\tpublic override HashSet<uint> NodeIds => new()");
        sb.AppendLine("\t\t{");
        foreach (var nodeId in editingRoute.NodeIds.OrderBy(x => x))
        {
            sb.AppendLine($"            {nodeId},");
        }
        sb.AppendLine("\t\t};");
        sb.AppendLine();

        // ItemIds
        sb.AppendLine("\t\tpublic override HashSet<uint> ItemIds => new()");
        sb.AppendLine("\t\t{");
        foreach (var itemId in editingRoute.ItemIds.OrderBy(x => x))
        {
            sb.AppendLine($"            {itemId},");
        }
        sb.AppendLine("\t\t};");
        sb.AppendLine();

        // NodeInfo
        sb.AppendLine("\t\tpublic override List<NodeInfo> Nodes => new()");
        sb.AppendLine("\t\t{");
        foreach (var nodeInfo in editingRoute.NodeInfo)
        {
            sb.AppendLine("\t\t\t new NodeInfo");
            sb.AppendLine("            {");
            sb.AppendLine($"\t\t\t NodeId = {nodeInfo.NodeId},");
            sb.AppendLine($"\t\t\t NodePosition = new Vector3({nodeInfo.NodePosition.X}f, {nodeInfo.NodePosition.Y}f, {nodeInfo.NodePosition.Z}f),");
            sb.AppendLine($"\t\t\t LandZone = new Vector3({nodeInfo.LandZone.X}f, {nodeInfo.LandZone.Y}f, {nodeInfo.LandZone.Z}f),");
            sb.AppendLine($"\t\t\t RadialPositioning = {nodeInfo.RadialPositioning},");
            sb.AppendLine($"\t\t\t InnerRadius = {nodeInfo.InnerRadius}f,");
            sb.AppendLine($"\t\t\t OuterRadius = {nodeInfo.OuterRadius}f,");
            sb.AppendLine($"\t\t\t StartAngle = {nodeInfo.StartAngle}f,");
            sb.AppendLine($"\t\t\t EndAngle = {nodeInfo.EndAngle}f,");
            sb.AppendLine($"\t\t\t RotationOffset = {nodeInfo.RotationOffset}f");
            sb.AppendLine("\t\t },");
        }
        sb.AppendLine("};");

        return sb.ToString();
    }
}