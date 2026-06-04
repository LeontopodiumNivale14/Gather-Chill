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

        foreach (var target in targets)
        {
            if (Gather_Util.Ignore_Routes.Contains(target.RouteId))
                continue;

            var route = P.routeEditor.GetRoute(target.RouteId);
            if (route == null || route.NodeInfo.Count == 0)
                continue;

            if (!Gather_Util.SheetInfo.TryGetValue(target.RouteId, out var sheet))
                sheet = null;

            if (sheet != null && !sheet.ItemIds.Contains(target.ItemId))
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
                    .Select(x => x.target)
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

    private static GatherPointInfo? GetSheet(uint routeId) =>
        Gather_Util.SheetInfo.TryGetValue(routeId, out var sheet) ? sheet : null;
}
