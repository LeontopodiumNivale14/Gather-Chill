using System.Collections.Generic;
using GatherChill.Utilities.Utility;

namespace GatherChill.Scheduler;

internal sealed class GatherTarget
{
    public uint RouteId { get; init; }
    public uint ItemId { get; init; }

    /// <summary>How many more to gather this queue. 0 = skip (row stays on list only).</summary>
    public int TargetQuantity { get; set; }

    /// <summary>Inventory count when the queue started (or target became active).</summary>
    public int? BaselineCount { get; set; }

    public GatherTarget(uint routeId, uint itemId, int targetQuantity = 0)
    {
        RouteId = routeId;
        ItemId = itemId;
        TargetQuantity = targetQuantity;
    }

    public bool TryGetGatherProgress(out int gathered, out int goal)
    {
        goal = TargetQuantity;
        gathered = 0;
        if (goal <= 0)
            return false;

        if (!Utils.GetItemCount(ItemId, out var have))
            return true;

        var baseline = BaselineCount ?? have;
        gathered = Math.Max(0, have - baseline);
        return true;
    }
}

internal sealed class GatherZoneBatch
{
    public uint TerritoryId { get; init; }
    public string ZoneName { get; init; } = string.Empty;
    public List<GatherTarget> Targets { get; init; } = new();
}
