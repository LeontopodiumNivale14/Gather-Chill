using ECommons.Throttlers;
using GatherChill.Enums;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Scheduler.Tasks
{
    internal class Task_Wait
    {
        public static void Enqueue()
        {
            P.TaskManager.Enqueue(() => WaitForTimer(), "Waiting for timednode Timer");
        }

        private static bool WaitForTimer()
        {
            if (!EzThrottler.Throttle("Timer Check", 1000))
                return false;

            string tag = "Waiting for timed nodes";

            if (Gather_Helper.AnyTimedNodes(out _, out var nextUp))
            {
                IceLogging.Verbose("We have a timed node that is up woo! We're gonna start the process again and check to see which to gather", tag);
                Gather_Helper.State = IceState.Start;
                return true;
            }
            else
            {
                if (nextUp.HasValue)
                {
                    var ts = nextUp.Value.Duration();
                    var formatted = $"{(int)ts.TotalMinutes}:{ts.Seconds:D2}";

                    IceLogging.Verbose($"Next timed node is within: {formatted}");
                }
                else
                {
                    IceLogging.Verbose("We playing the long waiting game... woooooo");
                }
            }

            return false;
        }
    }
}
