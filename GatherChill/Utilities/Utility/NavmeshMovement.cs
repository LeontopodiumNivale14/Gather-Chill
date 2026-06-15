using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.Types;
using ECommons.GameHelpers;
using ECommons.UIHelpers.AddonMasterImplementations;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using GatherChill.ConfigFiles;
using GatherChill.GatheringInfo;
using GatherChill.IPC;
using Lumina.Excel.Sheets;
using System.Collections.Generic;
using static ECommons.UIHelpers.AddonMasterImplementations.AddonMaster;

namespace GatherChill.Utilities.Utility;

/// <summary>
/// Shared navmesh helpers used by route gathering and the route editor.
/// Keeps movement constants, fly/mount eligibility, node standoff math, and gathering-session guards
/// in one place so <see cref="Scheduler.Tasks.Task_NavmeshMove"/> and <see cref="Scheduler.Tasks.GatherRouteNavigation"/> stay thin.
/// </summary>
internal static unsafe class NavmeshMovement
{
    // Distance thresholds for the gather-route approach pipeline (see GatherRouteNavigation).
    public const float LoadRange = 75f;              // Client node list updates within this range
    public const float FanApproachCloseRange = 50f;  // Close enough to switch from fly fan to ground fan
    public const float FinalApproachCloseRange = 0.5f;
    public const float LongMoveMountDistance = 30f; // Ground moves beyond this auto-mount
    public const float PreferFlyDistance = 25f;       // Auto-fly when target is farther than this
    public const float NearbyObjectCullDistance = 30f;
    public const float InteractRetrySlack = 0.25f;  // Small tolerance on the final step to the node

    public static float InteractDistance => C.NavmeshInteractDistance;

    /// <summary>FFXIV gathering-node interact range (tighter than navmesh arrival).</summary>
    public const float GatherInteractDistance = 3f;

    public const float GroundValidationWalkRange = 5f; // Fly until this close, then dismount for a short ground walk
    /// <summary>Max horizontal drift allowed when snapping a gather point onto navmesh floor.</summary>
    public const float GatherFanMaxNavDrift = 2f;
    /// <summary>Max horizontal drift from route anchor to accept a spawned gathering object.</summary>
    public const float SpawnMatchDistance = 5f;

    /// <summary>Keep approach points off node centers embedded in walls/cliffs.</summary>
    public const float GatherNodeStandoff = 3f;

    /// <summary>Below this horizontal distance from node center, treat the point as "on" the node and apply standoff.</summary>
    private const float NodeCenterEpsilon = 0.5f;

    // One-shot flag: we only StopPath() once per gather window so vnavmesh keeps its loaded mesh.
    private static bool _haltedNavForGathering;

    /// <summary>True while a gather session is actually in progress (not merely addon memory).</summary>
    public static bool IsGatheringSessionActive()
    {
        if (Svc.Condition[ConditionFlag.Gathering])
            return true;

        if (Svc.Condition[ConditionFlag.ExecutingGatheringAction])
            return true;

        if (GenericHelpers.TryGetAddonMaster<GatheringMasterpiece>("GatheringMasterpiece", out var collectable) && collectable.IsAddonReady)
            return true;

        return false;
    }

    /// <summary>Stop path following once per gather session. Do not call StopCompletely/PathfindCancelAll — that can reset vnavmesh.</summary>
    public static void HaltNavmeshForGathering()
    {
        if (_haltedNavForGathering)
            return;

        _haltedNavForGathering = true;
        P.navmesh.StopPath();
        if (NavmeshRuntime.OwnsPath)
            NavmeshRuntime.SetOwnsPath(false);
    }

    public static void ResetGatheringNavHalt()
    {
        _haltedNavForGathering = false;
        P.navmesh.NotifyGatheringSessionEnded();
    }

    public static float HorizontalDistanceBetween(Vector3 a, Vector3 b)
    {
        var delta = a - b;
        return MathF.Sqrt(delta.X * delta.X + delta.Z * delta.Z);
    }

