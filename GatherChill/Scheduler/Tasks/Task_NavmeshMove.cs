using Dalamud.Game.ClientState.Conditions;
using ECommons.GameHelpers;
using ECommons.Logging;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game;
using GatherChill.ConfigFiles;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Utility;

namespace GatherChill.Scheduler.Tasks
{
    internal class Task_NavmeshMove
    {
        private const float PathTolerance = 0.25f;
        private const float StuckDistanceThreshold = 1.0f;
        private const int StuckTimeThresholdMs = 3000;
        private const int MaxStuckRecoveries = 3;
        private const float GroundDismountDistance = 3f;

        private static Vector3 _lastPosition;
        private static DateTime _lastPositionChange = DateTime.Now;
        private static int _stuckAttempts;

        public static bool? Task_FlyTo(Vector3 pos, bool waitForBusy = true, float distance = 2.0f, bool stayMounted = false) =>
            MoveTo(pos, forceFly: true, waitForBusy, distance, stayMounted);

        public static bool? Task_GroundTo(Vector3 pos, bool waitForBusy = true, float distance = 2.0f, bool stayMounted = false) =>
            MoveTo(pos, forceFly: false, waitForBusy, distance, stayMounted);

        public static void ReleaseOwnedPath() => P.navmesh.StopIfOwned();

        private static bool? MoveTo(Vector3 pos, bool forceFly, bool waitForBusy, float closeRange, bool stayMounted)
        {
            if (NavmeshMovement.IsGatheringSessionActive())
            {
                NavmeshMovement.HaltNavmeshForGathering();
                return true;
            }

            var useFly = NavmeshMovement.WantsFlyPath(forceFly, pos);
            var context = useFly ? "FlyTo" : "GroundTo";

            if (!C.NavmeshMovementEnabled)
            {
                if (NavmeshMovement.IsWithinHorizontalRange(pos, closeRange))
                    return true;

                NavmeshRuntime.SetFailure("Navmesh movement disabled in config");
                return false;
            }

            if (!TryEnsureNavmeshReady(context))
                return false;

            pos = NavmeshMovement.ResolvePathPoint(pos);
            NavmeshRuntime.SetMoveAttempt(context, pos, closeRange);

            if (P.navmesh.TickArrival(pos, closeRange))
                return HandleArrival(stayMounted, context);

            if (P.navmesh.IsMoving())
            {
                NavmeshRuntime.SetOwnsPath(true);

                if (CheckAndHandleStuck(context))
                    return false;

                if (useFly && !Player.Mounted)
                {
                    if (EzThrottler.Throttle("Emergency Navmesh Stop | Fly"))
                        StopOwned();

                    return false;
                }

                if (!useFly && !Player.Mounted && Player.DistanceTo(pos) > NavmeshMovement.LongMoveMountDistance)
                {
                    if (EzThrottler.Throttle("Using mount"))
                        Utils.MountAction();
                }

                if (!useFly && Player.DistanceTo(pos) <= GroundDismountDistance && !stayMounted)
                {
                    if (EzThrottler.Throttle("Dismounting the mount"))
                        Utils.Dismount();
                }

                if (Player.IsMoving && waitForBusy)
                    return false;

                return false;
            }

            if (!useFly && Player.DistanceTo(pos) >= NavmeshMovement.LongMoveMountDistance)
            {
                if (!Player.Mounted)
                {
                    if (EzThrottler.Throttle("Mounting for long ground move"))
                        Utils.MountAction();

                    return false;
                }
            }
            else if (useFly && !Player.Mounted)
            {
                if (EzThrottler.Throttle("Telling us to mount"))
                    Utils.MountAction();

                return false;
            }

            if (EzThrottler.Throttle($"Commence Navmesh Movement ({context})"))
            {
                if (forceFly && !useFly && EzThrottler.Throttle("Fly not unlocked, using ground", 5000))
                    LogNav("Flight not unlocked in this zone; using ground movement.", false);

                P.navmesh.SetTolerance(PathTolerance);
                IceLogging.DestinationLogs.Log(pos);
                BeginStuckTracking();
                if (!TryStartMove(pos, useFly, closeRange) &&
                    useFly &&
                    !TryStartMove(pos, fly: false, closeRange))
                {
                    var msg = $"Could not start movement to {pos}";
                    NavmeshRuntime.SetFailure(msg);
                    if (EzThrottler.Throttle($"Navmesh pathfind failed ({context})", 2000))
                        LogNav(msg, true);

                    return false;
                }

                if (!P.navmesh.IsMoving())
                {
                    var msg = $"Movement did not start for {pos}";
                    NavmeshRuntime.SetFailure(msg);
                    if (EzThrottler.Throttle($"Navmesh did not start ({context})", 2000))
                        LogNav(msg, true);

                    return false;
                }

                NavmeshRuntime.SetOwnsPath(true);
            }

            return false;
        }

