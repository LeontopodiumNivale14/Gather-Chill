using System.Collections.Generic;
using System.Linq;
using ECommons.GameHelpers;
using GatherChill.ConfigFiles;
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

    public static void ClearPending() => PendingTargets.Clear();

    public static void AddTarget(GatherTarget target)
    {
        if (PendingTargets.Any(t => t.RouteId == target.RouteId && t.ItemId == target.ItemId))
            return;

        PendingTargets.Add(target);
    }

    public static void AddRouteItems(uint routeId)
    {
        if (!Gather_Util.SheetInfo.TryGetValue(routeId, out var sheet))
            return;

        foreach (var itemId in sheet.ItemIds)
            AddTarget(new GatherTarget(routeId, itemId, 0));
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
        Batches = GatherQueuePlanner.Plan(PendingTargets, C.SkipInactiveTimedNodes);
        if (Batches.Count == 0 || Batches.All(b => b.Targets.Count == 0))
        {
            IceLogging.Warning("Gather list is empty or has no valid routes with node data.");
            return false;
        }

        BatchIndex = 0;
        TargetIndex = 0;
        Active = true;
        ApplyCurrentTarget();
        IceLogging.Info($"Started gather queue: {Batches.Count} zone(s), {Batches.Sum(b => b.Targets.Count)} target(s).");
        return true;
    }

    public static void Stop()
    {
        Active = false;
        Batches.Clear();
        BatchIndex = 0;
        TargetIndex = 0;
    }

    public static void CompleteCurrentTarget()
    {
        if (!Active || Batches.Count == 0)
            return;

        var batch = Batches[BatchIndex];
        var finished = CurrentTarget();
        if (finished != null && finished.TargetQuantity > 0 && Utils.GetItemCount(finished.ItemId, out var have))
            IceLogging.Info($"Finished {finished.ItemId} in {batch.ZoneName} ({have}/{finished.TargetQuantity}).");
        else
            IceLogging.Info($"Finished route {SchedulerMain.RouteId} item {SchedulerMain.ItemId} in {batch.ZoneName}.");

        TargetIndex++;
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

        if (target.TargetQuantity <= 0)
            return true;

        return Utils.GetItemCount(target.ItemId, out var count) && count >= target.TargetQuantity;
    }

    public static string ProgressLabel
    {
        get
        {
            if (!Active || Batches.Count == 0)
                return "Idle";

            var batch = Batches[BatchIndex];
            var target = batch.Targets[TargetIndex];
            var qty = target.TargetQuantity > 0 && Utils.GetItemCount(target.ItemId, out var have)
                ? $" — {have}/{target.TargetQuantity}"
                : string.Empty;

            return $"Zone {BatchIndex + 1}/{Batches.Count} ({batch.ZoneName}) — target {TargetIndex + 1}/{batch.Targets.Count}{qty}";
        }
    }
}
