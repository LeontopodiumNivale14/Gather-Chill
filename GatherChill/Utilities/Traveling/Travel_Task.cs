using Dalamud.Game.ClientState.Conditions;
using ECommons.GameHelpers;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game;
using GatherChill.GatheringInfo;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Utility;

namespace GatherChill.Utilities.Traveling;

public static partial class TravelUtil
{
    private static (uint baseId, bool flyingRequired) LastNode = (0, false);

    public static void Gathering_TravelCheck(NodeLocation node)
    {
        bool flyingRequired = node.RequiresFlying;
        bool optionalFly = C.OptionalFly;
        bool minFlyDistance = C.Fly_MinDistance < Player.DistanceTo(node.Position);

        var navtask = P.navTask;

        if (flyingRequired || LastNode.flyingRequired || (optionalFly && minFlyDistance))
            navtask.Enqueue(() => Task_FlyTo(Gather_RandomFanPosition(node, true), false, 50, true), "Locating node: Flying");
        else if (Svc.Condition[ConditionFlag.Diving])
            navtask.Enqueue(() => Task_SwimTo(Gather_RandomFanPosition(node, false), false, 50, true), "Locating node: Swimming");
        else
            navtask.Enqueue(() => Task_GroundTo(Gather_RandomFanPosition(node, false), false, 50, true), "Locating node: Ground");

    }
    public static void Gathering_TravelToNode(NodeLocation node, uint baseId)
    {
        bool UpdateLastInfo()
        {
            LastNode.baseId = baseId;
            LastNode.flyingRequired = node.RequiresFlying;
            return true;
        }

        void EnqueueFly()
        {
            P.navTask.EnqueueMulti
            (
                new(() => Task_FlyTo(Gather_RandomFanPosition(node, true)), "Flying to destination"),
                new(() => Task_GroundTo(Gather_RandomFanPosition(node, false)), "Moving closer to the node"),
                new(() => Task_MoveCloser(node.Position), "Last check to make sure we're close enough"),
                new(UpdateLastInfo, "Setting last Node Info")
            );
        }

        if (node.RequiresFlying || Svc.Condition[ConditionFlag.InFlight])
        {
            EnqueueFly();
            return;
        }

        if (LastNode.flyingRequired && LastNode.baseId != baseId)
        {
            EnqueueFly();
            return;
        }

        if (Svc.Condition[ConditionFlag.Diving])
        {
            P.navTask.EnqueueMulti
            (
                new(() => Task_SwimTo(Gather_RandomFanPosition(node, false)), "Moving closer to the node"),
                new(() => Task_MoveCloser(node.Position), "Last check to make sure we're close enough"),
                new(UpdateLastInfo, "Setting last Node Info")
            );
            return;
        }

        // Distance on the left: "far enough to be worth flying"
        bool farEnoughToFly = Player.DistanceTo(node.Position) >= C.Fly_MinDistance;
        if (C.OptionalFly && farEnoughToFly)
        {
            EnqueueFly();
            return;
        }

        P.navTask.EnqueueMulti
        (
            new(() => Task_GroundTo(Gather_RandomFanPosition(node, false)), "Moving closer to the node"),
            new(() => Task_MoveCloser(node.Position), "Last check to make sure we're close enough"),
            new(UpdateLastInfo, "Setting last Node Info")
        );
    }

