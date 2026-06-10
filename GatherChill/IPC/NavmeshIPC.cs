using ECommons.EzIpcManager;
using ECommons.GameHelpers;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Utility;
using System;
using System.Collections.Generic;

#nullable disable
namespace GatherChill.IPC;

/// <summary>
/// IPC bridge to the vnavmesh plugin. Higher-level movement lives in
/// <see cref="Scheduler.Tasks.Task_NavmeshMove"/>; this class wraps EzIPC calls,
/// mesh loading, and safe stop helpers (StopIfOwned vs StopCompletely).
/// </summary>
public class NavmeshIPC
{
    public const string Name = "vnavmesh";
    public const string Repo = "https://puni.sh/api/repository/veyn";

    private static uint _lastTerritoryId;
    private static bool _reloadRequestedForTerritory;
    private static DateTime _lastReloadAttempt = DateTime.MinValue;

    public NavmeshIPC()
    {
        EzIPC.Init(this, Name);
        TryEnableAutoLoad();
    }

    public bool Installed => Utils.HasPlugin(Name);

    // --- Nav: mesh build / reload ---
    [EzIPC("Nav.%m")] public readonly Func<bool> IsReady;
    [EzIPC("Nav.%m")] public readonly Func<float> BuildProgress;
    [EzIPC("Nav.%m")] public readonly Func<bool> Reload;
    [EzIPC("Nav.%m")] public readonly Func<bool> Rebuild;
    [EzIPC("Nav.IsAutoLoad")] public readonly Func<bool> IsAutoLoad;
    [EzIPC("Nav.SetAutoLoad")] public readonly Action<bool> SetAutoLoad;
    [EzIPC("Nav.PathfindCancelAll")] public readonly Action NavPathfindCancelAll;
    [EzIPC("Nav.PathfindInProgress")] public readonly Func<bool> NavPathfindInProgress;
    [EzIPC("Nav.%m")] public readonly Func<Vector3, Vector3, bool, Vector3> Pathfind;

    // --- SimpleMove: one-shot pathfind + follow (used by Task_NavmeshMove) ---
    [EzIPC("SimpleMove.%m")] public readonly Func<Vector3, bool, bool> PathfindAndMoveTo;
    [EzIPC("SimpleMove.PathfindAndMoveCloseTo")] public readonly Func<Vector3, bool, float, bool> PathfindAndMoveCloseTo;
    [EzIPC("SimpleMove.%m")] public readonly Func<bool> PathfindInProgress;

    // --- Path: ongoing path control ---
    [EzIPC("Path.%m")] public readonly Action<List<Vector3>, bool> MoveTo;
    [EzIPC("Path.Stop")] public readonly Action PathStop;
    [EzIPC("Path.%m")] public readonly Action<bool> SetAlignCamera;
    [EzIPC("Path.%m")] public readonly Func<bool> IsRunning;
    [EzIPC("Path.%m")] public readonly Action<float> SetTolerance;

    // --- Query.Mesh: snap editor points onto walkable mesh ---
    [EzIPC("Query.Mesh.%m")] public readonly Func<Vector3, float, float, Vector3?> NearestPoint;
    [EzIPC("Query.Mesh.%m")] public readonly Func<Vector3, bool, float, Vector3?> PointOnFloor;
    [EzIPC("Query.Mesh.%m")] public readonly Func<Vector3, float, float, Vector3?> NearestPointReachable;

    // SmartNav IPC disabled until vnavmesh SmartNav API is updated (cross-zone travel will use Lifestream + SimpleMove).
    // [EzIPC("SmartNav.%m")] public readonly Action PathToFlag;
    // [EzIPC("SmartNav.Stop")] public readonly Action SmartNavStop;
    // [EzIPC("SmartNav.IsRunning")] public readonly Func<bool> SmartIsRunning;
    // [EzIPC("SmartNav.PathToPoint")] public readonly Action<uint, Vector3> Smart_PathToPoint;
    // [EzIPC("SmartNav.PathToTerritory")] public readonly Action<uint> Smart_PathToTerritory;

    public bool NavRunning() => IsRunning();

    public bool IsPathfindInProgress() =>
        (PathfindInProgress?.Invoke() ?? false) || (NavPathfindInProgress?.Invoke() ?? false);

    public bool IsMoving() => IsRunning() || IsPathfindInProgress();