    public static bool IsWithinHorizontalRange(Vector3 destination, float range) =>
        P.navmesh.IsWithinHorizontalRange(destination, range);

    public static float HorizontalDistance(Vector3 destination)
    {
        var delta = Player.Position - destination;
        return MathF.Sqrt(delta.X * delta.X + delta.Z * delta.Z);
    }

    public static bool IsFlyingUnlocked()
    {
        var uiState = UIState.Instance();
        return uiState != null && uiState->PlayerState.CanFly;
    }

    public static bool CanMountInCurrentTerritory()
    {
        var row = Svc.Data.GetExcelSheet<TerritoryType>()?.GetRowOrDefault(Player.Territory.RowId);
        return row != null && row.Value.Mount;
    }

    public static bool CanUseFlyMovement() =>
        IsFlyingUnlocked() && CanMountInCurrentTerritory();

    public static bool ShouldUseFlyPath(Vector3 target) =>
        CanUseFlyMovement() && Player.DistanceTo(target) > PreferFlyDistance;

    public static bool WantsFlyPath(bool requestFly, Vector3 target) =>
        requestFly ? CanUseFlyMovement() : ShouldUseFlyPath(target);

    /// <summary>Vertical gap above the player past which we ascend by flying instead of walking up.</summary>
    public const float FlyUpHeightThreshold = 2f;

    /// <summary>
    /// Fly to the gather stand point when it is far OR meaningfully above us. The height term is what
    /// rescues ledge nodes ("GBR's problem"): the author's gather fan sits up on the ledge, so we fly up
    /// onto it instead of ground-walking into the cliff below.
    /// </summary>
    public static bool ShouldFlyToGatherStand(NodeLocation node, Vector3 standPoint) =>
        node.AllowFlying && CanUseFlyMovement() && !Svc.Condition[ConditionFlag.Diving]
        && (Player.DistanceTo(standPoint) > PreferFlyDistance || standPoint.Y - Player.Position.Y > FlyUpHeightThreshold);

    public static bool IsNearGameObject(IGameObject gameObject, float distance) =>
        Player.DistanceTo(gameObject) <= distance;

    public static bool IsWithinLoadRangeOf(Vector3 position) =>
        Player.DistanceTo(position) <= LoadRange;

    public static bool IsNodeInClientRange(IGameObject node) =>
        IsWithinLoadRangeOf(node.Position);

    /// <summary>Within 75y the client populates gathering points; returns targetable node at this route spot if up.</summary>
    public static IGameObject? GetAvailableNodeAtLocation(uint baseId, Vector3 routeLocation, float maxDistance = SpawnMatchDistance)
    {
        var node = GetGatheringNodeNearLocation(baseId, routeLocation, maxDistance);
        if (node == null)
            return null;

        if (IsNodeInClientRange(node) || IsWithinLoadRangeOf(routeLocation))
            return node;

        return null;
    }

    public static IGameObject? GetNearestGatheringNode(uint baseId) =>
        Svc.Objects
            .Where(obj => obj.BaseId == baseId)
            .Where(obj => obj.IsTargetable)
            .Where(obj => obj.ObjectKind == ObjectKind.GatheringPoint)
            .OrderBy(obj => Player.DistanceTo(obj))
            .FirstOrDefault();

    /// <summary>Best targetable node to interact with after walking the route gather fan.</summary>
    public static IGameObject? ResolveInteractNode(uint baseId, Vector3 routeLocation, IGameObject? knownNode = null)
    {
        if (knownNode != null && knownNode.IsTargetable && knownNode.BaseId == baseId
            && IsNearGameObject(knownNode, InteractDistance * 1.5f))
            return knownNode;

        var atAnchor = GetGatheringNodeNearLocation(baseId, routeLocation);
        if (atAnchor != null && IsNearGameObject(atAnchor, InteractDistance * 1.5f))
            return atAnchor;

        var nearest = GetNearestGatheringNode(baseId);
        if (nearest != null && IsNearGameObject(nearest, InteractDistance * 2f))
            return nearest;

        return null;
    }

