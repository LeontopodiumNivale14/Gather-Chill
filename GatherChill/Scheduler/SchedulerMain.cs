

#nullable disable
using GatherChill.Enums;
using GatherChill.Scheduler.Tasks;

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
            P.navmesh.Stop();
            P.taskManager.Abort();

            RouteId = null;
            ItemId = null;

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
