using ECommons.GameHelpers;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Component.GUI;
using GatherChill.Scheduler.Handlers;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Scheduler.Tasks
{
    internal class Task_Spiritbond
    {
        public static unsafe bool Enqueue()
        {
            string tag = "Task: Spiritbond";

            if (Svc.Condition[ConditionFlag.Occupied39])
                return false;

            if (C.Extract_Spiritbond && Utils.Spiritbond_AnyReady() && Utils.GetFreeSlotCount() > 1)
            {
                if (Player.Mounted)
                    Utils.Dismount();

                if (GenericHelpers.TryGetAddonByName<AtkUnitBase>("Materialize", out var materialize) && GenericHelpers.IsAddonReady(materialize))
                {
                    if (!EzThrottler.Throttle("Spiritbond Extraction"))
                        return false;

                    IceLogging.Verbose("Sending materia extraction click", tag);
                    Ecom_Handler.FireCallback("Materialize", true, 2, 0);
                }
                else
                {
                    if (!EzThrottler.Throttle("Open Material Extraction"))
                        return false;

                    IceLogging.Verbose("Opening Materia Extraction Window", tag);
                    ActionManager.Instance()->UseAction(ActionType.GeneralAction, 14);
                }
            }
            else
            {
                IceLogging.Debug("Spiritbond task is complete. Just double checking to close any extra windows...", tag);
                if (GenericHelpers.TryGetAddonByName<AtkUnitBase>("Materialize", out var materialize) && GenericHelpers.IsAddonReady(materialize))
                {
                    if (EzThrottler.Throttle("Closing Materia Window"))
                    {
                        IceLogging.Verbose("Closing Materia Window");
                        materialize->Close(true);
                    }

                    return false;
                }

                return true;
            }

            return false;
        }
    }
}
