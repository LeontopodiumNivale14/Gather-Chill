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
    private static int _interactRetries;

    public static void ResetInteractRetries() => _interactRetries = 0;

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

        if (NavmeshMovement.CanUseFlyMovement())
            return Task_NavmeshMove.Task_FlyTo(fanPoint, waitForBusy: false, closeRange, stayMounted) == true;

        return Task_NavmeshMove.Task_GroundTo(fanPoint, waitForBusy: false, closeRange, stayMounted) == true;
    }

    public static bool TryGroundToPoint(Vector3 point, float closeRange = NavmeshMovement.FinalApproachCloseRange) =>
        Task_NavmeshMove.Task_GroundTo(NavmeshMovement.ResolvePathPoint(point), waitForBusy: true, closeRange) == true;

    public static bool TryFlyToPoint(Vector3 point, float closeRange, bool stayMounted = false) =>
        Task_NavmeshMove.Task_FlyTo(NavmeshMovement.ResolvePathPoint(point), waitForBusy: true, closeRange, stayMounted) == true;

    public static void EnqueueApproach(IGameObject node, GatheringNode group, NodeLocation targetLocation)
    {
        var fanPoint = NodeLocationExtensions.GetRandomFlightPosition(targetLocation, Player.Position);
        var walkPoint = GetWalkPoint(targetLocation, fanPoint);

        if (NavmeshMovement.ShouldUseFlyPath(node.Position) && !Svc.Condition[ConditionFlag.Diving])
        {
            IceLogging.Debug("Approach: fly then ground");
            P.taskManager.EnqueueMulti
            (
                new(() => Task_NavmeshMove.Task_FlyTo(fanPoint, true, NavmeshMovement.FinalApproachCloseRange, true), "Fly to fan", TaskConfig),
                new(() => Task_NavmeshMove.Task_GroundTo(walkPoint, true, NavmeshMovement.NodePathCloseRange), "Ground to node", TaskConfig),
                new(() => Task_GatherRoute.InteractWithNode(node.BaseId), "Interact with node", TaskConfig)
            );
        }
        else
        {
            IceLogging.Debug("Approach: ground only");
            P.taskManager.EnqueueMulti
            (
                new(() => Task_NavmeshMove.Task_GroundTo(walkPoint, true, NavmeshMovement.NodePathCloseRange), "Ground to node", TaskConfig),
                new(() => Task_GatherRoute.InteractWithNode(node.BaseId), "Interact with node", TaskConfig)
            );
        }
    }

    public static bool TryCompleteInteract(uint nodeId)
    {
        var targetNode = NavmeshMovement.GetNearestGatheringNode(nodeId);

        if (Svc.Condition[ConditionFlag.Gathering] &&
            (GenericHelpers.TryGetAddonMaster<Gathering>("Gathering", out var gather) && gather.IsAddonReady ||
             GenericHelpers.TryGetAddonMaster<GatheringMasterpiece>("GatheringMasterpiece", out var collectable) && collectable.IsAddonReady))
        {
            ResetInteractRetries();
            IceLogging.Info("Gathering window visible, continuing");
            return true;
        }

        if (targetNode == null)
        {
            ResetInteractRetries();
            IceLogging.Debug("No targetable gathering node found, skipping");
            return true;
        }

        P.navmesh.StopIfOwned();

        if (P.navmesh.TickArrival(targetNode.Position, NavmeshMovement.NodePathCloseRange) ||
            NavmeshMovement.IsNearGatheringNode(nodeId, NavmeshMovement.InteractDistance))
        {
            if (!Player.IsJumping && EzThrottler.Throttle("Target + Interaction throttle"))
            {
                Utils.TargetgameObject(targetNode);
                Utils.InteractWithObject(targetNode);
            }

            return false;
        }

        if (_interactRetries < C.NavmeshInteractRetries &&
            NavmeshMovement.HorizontalDistance(targetNode.Position) > NavmeshMovement.NodePathCloseRange + NavmeshMovement.InteractRetrySlack &&
            EzThrottler.Throttle("Gather interact nav retry", 2000))
        {
            _interactRetries++;
            var walkPoint = NavmeshMovement.ResolvePathPoint(targetNode.Position);
            if (P.navmesh.TryMoveTo(walkPoint, Player.Mounted && NavmeshMovement.CanUseFlyMovement(), NavmeshMovement.NodePathCloseRange))
                return false;
        }

        return false;
    }

    private static Vector3 GetWalkPoint(NodeLocation targetLocation, Vector3 fanPoint)
    {
        if (targetLocation.UseSpecificWalkingSpots && targetLocation.WalkablePositions.Count > 0)
        {
            return targetLocation.WalkablePositions
                .OrderBy(x => Vector3.Distance(fanPoint, x))
                .First();
        }

        return NodeLocationExtensions.GetRandomGatherPosition(targetLocation, Player.Position);
    }
}
