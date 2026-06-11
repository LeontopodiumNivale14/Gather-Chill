using ECommons.GameHelpers;
using ECommons.Throttlers;
using GatherChill.Utilities.Tools;

namespace GatherChill.Scheduler;

internal static class GatherZoneTravel
{
    public static bool TryEnsureTerritory(uint territoryId, string zoneName)
    {
        if (Player.Territory.RowId == territoryId)
            return true;

        if (EzThrottler.Throttle($"Wrong zone {territoryId}", 5000))
            IceLogging.Warning($"Gather queue needs {zoneName} ({territoryId}). Travel there manually — automatic zone travel is not wired yet.");

        return false;
    }
}
