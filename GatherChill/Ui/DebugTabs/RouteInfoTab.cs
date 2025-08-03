using GatherChill.Utilities.GatheringHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.Ui.DebugTabs
{
    internal class RouteInfoTab
    {
        public static void Draw()
        {
            if (ImGui.BeginTable("New Route Information Table", 7, ImGuiTableFlags.Borders | ImGuiTableFlags.SizingFixedFit))
            {
                ImGui.TableSetupColumn("#");
                ImGui.TableSetupColumn("Type");
                ImGui.TableSetupColumn("Expansion");
                ImGui.TableSetupColumn("Zone");
                ImGui.TableSetupColumn("Position");
                ImGui.TableSetupColumn("NodeIds");
                ImGui.TableSetupColumn("Items");

                ImGui.TableHeadersRow();

                foreach (var entry in GatherClasses.RouteDatabase)
                {
                    ImGui.TableNextRow();

                    // 1st Column | ID #
                    ImGui.TableSetColumnIndex(0);
                    ImGui.Text(entry.Key.ToString());

                    // 2nd Column | Type
                    ImGui.TableNextColumn();
                    var image = GatherClasses.GatheringTypeIcons[entry.Value.GatheringType].MainIcon;
                    ImGui.Image(image.GetWrapOrEmpty().ImGuiHandle, new Vector2(25, 25));

                    // Expansion
                    ImGui.TableNextColumn();
                    ImGui.Text(entry.Value.ExpansionName);

                    // Zone
                    ImGui.TableNextColumn();
                    ImGui.Text(entry.Value.ZoneName);

                    // Column 3 | Map Location
                    ImGui.TableNextColumn();
                    if (ImGui.Button($"Map {entry.Value.MapCenter.X} | {entry.Value.MapCenter.Y}"))
                    {
                        Svc.Chat.Print($"Map Location: {entry.Value.MapCenter.X} | {entry.Value.MapCenter.Y}");
                    }
                    if (ImGui.IsItemHovered())
                    {
                        ImGui.BeginTooltip();
                        ImGui.Text($"Radius: {entry.Value.MapRadius}");
                        ImGui.EndTooltip();
                    }

                    // Column 4 | NodeIds - Hover for popup
                    ImGui.TableNextColumn();
                    string nodeList = string.Join(", ", entry.Value.NodeIds);
                    ImGui.Text($"NodeIds ({entry.Value.NodeIds.Count})");

                    if (ImGui.IsItemHovered())
                    {
                        ImGui.BeginTooltip();
                        if (ImGui.BeginTable($"NodeIds_Tooltip_{entry.Key}", 2, ImGuiTableFlags.Borders))
                        {
                            ImGui.TableSetupColumn("Index");
                            ImGui.TableSetupColumn("NodeId");
                            ImGui.TableHeadersRow();

                            int index = 0;
                            foreach (var nodeId in entry.Value.NodeIds)
                            {
                                ImGui.TableNextRow();
                                ImGui.TableSetColumnIndex(0);
                                ImGui.Text(index.ToString());
                                ImGui.TableNextColumn();
                                ImGui.Text(nodeId.ToString());
                                index++;
                            }
                            ImGui.EndTable();
                        }
                        ImGui.EndTooltip();
                    }

                    // Column 5 | Items
                    ImGui.TableNextColumn();
                    string itemsText = "No Items";
                    if (entry.Value.Items.Count > 1)
                    {
                        itemsText = string.Join(", ", entry.Value.Items.Values);
                    }
                    else if (entry.Value.Items.Count > 0)
                    {
                        itemsText = entry.Value.Items.Values.First().Trim();
                    }
                    ImGui.Text(itemsText);
                    string itemIdList = string.Join(", ", entry.Value.ItemIds.ToArray());
                    ImGuiEx.HelpMarker(itemIdList);
                }

                ImGui.EndTable();
            }
        }
    }
}
