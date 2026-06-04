using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects.Types;
using ECommons.GameHelpers;
using ECommons.Throttlers;
using GatherChill.ConfigFiles;
using GatherChill.GatheringInfo;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Utility;
using static ECommons.UIHelpers.AddonMasterImplementations.AddonMaster;

namespace GatherChill.Scheduler.Tasks;

internal static class GatherRouteNavigation
{
    private static uint _gatherFanNodeId;
    private static Vector3? _gatherFanPoint;

    public static bool IsGatheringSessionActive() => NavmeshMovement.IsGatheringSessionActive();

    public static void StopMovementForGathering() => NavmeshMovement.HaltNavmeshForGathering();

    public static void ResetInteractRetries()
    {
        _gatherFanNodeId = 0;
        _gatherFanPoint = null;
    }

    public static bool IsCorrectTerritory(GatheringRoute route)
    {
        if (route.TerritoryId == Player.Territory.RowId)
            return true;

        if (EzThrottler.Throttle($"Wrong territory {route.TerritoryId}", 5000))
            IceLogging.Warning($"Route {route.RouteId} expects territory {route.TerritoryId}, current is {Player.Territory.RowId}.");

        return false;
    }

    public static bool TryFlyToLocation(NodeLocation location, float closeRange, bool stayMounted)
    {
        var fanPoint = NavmeshMovement.ResolvePathPoint(
            NodeLocationExtensions.GetRandomFlightPosition(location, Player.Position));

        if (location.AllowFlying && NavmeshMovement.CanUseFlyMovement())
            return Task_NavmeshMove.Task_FlyTo(fanPoint, waitForBusy: false, closeRange, stayMounted) == true;

        return Task_NavmeshMove.Task_GroundTo(fanPoint, waitForBusy: false, closeRange, stayMounted) == true;
    }

    public static bool TryGroundToPoint(Vector3 point, float closeRange = NavmeshMovement.FinalApproachCloseRange) =>
        Task_NavmeshMove.Task_GroundTo(NavmeshMovement.ResolvePathPoint(point), waitForBusy: true, closeRange) == true;

    public static bool TryFlyToPoint(Vector3 point, float closeRange, bool stayMounted = false) =>
        Task_NavmeshMove.Task_FlyTo(NavmeshMovement.ResolvePathPoint(point), waitForBusy: true, closeRange, stayMounted) == true;

    public static void EnqueueApproach(IGameObject node, GatheringNode group, NodeLocation targetLocation)
    {
        var nodePos = node.Position;
        var flightFan = NavmeshMovement.ResolvePathPoint(
            NodeLocationExtensions.GetRandomFlightPosition(targetLocation, Player.Position, nodePos));
        var gatherFan = NavmeshMovement.ResolvePathPoint(
            NavmeshMovement.ApplyNodeStandoff(GetGatherFanPoint(targetLocation, flightFan, nodePos), nodePos));

        _gatherFanNodeId = node.BaseId;
        _gatherFanPoint = gatherFan;

        if (NavmeshMovement.ShouldUseFlyApproachForNode(targetLocation, node.Position))
        {
            IceLogging.Debug("Approach: fly then ground to gather fan");
            P.taskManager.EnqueueMulti
            (
                new(() => Task_NavmeshMove.Task_FlyTo(flightFan, true, NavmeshMovement.FinalApproachCloseRange, true), "Fly to fan", TaskConfig),
                new(() => Task_NavmeshMove.Task_GroundTo(gatherFan, true, NavmeshMovement.GatherFanCloseRange), "Ground to gather fan", TaskConfig),
                new(() => Task_GatherRoute.InteractWithNode(node.BaseId), "Interact with node", TaskConfig)
            );
        }
        else
        {
            IceLogging.Debug("Approach: ground to gather fan");
            P.taskManager.EnqueueMulti
            (
                new(() => Task_NavmeshMove.Task_GroundTo(gatherFan, true, NavmeshMovement.GatherFanCloseRange), "Ground to gather fan", TaskConfig),
                new(() => Task_GatherRoute.InteractWithNode(node.BaseId), "Interact with node", TaskConfig)
            );
        }
    }

    public static bool TryCompleteInteract(uint nodeId, out bool gatheringWindowOpen)
    {
        gatheringWindowOpen = false;
        var targetNode = NavmeshMovement.GetNearestGatheringNode(nodeId);

        if (IsGatheringSessionActive())
        {
            gatheringWindowOpen = true;
            StopMovementForGathering();
            ResetInteractRetries();
            IceLogging.Info("Gathering window visible, continuing");
            return true;
        }

        if (targetNode == null)
        {
            if (EzThrottler.Throttle("No targetable gathering node", 2000))
                IceLogging.Debug("No targetable gathering node at fan, retrying interact");

            return false;
        }

        if (_gatherFanPoint is { } fan && _gatherFanNodeId == nodeId && !IsAtGatherFan(fan))
        {
            if (Player.Mounted)
            {
                Utils.Dismount();
                return false;
            }

            if (P.navmesh.TryMoveTo(fan, fly: false, NavmeshMovement.GatherFanCloseRange))
                return false;
        }

        if (Player.Mounted)
            Utils.Dismount();

        if (!Player.Mounted && !Player.IsJumping && EzThrottler.Throttle("Target + Interaction throttle"))
        {
            Utils.TargetgameObject(targetNode);
            Utils.InteractWithObject(targetNode);
        }

        return false;
    }

    private static bool IsAtGatherFan(Vector3 fan) =>
        Player.DistanceTo(fan) <= NavmeshMovement.GatherFanCloseRange + NavmeshMovement.InteractRetrySlack;

    private static Vector3 GetGatherFanPoint(NodeLocation targetLocation, Vector3 flightFanPoint, Vector3 nodeWorldPos)
    {
        if (targetLocation.UseSpecificWalkingSpots && targetLocation.WalkablePositions.Count > 0)
        {
            return targetLocation.WalkablePositions
                .OrderBy(x => Vector3.Distance(flightFanPoint, x))
                .First();
        }

        return NodeLocationExtensions.GetRandomGatherPosition(targetLocation, Player.Position, nodeWorldPos);
    }
}
