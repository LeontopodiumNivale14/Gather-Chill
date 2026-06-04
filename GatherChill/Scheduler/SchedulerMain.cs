

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
        internal static bool DisablePlugin()
        {
            P.navmesh.SmartStop();
            Task_NavmeshMove.ReleaseOwnedPath();
            NavmeshRuntime.Reset();
            P.taskManager.Abort();

            RouteId = null;
            ItemId = null;
            State = IceState.Idle;

            return true;
        }

        internal static IceState State = IceState.Idle;
        internal static uint? RouteId = 0;
        internal static uint? ItemId = 0;

        internal static void Tick()
        {
            if (P.taskManager.NumQueuedTasks == 0 && State != IceState.Idle)
            {
                Task_GatherRoute.Enqueue(RouteId.Value, ItemId.Value);
            }
        }
    }
}
