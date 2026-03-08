using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Interface.Utility.Raii;
using GatherChill.GatheringInfo;
using GatherChill.Utilities;
using System.Collections.Generic;

namespace GatherChill.Ui.RouteWindowTabs
{
    internal class Route_Editor
    {
        public static uint SelectedRoute = 0;

        private static uint selectedNodeId = 0;
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

        private static void NodeDetails(GatheringRoute routeInfo)
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
                        {
                            P.routeEditor.AddNodeLocationIfMissing(routeInfo, node.BaseId, node.Position);
                        }
                    }
                    foreach (var nodeId in routeInfo.NodeIds)
                    {
                        ImGui.Text($"{nodeId}");
                    }
                }
                ImGui.EndChild();

                ImGui.TableNextColumn();
                if (ImGui.BeginTable("Node Info | Groups", 3, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingFixedFit))
                {
                    ImGui.TableSetupColumn("Node Id");
                    ImGui.TableSetupColumn("Count");
                    ImGui.TableSetupColumn("Group");

                    ImGui.TableHeadersRow();

                    foreach (var nodeGroup in routeInfo.NodeInfo.OrderBy(x => x.GroupId))
                    {
                        if (selectedNodeId == 0)
                            selectedNodeId = nodeGroup.NodeId;

                        ImGui.PushID($"{nodeGroup.NodeId}");

                        ImGui.TableNextRow();
                        ImGui.TableSetColumnIndex(0);
                        if (ImGui.Button($"{nodeGroup.NodeId}"))
                        {
                            selectedNodeId = nodeGroup.NodeId;
                        }

                        ImGui.TableNextColumn();
                        ImGui_Util.Table_VertCenterText($"{nodeGroup.Locations.Count()}");

                        ImGui.TableNextColumn();
                        List<int> groupIds = new() { 0, 1, 2, 3, 4, 5 };
                        var currentGroup = nodeGroup.GroupId;
                        ImGui.SetNextItemWidth(100);
                        if (ImGui.BeginCombo("##nodeGroup", $"{currentGroup}"))
                        {
                            foreach (var group in groupIds)
                            {
                                bool isSelected = group == currentGroup;
                                if (ImGui.Selectable($"Group {group}", isSelected))
                                {
                                    nodeGroup.GroupId = group;
                                }
                                if (isSelected)
                                    ImGui.SetItemDefaultFocus();
                            }
                            ImGui.EndCombo();
                        }

                        ImGui.PopID();
                    }

                    ImGui.EndTable();
                }

                #endregion

                #region Location Editor

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                if (selectedNodeId != 0)
                {

                }

                #endregion

                ImGui.EndTable();
            }
        }
    }
}