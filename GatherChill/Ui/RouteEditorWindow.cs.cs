// First, let's add some helper methods to RouteConfigManager for editing operations
// Add these methods to your existing RouteConfigManager.cs file:

using Dalamud.Interface.Colors;
using Dalamud.Interface.Utility.Raii;
using ECommons.Logging;
using GatherChill.Yaml.ConfigFiles;
using System.Collections.Generic;
using System.IO;

namespace GatherChill.Yaml;

public static partial class RouteConfigManager
{
    // Add these methods to your existing RouteConfigManager class:

    public static void SaveRouteConfig(RouteConfig config)
    {
        string filePath = config.GetDefaultPath();
        config.Save(filePath);
        PluginLog.Information($"Saved route config: {Path.GetFileName(filePath)}");
    }

    public static bool MoveRouteConfig(RouteConfig config, string oldZoneName, uint oldZoneId, string oldExpansionName)
    {
        try
        {
            // Get old and new paths
            string oldPath = RouteConfig.GetRoutePath(config.RouteId, oldZoneId, oldZoneName, oldExpansionName);
            string newPath = config.GetDefaultPath();

            // If paths are the same, no need to move
            if (oldPath == newPath)
                return true;

            // Create new directory structure
            Directory.CreateDirectory(Path.GetDirectoryName(newPath)!);

            // Move the file if old one exists
            if (File.Exists(oldPath))
            {
                File.Move(oldPath, newPath);
                PluginLog.Information($"Moved route config from {oldPath} to {newPath}");

                // Clean up empty directories
                CleanupEmptyDirectories(Path.GetDirectoryName(oldPath));
            }
            else
            {
                // Just save to new location
                config.Save(newPath);
                PluginLog.Information($"Saved route config to new location: {newPath}");
            }

            return true;
        }
        catch (Exception ex)
        {
            PluginLog.Error($"Failed to move route config: {ex.Message}");
            return false;
        }
    }

    public static void DeleteRouteConfig(RouteConfig config)
    {
        try
        {
            string filePath = config.GetDefaultPath();
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                PluginLog.Information($"Deleted route config: {Path.GetFileName(filePath)}");

                // Clean up empty directories
                CleanupEmptyDirectories(Path.GetDirectoryName(filePath));
            }
        }
        catch (Exception ex)
        {
            PluginLog.Error($"Failed to delete route config: {ex.Message}");
        }
    }

    private static void CleanupEmptyDirectories(string? directoryPath)
    {
        if (string.IsNullOrEmpty(directoryPath) || !Directory.Exists(directoryPath))
            return;

        try
        {
            // Don't delete the root Routes directory
            if (Path.GetFileName(directoryPath).Equals("Routes", StringComparison.OrdinalIgnoreCase))
                return;

            // If directory is empty, delete it and check parent
            if (!Directory.EnumerateFileSystemEntries(directoryPath).Any())
            {
                Directory.Delete(directoryPath);
                PluginLog.Debug($"Removed empty directory: {directoryPath}");

                // Recursively clean parent directories
                CleanupEmptyDirectories(Path.GetDirectoryName(directoryPath));
            }
        }
        catch (Exception ex)
        {
            PluginLog.Warning($"Failed to cleanup directory {directoryPath}: {ex.Message}");
        }
    }

    public static List<(string Display, RouteConfig Config)> GetAllRoutesForSelection()
    {
        var routes = new List<(string Display, RouteConfig Config)>();
        var allConfigs = LoadAllRouteConfigs();

        foreach (var config in allConfigs.OrderBy(c => c.ExpansionName).ThenBy(c => c.ZoneName).ThenBy(c => c.RouteId))
        {
            string display = $"[{config.ExpansionName}] {config.ZoneName} - Route {config.RouteId}";
            routes.Add((display, config));
        }

        return routes;
    }
}

// Now let's create the Route Editor Window
// Create this as a new file: GatherChill/Ui/RouteEditorWindow.cs

using Dalamud.Interface.Colors;
using Dalamud.Interface.Utility.Raii;
using ECommons.Logging;
using GatherChill.Yaml;
using GatherChill.Yaml.ConfigFiles;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

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

    public RouteEditorWindow() :
        base($"Route Editor {P.GetType().Assembly.GetName().Version} ###RouteEditor")
    {
        Flags = ImGuiWindowFlags.None;
        SizeConstraints = new()
        {
            MinimumSize = new Vector2(800, 600),
            MaximumSize = new Vector2(1200, 1000),
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

        ImGui.Separator();

        // Basic route info
        DrawBasicRouteInfo();

        ImGui.Separator();

        // Map info
        DrawMapInfo();

        ImGui.Separator();

        // Node IDs section
        DrawNodeIdsSection();

        ImGui.Separator();

        // Items section
        DrawItemsSection();
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
        if (ImGui.CollapsingHeader("Node IDs"))
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
    }

    private void DrawItemsSection()
    {
        if (ImGui.CollapsingHeader("Items"))
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
            ItemIds = new HashSet<uint>(config.ItemIds)
        };

        // Store original values for move detection
        originalZoneName = config.ZoneName;
        originalZoneId = config.ZoneId;
        originalExpansionName = config.ExpansionName;
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
            ItemIds = new HashSet<uint>()
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
}