    public float GetBuildProgress() => Installed ? BuildProgress() : -1f;

    /// <summary>vnavmesh reports -1 when idle; [0, 1) while loading from cache or building.</summary>
    public bool IsBuildInProgress()
    {
        var progress = GetBuildProgress();
        return progress >= 0f && progress < 1f;
    }

    private void TryEnableAutoLoad()
    {
        if (!Installed || IsAutoLoad == null || SetAutoLoad == null)
            return;

        try
        {
            if (IsAutoLoad() == false)
                SetAutoLoad(true);
        }
        catch (Exception ex)
        {
            IceLogging.Verbose($"Could not enable vnavmesh auto-load: {ex.Message}");
        }
    }

    /// <summary>
    /// Nudge vnavmesh to load the current zone once. Repeated Reload() cancels in-progress builds (Mesh stuck at 0%).
    /// </summary>
    public void TryEnsureNavMeshLoading()
    {
        if (!Installed)
            return;

        // Never poke vnavmesh load/reload during harvest — Reload() clears mesh (Mesh: 0%).
        if (NavmeshMovement.IsGatheringSessionActive())
            return;

        if (IsReady())
        {
            _reloadRequestedForTerritory = false;
            return;
        }

        if (IsBuildInProgress())
            return;

        var territoryId = Player.Territory.RowId;
        if (_lastTerritoryId != territoryId)
        {
            _lastTerritoryId = territoryId;
            _reloadRequestedForTerritory = false;
        }

        if (_reloadRequestedForTerritory)
            return;

        var now = DateTime.UtcNow;
        if ((now - _lastReloadAttempt).TotalSeconds < 10)
            return;

        _reloadRequestedForTerritory = true;
        _lastReloadAttempt = now;
        Reload?.Invoke();
    }

    public Vector3? TryGetPointOnFloor(Vector3 position, bool allowUnlandable = false, float halfExtentXz = 3f) =>
        Installed && PointOnFloor(position, allowUnlandable, halfExtentXz) is { } point
            ? point
            : null;

    public bool IsWithinHorizontalRange(Vector3 destination, float range)
    {
        var delta = Player.Position - destination;
        return delta.X * delta.X + delta.Z * delta.Z <= range * range;
    }

    public bool TryPathfindAndMoveTo(Vector3 destination, bool fly = false) =>
        Installed && IsReady() && PathfindAndMoveTo(destination, fly);

    public bool TryPathfindAndMoveCloseTo(Vector3 destination, bool fly, float range) =>
        Installed && IsReady() && PathfindAndMoveCloseTo(destination, fly, range);

    /// <summary>Pathfind toward destination; optional horizontal closeRange treats arrival as success.</summary>
    public bool TryMoveTo(Vector3 destination, bool fly, float closeRange = 0f)
    {
        if (closeRange > 0f && IsWithinHorizontalRange(destination, closeRange))
            return true;

        var started = closeRange > 0f
            ? TryPathfindAndMoveCloseTo(destination, fly, closeRange)
            : TryPathfindAndMoveTo(destination, fly);

        if (!started)
            return false;

        return IsMoving() || (closeRange > 0f && IsWithinHorizontalRange(destination, closeRange));
    }

    public bool TickArrival(Vector3 destination, float closeRange)
    {
        if (IsWithinHorizontalRange(destination, closeRange) && IsMoving())
            StopPath();

        return IsWithinHorizontalRange(destination, closeRange) && !IsMoving();
    }

    public void StopPath()
    {
        if (Installed)
            PathStop();
    }

    public void StopCompletely()
    {
        if (!Installed)
            return;

        PathStop();
        NavPathfindCancelAll?.Invoke();
    }

    /// <summary>
    /// Stop only when we started the path. Avoids cancelling unrelated vnavmesh usage
    /// (manual /other plugins) and is safer than StopCompletely during gathering.
    /// </summary>
    public void StopIfOwned()
    {
        if (!NavmeshRuntime.OwnsPath)
            return;

        StopCompletely();
        NavmeshRuntime.SetOwnsPath(false);
    }

    public bool IsPathingOrFinding() => IsMoving();

    public void SmartPath(uint territory, Vector3? position = null)
    {
        // SmartNav disabled — see commented IPC above.
    }

    public void SmartStop() => StopCompletely();
}
