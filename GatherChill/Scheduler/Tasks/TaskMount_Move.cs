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
    internal static class TaskMount_Move
    {
        public static void Enqueue(Vector3 destination, float tolerance)
        {
            P.taskManager.Enqueue(() => Mount_Move(destination, tolerance));
        }

        internal unsafe static bool? Mount_Move(Vector3 destination, float toleranceDistance = 3f)
        {
            if (GetDistanceToPlayer(destination) <= toleranceDistance)
            {
                P.navmesh.Stop();
                return true;
            }

            if (PlayerNotBusy() && !Player.Mounted)
            {
                if (EzThrottler.Throttle("Mounting"))
                {
                    ActionManager.Instance()->UseAction(ActionType.GeneralAction, 9);
                    PluginDebug("Attempting to mount up");
                }
            }

            if (P.navmesh.PathfindInProgress() || P.navmesh.IsRunning() || PlayerHandlers.IsMoving()) return false;
            P.navmesh.SetAlignCamera(false);
            P.navmesh.PathfindAndMoveTo(destination, false);

            return false;
        }
    }
}
