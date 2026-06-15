using System.Collections.Generic;
using System.Linq;
using ECommons.GameHelpers;
using GatherChill.GatheringInfo;
using GatherChill.Utilities.GatheringHelpers;
using static GatherChill.Utilities.GatheringHelpers.EorzeaTimeUtil;

namespace GatherChill.Scheduler;

internal static class GatherQueuePlanner
{
    public static List<GatherZoneBatch> Plan(IEnumerable<GatherTarget> targets, bool skipInactiveTimed = false)
    {
        var valid = new List<(GatherTarget target, GatheringRoute route, GatherPointInfo? sheet, TimedPriority priority)>();
        var routeItemsCache = new Dictionary<uint, HashSet<uint>>();

        foreach (var target in targets)
        {
            if (Gather_Util.Ignore_Routes.Contains(target.RouteId))
                continue;

            var route = P.routeEditor.GetRoute(target.RouteId);
            if (route == null || route.NodeInfo.Count == 0)
                continue;

            if (!routeItemsCache.TryGetValue(route.RouteId, out var routeItems))
            {
                routeItems = Gather_Util.GetItemIdsForRoute(route).ToHashSet();
                routeItemsCache[route.RouteId] = routeItems;
            }

            if (!routeItems.Contains(target.ItemId))
                continue;

            Gather_Util.SheetInfo.TryGetValue(target.RouteId, out var sheet);

            // Want 0 = listed but not part of this queue run (see Gather List tooltip).
            if (target.TargetQuantity <= 0)
                continue;

            var priority = GetTimedPriority(sheet);
            if (skipInactiveTimed && priority == TimedPriority.TimedInactive)
                continue;

            valid.Add((target, route, sheet, priority));
        }

        var batches = valid
            .GroupBy(x => x.route.TerritoryId)
            .Select(g =>
            {
                var ordered = g
                    .OrderByDescending(x => (int)x.priority)
                    .ThenBy(x => x.route.RouteId)
                    .ThenBy(x => x.target.ItemId)
                    .Select(x => new GatherTarget(x.target.RouteId, x.target.ItemId, x.target.TargetQuantity))
                    .ToList();

                var zoneName = g.First().route.ZoneName ?? g.First().sheet?.ZoneName ?? $"Territory {g.Key}";
                return new GatherZoneBatch
                {
                    TerritoryId = g.Key,
                    ZoneName = zoneName,
                    Targets = ordered,
                };
            })
            .OrderByDescending(b => b.Targets.Any(t => GetTimedPriority(GetSheet(t.RouteId)) == TimedPriority.ActiveNow))
            .ThenBy(b => b.TerritoryId == Player.Territory.RowId ? 0 : 1)
            .ThenBy(b => b.ZoneName)
            .ToList();

        return batches;
    }

    public static string DescribePlanFailure(IEnumerable<GatherTarget> targets, bool skipInactiveTimed)
    {
        var rows = targets.ToList();
        if (rows.Count == 0)
            return "Gather list is empty — use List+ or add items first.";

        var reasons = new List<string>();
        var wantZero = rows.Count(t => t.TargetQuantity <= 0);
        if (wantZero == rows.Count)
            reasons.Add("every row has Want = 0");

        var missingRoute = 0;
        var unknownItem = 0;
        var timedOff = 0;

        foreach (var target in rows.Where(t => t.TargetQuantity > 0))
        {
            if (Gather_Util.Ignore_Routes.Contains(target.RouteId))
                continue;

            var route = P.routeEditor.GetRoute(target.RouteId);
            if (route == null || route.NodeInfo.Count == 0)
            {
                missingRoute++;
                continue;
            }

            if (!Gather_Util.RouteContainsItem(route, target.ItemId))
            {
                unknownItem++;
                continue;
            }

            if (skipInactiveTimed
                && Gather_Util.SheetInfo.TryGetValue(target.RouteId, out var sheet)
                && GetTimedPriority(sheet) == TimedPriority.TimedInactive)
            {
                timedOff++;
            }
        }

        if (missingRoute > 0)
            reasons.Add($"{missingRoute} row(s) missing route node data");
        if (unknownItem > 0)
            reasons.Add($"{unknownItem} row(s) item not on route");
        if (timedOff > 0)
            reasons.Add($"{timedOff} timed row(s) outside their window");

        if (reasons.Count == 0)
            return "No valid queue targets — check Want values and route data.";

        return "Could not start gather queue: " + string.Join("; ", reasons) + ".";
    }

    private static GatherPointInfo? GetSheet(uint routeId) =>
        Gather_Util.SheetInfo.TryGetValue(routeId, out var sheet) ? sheet : null;
}