    // - - - - Task for the above ^ Because I can't be bothered to throw this in a region - - - - //
    public static bool Task_GroundTo(Vector3 pos, bool waitForBusy = true, float distance = 2.0f, bool stayMounted = false)
    {
        const string tag = "Navmesh: Ground -> Destination";
        Vector2 v2Pos = new(pos.X, pos.Z);

        var currentDistance = Player.DistanceTo(v2Pos);

        bool useMount = C.UseMount && Player.CanMount;
        int mount_MinDistance = C.Mount_MinDistance;
        int mount_DismountDist = C.Mount_DismountDistance;

        if (!P.navmesh.Installed)
        {
            IceLogging.Info("We seem to be missing navmesh... so we're just going to exit here", tag);
            return true;
        }
        else if (P.navmesh.IsRunning())
        {
            bool dismountRange = currentDistance < mount_DismountDist;
            bool mountRange = currentDistance > mount_MinDistance;

            if (C.AttemptToUnstuck)
            {
                if (CheckAndHandleStuck())
                    return false;
            }

            if (dismountRange && Player.Mounted)
            {
                if (EzThrottler.Throttle("Dismounting off the mount"))
                    IceLogging.Verbose("We're withing dismount range, so going to stay off the mount", tag);

                Utils.Dismount();
            }
            else if (useMount && !Player.Mounted && mountRange && !dismountRange)
            {
                Utils.MountAction();
            }


            if (Player.IsMoving && waitForBusy)
            {
                if (EzThrottler.Throttle("Busy_MoveCheck"))
                    IceLogging.Verbose("We're currently moving, and we were told to wait for us to not be busy. Waiting patiently.", tag);

                return false;
            }
            else if (!waitForBusy && currentDistance <= distance)
            {
                if (EzThrottler.Throttle("Busy_CloseEnough"))
                {
                    IceLogging.Verbose("We're within stopping distance, so stopping navmesh", tag);
                    P.navmesh.PathStop();
                }
            }
        }
        else if (!P.navmesh.IsReady())
        {
            if (EzThrottler.Throttle("Waiting on navmesh", 500))
            {
                var navProgress = P.navmesh.BuildProgress();
                IceLogging.Debug($"Waiting for navmesh to finish building. Currently at: {navProgress:N2}", tag);
            }
        }
        else if (!P.navmesh.IsRunning())
        {
            if (currentDistance < distance)
            {
                IceLogging.Verbose("We've met the distance threshold to our destination, continuing on", tag);
                ResetInfo();
                return true;
            }
            else
            {
                if (EzThrottler.Throttle("telling navmesh to start ground movement"))
                {
                    P.navmesh.SetTolerance(0.25f);
                    IceLogging.Verbose("We're setting the tolerance to 0.25f here", tag);
                    P.navmesh.PathfindAndMoveTo(pos, false);
                }
            }
        }

        return false;
    }
    public static bool Task_FlyTo(Vector3 pos, bool waitForBusy = true, float distance = 2.0f, bool stayMounted = false)
    {
        bool isFlying = Svc.Condition[ConditionFlag.InFlight];
        bool mounted = Player.Mounted;

        if (!P.navmesh.Installed)
            return true;

        else if (!P.navmesh.IsReady())
        {
            if (EzThrottler.Throttle("Waiting on navmesh", 1000))
            {
                var navProgress = P.navmesh.BuildProgress();
            }
        }
        else if (P.navmesh.IsRunning())
        {
            if (CheckAndHandleStuck())
                return false;

            if (!mounted)
            {
                // We should never be not mounting here, so going to just stop the current navmesh and restart it
                if (EzThrottler.Throttle("Emergency Navmesh Stop | Fly"))
                    P.navmesh.PathStop();

                return false;
            }

            if (Player.IsMoving && waitForBusy)
            {
                // We were told to wait for us to stop moving, so we're going to do so
                return false;
            }
            else if (!waitForBusy && Player.DistanceTo(pos) <= distance)
            {
                if (EzThrottler.Throttle("Telling navmesh to stop"))
                {
                    P.navmesh.PathStop();
                }
            }

        }
        else if (!P.navmesh.IsRunning())
        {
            if (Player.DistanceTo(new Vector2(pos.X, pos.Z)) < distance)
            // if (Player.DistanceTo(pos) < distance)
            {
                // We're close enough to the area, time to check for mounting/flying
                if (mounted && !stayMounted)
                {
                    Utils.Dismount();
                    return false;
                }
                else if (Player.IsJumping)
                {
                    return false;
                }
                else
                {
                    ResetInfo();
                    return true;
                }
            }
            else if (!Player.Mounted)
            {
                // We have fly set to true, but not mounted to actually fly so, starting with that
                if (EzThrottler.Throttle("Telling us to mount"))
                    Utils.MountAction();

                return false;
            }
            else
            {
                // All other conditions have been met to start navmesh moving
                // And we're not close enough to the point, so starting to move now
                if (EzThrottler.Throttle("Commence Navmesh Movement"))
                {
                    P.navmesh.SetTolerance(0.25f);
                    IceLogging.DestinationLogs.Log(pos);
                    P.navmesh.PathfindAndMoveTo(pos, true);
                }
            }
        }

        return false;
    }
    public static bool Task_SwimTo(Vector3 pos, bool waitForBusy = true, float distance = 2.0f, bool stayMounted = false)
    {
        const string tag = "Navmesh: Ground -> Destination";
        Vector2 v2Pos = new(pos.X, pos.Z);

        var currentDistance = Player.DistanceTo(pos);

        bool useMount = C.UseMount && Player.CanMount;
        int mount_MinDistance = C.Mount_MinDistance;
        int mount_DismountDist = C.Mount_DismountDistance;

        if (!P.navmesh.Installed)
        {
            IceLogging.Info("We seem to be missing navmesh... so we're just going to exit here", tag);
            return true;
        }
        else if (P.navmesh.IsRunning())
        {
            bool dismountRange = currentDistance < mount_DismountDist;
            bool mountRange = currentDistance > mount_MinDistance;

            if (C.AttemptToUnstuck)
            {
                if (CheckAndHandleStuck())
                    return false;
            }

            if (dismountRange && Player.Mounted)
            {
                if (EzThrottler.Throttle("Dismounting off the mount"))
                    IceLogging.Verbose("We're withing dismount range, so going to stay off the mount", tag);

                Utils.Dismount();
            }
            else if (useMount && !Player.Mounted && mountRange && !dismountRange)
            {
                Utils.MountAction();
            }


            if (Player.IsMoving && waitForBusy)
            {
                if (EzThrottler.Throttle("Busy_MoveCheck"))
                    IceLogging.Verbose("We're currently moving, and we were told to wait for us to not be busy. Waiting patiently.", tag);

                return false;
            }
            else if (!waitForBusy && currentDistance <= distance)
            {
                if (EzThrottler.Throttle("Busy_CloseEnough"))
                {
                    IceLogging.Verbose("We're within stopping distance, so stopping navmesh", tag);
                    P.navmesh.PathStop();
                }
            }
        }
        else if (!P.navmesh.IsReady())
        {
            if (EzThrottler.Throttle("Waiting on navmesh", 500))
            {
                var navProgress = P.navmesh.BuildProgress();
                IceLogging.Debug($"Waiting for navmesh to finish building. Currently at: {navProgress:N2}", tag);
            }
        }
        else if (!P.navmesh.IsRunning())
        {
            if (currentDistance < distance)
            {
                IceLogging.Verbose("We've met the distance threshold to our destination, continuing on", tag);
                ResetInfo();
                return true;
            }
            else
            {
                if (EzThrottler.Throttle("telling navmesh to start swim movement"))
                {
                    P.navmesh.SetTolerance(0.25f);
                    IceLogging.Verbose("We're setting the tolerance to 0.25f here", tag);
                    P.navmesh.PathfindAndMoveTo(pos, true);
                }
            }
        }

        return false;
    }
    public static bool Task_MoveCloser(Vector3 pos, float distance = 3.4f)
    {
        string tag = "Task: Move Closer Check";
        var currentDistance = Player.DistanceTo(pos);

        if (P.navmesh.IsRunning())
        {
            if (EzThrottler.Throttle("Waiting for navmesh to finish..."))
                IceLogging.Verbose("Waiting for navmesh to finish currently, so we wait", tag);
        }
        else if (currentDistance < distance)
        {
            IceLogging.Verbose("We don't need to move closer, so not gonna worry bout it", tag);
            return true;
        }
        else
        {
            IceLogging.Verbose($"We're not close enough to the node somehow??? Minimum interaction range is {distance:N0} (as far as we can tell...)", tag);
            IceLogging.Verbose("Telling navmesh to move closer with a hard stop distance", tag);

            P.navmesh.PathfindAndMoveCloseTo(pos, Svc.Condition[ConditionFlag.Diving], distance);
        }

        return false;
    }

