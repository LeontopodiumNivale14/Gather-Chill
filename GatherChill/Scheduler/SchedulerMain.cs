using Dalamud.Game.ClientState.Conditions;
using ECommons.Throttlers;
using GatherChill.Enums;
using GatherChill.Scheduler.Tasks;
using GatherChill.Utilities;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Replay;

namespace GatherChill.Scheduler
{
    internal static class SchedulerMain
    {
        /// <summary>
        /// Stops gathering, same as the Stop button.
        /// </summary>
        internal static void DisablePlugin() => DisablePlugin(GatherJobState.Cancelled, "Stopped in Gather n Chill");

        /// <summary>
        /// Stops gathering because something went wrong. An IPC job reports it as failed.
        /// </summary>
        internal static void Fail(string reason) => DisablePlugin(GatherJobState.Failed, reason);

        /// <summary>
        /// True from the first non-idle tick until we return to idle.
        /// Gates the overlay open/close so each only runs once per run.
        /// </summary>
        private static bool runStarted = false;

        internal static void DisablePlugin(GatherJobState outcome, string reason)
        {
            GatherJobs.End(outcome, reason);
            Gather_Helper.State = IceState.Idle;
            P.TaskManager.Abort();
            P.navTask.Abort();
            P.navmesh.Stop();

            Gather_Helper.GatherRoute = new() { itemId = 0, routeId = 0 };
            Gather_Helper.ClearTargeting();

            ReplayRecorder.EndSession("Stopped");
        }

        internal static void Tick()
        {
            if (Gather_Helper.State == IceState.Idle)
            {
                Gather_Helper.ResetProgress();
                OnRunStopped();
                return;
            }

            OnRunStarted();

            Gather_Helper.SyncProgress();

            // Timed-route checks run outside of Idle, but never interrupt an active gather
            if (!Svc.Condition[ConditionFlag.Gathering] && ShouldSoftReset())
            {
                SoftReset();
                return;
            }

            if (P.TaskManager.NumQueuedTasks != 0)
                return;

            Action enqueue = Gather_Helper.State switch
            {
                IceState.Start => Task_Start.Enqueue,
                IceState.Teleport => Task_Teleport.Enqueue,
                IceState.Gather => Task_GatherRoute.NormalItem_Enqueue,
                _ => DisablePlugin
            };
            enqueue();
        }

        // Whenever a run is started, on the first tick checks to see if we need to auto-open
        private static void OnRunStarted()
        {
            if (runStarted)
                return;

            runStarted = true;

            if (C.Overlay_OpenUponRun)
                P.gatherOverlay.IsOpen = true;
        }

        // Once a run is finished, it'll close the overlay if we have it configured
        private static void OnRunStopped()
        {
            if (!runStarted)
                return;

            runStarted = false;

            if (C.Overlay_CloseUponStop)
                P.gatherOverlay.IsOpen = false;
        }

        private static bool ShouldSoftReset()
        {
            var currentRoute = Gather_Helper.GatherRoute.routeId;

            // Already gathered this timed route
            if (Gather_Helper.GatheredTimedRoutes.Contains(currentRoute))
                return true;

            // No route picked yet (Start state), nothing to compare against
            if (currentRoute == 0)
                return false;

            if (!Gather_Util.Sheet_RouteInfo.TryGetValue(currentRoute, out var routeInfo))
                return false;

            // On a timed route: reset once its window closes
            if (routeInfo.Kind is not GatherNodeKind.Regular)
                return !routeInfo.TimedInfo.Any(x => x.IsUp());

            // On a regular route: reset if any timed route is up
            if (!EzThrottler.Throttle("TimedRouteCheck", 1000))
                return false;

            return Gather_Helper.AnyTimedNodes();
        }

        private static void SoftReset()
        {
            Gather_Helper.GatherRoute = new() { itemId = 0, routeId = 0 };
            Gather_Helper.State = IceState.Start;

            P.TaskManager.Abort();
            P.navTask.Abort();

            if (P.navmesh.IsRunning())
                P.navmesh.Stop();
        }
    }
}