using ECommons.Automation;
using ECommons.GameHelpers;
using ECommons.Throttlers;
using GatherChill.Scheduler.Handlers;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Traveling;
using static ECommons.UIHelpers.AddonMasterImplementations.AddonMaster;

namespace GatherChill.Scheduler.Tasks
{
    internal class Task_Teleport
    {
        public static void Enqueue()
        {
            P.TaskManager.Enqueue(() => Travel_Task(), "Checking for teleporting necessary?");
        }

        private static unsafe bool Travel_Task()
        {
            string tag = "Task Travel: Teleport";

            var selectedInfo = Gather_Helper.GatherRoute;
            var routeInfo = P.routeEditor.GetRoute(selectedInfo.routeId);

            if (!P.navTask.IsBusy)
            {
                if (Player.Territory.RowId == routeInfo.TerritoryId)
                {
                    IceLogging.Debug("We've reached our area, continuing to gather now (or atleast travel to)", tag);
                    // We're currently in the correct territory, no need to teleport
                    Gather_Helper.State = Enums.IceState.Gather;
                    return true;
                }
                else
                {
                    if (GenericHelpers.TryGetAddonMaster<Gathering>(out var gatherItem) && gatherItem.IsAddonReady)
                    {
                        if (EzThrottler.Throttle("Closing Gathering Addon"))
                            IceLogging.Debug("Closing the gathering addon cause we need to teleport", tag);
                        Ecom_Handler.CloseWindow(gatherItem.Base);
                    }
                    else
                    {
                        const uint IdyllshireAetheryteId = 75; // Idyllshire -> Drivanian AetheryteId

                        if (routeInfo.AetheryteId == IdyllshireAetheryteId)
                        {
                            IceLogging.Debug("We need to take the idyllshire teleport, queueing it up", tag);
                            var shardInfo = TravelUtil.Aethernet[IdyllshireAetheryteId];

                            P.navTask.Enqueue(() => Task_NavmeshMove.TeleportToArea(routeInfo.AetheryteId), "Teleporting to Idyllshire");
                            P.navTask.Enqueue(() => Task_NavmeshMove.Task_GroundToAetheryte(shardInfo.Position, shardInfo.Position, false, 11), "Moving to Idyllshire Shard");
                            P.navTask.Enqueue(() => Task_NavmeshMove.LeaveIdyllshire(IdyllshireAetheryteId), "leaving Idllshire");
                        }
                        else
                        {
                            IceLogging.Debug("Just our standard teleport task. So off we go woo!", tag);
                            P.navTask.Enqueue(() => Task_NavmeshMove.TeleportToArea(routeInfo.AetheryteId), $"Teleporting to {routeInfo.AetheryteId}");
                        }
                        return false;
                    }
                }
            }
            else
            {
                if (EzThrottler.Throttle("Waiting for travel task", 2000))
                    IceLogging.Verbose("Waiting for travel task to be finished. . . ");
            }

            return false;
        }
    }
}
