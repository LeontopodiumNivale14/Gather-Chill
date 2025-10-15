using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GatherChill.GatheringInfo;
using Lumina.Excel.Sheets;

namespace GatherChill.Ui.DebugTabs
{
    internal class RouteEditorV2
    {
        private static uint? selectedExpansionId = null;
        private static uint selectedZoneId = 0;
        private static uint selectedRouteId = 0;

        private static RouteData routeData = new RouteData();
        private static bool routesLoaded = false;

        // Display names for expansions (optional - makes the UI nicer)
        private static readonly Dictionary<uint, string> ExpansionNames = new()
        {
            [0] = "A Realm Reborn",
            [1] = "Heavensward",
            [2] = "Stormblood",
            [3] = "Shadowbringers",
            [4] = "Endwalker",
            [5] = "Dawntrail"
        };

        public static void Draw()
        {
            // Load routes on first draw
            if (!routesLoaded)
            {
                routeData.LoadAllRoutes();
                routesLoaded = true;
            }

            if (ImGui.BeginTable("Gathering Route Editor V2", 2, ImGuiTableFlags.Resizable | ImGuiTableFlags.SizingFixedFit))
            {
                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);

                if (ImGui.BeginChild("Gathering Route Editor - Route Selector", new Vector2(400, 0), true))
                {
                    ImGui.Text("Route Selection");
                    ImGui.Separator();

                    // Expansion Dropdown
                    DrawExpansionDropdown();

                    // Zone Dropdown (only show if expansion selected)
                    if (selectedExpansionId.HasValue && routeData.GatheringInfo.ContainsKey(selectedExpansionId.Value))
                    {
                        DrawZoneDropdown();
                    }

                    ImGui.Separator();

                    // Route List (only show if zone selected)
                    if (selectedZoneId > 0 &&
                        routeData.GatheringInfo.ContainsKey(selectedExpansionId.Value) &&
                        routeData.GatheringInfo[selectedExpansionId.Value].ContainsKey(selectedZoneId))
                    {
                        DrawRouteList();
                    }
                }
                ImGui.EndChild();

                ImGui.TableSetColumnIndex(1);
                if (ImGui.BeginChild("Gathering Route Editor - Editing Route", new Vector2(0, 0), true))
                {
                    if (selectedRouteId > 0 &&
                        routeData.GatheringInfo.ContainsKey(selectedExpansionId.Value) &&
                        routeData.GatheringInfo[selectedExpansionId.Value].ContainsKey(selectedZoneId) &&
                        routeData.GatheringInfo[selectedExpansionId.Value][selectedZoneId].ContainsKey(selectedRouteId))
                    {
                        var route = routeData.GatheringInfo[selectedExpansionId.Value][selectedZoneId][selectedRouteId];
                        DrawRouteEditor(route);
                    }
                    else
                    {
                        ImGui.Text("Select a route to edit");
                    }
                }
                ImGui.EndChild();

                ImGui.EndTable();
            }
        }
        private static void DrawExpansionDropdown()
        {
            ImGui.Text("Expansion:");
            ImGui.SetNextItemWidth(-1);

            // Get current expansion name for display
            string currentExpansion = !selectedExpansionId.HasValue ? "Select Expansion..." :   // <-- Change this
                                     ExpansionNames.ContainsKey(selectedExpansionId.Value) ?
                                     ExpansionNames[selectedExpansionId.Value] :
                                     $"Expansion {selectedExpansionId.Value}";

            if (ImGui.BeginCombo("##ExpansionCombo", currentExpansion))
            {
                foreach (var expansionId in routeData.GatheringInfo.Keys.OrderBy(x => x))
                {
                    string expansionName = ExpansionNames.ContainsKey(expansionId) ?
                                          ExpansionNames[expansionId] :
                                          $"Expansion {expansionId}";

                    bool isSelected = selectedExpansionId.HasValue && selectedExpansionId.Value == expansionId;  // <-- Change this

                    if (ImGui.Selectable(expansionName, isSelected))
                    {
                        selectedExpansionId = expansionId;  // This works fine
                        selectedZoneId = 0; // Reset zone selection
                        selectedRouteId = 0; // Reset route selection
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

            string currentZone = selectedZoneId == 0 ? "Select Zone..." : $"{TerritoryName(selectedZoneId)} ({selectedZoneId})";  // <-- Change this line

            if (ImGui.BeginCombo("##ZoneCombo", currentZone))
            {
                var zones = routeData.GatheringInfo[selectedExpansionId.Value];

                foreach (var zoneId in zones.Keys.OrderBy(x => x))
                {
                    bool isSelected = selectedZoneId == zoneId;
                    string zoneName = $"{TerritoryName(zoneId)} ({zoneId})";  // <-- And this line

                    if (ImGui.Selectable(zoneName, isSelected))
                    {
                        selectedZoneId = zoneId;
                        selectedRouteId = 0; // Reset route selection
                    }

                    if (isSelected)
                        ImGui.SetItemDefaultFocus();
                }

                ImGui.EndCombo();
            }
        }
        private static string TerritoryName(uint id)
        {
            if (Svc.Data.GetExcelSheet<TerritoryType>().TryGetRow(id, out var row))
            {
                if (row.PlaceName.RowId != 0)
                {
                    return Svc.Data.GetExcelSheet<PlaceName>().GetRow(row.PlaceName.RowId).Name.ToString();
                }
                else
                {
                    return id.ToString();
                }
            }
            else
            {
                return id.ToString();
            }
        }
        private static void DrawRouteList()
        {
            ImGui.Text("Routes:");
            ImGui.Separator();

            if (ImGui.BeginChild("RouteListChild", new Vector2(-1, -1), true))
            {
                var routes = routeData.GatheringInfo[selectedExpansionId.Value][selectedZoneId];

                foreach (var (routeId, route) in routes.OrderBy(x => x.Key))
                {
                    bool isSelected = selectedRouteId == routeId;

                    // Determine route type for display
                    string routeType = (route.GatherType == 0 || route.GatherType == 1) ? "MIN" : "BTN";
                    string displayName = $"{routeType}_{routeId}";

                    if (ImGui.Selectable(displayName, isSelected))
                    {
                        selectedRouteId = routeId;
                    }

                    // Tooltip with more info
                    if (ImGui.IsItemHovered())
                    {
                        ImGui.BeginTooltip();
                        ImGui.Text($"Route ID: {routeId}");
                        ImGui.Text($"Type: {routeType}");
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
            ImGui.Text($"Editing Route: {route.Id}");
            ImGui.Separator();

            // Display route information
            ImGui.Text($"Expansion ID: {route.ExpansionId}");
            ImGui.Text($"Zone ID: {route.ZoneId}");
            ImGui.Text($"Gather Type: {route.GatherType}");
            ImGui.Text($"Map Position: {route.MapPosition.X}, {route.MapPosition.Y}");
            ImGui.Text($"Radius: {route.Radius}");

            ImGui.Separator();

            if (ImGui.CollapsingHeader("Node IDs", ImGuiTreeNodeFlags.DefaultOpen))
            {
                foreach (var nodeId in route.NodeIds)
                {
                    ImGui.BulletText(nodeId.ToString());
                }
            }

            if (ImGui.CollapsingHeader("Item IDs"))
            {
                foreach (var itemId in route.ItemIds)
                {
                    ImGui.BulletText(itemId.ToString());
                }
            }

            if (ImGui.CollapsingHeader("Node Details"))
            {
                foreach (var node in route.Nodes)
                {
                    ImGui.Text($"Node {node.NodeId}:");
                    ImGui.Indent();
                    ImGui.Text($"Position: {node.NodePosition}");
                    ImGui.Text($"Landing: {node.LandingInfo.LandZone}");
                    ImGui.Unindent();
                    ImGui.Separator();
                }
            }
        }
    }
}