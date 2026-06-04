using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.Types;
using ECommons.GameHelpers;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using GatherChill.ConfigFiles;
using GatherChill.GatheringInfo;
using GatherChill.IPC;
using Lumina.Excel.Sheets;

namespace GatherChill.Utilities.Utility;

internal static unsafe class NavmeshMovement
{
    public const float LoadRange = 75f;
    public const float FanApproachCloseRange = 50f;
    public const float FinalApproachCloseRange = 0.5f;
    public const float LongMoveMountDistance = 30f;
    public const float PreferFlyDistance = 25f;
    public const float NearbyObjectCullDistance = 30f;
    public const float InteractRetrySlack = 0.5f;

    public static float InteractDistance => C.NavmeshInteractDistance;

    public const float GatherFanCloseRange = FinalApproachCloseRange;

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
