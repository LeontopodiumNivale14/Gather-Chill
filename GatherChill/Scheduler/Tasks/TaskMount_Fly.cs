using ECommons.GameHelpers;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game;
using GatherChill.Scheduler.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.Scheduler.Tasks
{
    internal class TaskMount_Fly
    {
        public static void Enqueue(Vector3 destination, bool stop, float tolerance)
        {
            P.taskManager.Enqueue(() => Mount_Fly(destination, stop, tolerance), "Task Mount + Fly");
        }

        internal unsafe static bool? Mount_Fly(Vector3 destination, bool stop, float toleranceDistance = 3f)
        {
            if (GetDistanceToPlayer(destination) <= toleranceDistance)
            {
                if (stop)
                {
                    P.navmesh.Stop();
                }
                return true;
            }

            if (PlayerNotBusy() && !Player.Mounted)
            {
                if (EzThrottler.Throttle("Mounting"))
                {
                    ActionManager.Instance()->UseAction(ActionType.GeneralAction, 9);
                    PluginDebug("Attempting to mount up");
                }
                return false;
            }

            if (NavDestination != destination)
            {
                P.navmesh.SetAlignCamera(false);
                P.navmesh.PathfindAndMoveTo(destination, true);
                NavDestination = destination;
                PluginDebug("Setting the destination + flying to destination");
            }

            if (P.navmesh.PathfindInProgress() || P.navmesh.IsRunning() || PlayerHandlers.IsMoving()) return false;

            return false;
        }
    }
}
