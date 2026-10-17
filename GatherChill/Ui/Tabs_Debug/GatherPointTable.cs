using GatherChill.Utilities.GatheringHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui.Tabs_Debug;

internal class GatherPointTable
{
    private static uint _NodeIdSearch = 0;

    public static void Draw()
    {
        if (ImGui.BeginTable("GatherPointTable", 8, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.Resizable | ImGuiTableFlags.ScrollY | ImGuiTableFlags.Sortable | ImGuiTableFlags.SizingFixedFit))
        {
            // Setup columns
            ImGui.TableSetupColumn("ID");
            ImGui.TableSetupColumn("Node IDs");
            ImGui.TableSetupColumn("Type");
            ImGui.TableSetupColumn("Level");
            ImGui.TableSetupColumn("Zone Name");
            ImGui.TableSetupColumn("Place Name");
            ImGui.TableSetupColumn("Expansion");
            ImGui.TableSetupColumn("Items");
            ImGui.TableSetupScrollFreeze(0, 2);
            ImGui.TableHeadersRow();

            // Get sorting specs
            var sortSpecs = ImGui.TableGetSortSpecs();
            IEnumerable<KeyValuePair<uint, GatherPointInfo>> sortedData = Sheet_RouteInfo;

            if (sortSpecs.SpecsCount > 0)
            {
                var spec = sortSpecs.Specs;
                sortedData = spec.ColumnIndex switch
                {
                    0 => spec.SortDirection == ImGuiSortDirection.Ascending
                        ? Sheet_RouteInfo.OrderBy(x => x.Key)
                        : Sheet_RouteInfo.OrderByDescending(x => x.Key),
                    1 => spec.SortDirection == ImGuiSortDirection.Ascending
                        ? Sheet_RouteInfo.OrderBy(x => x.Value.NodeIds?.FirstOrDefault() ?? 0)
                        : Sheet_RouteInfo.OrderByDescending(x => x.Value.NodeIds?.FirstOrDefault() ?? 0),
                    2 => spec.SortDirection == ImGuiSortDirection.Ascending
                        ? Sheet_RouteInfo.OrderBy(x => x.Value.Job)
                        : Sheet_RouteInfo.OrderByDescending(x => x.Value.Job),
                    3 => spec.SortDirection == ImGuiSortDirection.Ascending
                        ? Sheet_RouteInfo.OrderBy(x => x.Value.Level)
                        : Sheet_RouteInfo.OrderByDescending(x => x.Value.Level),
                    4 => spec.SortDirection == ImGuiSortDirection.Ascending
                        ? Sheet_RouteInfo.OrderBy(x => x.Value.ZoneName)
                        : Sheet_RouteInfo.OrderByDescending(x => x.Value.ZoneName),
                    5 => spec.SortDirection == ImGuiSortDirection.Ascending
                        ? Sheet_RouteInfo.OrderBy(x => x.Value.PlaceName)
                        : Sheet_RouteInfo.OrderByDescending(x => x.Value.PlaceName),
                    6 => spec.SortDirection == ImGuiSortDirection.Ascending
                        ? Sheet_RouteInfo.OrderBy(x => x.Value.ExpId)
                        : Sheet_RouteInfo.OrderByDescending(x => x.Value.ExpId),
                    _ => Sheet_RouteInfo
                };
            }

            ImGui.TableNextRow();
            ImGui.TableSetColumnIndex(1);
            ImGui.InputUInt("###NodeIdSearch", ref _NodeIdSearch);

            // Draw rows
            foreach (var kvp in sortedData)
            {
                if (_NodeIdSearch != 0 && !kvp.Value.NodeIds.Contains(_NodeIdSearch))
                    continue;

                ImGui.TableNextRow();

                ImGui.TableNextColumn();
                ImGui.Text(kvp.Key.ToString());

                ImGui.TableNextColumn();
                // Display all NodeIds as comma-separated list
                if (kvp.Value.NodeIds != null && kvp.Value.NodeIds.Count > 0)
                {
                    ImGui.Text(string.Join(", ", kvp.Value.NodeIds));
                }
                else
                {
                    ImGui.Text("");
                }

                ImGui.TableNextColumn();
                ImGui.Text(kvp.Value.Job.ToString());

                ImGui.TableNextColumn();
                ImGui.Text(kvp.Value.Level.ToString());

                ImGui.TableNextColumn();
                ImGui.Text(kvp.Value.ZoneName ?? "");

                ImGui.TableNextColumn();
                ImGui.Text(kvp.Value.PlaceName ?? "");

                ImGui.TableNextColumn();
                ImGui.TextDisabled($"{kvp.Value.ExpId}");
                ImGui.SameLine();
                if (Gather_Util.Sheet_Expansion.TryGetValue(kvp.Value.ExpId, out var expansionInfo))
                {
                    ImGui.Text($"{expansionInfo.Name}");
                }

                ImGui.TableNextColumn();
                if (kvp.Value.ItemIds != null && kvp.Value.ItemIds.Count > 0)
                {
                    ImGui.Text(string.Join(", ", kvp.Value.ItemIds));
                }
                else
                {
                    ImGui.Text("");
                }
            }

            ImGui.EndTable();
        }
    }
}
