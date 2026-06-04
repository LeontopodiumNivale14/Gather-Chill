using System.Collections.Generic;
using System.Linq;
using Dalamud.Interface.Utility.Raii;
using GatherChill.Scheduler;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Utility;
using Lumina.Excel.Sheets;
using static GatherChill.Utilities.GatheringHelpers.Gather_Util;

namespace GatherChill.Ui.RouteWindowTabs;

internal static class Route_GatherList
{
    public static void Draw()
    {
        if (GatherQueueSession.Active)
        {
            ImGui.TextColored(new Vector4(0.5f, 1f, 0.5f, 1f), GatherQueueSession.ProgressLabel);
            if (ImGui.Button("Stop queue"))
                SchedulerMain.Stop();
            ImGui.Separator();
        }

        ImGui.TextWrapped("Build a list, then start. Items are gathered per zone (current zone first). Timed windows open now are prioritized.");

        var skipInactive = C.SkipInactiveTimedNodes;
        if (ImGui.Checkbox("Skip timed nodes outside their window", ref skipInactive))
            C.SkipInactiveTimedNodes = skipInactive;

        if (ImGui.Button("Add current zone (filtered routes)"))
        {
            var routes = P.routeEditor.Routes
                .Where(x => !Ignore_Routes.Contains(x.Key))
                .Where(x => !Route_Selector.Filter_Expansion || x.Value.ExpansionId == Route_Selector.FilterExpansionId)
                .Where(x => !Route_Selector.Filter_Zone || x.Value.TerritoryId == ECommons.GameHelpers.Player.Territory.RowId)
                .Where(x => Route_Selector.FilterJob == 0 || x.Value.GatheringJobId == Route_Selector.FilterJob)
                .Select(x => x.Key);

            GatherQueueSession.AddCurrentZoneFromRoutes(routes);
        }

        if (ImGui.IsItemHovered())
            ImGui.SetTooltip("Uses the same filters as Route Selector (expansion, zone, job).");

        ImGui.SameLine();
        if (ImGui.Button("Clear list"))
            GatherQueueSession.ClearPending();

        ImGui.SameLine();
        using (var disabled = ImRaii.Disabled(GatherQueueSession.PendingTargets.Count == 0 || GatherQueueSession.Active))
        {
            if (ImGui.Button("Start gather queue"))
                GatherQueueSession.Start();
        }

        DrawTargetTable(GatherQueueSession.PendingTargets, C.SkipInactiveTimedNodes);

        if (GatherQueueSession.PendingTargets.Count > 0 &&
            GatherQueuePlanner.Plan(GatherQueueSession.PendingTargets, C.SkipInactiveTimedNodes).Count == 0)
            ImGui.TextDisabled("No valid entries (need routes with node data).");
    }

    private static void DrawTargetTable(List<GatherTarget> targets, bool skipInactiveTimed)
    {
        if (targets.Count == 0)
        {
            ImGui.TextDisabled("Gather list is empty.");
            return;
        }

        var planned = GatherQueuePlanner.Plan(targets, skipInactiveTimed);
        var plannedSet = planned.SelectMany(b => b.Targets).ToHashSet();

        if (ImGui.BeginTable("GatherListPlan", 7, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingStretchProp))
        {
            ImGui.TableSetupColumn("Zone");
            ImGui.TableSetupColumn("Route");
            ImGui.TableSetupColumn("Item");
            ImGui.TableSetupColumn("Have");
            ImGui.TableSetupColumn("Want");
            ImGui.TableSetupColumn("Timed");
            ImGui.TableSetupColumn("");
            ImGui.TableHeadersRow();

            foreach (var target in targets)
            {
                ImGui.TableNextRow();
                var inPlan = plannedSet.Contains(target);
                if (!inPlan)
                    ImGui.TableSetBgColor(ImGuiTableBgTarget.RowBg0, ImGui.GetColorU32(new Vector4(0.5f, 0.2f, 0.2f, 0.35f)));

                ImGui.TableSetColumnIndex(0);
                var zoneName = "-";
                if (P.routeEditor.GetRoute(target.RouteId) is { } route)
                    zoneName = route.ZoneName ?? zoneName;
                ImGui.Text(zoneName);

                ImGui.TableNextColumn();
                ImGui.Text($"{target.RouteId}");

                ImGui.TableNextColumn();
                var itemName = $"Item {target.ItemId}";
                if (Svc.Data.GetExcelSheet<Item>().TryGetRow(target.ItemId, out var item))
                    itemName = item.Name.ToString();
                ImGui.Text(itemName);

                ImGui.TableNextColumn();
                if (Utils.GetItemCount(target.ItemId, out var have))
                    ImGui.Text($"{have}");
                else
                    ImGui.Text("-");

                ImGui.TableNextColumn();
                ImGui.PushID($"{target.RouteId}_{target.ItemId}_qty");
                var wantStr = target.TargetQuantity.ToString();
                ImGui.SetNextItemWidth(56);
                if (ImGui.InputText("##want", ref wantStr, 8, ImGuiInputTextFlags.CharsDecimal))
                {
                    if (int.TryParse(wantStr, out var parsed))
                        target.TargetQuantity = Math.Max(0, parsed);
                    else if (wantStr.Length == 0)
                        target.TargetQuantity = 0;
                }
                ImGui.PopID();
                if (ImGui.IsItemHovered())
                    ImGui.SetTooltip("Inventory count to reach. 0 = one route pass only.");

                ImGui.TableNextColumn();
                if (SheetInfo.TryGetValue(target.RouteId, out var sheet) && sheet.TimedInfo.Count > 0)
                {
                    var active = EorzeaTimeUtil.IsAnyWindowActive(sheet.TimedInfo, EorzeaTimeUtil.GetCurrentTimeRaw());
                    ImGui.Text(active ? "OPEN" : "timed");
                }
                else
                    ImGui.Text("-");

                ImGui.TableNextColumn();
                ImGui.PushID($"{target.RouteId}_{target.ItemId}_rm");
                if (ImGui.SmallButton("Remove"))
                    targets.Remove(target);
                ImGui.PopID();
            }

            ImGui.EndTable();
        }
    }
}
