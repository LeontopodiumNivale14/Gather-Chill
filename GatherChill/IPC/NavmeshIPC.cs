using ECommons.EzIpcManager;
using ECommons.GameHelpers;
using GatherChill.Utilities.Utility;
using System;
using System.Collections.Generic;

#nullable disable
namespace GatherChill.IPC;

public class NavmeshIPC
{
    public const string Name = "vnavmesh";
    public const string Repo = "https://puni.sh/api/repository/veyn";

    public NavmeshIPC() => EzIPC.Init(this, Name);
    public bool Installed => Utils.HasPlugin(Name);

    [EzIPC("Nav.%m")] public readonly Func<bool> IsReady;
    [EzIPC("Nav.%m")] public readonly Func<float> BuildProgress;
    [EzIPC("Nav.%m")] public readonly Func<bool> Reload;
    [EzIPC("Nav.%m")] public readonly Func<bool> Rebuild;
    [EzIPC("Nav.PathfindCancelAll")] public readonly Action NavPathfindCancelAll;
    [EzIPC("Nav.PathfindInProgress")] public readonly Func<bool> NavPathfindInProgress;
    [EzIPC("Nav.%m")] public readonly Func<Vector3, Vector3, bool, Vector3> Pathfind;

    [EzIPC("SimpleMove.%m")] public readonly Func<Vector3, bool, bool> PathfindAndMoveTo;
    [EzIPC("SimpleMove.PathfindAndMoveCloseTo")] public readonly Func<Vector3, bool, float, bool> PathfindAndMoveCloseTo;
    [EzIPC("SimpleMove.%m")] public readonly Func<bool> PathfindInProgress;

    [EzIPC("Path.%m")] public readonly Action<List<Vector3>, bool> MoveTo;
    [EzIPC("Path.Stop")] public readonly Action PathStop;
    [EzIPC("Path.%m")] public readonly Action<bool> SetAlignCamera;
    [EzIPC("Path.%m")] public readonly Func<bool> IsRunning;
    [EzIPC("Path.%m")] public readonly Action<float> SetTolerance;

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

    public bool IsBuildInProgress()
    {
        var progress = GetBuildProgress();
        return progress >= 0f && progress < 1f;
    }

    public void TryEnsureNavMeshLoading()
    {
        if (!Installed || IsReady())
            return;

        if (IsBuildInProgress())
            return;

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
