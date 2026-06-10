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
    public const float LoadRange = 75f;              // Skip nav if already within node load range
    public const float FanApproachCloseRange = 50f;  // Close enough to switch from fly fan to ground fan
    public const float FinalApproachCloseRange = 0.5f;
    public const float LongMoveMountDistance = 30f; // Ground moves beyond this auto-mount
    public const float PreferFlyDistance = 25f;       // Auto-fly when target is farther than this
    public const float NearbyObjectCullDistance = 30f;
    public const float InteractRetrySlack = 0.5f;     // Extra tolerance when re-walking to gather fan after failed interact

    public static float InteractDistance => C.NavmeshInteractDistance;

    public const float GatherFanCloseRange = FinalApproachCloseRange;

    /// <summary>Keep approach points off node centers embedded in walls/cliffs.</summary>
    public const float GatherNodeStandoff = 3f;

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

    public static void ResetGatheringNavHalt() => _haltedNavForGathering = false;

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

    public static bool ShouldUseFlyPathForNode(NodeLocation node, Vector3 target) =>
        node.AllowFlying && ShouldUseFlyPath(target);

    public static bool ShouldUseFlyApproachForNode(NodeLocation node, Vector3 target) =>
        node.AllowFlying && ShouldUseFlyPath(target) && !Svc.Condition[ConditionFlag.Diving];

    public static bool IsNearGameObject(IGameObject gameObject, float distance) =>
        Player.DistanceTo(gameObject) <= distance;

    public static IGameObject? GetNearestGatheringNode(uint baseId) =>
        Svc.Objects
            .Where(obj => obj.BaseId == baseId)
            .Where(obj => obj.IsTargetable)
            .Where(obj => obj.ObjectKind == ObjectKind.GatheringPoint)
            .OrderBy(obj => Player.DistanceTo(obj))
            .FirstOrDefault();

    public static bool IsNearGatheringNode(uint baseId, float distance)
    {
        var node = GetNearestGatheringNode(baseId);
        return node != null && IsNearGameObject(node, distance);
    }

    /// <summary>
    /// Route fan points can land on the node center; many nodes sit in cliffs/walls.
    /// Push the standoff horizontally toward the player so pathfinding targets walkable ground.
    /// </summary>
    public static Vector3 ApplyNodeStandoff(Vector3 approachPoint, Vector3 nodePos, float minHorizontalDistance = GatherNodeStandoff)
    {
        var offset = approachPoint - nodePos;
        offset.Y = 0;
        if (offset.Length() >= minHorizontalDistance)
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
