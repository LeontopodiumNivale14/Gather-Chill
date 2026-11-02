using ECommons.GameHelpers;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Component.GUI;
using GatherChill.Enums;
using GatherChill.Scheduler.Handlers;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using static ECommons.UIHelpers.AddonMasterImplementations.AddonMaster;

namespace GatherChill.Scheduler.Tasks
{
    internal class Task_SelfRepair
    {
        private static uint DarkMatter_8Id = 33916;

        public static unsafe bool Enqueue()
        {
            string tag = "Task: Self Repair";
            bool selfRepair = C.SelfRepair;

            if (!selfRepair)
            {
                IceLogging.Debug("Self repair is turned off, so we're just skipping this check", tag);
                return true;
            }

            if (Svc.Condition[ConditionFlag.Occupied39])
            {
                if (EzThrottler.Throttle("Waiting for repair to finish"))
                    IceLogging.Verbose("Waiting for repair to finish", tag);

                return false;
            }

            if (!Utils.NeedsRepair())
            {
                if (GenericHelpers.TryGetAddonMaster<SelectYesno>(out var yesNo) && yesNo.IsAddonReady)
                {
                    // Closing any surprise repair windows
                    if (EzThrottler.Throttle("Closing window"))
                        Ecom_Handler.CloseWindow(yesNo.Base);
                }
                else if (GenericHelpers.TryGetAddonByName<AtkUnitBase>("Repair", out var repairAddon) && GenericHelpers.IsAddonReady(repairAddon))
                {
                    if (EzThrottler.Throttle("Closing Repair"))
                        Ecom_Handler.CloseWindow(repairAddon);
                }
                else
                {
                    IceLogging.Debug("Repair task has been completed. We're continuing on with our gathering now", tag);
                    return true;
                }
            }
            else
            {
                // We need to repair ourself. Time to check for dark matter counts and stuff
                if (Utils.GetItemCount(DarkMatter_8Id) < C.Minimum_DarkMatter)
                {
                    IceLogging.Debug("We are below our minimum dark count and we need to repair. Going to stop for safety", tag);
                    SchedulerMain.DisablePlugin();
                    return true;
                }
                else
                {
                    if (Player.Mounted || Player.IsJumping)
                    {
                        if (EzThrottler.Throttle("Dismount Throttle"))
                            Utils.Dismount();
                    }

                    if (GenericHelpers.TryGetAddonMaster<SelectYesno>(out var yesNo) && yesNo.IsAddonReady)
                    {
                        if (EzThrottler.Throttle("Selecting Yes for repair"))
                            yesNo.Yes();
                    }
                    else if (GenericHelpers.TryGetAddonByName<AtkUnitBase>("Repair", out var repairAddon) && GenericHelpers.IsAddonReady(repairAddon))
                    {
                        if (FrameThrottler.Throttle("Firing off repair request", 300))
                        {
                            IceLogging.Debug("Repair Callback", "[Self Repair Task]");
                            ECommons.Automation.Callback.Fire(repairAddon, true, 0);
                        }
                    }
                    else
                    {
                        if (EzThrottler.Throttle("Opening Self Repair", 1000))
                            ActionManager.Instance()->UseAction(ActionType.GeneralAction, 6);
                    }
                }
            }

            return false;
        }
    }
}
