using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects.Types;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game.Control;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using GatherChill.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.Scheduler.Tasks
{
    internal static class TaskGatherInteract
    {
        public static void Enqueue(ulong dataId)
        {
            IGameObject? gameObject = null;
            P.taskManager.Enqueue(PlayerNotBusy, "Waiting for player to not be busy");
            P.taskManager.Enqueue(() => TryGetObjectByDataId(dataId, out gameObject), "Getting Objec by DataId");
            P.taskManager.Enqueue(() => InteractGather(gameObject), "Interacting with Object");
        }

        internal unsafe static bool? InteractGather(IGameObject? gameObject)
        {
            if (Svc.Condition[ConditionFlag.Gathering])
            {
                return true;
            }
            else
            {
                if (EzThrottler.Throttle("Trying to interact w/ node"))
                {
                    try
                    {
                        var gameObjectPointer = (GameObject*)gameObject.Address;
                        TargetSystem.Instance()->InteractWithObject(gameObjectPointer, false);
                    }
                    catch (Exception ex)
                    {
                        Svc.Log.Info($"InteractWithObject: Exception: {ex}");
                    }
                }
            }

            return false;
        }
    }
}
