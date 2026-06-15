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

/// <summary>
/// Gathering-specific navigation layered on <see cref="Task_NavmeshMove"/>.
/// The close approach flies up to the route author's configured gather fan (or walk spots), then settles
/// onto it and interacts. <see cref="TryCompleteInteract"/> retries targeting when the node didn't open.
/// </summary>
internal static class GatherRouteNavigation
{
    // Remember which gather fan we walked to so interact retries can re-path if we're still short.
    private static uint _gatherFanNodeId;
    private static Vector3? _gatherFanPoint;
    private static NodeLocation? _gatherFanLocation;

    // Cached gather fan while approaching a route location to validate node spawn.
    private static Vector3? _validationGatherFan;
    private static Vector3 _validationNodePos;

    public static bool IsGatheringSessionActive() => NavmeshMovement.IsGatheringSessionActive();

    public static void StopMovementForGathering() => NavmeshMovement.HaltNavmeshForGathering();

    public static void ResetInteractRetries()
    {
        _gatherFanNodeId = 0;
        _gatherFanPoint = null;
        _gatherFanLocation = null;
    }

    public static void ResetValidationApproach()
    {
        _validationGatherFan = null;
        _validationNodePos = default;
    }

    public static bool IsCorrectTerritory(GatheringRoute route)
    {
        if (route.TerritoryId == Player.Territory.RowId)
            return true;

        if (EzThrottler.Throttle($"Wrong territory {route.TerritoryId}", 5000))
            IceLogging.Warning($"Route {route.RouteId} expects territory {route.TerritoryId}, current is {Player.Territory.RowId}.");

        return false;
    }

    public static bool TryFlyToLocation(NodeLocation location, float closeRange, bool stayMounted, Vector3? nodeWorldPos = null)
    {
        var fanPoint = NavmeshMovement.ResolvePathPoint(
            NodeLocationExtensions.GetRandomFlightPosition(location, Player.Position, nodeWorldPos));

        if (location.AllowFlying && NavmeshMovement.CanUseFlyMovement())
            return Task_NavmeshMove.Task_FlyTo(fanPoint, waitForBusy: false, closeRange, stayMounted) == true;

        return Task_NavmeshMove.Task_GroundTo(fanPoint, waitForBusy: false, closeRange, stayMounted) == true;
    }

    /// <summary>Fly in until within 75y of the route anchor so the client node list can update.</summary>
    public static bool TryTravelWithinLoadRange(NodeLocation location)
    {
        if (NavmeshMovement.IsWithinLoadRangeOf(location.Position))
            return true;

        TryFlyToLocation(location, NavmeshMovement.FanApproachCloseRange, stayMounted: true);
        return false;
    }

    /// <summary>
    /// Fly up to the route's gather fan, then settle onto it, until the live node is within interact range.
    /// </summary>
    public static bool TryApproachGatherFan(NodeLocation location, IGameObject liveNode)
    {
        var nodeCenter = liveNode.Position;

        if (_validationGatherFan is null || _validationNodePos != nodeCenter)
        {
            _validationNodePos = nodeCenter;
            _validationGatherFan = ResolveGatherStandPoint(location, liveNode);
        }

        var approachPoint = _validationGatherFan.Value;

        if (NavmeshMovement.IsWithinGatherInteractRange(liveNode))
        {
            // In interact range: drop the mount so targeting/gathering can fire.
            if (Player.Mounted)
            {
                Utils.Dismount();
                return false;
            }

            return true;
        }

        // Fly up to the gather fan when it's far or above us (ledge nodes). Stay mounted so this doesn't
        // fight MoveTo's auto-mount — a premature dismount leaves us mounting/dismounting in place.
        if (NavmeshMovement.ShouldFlyToGatherStand(location, approachPoint))
        {
            Task_NavmeshMove.Task_FlyTo(
                NavmeshMovement.ResolvePathPoint(approachPoint),
                waitForBusy: false,
                NavmeshMovement.GroundValidationWalkRange,
                stayMounted: true);
            return false;
        }

        // Far on flat ground: ground-travel toward the fan (auto-mounts for long hops), still mounted.
        if (NavmeshMovement.HorizontalDistance(approachPoint) > NavmeshMovement.GroundValidationWalkRange)
        {
            Task_NavmeshMove.Task_GroundTo(approachPoint, waitForBusy: false, NavmeshMovement.GroundValidationWalkRange);
            return false;
        }

        // Final settle: dismount and walk the last few yalms onto the gather fan.
        if (Player.Mounted)
        {
            Utils.Dismount();
            return false;
        }

        return Task_NavmeshMove.Task_GroundTo(
            approachPoint,
            waitForBusy: false,
            NavmeshMovement.FinalApproachCloseRange) == true
            && NavmeshMovement.IsWithinGatherInteractRange(liveNode);
    }

