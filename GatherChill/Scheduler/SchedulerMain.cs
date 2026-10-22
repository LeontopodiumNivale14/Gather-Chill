using Dalamud.Game.ClientState.Conditions;
using GatherChill.Enums;
using GatherChill.Scheduler.Tasks;
using GatherChill.Utilities;
using GatherChill.Utilities.GatheringHelpers;

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

            Gather_Helper.GatherRoute = new() { itemId = 0, routeId = 0};

        }

        internal static void Tick()
        {
            if (Gather_Helper.State != IceState.Idle)
            {
                if (Gather_Helper.State != IceState.Start)
                {
                    var currentRoute = Gather_Helper.GatherRoute.routeId;

                    if (Gather_Helper.AnyTimedNodes(out var currentlyUp) && !currentlyUp.Contains(currentRoute))
                    {
                        if (!Svc.Condition[ConditionFlag.Gathering])
                        {
                            Gather_Helper.State = IceState.Start;
                            P.TaskManager.Abort();
                            return;
                        }
                    }
                }

                // Have this separates becuase I need the current route to be checked for timed stuff outside when we're not idle
                if (P.TaskManager.NumQueuedTasks == 0)
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
}
