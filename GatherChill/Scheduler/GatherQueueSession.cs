using System.Collections.Generic;
using System.Linq;
using ECommons.GameHelpers;
using GatherChill.ConfigFiles;
using GatherChill.GatheringInfo;
using GatherChill.Scheduler.Tasks;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Utility;

namespace GatherChill.Scheduler;

internal static class GatherQueueSession
{
    internal static List<GatherTarget> PendingTargets { get; } = new();
    internal static List<GatherZoneBatch> Batches { get; private set; } = new();
    internal static int BatchIndex { get; private set; }
    internal static int TargetIndex { get; private set; }
    internal static bool Active { get; private set; }
    public static string? LastStartError { get; private set; }

    public static void ClearPending()
    {
        PendingTargets.Clear();
        LastStartError = null;
    }

    public static void AddTarget(GatherTarget target)
    {
        if (PendingTargets.Any(t => t.RouteId == target.RouteId && t.ItemId == target.ItemId))
            return;

        PendingTargets.Add(target);
    }

    public static int AddRouteItems(uint routeId, int defaultQuantity = 1)
    {
        var route = P.routeEditor.GetRoute(routeId);
        if (route == null)
        {
            IceLogging.Warning($"List+: route {routeId} is not loaded.");
            return 0;
        }

        if (route.RouteId != routeId)
        {
            IceLogging.Warning($"List+: route key {routeId} mismatches route file id {route.RouteId}; using route file.");
            routeId = route.RouteId;
        }

        if (Gather_Util.SheetInfo.TryGetValue(routeId, out var sheet) && sheet.TerritoryId != route.TerritoryId)
        {
            IceLogging.Warning(
                $"List+: sheet territory for route {routeId} does not match route file ({sheet.ZoneName} vs {route.ZoneName}).");
            return 0;
        }

        var itemIds = Gather_Util.GetItemIdsForRoute(route);
        if (itemIds.Count == 0 && sheet != null)
            itemIds = sheet.ItemIds;

        if (itemIds.Count == 0)
        {
            IceLogging.Warning($"List+: no items found for route {routeId} ({route.ZoneName}).");
            return 0;
        }

        var added = 0;
        foreach (var itemId in itemIds)
        {
            if (PendingTargets.Any(t => t.RouteId == routeId && t.ItemId == itemId))
                continue;

            AddTarget(new GatherTarget(routeId, itemId, defaultQuantity));
            added++;
        }

        if (added > 0)
            IceLogging.Info($"List+: added {added} item(s) from route {routeId} ({route.ZoneName}).");

        return added;
    }

    public static void AddCurrentZoneFromRoutes(IEnumerable<uint> routeIds)
    {
        var territoryId = Player.Territory.RowId;
        foreach (var routeId in routeIds)
        {
            var route = P.routeEditor.GetRoute(routeId);
            if (route == null || route.TerritoryId != territoryId)
                continue;

            AddRouteItems(routeId);
        }
    }

    public static bool Start()
    {
        LastStartError = null;
        Batches = GatherQueuePlanner.Plan(PendingTargets, C.SkipInactiveTimedNodes);
        if (Batches.Count == 0 || Batches.All(b => b.Targets.Count == 0))
        {
            LastStartError = GatherQueuePlanner.DescribePlanFailure(PendingTargets, C.SkipInactiveTimedNodes);
            IceLogging.Warning(LastStartError);
            return false;
        }

        var activeCount = Batches.Sum(b => b.Targets.Count);
        var skipped = PendingTargets.Count - activeCount;
        if (skipped > 0)
            IceLogging.Info($"Gather queue: {activeCount} active target(s), {skipped} skipped (Want = 0, invalid route, or timed off).");

        BatchIndex = 0;
        TargetIndex = 0;
        Active = true;
        P.taskManager.Abort();
        Task_GatherRoute.Reset();
        SnapshotBaselines();
        ApplyCurrentTarget();
        IceLogging.Info($"Started gather queue: {Batches.Count} zone(s), {activeCount} target(s).");
        return true;
    }

    public static void Stop()
    {
        Active = false;
        Batches.Clear();
        BatchIndex = 0;
        TargetIndex = 0;
    }

