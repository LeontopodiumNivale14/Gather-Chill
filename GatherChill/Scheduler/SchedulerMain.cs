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
                var currentRoute = Gather_Helper.GatherRoute.routeId;
                if (!Svc.Condition[ConditionFlag.Gathering])
                {
                    if (Gather_Helper.GatheredTimedRoutes.Contains(currentRoute))
                    {
                        SoftReset();
                        return;
                    }
                    else if (Gather_Util.Sheet_RouteInfo.TryGetValue(currentRoute, out var routeInfo) && routeInfo.Kind is not GatherNodeKind.Regular)
                    {
                        if (!routeInfo.TimedInfo.Any(x => x.IsUp()))
                        {
                            SoftReset();
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

        private static void SoftReset()
        {
            var currentRoute = Gather_Helper.GatherRoute.routeId;

            Gather_Helper.GatherRoute = new() { itemId = 0, routeId = 0 };

            Gather_Helper.State = IceState.Start;
            P.TaskManager.Abort();
            P.navTask.Abort();
            if (P.navmesh.IsRunning())
                P.navmesh.Stop();
        }
    }
}