    /// <summary>Final ground settle onto the gather fan until the live node is within game interact range.</summary>
    public static bool WalkToInteractNode(IGameObject node)
    {
        if (Player.Mounted)
        {
            Utils.Dismount();
            return false;
        }

        if (NavmeshMovement.IsWithinGatherInteractRange(node))
            return true;

        // Reuse the gather-fan point EnqueueApproach already resolved so the random fan pick stays put.
        var approachPoint = _gatherFanPoint is { } cached && _gatherFanNodeId == node.BaseId
            ? cached
            : NavmeshMovement.GetInteractApproachPoint(node);
        _gatherFanNodeId = node.BaseId;
        _gatherFanPoint = approachPoint;

        var arrived = Task_NavmeshMove.Task_GroundTo(
            approachPoint,
            waitForBusy: true,
            NavmeshMovement.FinalApproachCloseRange) == true;

        if (!arrived)
            return false;

        if (NavmeshMovement.IsWithinGatherInteractRange(node))
            return true;

        if (EzThrottler.Throttle($"Short of interact range {node.BaseId}", 2000))
            IceLogging.Debug(
                $"Reached approach point but still {Player.DistanceTo(node):N2}y from node {node.BaseId} (need {NavmeshMovement.GatherInteractDistance}y)");

        return false;
    }

    /// <summary>
    /// Queue the close approach to the route's gather fan: fly up to it when far/elevated, then settle and
    /// interact; otherwise just ground-settle and interact. The gather fan comes from the route NodeLocation
    /// (configured fan / walk spots), falling back to a computed point only when none is set.
    /// </summary>
    public static void EnqueueApproach(IGameObject node, GatheringNode group, NodeLocation targetLocation)
    {
        var standPoint = ResolveGatherStandPoint(targetLocation, node);

        _gatherFanNodeId = node.BaseId;
        _gatherFanPoint = standPoint;
        _gatherFanLocation = targetLocation;

        if (NavmeshMovement.ShouldFlyToGatherStand(targetLocation, standPoint))
        {
            IceLogging.Debug($"Approach: fly up to gather fan, then settle on node {node.BaseId}");
            P.taskManager.EnqueueMulti
            (
                new(() => Task_NavmeshMove.Task_FlyTo(NavmeshMovement.ResolvePathPoint(standPoint), true, NavmeshMovement.GroundValidationWalkRange, true), "Fly to gather fan", TaskConfig),
                new(() => WalkToInteractNode(node), "Settle on gather fan", TaskConfig),
                new(() => Task_GatherRoute.InteractWithNode(node.BaseId), "Interact with node", TaskConfig)
            );
        }
        else
        {
            IceLogging.Debug($"Approach: ground walk to gather fan on node {node.BaseId}");
            P.taskManager.EnqueueMulti
            (
                new(() => WalkToInteractNode(node), "Walk to gather fan", TaskConfig),
                new(() => Task_GatherRoute.InteractWithNode(node.BaseId), "Interact with node", TaskConfig)
            );
        }
    }

    /// <summary>
    /// Called each tick while waiting for the gathering window. Returns true when gathering started.
    /// Re-targets and re-walks to gather fan if interact failed but the node is still targetable.
    /// </summary>
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

        if (_gatherFanPoint is { } approachPt && _gatherFanNodeId == nodeId
            && !NavmeshMovement.IsWithinGatherInteractRange(targetNode))
        {
            if (Player.Mounted)
            {
                Utils.Dismount();
                return false;
            }

            // Re-path to the cached gather-fan stand point — don't re-roll the random fan each tick.
            if (P.navmesh.TryMoveTo(approachPt, fly: false, NavmeshMovement.FinalApproachCloseRange))
                return false;
        }

        if (Player.Mounted)
            Utils.Dismount();

        if (!Player.Mounted && !Player.IsJumping && targetNode != null
            && NavmeshMovement.IsWithinGatherInteractRange(targetNode)
            && EzThrottler.Throttle("Target + Interaction throttle"))
        {
            Utils.TargetgameObject(targetNode);
            Utils.InteractWithObject(targetNode);
        }

        return false;
    }

    /// <summary>
    /// Where to stand to gather: the route author's configured gather fan / walk spots, drift-corrected to
    /// the live node and snapped onto floor. Only when a node has no gather fan configured do we fall back
    /// to a point computed from the live node toward the player.
    /// </summary>
    private static Vector3 ResolveGatherStandPoint(NodeLocation location, IGameObject node)
    {
        var nodeCenter = node.Position;
        var hasWalkSpots = location.UseSpecificWalkingSpots && location.WalkablePositions.Count > 0;

        if (hasWalkSpots || HasGatherFan(location))
            return NavmeshMovement.ResolveGatherApproachPoint(
                location.GetRandomGatherPosition(Player.Position, nodeCenter), nodeCenter);

        return NavmeshMovement.GetInteractApproachPoint(node);
    }

    /// <summary>True when the author set a real gather-fan arc, not the default 0–0 stub.</summary>
    private static bool HasGatherFan(NodeLocation location) =>
        MathF.Abs(location.Gathering_FanInfo.Fan_EndAngle - location.Gathering_FanInfo.Fan_StartAngle) > 0.01f;
}
