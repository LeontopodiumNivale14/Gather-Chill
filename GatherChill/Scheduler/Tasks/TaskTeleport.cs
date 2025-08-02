using Dalamud.Game.ClientState.Conditions;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.Scheduler.Tasks
{
    internal static class TaskTeleport
    {
        internal static unsafe void Enqueue(uint aetherytId, uint targetTerritoryId)
        {
            P.taskManager.Enqueue(() => TeleporttoAethery(aetherytId, targetTerritoryId));
        }

        internal static unsafe bool? TeleporttoAethery(uint aetherytID, uint targetTerritoryId)
        {
            if (IsScreenReady())
            {
                if (IsInZone(targetTerritoryId))
                    return true;
                else if (targetTerritoryId == 129 && IsInZone(128))
                    return true;
            }

            if (!Svc.Condition[ConditionFlag.Casting] && PlayerNotBusy() && !IsBetweenAreas && !IsInZone(targetTerritoryId))
            {
                if (EzThrottler.Throttle("Teleport Throttle", 1000))
                {
                    PluginVerbos($"Teleporting to {aetherytID} at {targetTerritoryId}");
                    Telepo.Instance()->Teleport(aetherytID, 0);
                    return false;
                }
            }
            return false;
        }
    }
}
