

#nullable disable
using GatherChill.Enums;
using GatherChill.Scheduler.Tasks;

namespace GatherChill.Scheduler
{
    internal static class SchedulerMain
    {
        internal static void DisablePlugin()
        {
            Gather_Helper.State = IceState.Idle;
            P.TaskManager.Abort();
            P.navTask.Abort();
            P.navmesh.Stop();

            Gather_Helper.GatherRoute = new();

        }
        internal static uint RouteId = 0;
        internal static uint ItemId = 0;

        internal static void Tick()
        {
            if (P.TaskManager.NumQueuedTasks == 0 && Gather_Helper.State != IceState.Idle)
            {
                Action enqueue = Gather_Helper.State switch
                {
                    IceState.Start => Task_Start.Enqueue,
                    IceState.Teleport => Task_Teleport.Enqueue,
                    IceState.Gather => Task_GatherRoute.NormalItem_Enqueue,
                    _ => DisablePlugin
                };
                enqueue();
            }
        }
    }
}
