using ECommons.Automation;
using ECommons.GameHelpers;
using GatherChill.Scheduler.Handlers;
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
            var selectedInfo = Gather_Helper.GatherRoute;
            var routeInfo = P.routeEditor.GetRoute(selectedInfo.routeId);

            if (!P.navTask.IsBusy)
            {

                if (Player.Territory.RowId == routeInfo.TerritoryId)
                {
                    // We're currently in the correct territory, no need to teleport
                    Gather_Helper.State = Enums.IceState.Gather;
                    return true;
                }
                else
                {
                    if (GenericHelpers.TryGetAddonMaster<Gathering>(out var gatherItem) && gatherItem.IsAddonReady)
                    {
                        GenericHandlers.CloseWindow(gatherItem.Base);
                    }
                    else
                    {
                        P.navTask.Enqueue(() => Task_NavmeshMove.TeleportToArea(routeInfo.AetheryteId));
                        return false;
                    }
                }
            }
            else
            {

            }

            return false;
        }
    }
}
