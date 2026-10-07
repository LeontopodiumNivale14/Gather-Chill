

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
            State = IceState.Idle;
            P.TaskManager.Abort();
            P.navTask.Abort();

            RouteId = 0;
            ItemId = 0;

            return true;
        }

        internal static IceState State = IceState.Idle;
        internal static uint RouteId = 0;
        internal static uint ItemId = 0;

        internal static void Tick()
        {
            if (P.TaskManager.NumQueuedTasks == 0 && State != IceState.Idle)
            {
                Task_GatherRoute.NormalItem_Enqueue(RouteId, ItemId);
            }
        }
    }
}
