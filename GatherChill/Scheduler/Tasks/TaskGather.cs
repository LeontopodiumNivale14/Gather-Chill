using Dalamud.Game.ClientState.Conditions;
using ECommons.Automation;
using ECommons.GameHelpers;
using ECommons.Logging;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Component.GUI;
using GatherChill.Scheduler.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ECommons.UIHelpers.AddonMasterImplementations.AddonMaster;

namespace GatherChill.Scheduler.Tasks
{
    internal static class TaskGather
    {
        private static uint GatherItemId = 0;
        public static void Enqueue(uint itemId)
        {
            P.taskManager.Enqueue(GatheringAddonReady, "Waiting for Gathering Addon to be ready");
            P.taskManager.Enqueue(() => GatherItem(), $"Task Gathering Item {itemId}" ,configuration: DConfig);
            P.taskManager.Enqueue(() => LeaveGatheringMenu(), "Leaving Gathering Menu");
        }

        internal unsafe static bool? GatheringAddonReady()
        {
            if (TryGetAddonMaster<Gathering>("Gathering", out var m) && m.IsAddonReady)
            {
                return true;
            }

            return false;
        }

        internal unsafe static bool? GatherItem()
        {
            uint Boon1 = 0;
            uint Boon2 = 0;
            uint Tidings = 0;
            uint Yield1 = 0;
            uint Yield2 = 0;
            uint IntegInc = 0;
            uint BonusInteg = 0;

            GatheringConfig? itemEntry = null;

            if (GetClassJobId() == 17)
            {
                Boon1 = GathActionDict["BoonIncrease1"].BtnActionId;
                Boon2 = GathActionDict["BoonIncrease2"].BtnActionId;
                Tidings = GathActionDict["Tidings"].BtnActionId;
                Yield1 = GathActionDict["Yield1"].BtnActionId;
                Yield2 = GathActionDict["Yield2"].BtnActionId;
                IntegInc = GathActionDict["IntegrityIncrease"].BtnActionId;
                BonusInteg = GathActionDict["BonusIntegrityChance"].BtnActionId;
            }
            else if (GetClassJobId() == 16)
            {
                Boon1 = GathActionDict["BoonIncrease1"].MinActionId;
                Boon2 = GathActionDict["BoonIncrease2"].MinActionId;
                Tidings = GathActionDict["Tidings"].MinActionId;
                Yield1 = GathActionDict["Yield1"].MinActionId;
                Yield2 = GathActionDict["Yield2"].MinActionId;
                IntegInc = GathActionDict["IntegrityIncrease"].MinActionId;
                BonusInteg = GathActionDict["BonusIntegrityChance"].MinActionId;
            }

            if (!Svc.Condition[ConditionFlag.Gathering])
            {
                return true;
            }

            bool foundItem = false;

            if (TryGetAddonMaster<Gathering>("Gathering", out var m) && m.IsAddonReady)
            {
                if (!Svc.Condition[ConditionFlag.Gathering42])
                {
                    foreach (var g in m.GatheredItems)
                    {
                        itemEntry = C.GatheringList.Where(x => x.ItemId == g.ItemID)
                                                                    .Where(x => GetItemCount((int)x.ItemId) < x.GatheringAmount)
                                                                    .FirstOrDefault();
                        
                        if (itemEntry != null)
                        {
                            foundItem = true;
                            int boonChance = g.BoonChance;
                            bool missingDur = m.CurrentIntegrity < m.TotalIntegrity;
                            bool useAction = false;

                            if (BoonIncrease2Bool(boonChance) && !missingDur)
                            {
                                useAction = true;
                                if (EzThrottler.Throttle("Boon2 Action Usage"))
                                {
                                    PluginDebug("Activating Boon% 2");
                                    ActionManager.Instance()->UseAction(ActionType.Action, Boon2);
                                }
                            }
                            else if (BoonIncrease1Bool(boonChance) && !missingDur)
                            {
                                useAction = true;
                                if (EzThrottler.Throttle("Boon1 Action Usage"))
                                {
                                    PluginDebug("Activating Boon% 1");
                                    ActionManager.Instance()->UseAction(ActionType.Action, Boon1);
                                }
                            }
                            else if (TidingsBool())
                            {
                                useAction = true;
                                if (EzThrottler.Throttle("Tidings Action Usage") && !missingDur)
                                {
                                    PluginDebug("Activating Bonus Item from Tidings");
                                    ActionManager.Instance()->UseAction(ActionType.Action, Tidings);
                                }
                            }
                            else if (Yield2Bool())
                            {
                                useAction = true;
                                if (EzThrottler.Throttle("Using Yield2 Action Usage") && !missingDur)
                                {
                                    PluginDebug("Activating Kings Yield II [or equivelent]");
                                    ActionManager.Instance()->UseAction(ActionType.Action, Yield2);
                                }

                            }
                            else if (Yield1Bool())
                            {
                                useAction = true;
                                if (EzThrottler.Throttle("Using Yield1 Action Usage") && !missingDur)
                                {
                                    PluginDebug("Activating Kings Yield II [or equivelent]");
                                    ActionManager.Instance()->UseAction(ActionType.Action, Yield1);
                                }
                            }
                            else if (BonusIntegrityBool(missingDur))
                            {
                                useAction = true;
                                if (EzThrottler.Throttle("Using Bonus Intregrity Usage"))
                                {
                                    PluginDebug("Activating Bonus Yield Button");
                                    ActionManager.Instance()->UseAction(ActionType.Action, BonusInteg);
                                }
                            }
                            else if (IntegrityBool(missingDur))
                            {
                                useAction = true;
                                if (EzThrottler.Throttle("Missing Dur, using action"))
                                {
                                    PluginDebug("Activing Integrity Increase Button [Hoping for bonus Integ]");
                                    ActionManager.Instance()->UseAction(ActionType.Action, IntegInc);
                                }
                            }

                            if (!useAction)
                            {
                                PluginDebug("Should be gathering an item...");
                                if (EzThrottler.Throttle("Gathering" + g.ItemID, 100))
                                {
                                    PluginDebug($"Gathering {g.ItemID}");
                                    g.Gather();
                                }
                            }

                            else if (EzThrottler.Throttle("Gathering" + g.ItemID, 100))
                            {
                                PluginDebug($"Gathering {g.ItemID}");
                                g.Gather();
                            }
                        }
                    }

                    if (foundItem)
                    {
                        return false;
                    }
                    else
                    {
                        PluginLog.Debug("No items were found at this node, and you still have durability left. Leaving the node");
                        return true;
                    }
                }
            }

            return false;
        }

        internal unsafe static bool? LeaveGatheringMenu()
        {
            if (!Svc.Condition[ConditionFlag.Gathering])
            {
                return true;
            }

            if (TryGetAddonMaster<Gathering>("Gathering", out var m) && m.IsAddonReady)
            {
                if (EzThrottler.Throttle("Closing Gathering Menu"))
                {
                    GenericHandlers.FireCallback("Gathering", true, -1);
                }
            }

            return false;
        }
    }
}