    private static Vector3 _lastPosition = Vector3.Zero;
    private static long _lastPositionChangeTick = Environment.TickCount64;
    private static int _stuckAttempts = 0;
    private const float STUCK_DISTANCE_THRESHOLD = 0.5f; // Moved less than this = not making progress
    private const int STUCK_TIME_THRESHOLD = 3000;       // ms before considering stuck

    private static void ResetInfo()
    {
        _lastPosition = Player.Position;
        _lastPositionChangeTick = Environment.TickCount64;
        _stuckAttempts = 0;
    }

    private static unsafe bool CheckAndHandleStuck()
    {
        var currentPos = Player.Position;
        var now = Environment.TickCount64;

        if (!C.AttemptToUnstuck)
            return false;

        // Moved farther than the threshold -> not stuck, reset tracking
        if (Vector3.Distance(currentPos, _lastPosition) > STUCK_DISTANCE_THRESHOLD)
        {
            ResetInfo();
            return false;
        }

        // Haven't been still long enough yet
        if (now - _lastPositionChangeTick <= STUCK_TIME_THRESHOLD)
            return false;

        // Stuck. Only count an attempt when we actually act on it
        if (!EzThrottler.Throttle("Stuck - handling", 1000))
            return true;

        _stuckAttempts++;
        _lastPositionChangeTick = now; // Give the attempt time to work

        // Jumping is useless in flight, so skip straight to stopping the path
        var isFlying = Svc.Condition[ConditionFlag.InFlight] || Svc.Condition[ConditionFlag.Diving];

        if (_stuckAttempts == 3 && !isFlying)
        {
            ActionManager.Instance()->UseAction(ActionType.GeneralAction, 2);
            return true;
        }

        // Jump didn't help (or we're flying): stop navmesh so the caller can re-path
        P.navmesh.PathStop();
        _stuckAttempts = 0;
        return true;
    }
}
