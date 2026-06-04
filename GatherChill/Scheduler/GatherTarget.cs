using System.Collections.Generic;

namespace GatherChill.Scheduler;

internal sealed class GatherTarget
{
    public uint RouteId { get; init; }
    public uint ItemId { get; init; }

    /// <summary>Inventory count to reach. 0 = complete after one route pass.</summary>
    public int TargetQuantity { get; set; }

    public GatherTarget(uint routeId, uint itemId, int targetQuantity = 0)
    {
        RouteId = routeId;
        ItemId = itemId;
        TargetQuantity = targetQuantity;
    }
}

internal sealed class GatherZoneBatch
{
    public uint TerritoryId { get; init; }
    public string ZoneName { get; init; } = string.Empty;
    public List<GatherTarget> Targets { get; init; } = new();
}
