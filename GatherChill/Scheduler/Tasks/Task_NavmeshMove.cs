using Dalamud.Game.ClientState.Conditions;
using ECommons.GameHelpers;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game;
using GatherChill.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Scheduler.Tasks
{
    internal class Task_NavmeshMove
    {
        public static bool? Task_NavTo(Vector3 pos, bool waitForBusy = true, float distance = 2.0f, bool stayMounted = false, bool fly = false)
        {
            bool usingCosmoliner = Svc.Condition[ConditionFlag.Unknown101];
            bool isFlying = Svc.Condition[ConditionFlag.InFlight];
            bool mounted = Player.Mounted;
            float minMountDistance = 50;
            float dismountDistance = 15;

            bool JumpIfStuck = true;
            bool useMount = true;

            if (!P.navmesh.Installed)
            {
                return true;
            }
            else if (P.navmesh.IsRunning())
            {
                if (JumpIfStuck)
                {
                    if (CheckAndHandleStuck())
                    {
                        return false;
                    }
                }

                if (!mounted && Player.DistanceTo(pos) > minMountDistance)
                {
                    if (useMount)
                    {
                        if (EzThrottler.Throttle("Using mount"))
                            Utils.MountAction();
                    }
                }

                // Only dismount if we're close AND (not flying OR we're on the ground)
                if (Player.DistanceTo(pos) <= dismountDistance && !stayMounted)
                {
                    // Don't dismount while flying unless we've landed
                    bool canDismount = !fly || !isFlying;

                    if (canDismount && EzThrottler.Throttle("Dismounting the mount"))
                    {
                        Utils.Dismount();
                    }
                }

                if (Player.IsMoving && waitForBusy)
                {
                    if (EzThrottler.Throttle("Throttle message tehe"))
                        return false;
                }
                else if (!waitForBusy && Player.DistanceTo(pos) <= distance)
                {
                    if (EzThrottler.Throttle("Telling navmesh to stop"))
                    {
                        P.navmesh.Stop();
                    }
                }

                if (usingCosmoliner)
                {
                    if (EzThrottler.Throttle("Telling navmesh to stop"))
                        P.navmesh.Stop();
                }
            }
            else if (!P.navmesh.IsReady())
            {
                if (EzThrottler.Throttle("Waiting on navmesh", 1000))
                {
                    var navProgress = P.navmesh.BuildProgress();
                }
            }
            else if (!P.navmesh.IsRunning())
            {
                if (usingCosmoliner)
                {
                    return false;
                }

                if (Player.DistanceTo(pos) < distance)
                {
                    if (mounted && !stayMounted && !fly)
                    {
                        if (EzThrottler.Throttle("Dismounting the mount"))
                        {
                            Utils.Dismount();
                        }
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
                else if (fly && !Player.Mounted)
                {
                    if (EzThrottler.Throttle("Telling us to mount"))
                        Utils.MountAction();
                }
                else
                {
                    if (EzThrottler.Throttle("Telling navmesh to start"))
                    {
                        P.navmesh.SetTolerance(0.25f);
                        P.navmesh.PathfindAndMoveTo(pos, fly);
                    }
                }
            }

            return false;
        }

        public static bool? Task_MoveToV2(Vector3 pos, bool waitForBusy = true, float distance = 2.0f, bool stayMounted = false, bool fly = false)
        {
            bool isFlying = Svc.Condition[ConditionFlag.InFlight];
            bool mounted = Player.Mounted;

            // Config versions to change later when I actually do this better...
            float minMountDistance = 50;
            float dismountDistance = 15;
            bool JumpIfStuck = true;
            bool useMount = true;

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
                if (JumpIfStuck)
                {
                    if (CheckAndHandleStuck())
                    {
                        return false;
                    }
                }

                if (!mounted)
                {
                    if (fly)
                    {
                        // We should never be not mounting here, so going to just stop the current navmesh and restart it
                        if (EzThrottler.Throttle("Emergency Navmesh Stop | Fly"))
                            P.navmesh.Stop();

                        return false;
                    }

                    if (useMount && Player.DistanceTo(pos) > minMountDistance)
                    {
                        // Just normal movement, but we can mount while moving so, going to do so

                        if (EzThrottler.Throttle("Using mount"))
                            Utils.MountAction();
                    }
                }

                if (!fly)
                {
                    // We should never be trying to dismount while fly is active
                    if (Player.DistanceTo(pos) <= dismountDistance && !stayMounted)
                    {
                        if (EzThrottler.Throttle("Dismounting the mount"))
                        {
                            Utils.Dismount();
                        }
                    }
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
                        P.navmesh.Stop();
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
                else if (fly && !Player.Mounted)
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
                        P.navmesh.PathfindAndMoveTo(pos, fly);
                    }
                }
            }

            return false;
        }

        private static Vector3 _lastPosition = Vector3.Zero;
        private static DateTime _lastPositionChange = DateTime.Now;
        private static int _stuckAttempts = 0;
        private const float STUCK_DISTANCE_THRESHOLD = 1.0f; // Consider stuck if moved less than this
        private const int STUCK_TIME_THRESHOLD = 3000; // Time in ms before considering stuck

        private static unsafe bool CheckAndHandleStuck()
        {
            var currentPos = Player.Position;
            var timeSinceLastChange = (DateTime.Now - _lastPositionChange).TotalMilliseconds;

            // Check if we've moved significantly
            if (Vector3.Distance(currentPos, _lastPosition) > STUCK_DISTANCE_THRESHOLD)
            {
                // We moved, reset tracking
                ResetInfo();
                return false;
            }

            // We haven't moved much, check if we've been stuck long enough
            if (timeSinceLastChange > STUCK_TIME_THRESHOLD)
            {
                _stuckAttempts++;

                if (_stuckAttempts == 1)
                {
                    // First attempt: try jumping
                    if (EzThrottler.Throttle("Stuck - attempting jump", 1000))
                    {
                        ActionManager.Instance()->UseAction(ActionType.GeneralAction, 2);
                        _lastPositionChange = DateTime.Now; // Give it time to work
                    }
                    return true;
                }
                else if (_stuckAttempts >= 2)
                {
                    // Second attempt: stop navmesh after jump had time to execute
                    if (EzThrottler.Throttle("Stuck - stopping navmesh", 1000))
                    {
                        P.navmesh.Stop();
                        _stuckAttempts = 0; // Reset for next time
                        _lastPositionChange = DateTime.Now;
                    }
                    return true;
                }
            }

            return false;
        }

        public static void ResetInfo()
        {
            _lastPosition = Vector3.Zero;
            _lastPositionChange = DateTime.Now;
            _stuckAttempts = 0;
        }
    }
}
