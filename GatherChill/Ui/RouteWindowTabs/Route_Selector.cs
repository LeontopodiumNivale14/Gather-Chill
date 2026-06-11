using Dalamud.Interface.Utility.Raii;
using ECommons.GameHelpers;
using GatherChill.GatheringInfo;
using GatherChill.Scheduler;
using GatherChill.Utilities;
using GatherChill.Utilities.GatheringHelpers;
using Lumina.Excel.Sheets;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui.RouteWindowTabs
{
    internal class Route_Selector
    {
        public static bool Filter_Expansion = false;
        public static bool Filter_Zone = false;

        public static uint FilterExpansionId = 0;
        public static uint FilterTerritoryId = 0;
        public static uint FilterJob = 0;

        public static Dictionary<uint, string> Job_FilterOptions = new()
        {
            [0] = "All",
            [16] = "MIN",
            [17] = "BTN",
            [18] = "FSH",
        };
        

        public enum RouteSortMode
        {
            ByRouteId,
            BestPath
        }

        public static RouteSortMode CurrentSortMode = RouteSortMode.ByRouteId;

        public static void Draw()
        {
            ImGui.Checkbox("Filter by Expansion", ref Filter_Expansion);
            if (Filter_Expansion)
            {
                ImGui.SameLine();
                // Expansion names matching your ExpansionId values
                string[] expansionNames = ["ARR", "HW", "StB", "ShB", "EW", "DT"];
                int expansionIndex = (int)FilterExpansionId;
                ImGui.SetNextItemWidth(100);
                if (ImGui.Combo("##ExpansionFilter", ref expansionIndex, expansionNames, expansionNames.Length))
                    FilterExpansionId = (uint)expansionIndex;
            }

            ImGui.Checkbox("Filter by current Zone", ref Filter_Zone);

            var keys = Job_FilterOptions.Keys.ToArray();
            var labels = Job_FilterOptions.Values.ToArray();

            int currentIndex = Array.IndexOf(keys, FilterJob);
            if (currentIndex < 0) currentIndex = 0;

            ImGui.SetNextItemWidth(200);
            if (ImGui.Combo("##JobFilter", ref currentIndex, labels, labels.Length))
            {
                FilterJob = keys[currentIndex];
            }

            if (ImGui.RadioButton("Sort by ID", CurrentSortMode == RouteSortMode.ByRouteId))
                CurrentSortMode = RouteSortMode.ByRouteId;
            ImGui.SameLine();
            if (ImGui.RadioButton("Best Path", CurrentSortMode == RouteSortMode.BestPath))
                CurrentSortMode = RouteSortMode.BestPath;

            var size = ImGui.GetContentRegionAvail();
            if (ImGui.BeginChild("Child: Route Selector", size, true))
            {
                if (ImGui.BeginTable("Route Selector Table", 5, ImGuiTableFlags.RowBg | ImGuiTableFlags.Borders | ImGuiTableFlags.SizingFixedFit))
                {
                    ImGui.TableSetupColumn("Route ID");
                    ImGui.TableSetupColumn("Class");
                    ImGui.TableSetupColumn("Location");
                    ImGui.TableSetupColumn("Items");
                    ImGui.TableSetupColumn("Time Slots");

                    // Filtering out the pre-requisites of filtering first (Expansion + ZoneID if need be)
                    var filtered = P.routeEditor.Routes
                        .Where(x => !Filter_Expansion || x.Value.ExpansionId == FilterExpansionId)
                        .Where(x => !Filter_Zone || x.Value.TerritoryId == Player.Territory.RowId)
                        .Where(x => FilterJob == 0 || x.Value.GatheringJobId == FilterJob);

                    // Then sorting it via the mode (so I don't have to travel across the lands reaching far and wide >.>)
                    IEnumerable<KeyValuePair<uint, GatheringRoute>> sortedTable = CurrentSortMode switch
                    {
                        RouteSortMode.BestPath => SortByBestPath(filtered),
                        _ => filtered.OrderBy(x => x.Value.TerritoryId).ThenBy(x => x.Key),
                    };

                    foreach (var route in sortedTable)
                    {
                        ImGui.TableNextRow();
                        if (Route_Editor.SelectedRoute == route.Key)
                            ImGui.TableSetBgColor(ImGuiTableBgTarget.RowBg0, ImGui.GetColorU32(new Vector4(0.0f, 1.0f, 0.2f, 0.25f)));
                        if (Ignore_Routes.Contains(route.Key))
                            ImGui.TableSetBgColor(ImGuiTableBgTarget.RowBg0, ImGui.GetColorU32(new Vector4(0.9f, 0.5f, 0.5f, 1.0f)));

                        using (ImRaii.Disabled(Ignore_Routes.Contains(route.Key)))
                        {
                            ImGui.PushID($"{route.Key}");

                            ImGui.TableSetColumnIndex(0);

                            if (ImGui.Button($"{route.Key}"))
                            {
                                Route_Editor.UpdateRoute(route.Key);
                            }
                            if (ImGui.IsItemHovered())
                                ImGui.SetTooltip("Open route in editor");
                            ImGui.SameLine();
                            if (ImGui.SmallButton("List+"))
                            {
                                GatherQueueSession.AddRouteItems(route.Key);
                            }
                            if (ImGui.IsItemHovered())
                                ImGui.SetTooltip("Add all items from this route to the Gather List");

                            ImGui.TableNextColumn();
                            var job = route.Value.GatheringJobId;
                            if (Gather_Util.JobIcons.TryGetValue(job, out var jobIcon))
                            {
                                ImGui.Image(jobIcon.GetWrapOrEmpty().Handle, new(24, 24));
                            }

                            ImGui.TableNextColumn();
                            ImGui.Text($"{route.Value.ZoneName}");
                            if (ImGui.IsItemHovered())
                            {
                                ImGui.SetTooltip($"ID: {route.Value.TerritoryId}");
                            }
                            if (SheetInfo.TryGetValue(route.Key, out var gatherPointInfo))
                            {
                                ImGui.SameLine();
                                if (ImGuiEx.IconButton(FontAwesomeIcon.Flag, $"{route.Key}_Map"))
                                {
                                    gatherPointInfo.Map.OpenMap($"Route {route.Key}");
                                }
                                if (ImGui.IsItemHovered())
                                {
                                    if (ImGui.IsMouseClicked(ImGuiMouseButton.Right))
                                    {
                                        Svc.Commands.ProcessCommand("/vnav flyflag");
                                    }

                                    ImGui.SetTooltip("Left click to set flag\n" +
                                        "Right click to fly to flag");
                                }

                                ImGui.TableNextColumn();
                                foreach (var item in gatherPointInfo.ItemIds)
                                {
                                    if (Svc.Data.GetExcelSheet<Item>().TryGetRow(item, out var itemInfo))
                                    {
                                        var iconId = (int)itemInfo.Icon;
                                        var icon = Svc.Texture.GetFromGameIcon(iconId).GetWrapOrEmpty();
                                        ImGui.Image(icon.Handle, new(24, 24));
                                        if (ImGui.IsItemHovered())
                                        {
                                            ImGui.SetTooltip($"{itemInfo.Name}");
                                        }
                                        if (ImGui.IsItemClicked(ImGuiMouseButton.Left))
                                        {
                                            ImGui.SetClipboardText($"{itemInfo.Name}");
                                        }
                                        ImGui.SameLine();
                                    }
                                }

                                ImGui.TableNextColumn();
                                if (gatherPointInfo.TimedInfo.Count > 0)
                                {
                                    foreach (var time in gatherPointInfo.TimedInfo)
                                    {
                                        ImGui.Text($"{time.StartFormatted} - {time.EndFormatted}");
                                        ImGui.SameLine();
                                    }
                                }
                            }

                            ImGui.PopID();
                        }
                    }

                    ImGui.EndTable();
                }
            }
            ImGui.EndChild();
        }

        private static List<KeyValuePair<uint, GatheringRoute>> SortByBestPath(IEnumerable<KeyValuePair<uint, GatheringRoute>> routes)
        {
            var remaining = routes.ToList();
            if (remaining.Count == 0) return remaining;

            var result = new List<KeyValuePair<uint, GatheringRoute>>();

            // Group by territory, sort each group internally by nearest-neighbor
            var byTerritory = remaining
                .GroupBy(x => x.Value.TerritoryId)
                .OrderBy(g => g.Key); // stable territory ordering

            foreach (var group in byTerritory)
                result.AddRange(SortGroupByNearestNeighbor(group));

            return result;
        }
        private static List<KeyValuePair<uint, GatheringRoute>> SortGroupByNearestNeighbor(IEnumerable<KeyValuePair<uint, GatheringRoute>> routes)
        {
            var remaining = routes.ToList();
            var result = new List<KeyValuePair<uint, GatheringRoute>>(remaining.Count);

            // Seed from NW-most point within this territory
            var startIndex = remaining
                .Select((kvp, i) => (i, score: GetNWScore(kvp.Key)))
                .MinBy(x => x.score).i;

            var current = remaining[startIndex];
            remaining.RemoveAt(startIndex);
            result.Add(current);

            while (remaining.Count > 0)
            {
                var currentPos = GetFlagPos(current.Key);
                var nearestIndex = 0;
                var nearestDist = float.MaxValue;

                for (var i = 0; i < remaining.Count; i++)
                {
                    if (currentPos is not { } pos) break;

                    var dist = GetFlagDistance(remaining[i].Key, pos);
                    if (dist < nearestDist)
                    {
                        nearestDist = dist;
                        nearestIndex = i;
                    }
                }

                current = remaining[nearestIndex];
                remaining.RemoveAt(nearestIndex);
                result.Add(current);
            }

            return result;
        }

        // X + Y combined gives lowest score to the most NW point
        private static float GetNWScore(uint routeId)
        {
            var pos = GetFlagPos(routeId);
            return pos.HasValue ? pos.Value.X + pos.Value.Y : float.MaxValue;
        }
        private static Vector2? GetFlagPos(uint routeId)
        {
            if (!SheetInfo.TryGetValue(routeId, out var info)) return null;
            return new Vector2(info.Map.X, info.Map.Y);
        }
        private static float GetFlagDistance(uint routeId, Vector2 from)
        {
            var pos = GetFlagPos(routeId);
            return pos.HasValue ? Vector2.Distance(from, pos.Value) : float.MaxValue;
        }
    }
}