        private static bool TryStartMove(Vector3 pos, bool fly, float closeRange) =>
            P.navmesh.TryMoveTo(pos, fly, closeRange);

        private static bool? HandleArrival(bool stayMounted, string context)
        {
            if (Player.Mounted && !stayMounted)
            {
                Utils.Dismount();
                return false;
            }

            if (Player.IsJumping)
                return false;

            ClearStuckTracking();
            NavmeshRuntime.SetOwnsPath(false);
            LogNav($"Arrived ({context})", false);
            return true;
        }

        private static bool TryEnsureNavmeshReady(string context)
        {
            if (!P.navmesh.Installed)
            {
                var msg = "vnavmesh is not installed";
                NavmeshRuntime.SetFailure(msg);
                if (EzThrottler.Throttle($"Navmesh missing ({context})", 5000))
                    IceLogging.Error(msg);

                return false;
            }

            if (!P.navmesh.IsReady())
            {
                if (!NavmeshMovement.IsGatheringSessionActive())
                    P.navmesh.TryEnsureNavMeshLoading();

                if (EzThrottler.Throttle($"Waiting on navmesh ({context})", 1000))
                {
                    var navProgress = P.navmesh.GetBuildProgress();
                    var progressLabel = navProgress < 0f ? "idle (enable vnavmesh auto-load or wait)" : $"{navProgress:P0}";
                    LogNav($"Waiting for navmesh ({context}), mesh {progressLabel}", false);
                }

                return false;
            }

            return true;
        }

        private static unsafe bool CheckAndHandleStuck(string context)
        {
            var currentPos = Player.Position;
            var timeSinceLastChange = (DateTime.Now - _lastPositionChange).TotalMilliseconds;

            if (Vector3.Distance(currentPos, _lastPosition) > StuckDistanceThreshold)
            {
                BeginStuckTracking();
                NavmeshRuntime.SetStuckAttempts(_stuckAttempts);
                return false;
            }

            if (timeSinceLastChange <= StuckTimeThresholdMs)
                return false;

            _stuckAttempts++;
            NavmeshRuntime.SetStuckAttempts(_stuckAttempts);

            if (_stuckAttempts > MaxStuckRecoveries)
            {
                var msg = $"Stuck too many times ({context})";
                NavmeshRuntime.SetFailure(msg);
                StopOwned();
                if (EzThrottler.Throttle($"Navmesh stuck give up ({context})", 3000))
                    LogNav(msg, true);

                return false;
            }

            if (_stuckAttempts == 1)
            {
                if (EzThrottler.Throttle("Stuck - attempting jump", 1000))
                {
                    ActionManager.Instance()->UseAction(ActionType.GeneralAction, 2);
                    _lastPositionChange = DateTime.Now;
                    LogNav($"Stuck, jumping ({context})", false);
                }

                return true;
            }

            if (EzThrottler.Throttle("Stuck - stopping navmesh", 1000))
            {
                StopOwned();
                BeginStuckTracking();
                LogNav($"Stuck, restarting path ({context})", false);
            }

            return true;
        }

        private static void StopOwned() => P.navmesh.StopIfOwned();

        private static void LogNav(string message, bool warning)
        {
            if (warning)
                IceLogging.Warning(message);
            else if (C.NavmeshVerboseLogging)
                IceLogging.Debug(message);
        }

        private static void BeginStuckTracking()
        {
            _lastPosition = Player.Position;
            _lastPositionChange = DateTime.Now;
            _stuckAttempts = 0;
            NavmeshRuntime.SetStuckAttempts(0);
        }

        public static void ClearStuckTracking() => BeginStuckTracking();

        public static void ResetInfo() => BeginStuckTracking();
    }
}
