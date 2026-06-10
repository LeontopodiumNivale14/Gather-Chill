using ECommons.GameHelpers;
using ECommons.Throttlers;
using GatherChill.ConfigFiles;
using GatherChill.GatheringInfo;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Utility;

namespace GatherChill.Scheduler.Tasks;

/// <summary>
/// Route editor helpers: enqueue a single pathfind step so the player walks to a fan point
/// while editing routes ("Pathfind to gather fan" / "Pathfind to flight fan" buttons).
/// </summary>
internal static class NavmeshEditorPath
{
    private static bool _active;

    public static bool TryEnqueuePathTo(Vector3 destination, bool? forceFly = null, float? closeRange = null, string label = "Editor pathfind", NodeLocation? node = null)
    {
        if (!C.NavmeshMovementEnabled)
        {
            if (EzThrottler.Throttle("Editor navmesh disabled", 3000))
                IceLogging.Warning("Navmesh movement is disabled (/gatherchill s → Navigation).");

            return false;
        }

        if (!P.navmesh.Installed)
        {
            if (EzThrottler.Throttle("Editor vnavmesh missing", 3000))
                IceLogging.Error("vnavmesh is not installed.");

            return false;
        }

        destination = NavmeshMovement.ResolvePathPoint(destination);
        var range = closeRange ?? NavmeshMovement.FinalApproachCloseRange;

        if (node != null && !node.AllowFlying)
            forceFly = false;

        var useFly = ResolveUseFly(destination, forceFly);

        if (useFly && !NavmeshMovement.CanUseFlyMovement() && EzThrottler.Throttle("Editor fly unavailable", 3000))
            IceLogging.Debug("Flight not available here; using ground.");

        _active = true;
        P.taskManager.Enqueue(() => RunMoveStep(destination, useFly, range), label, TaskConfig);
        return true;
    }

    public static bool TryEnqueueGatherFan(NodeLocation node)
    {
        var pos = NodeLocationExtensions.GetRandomGatherPosition(node, Player.Position);
        return TryEnqueuePathTo(
            pos,
            forceFly: node.AllowFlying ? null : false,
            closeRange: NavmeshMovement.FinalApproachCloseRange,
            label: "Editor pathfind (gather fan)",
            node);
    }

    public static bool TryEnqueueFlightFan(NodeLocation node)
    {
        var pos = NodeLocationExtensions.GetRandomFlightPosition(node, Player.Position);
        return TryEnqueuePathTo(
            pos,
            forceFly: node.AllowFlying ? null : false,
            closeRange: NavmeshMovement.FinalApproachCloseRange,
            label: "Editor pathfind (flight fan)",
            node);
    }

    private static bool ResolveUseFly(Vector3 destination, bool? forceFly)
    {
        if (forceFly == false)
            return false;

        if (forceFly == true)
            return NavmeshMovement.CanUseFlyMovement();

        return NavmeshMovement.ShouldUseFlyPath(destination);
    }

    public static void Cancel() => _active = false;

    public static void Stop()
    {
        Cancel();
        Task_NavmeshMove.ReleaseOwnedPath();
        P.navmesh.StopCompletely();
    }

    private static bool RunMoveStep(Vector3 pos, bool fly, float closeRange)
    {
        if (!_active)
            return true;

        var result = fly
            ? Task_NavmeshMove.Task_FlyTo(pos, waitForBusy: true, closeRange, stayMounted: false)
            : Task_NavmeshMove.Task_GroundTo(pos, waitForBusy: true, closeRange, stayMounted: false);

        if (result == true)
            _active = false;

        return result == true;
    }
}