    public static bool LocationsRoughlyMatch(Vector3 routePosition, Vector3 worldPosition, float maxDistance = SpawnMatchDistance) =>
        HorizontalDistanceBetween(routePosition, worldPosition) <= maxDistance;

    public static IGameObject? GetGatheringNodeNearLocation(uint baseId, Vector3 routeLocation, float maxDistance = SpawnMatchDistance) =>
        Svc.Objects
            .Where(obj => obj.BaseId == baseId)
            .Where(obj => obj.IsTargetable)
            .Where(obj => obj.ObjectKind == ObjectKind.GatheringPoint)
            .Where(obj => LocationsRoughlyMatch(routeLocation, obj.Position, maxDistance))
            .OrderBy(obj => Vector3.Distance(routeLocation, obj.Position))
            .FirstOrDefault();

    /// <summary>Route location whose anchor best matches a spawned node within <see cref="SpawnMatchDistance"/>.</summary>
    public static (NodeLocation location, IGameObject node)? FindSpawnedNodeInGroup(GatheringNode group, float maxDistance = SpawnMatchDistance)
    {
        (NodeLocation location, IGameObject node)? best = null;
        var bestAnchorDist = float.MaxValue;

        foreach (var location in group.Locations)
        {
            var node = GetGatheringNodeNearLocation(group.NodeId, location.Position, maxDistance);
            if (node == null || !IsNodeInClientRange(node))
                continue;

            var anchorDist = HorizontalDistanceBetween(location.Position, node.Position);
            if (anchorDist < bestAnchorDist)
            {
                bestAnchorDist = anchorDist;
                best = (location, node);
            }
        }

        return best;
    }

    /// <summary>Spawned group whose node is closest to the player (any route group with an active spawn).</summary>
    public static (int index, GatheringNode group, NodeLocation location, IGameObject node)? FindNearestSpawnedGroup(
        IReadOnlyList<GatheringNode> groups)
    {
        (int index, GatheringNode group, NodeLocation location, IGameObject node)? best = null;
        var bestDist = float.MaxValue;

        for (var i = 0; i < groups.Count; i++)
        {
            var spawn = FindSpawnedNodeInGroup(groups[i]);
            if (spawn == null)
                continue;

            var dist = Player.DistanceTo(spawn.Value.node.Position);
            if (dist < bestDist)
            {
                bestDist = dist;
                best = (i, groups[i], spawn.Value.location, spawn.Value.node);
            }
        }

        return best;
    }

    /// <summary>True when every location in the group is within load range and none has a spawned node.</summary>
    public static bool IsGroupConfirmedUnavailable(GatheringNode group)
    {
        if (group.Locations.Count == 0)
            return true;

        if (FindSpawnedNodeInGroup(group) != null)
            return false;

        return group.Locations.All(loc => Player.DistanceTo(loc.Position) <= LoadRange);
    }

    /// <summary>
    /// Route fan points can land on the node center; many nodes sit in cliffs/walls.
    /// Only pushes when the point sits on the node center — configured gather fans (1–2y out) are kept as-is.
    /// </summary>
    public static Vector3 ApplyNodeStandoff(Vector3 approachPoint, Vector3 nodePos, float minHorizontalDistance = GatherNodeStandoff)
    {
        var offset = approachPoint - nodePos;
        offset.Y = 0;
        if (offset.Length() >= NodeCenterEpsilon)
            return approachPoint;

        var towardPlayer = Player.Position - nodePos;
        towardPlayer.Y = 0;
        if (towardPlayer.LengthSquared() < 0.01f)
            towardPlayer = new Vector3(0, 0, 1f);

        towardPlayer = Vector3.Normalize(towardPlayer);
        var standoff = nodePos + towardPlayer * minHorizontalDistance;
        standoff.Y = approachPoint.Y;
        return standoff;
    }

