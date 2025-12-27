

using Dalamud.Interface.Utility.Raii;
using ECommons.GameHelpers;
using GatherChill.Ui.RouteWindowTabs;
using GatherChill.Utilities;
using Lumina.Excel.Sheets;

namespace GatherChill.Ui;

public class RoutesWindow : Window
{
    public RoutesWindow() : base("Gathering Routes###GatherChillRoutesWindow")
    {
        Flags = ImGuiWindowFlags.None;
        SizeConstraints = new()
        {
            MinimumSize = new(800, 600)
        };
        P.windowSystem.AddWindow(this);

        // tab system
        customTabs = new CustomTabs();
        routeEditorV2 = new EditRouteV2();
    }

    private readonly CustomTabs customTabs;
    private readonly EditRouteV2 routeEditorV2;

    private bool viewOnlyZone = false;
    private bool OrderByRouteId = false;

    public override void Draw()
    {
        var routeLoader = GatherChill.P.routeLoader;

        if (routeLoader.Routes.Count == 0)
        {
            ImGui.TextColored(new Vector4(1, 1, 0, 1), "No routes loaded.");
            ImGui.Text("Place .json route files in:");
            ImGui.Text(routeLoader.RoutesDirectory);

            if (ImGui.Button("Reload Routes"))
            {
                routeLoader.LoadAllRoutes();
            }
            return;
        }

        ImGui.Text($"Loaded Routes: {routeLoader.Routes.Count}");
        ImGui.Separator();

        customTabs.DrawCategoryButton("View All Routes", "editor_ViewAll");
        customTabs.DrawCategoryButton("View Selected Route V2", "editor_ViewSelectedV2");
        customTabs.DrawCategoryButton("View Selected Route", "editor_ViewSelected");
        customTabs.EndCategoryButtonRow();

        if (customTabs.CategoryStates["editor_ViewAll"])
        {
            if (ImGui.Button("Reload All Routes"))
            {
                routeLoader.LoadAllRoutes();
            }

            ImGui.SameLine();

            if (ImGui.Button("Open Routes Folder"))
            {
                System.Diagnostics.Process.Start("explorer.exe", routeLoader.RoutesDirectory);
            }
            ImGui.SameLine();
            if (ImGui.Button("Add missing from sheets"))
            {
                P.routeLoader.GenerateRoutesFromSheetInfo(SheetInfo);
            }

            ImGui.Checkbox("View current zone only", ref viewOnlyZone);
            ImGui.SameLine();
            ImGui.Checkbox("Sort by routeID", ref OrderByRouteId);

            ImGui.Separator();

            using (var All_RouteViewer = ImRaii.Child("All Route Viewer Window", Vector2.Zero, false))
            {
                if (!All_RouteViewer.Success)
                {
                    return;
                }
                else
                {
                    // Display routes in a table
                    if (ImGui.BeginTable("RoutesTable", 7, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.Resizable | ImGuiTableFlags.SizingFixedFit))
                    {
                        ImGui.TableSetupColumn("Route ID");
                        ImGui.TableSetupColumn("Zone");
                        ImGui.TableSetupColumn("Job");
                        ImGui.TableSetupColumn("Level");
                        ImGui.TableSetupColumn("Nodes");
                        ImGui.TableSetupColumn("Author");
                        ImGui.TableSetupColumn("Items");
                        ImGui.TableHeadersRow();

                        var tableInfo = routeLoader.Routes;
                        if (OrderByRouteId)
                        {
                            tableInfo = routeLoader.Routes.OrderBy(x => x.Key)
                                                           .ToDictionary(x => x.Key, x => x.Value);
                        }

                        foreach (var route in tableInfo.Values)
                        {
                            if (viewOnlyZone)
                            {
                                if (Player.Territory.RowId != route.TerritoryId)
                                    continue;
                            }

                            ImGui.TableNextRow();

                            if (route.RouteId == customTabs.selectedRoute)
                            {
                                ImGui.TableSetBgColor(ImGuiTableBgTarget.RowBg0, ImGui.GetColorU32(new Vector4(0.4f, 0.8f, 0.4f, 0.3f)));
                            }

                            ImGui.TableNextColumn();
                            if (ImGui.Button($"{route.RouteId}##Edit Route_{route.RouteId}"))
                            {
                                customTabs.selectedRoute = route.RouteId;
                                foreach (var tabInfo in customTabs.CategoryStates)
                                {
                                    customTabs.CategoryStates[tabInfo.Key] = false;
                                }
                                customTabs.CategoryStates["editor_ViewSelectedV2"] = true;
                            }

                            ImGui.TableNextColumn();
                            ImGui.Text(route.ZoneName);

                            ImGui.TableNextColumn();
                            var jobName = route.GatheringJobId switch
                            {
                                16 => "MIN",
                                17 => "BTN",
                                18 => "FSH",
                                _ => "???"
                            };
                            ImGui.Text(jobName);

                            ImGui.TableNextColumn();
                            ImGui.Text(route.LevelRequirement.ToString());

                            ImGui.TableNextColumn();
                            ImGui.Text(route.NodeIds.Count.ToString());

                            ImGui.TableNextColumn();
                            ImGui.Text(route.Author ?? "Unknown");

                            ImGui.TableNextColumn();
                            if (Utils.SheetInfo.TryGetValue(route.RouteId, out var sheetInfo))
                            {
                                foreach (var item in sheetInfo.ItemIds)
                                {
                                    if (Svc.Data.GetExcelSheet<Item>().TryGetRow(item, out var itemInfo))
                                    {
                                        if (Svc.Texture.TryGetFromGameIcon((int)itemInfo.Icon, out var icon))
                                        {
                                            ImGui.Image(icon.GetWrapOrEmpty().Handle, new Vector2(24, 24));
                                            if (ImGui.IsItemHovered())
                                            {
                                                ImGui.BeginTooltip();
                                                ImGui.Text($"Item Name: {itemInfo.Name}");
                                                ImGui.Text($"Item ID: {item}");
                                                ImGui.EndTooltip();
                                            }
                                            ImGui.SameLine();
                                        }
                                    }
                                }
                            }
                        }

                        ImGui.EndTable();
                    }
                }
            }
        }
        else if (customTabs.CategoryStates["editor_ViewSelectedV2"])
        {
            routeEditorV2.Draw(customTabs.selectedRoute);
        }
    }
}