    private static void SnapshotBaselines()
    {
        foreach (var target in Batches.SelectMany(b => b.Targets))
        {
            var baseline = Utils.GetItemCount(target.ItemId, out var count) ? count : 0;
            target.BaselineCount = baseline;
            SyncBaselineToPending(target.RouteId, target.ItemId, baseline);
        }
    }

    private static void SyncBaselineToPending(uint routeId, uint itemId, int baseline)
    {
        var pending = PendingTargets.FirstOrDefault(t => t.RouteId == routeId && t.ItemId == itemId);
        if (pending != null)
            pending.BaselineCount = baseline;
    }

    public static GatherTarget? FindBatchTarget(uint routeId, uint itemId) =>
        Active
            ? Batches.SelectMany(b => b.Targets).FirstOrDefault(t => t.RouteId == routeId && t.ItemId == itemId)
            : null;

    public static void CompleteCurrentTarget()
    {
        if (!Active || Batches.Count == 0)
            return;

        var batch = Batches[BatchIndex];
        LogTargetFinished(CurrentTarget(), batch.ZoneName);

        TargetIndex++;
        while (TargetIndex < batch.Targets.Count && IsInventoryGoalMet(batch.Targets[TargetIndex]))
        {
            LogTargetFinished(batch.Targets[TargetIndex], batch.ZoneName, skipped: true);
            TargetIndex++;
        }

        if (TargetIndex < batch.Targets.Count)
        {
            ApplyCurrentTarget();
            return;
        }

        BatchIndex++;
        TargetIndex = 0;
        if (BatchIndex >= Batches.Count)
        {
            IceLogging.Info("Gather queue complete.");
            SchedulerMain.Stop();
            return;
        }

        IceLogging.Info($"Zone batch done. Next: {Batches[BatchIndex].ZoneName}.");
        ApplyCurrentTarget();
    }

    private static void ApplyCurrentTarget()
    {
        var target = Batches[BatchIndex].Targets[TargetIndex];
        SchedulerMain.SetRouteTarget(target.RouteId, target.ItemId, queueMode: true);
    }

    public static GatherZoneBatch? CurrentBatch =>
        Active && BatchIndex < Batches.Count ? Batches[BatchIndex] : null;

    public static GatherTarget? CurrentTarget() =>
        Active && BatchIndex < Batches.Count && TargetIndex < Batches[BatchIndex].Targets.Count
            ? Batches[BatchIndex].Targets[TargetIndex]
            : null;

    public static bool IsCurrentTargetQuantityMet()
    {
        var target = CurrentTarget();
        if (target == null)
            return true;

        return IsInventoryGoalMet(target);
    }

    private static bool IsInventoryGoalMet(GatherTarget target)
    {
        if (target.TargetQuantity <= 0)
            return false;

        if (!Utils.GetItemCount(target.ItemId, out var count))
            return false;

        var baseline = target.BaselineCount ?? count;
        return count >= baseline + target.TargetQuantity;
    }

    private static void LogTargetFinished(GatherTarget? target, string zoneName, bool skipped = false)
    {
        if (target == null)
            return;

        var prefix = skipped ? "Skipped" : "Finished";
        if (target.TargetQuantity > 0 && target.TryGetGatherProgress(out var gathered, out var goal))
            IceLogging.Info($"{prefix} item {target.ItemId} in {zoneName} (gathered {gathered}/{goal}).");
        else
            IceLogging.Info($"{prefix} route {target.RouteId} item {target.ItemId} in {zoneName}.");
    }

    public static string ProgressLabel
    {
        get
        {
            if (!Active || Batches.Count == 0)
                return "Idle";

            var batch = Batches[BatchIndex];
            var target = batch.Targets[TargetIndex];
            var qty = target.TryGetGatherProgress(out var gathered, out var goal)
                ? $" — {gathered}/{goal}"
                : string.Empty;

            return $"Zone {BatchIndex + 1}/{Batches.Count} ({batch.ZoneName}) — target {TargetIndex + 1}/{batch.Targets.Count}{qty}";
        }
    }
}