    /// <summary>Close enough for the client to accept a gathering-node interact.</summary>
    public static bool IsWithinGatherInteractRange(IGameObject node) =>
        Player.DistanceTo(node) <= GatherInteractDistance + InteractRetrySlack;

    /// <summary>Ground point to stand on when interacting — just inside game interact range on the approach side.</summary>
    public static Vector3 GetInteractApproachPoint(IGameObject node)
    {
        var nodePos = node.Position;
        var towardPlayer = Player.Position - nodePos;
        towardPlayer.Y = 0;
        if (towardPlayer.LengthSquared() < 0.01f)
            towardPlayer = new Vector3(0, 0, 1f);

        towardPlayer = Vector3.Normalize(towardPlayer);
        var approachRadius = GatherInteractDistance - 0.5f;
        var raw = nodePos + towardPlayer * approachRadius;
        raw.Y = nodePos.Y;
        return ResolveGatherApproachPoint(raw, nodePos);
    }

    /// <summary>
    /// Snap onto walkable floor but reject snaps that slid the point far from where we asked
    /// (e.g. onto a nearby path/ledge). Drift is measured from the requested point, not the node
    /// anchor — approach/fan points are intentionally 2–3y off the node, so anchoring the check
    /// there would reject every valid point and fall through to the standoff.
    /// </summary>
    public static Vector3 ResolveGatherApproachPoint(Vector3 position, Vector3 nodeAnchor)
    {
        if (!P.navmesh.Installed || !P.navmesh.IsReady())
            return position;

        var snapped = ResolveGroundPathPoint(position);
        if (HorizontalDistanceBetween(snapped, position) <= GatherFanMaxNavDrift)
            return snapped;

        var indoor = IsIndoorTerritory(Player.Territory.RowId);
        var underNode = P.navmesh.TryGetPointOnFloor(nodeAnchor, indoor, 1.5f);
        if (underNode != null)
            return ApplyNodeStandoff(underNode.Value, nodeAnchor, GatherNodeStandoff);

        return ApplyNodeStandoff(position, nodeAnchor, GatherNodeStandoff);
    }

    /// <summary>Snap gather-fan coordinates onto walkable floor at the target XZ, not the player's altitude.</summary>
    public static Vector3 ResolveGroundPathPoint(Vector3 position)
    {
        if (!P.navmesh.Installed || !P.navmesh.IsReady())
            return position;

        var indoor = IsIndoorTerritory(Player.Territory.RowId);
        var snapped = P.navmesh.TryGetPointOnFloor(position, indoor, 4f);
        return snapped ?? position;
    }

    /// <summary>
    /// Snap editor/route coordinates onto vnavmesh floor before pathfind. Handles indoor territories
    /// and elevated points (flying fans) without dropping the player below intended height.
    /// </summary>
    public static Vector3 ResolvePathPoint(Vector3 position)
    {
        if (!P.navmesh.Installed || !P.navmesh.IsReady())
            return position;

        var indoor = IsIndoorTerritory(Player.Territory.RowId);

        if (position.Y > 1f)
        {
            var snapped = P.navmesh.TryGetPointOnFloor(position, indoor, 2f);
            if (snapped != null && snapped.Value.Y >= position.Y - 1.5f)
                return snapped.Value;

            return position;
        }

        var playerFloor = P.navmesh.TryGetPointOnFloor(Player.Position, indoor) ?? Player.Position;
        var candidate = new Vector3(position.X, playerFloor.Y, position.Z);
        var floor = P.navmesh.TryGetPointOnFloor(candidate, indoor, 4f);
        if (floor != null && floor.Value.Y >= playerFloor.Y - 1.5f)
            return floor.Value;

        return candidate;
    }

    private static bool IsIndoorTerritory(uint territoryTypeId)
    {
        var row = Svc.Data.GetExcelSheet<TerritoryType>().GetRow(territoryTypeId);
        return row is { } t && t.TerritoryIntendedUse.RowId is 0 or 1 or 6;
    }
}
