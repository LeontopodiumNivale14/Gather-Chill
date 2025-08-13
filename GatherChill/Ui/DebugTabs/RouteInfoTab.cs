using ECommons.Logging;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Yaml;
using GatherChill.Yaml.ConfigFiles;
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
            if (ImGui.Button("Create index file"))
            {
                // Clear existing data to rebuild fresh
                GIndex.Expansions.Clear();

                foreach (var entry in GatherClasses.GatheringDatabase)
                {
                    var data = entry.Value;

                    var routeInfo = new GatherIndex.RouteInfo
                    {
                        MapCenter = data.MapCenter,
                        MapRadius = data.MapRadius,
                        GatheringType = data.GatheringType,
                        NodeIds = data.NodeIds,
                    };

                    // Use gathering type as route key, or create a more meaningful identifier
                    var routeKey = entry.Key;
                }

                GIndex.Save();

                // Optional: Log how many entries were processed
                var totalRoutes = GIndex.Expansions.Values
                    .SelectMany(exp => exp.Zones.Values)
                    .SelectMany(zone => zone.Routes.Values)
                    .Count();
                PluginLog.Information($"Created index with {totalRoutes} routes across {GIndex.Expansions.Count} expansions");
            }

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

                foreach (var entry in GatherClasses.GatheringDatabase)
                {
                    ImGui.TableNextRow();

                    // 1st Column | ID #
                    ImGui.TableSetColumnIndex(0);
                    ImGui.Text(entry.Key.ToString());

                    // 2nd Column | Type
                    ImGui.TableNextColumn();
                    var image = GatherClasses.GatheringTypeIcons[entry.Value.GatheringType].MainIcon;
                    ImGui.Image(image.GetWrapOrEmpty().Handle, new Vector2(25, 25));

                    // Expansion (these next 2 are problematic... need to look into why later)
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
                    if (entry.Value.Items?.Count > 1)
                    {
                        var limitedItems = entry.Value.Items.Values.Take(10).Where(v => !string.IsNullOrEmpty(v));
                        itemsText = string.Join(", ", limitedItems);
                        if (entry.Value.Items.Count > 10)
                            itemsText += "...";
                    }
                    else if (entry.Value.Items?.Count > 0)
                    {
                        var firstItem = entry.Value.Items.Values.FirstOrDefault();
                        itemsText = firstItem?.Trim() ?? "Invalid Item";
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
