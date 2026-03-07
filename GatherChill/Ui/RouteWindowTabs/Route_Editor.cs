using Dalamud.Interface.Utility.Raii;
using GatherChill.GatheringInfo;
using GatherChill.Utilities;

namespace GatherChill.Ui.RouteWindowTabs
{
    internal class Route_Editor
    {
        public static uint SelectedRoute = 0;

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
                string expansion = routeInfo.TerritoryId switch
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

                ImGui.EndTable();
            }
        }

        private static void ItemDetails(GatheringRoute routeInfo)
        {

        }

        private static void NodeDetails(GatheringRoute routeInfo)
        {

        }
    }
}