using ECommons.Throttlers;
using GatherChill.GrandCompany.Addons;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.GrandCompany.Tasks
{
    internal class Task_RefreshItems
    {
        public static void Enqueue()
        {
            IceLogging.Verbose("Queueing GC Supply Refresh");
            P.TaskManager.EnqueueMulti
                (
                    new(() => CloseInitialWindow(), "Making sure no timer windows are visible"),
                    new(() => OpenTimerWindow(), "Opening timer window"),
                    new(() => OpenGCSupplies(), "Opening GC Supplies Window"),
                    new(() => RefreshJobs(), "Refreshing GC Turnin List"),
                    new(() => CloseInitialWindow(), "Closing Windows")
                );
        }

        public static bool CloseInitialWindow()
        {
            string tag = "GC: Close Initial Window";

            if (GenericHelpers.TryGetAddonMaster<ContentsInfoDetail>(out var contentsInfoDetail) && contentsInfoDetail.IsAddonReady)
            {
                IceLogging.Verbose("Reporting that gc supply window is ready/visible");
                if (EzThrottler.Throttle("Closing GC Info Window", 100))
                {
                    IceLogging.Verbose("Closing the gc info window", tag);
                    GCInfo.CloseGcInfo();
                }
            }
            else if (GenericHelpers.TryGetAddonMaster<ContentsInfo>(out var contentInfo))
            {
                if (contentInfo.IsAddonReady)
                {
                    IceLogging.Verbose("Reporting the content window was ready");
                    if (EzThrottler.Throttle("Close Window", 100))
                    {
                        IceLogging.Verbose("Closing the timer window", tag);
                        GCInfo.CloseTimers();
                    }
                }
                else
                {
                    IceLogging.Debug("Initial window is not open! Which means we can start fresh", tag);
                    return true;
                }
            }
            else
            {
                IceLogging.Verbose("For some reason we got here? Which means none of the windows should be visible", tag);
                IceLogging.Verbose("Continuing becuase we want to be here reguardless", tag);
                return true;
            }

            return false;
        }
        public static bool OpenTimerWindow()
        {
            string tag = "GC: Open Timer Window";

            if (GenericHelpers.TryGetAddonMaster<ContentsInfo>(out var contentInfo) && contentInfo.IsAddonReady)
            {
                if (contentInfo.EntryCount == 0)
                {
                    if (EzThrottler.Throttle("Waiting for entry count"))
                        IceLogging.Verbose("Entry count was 0, wait for something to show... right?");

                    return false;
                }

                IceLogging.Debug("Contents window is open and fully loaded, going to continue on", tag);
                return true;
            }
            else
            {
                if (EzThrottler.Throttle("Open Timer Window"))
                {
                    IceLogging.Verbose("Opening timer window", tag);
                    GCInfo.OpenTimers();
                }
            }


            return false;
        }
        public static bool OpenGCSupplies()
        {
            string tag = "GC: Open Supply Window";

            if (GenericHelpers.TryGetAddonMaster<ContentsInfoDetail>(out var supplyWindow) && supplyWindow.IsAddonReady)
            {
                IceLogging.Verbose("Timer window should be opened properly now woooo", tag);
                return true;
            }
            else if (GenericHelpers.TryGetAddonMaster<ContentsInfo>(out var contentsInfo) && contentsInfo.IsAddonReady)
            {
                var timerIndex = contentsInfo.FindTimersIndex();
                if (timerIndex != -1)
                {
                    if (EzThrottler.Throttle("Opening Details window"))
                        contentsInfo.OpenGrandCompanyDetail();
                }
            }

            return false;
        }
        public static bool RefreshJobs()
        {
            string tag = "Gc: Refreshing Item List";

            if (GenericHelpers.TryGetAddonMaster<ContentsInfoDetail>(out var contentsInfoDetail) && contentsInfoDetail.IsAddonReady)
            {
                var gatherList = contentsInfoDetail.Provisioning(out var _);

                IceLogging.Verbose("Provisional/Gathering List was available! Updating the list", tag);
                GCInfo.RequestedItems = new();

                foreach (var type in gatherList)
                {
                    var jobInfo = Gather_Util.Job_IconIds.Where(x => x.Value.SecondardaryId == type.IconId).ToList();
                    if (jobInfo != null)
                    {
                        IceLogging.Verbose($"Adding: {jobInfo[0].Key} to the list of entries", tag);
                        GCInfo.RequestedItems.Add(jobInfo[0].Key, new()
                        {
                            ItemId = type.ItemId,
                            Amount = type.Amount,
                            Bonus = type.Bonus,
                        });
                    }
                }
            }

            return true;
        }
    }
}
