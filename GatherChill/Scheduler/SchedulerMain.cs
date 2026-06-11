

#nullable disable
using GatherChill.Enums;
using GatherChill.Scheduler.Tasks;
using GatherChill.Utilities.Utility;

namespace GatherChill.Scheduler
{
    internal static unsafe class SchedulerMain
    {
        internal static bool EnablePlugin()
        {
            return true;
        }
        internal static void Stop()
        {
            GatherQueueSession.Stop();
            NavmeshEditorPath.Cancel();
            P.navmesh.StopCompletely();
            Task_NavmeshMove.ReleaseOwnedPath();
            NavmeshRuntime.Reset();
            P.taskManager.Abort();
            Task_GatherRoute.Reset();

            RouteId = null;
            ItemId = null;
            State = IceState.Idle;
            QueueActive = false;
        }

        internal static bool DisablePlugin()
        {
            Stop();
            return true;
        }

        internal static IceState State = IceState.Idle;
        internal static uint? RouteId = 0;
        internal static uint? ItemId = 0;
        internal static bool QueueActive { get; private set; }

        internal static void SetRouteTarget(uint routeId, uint itemId, bool queueMode = false)
        {
            var routeChanged = RouteId != routeId;
            QueueActive = queueMode;
            State = IceState.Start;
            RouteId = routeId;
            ItemId = itemId;
            Task_GatherRoute.OnRouteTargetChanged(routeChanged);
        }

        internal static void CompleteCurrentTarget() => GatherQueueSession.CompleteCurrentTarget();

        internal static void Tick()
        {
            if (State == IceState.Idle || !RouteId.HasValue || !ItemId.HasValue)
                return;

            // NumQueuedTasks ignores the in-flight task; re-enqueueing every tick stacked travel while gathering.
            if (P.taskManager.NumQueuedTasks != 0 || P.taskManager.CurrentTask != null)
                return;

            if (NavmeshMovement.IsGatheringSessionActive())
                Task_GatherRoute.EnqueueGatheringSession();
            else
                Task_GatherRoute.Enqueue(RouteId.Value, ItemId.Value);
        }
    }
}
