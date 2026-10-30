using ECommons.Throttlers;
using GatherChill.ConfigFiles;
using GatherChill.Enums;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Utility;
using System.Collections.Generic;

namespace GatherChill.Scheduler
{
    internal class Gather_Helper
    {
        public class SelectedRoute
        {
            public uint itemId { get; set; } = 0;
            public uint routeId { get; set; } = 0;
        }

        public static SelectedRoute GatherRoute = new();
        public static IceState State = IceState.Idle;
        public static GatherType gatherMode = GatherType.Normal;

        // Set by the GatherItem IPC so only that item gets gathered. 0 = the whole list.
        public static uint OnlyItemId = 0;

        /// <summary>
        /// False if the GatherItem IPC limited this run to a different item.
        /// </summary>
        public static bool IsTargeted(uint itemId) => OnlyItemId == 0 || OnlyItemId == itemId;

        public class ProgressInfo
        {
            public uint ItemId { get; set; } = 0;

            // Inventory count when the gather started (e.g. 150). Set once.
            public int StartInventory { get; set; } = 0;

            // Gather goal when the gather started. Set once. Unused in Keep mode.
            public int GatherTarget { get; set; } = 0;

            private Config.ItemInfo? Info
            {
                get
                {
                    foreach (var info in C.GatherList)
                    {
                        if (info.ItemId == ItemId)
                            return info;
                    }

                    return null;
                }
            }

            public GatherMode Mode => Info?.Mode ?? GatherMode.Gather;

            public int Current => Utils.GetItemCount(ItemId);

            public int Gathered => Math.Max(Current - StartInventory, 0);

            // The overall goal, for display ("x / Target").
            public int Target
            {
                get
                {
                    var info = Info;
                    if (info == null)
                        return 0;

                    if (info.Mode == GatherMode.Keep)
                        return Math.Max(info.KeepAmount, 0);

                    return Math.Max(GatherTarget, 0);
                }
            }

            // What the overlay shows on the left of "x / Target".
            public int Progress => Mode == GatherMode.Keep ? Current : Gathered;

            // How many more are needed.
            public int Remaining => Math.Max(Target - Progress, 0);

            // 0..1 for progress bars.
            public float Fraction
            {
                get
                {
                    var target = Target;
                    if (target <= 0)
                        return 1f;

                    return Math.Clamp((float)Progress / target, 0f, 1f);
                }
            }

            public bool IsComplete => Remaining == 0;
        }

        public static List<ProgressInfo> ItemProgress = new();
        public static HashSet<uint> GatheredTimedRoutes = new();
        public static void RemoveTimedRoutes()
        {
            if (!EzThrottler.Throttle("Has Checked"))
                return;

            foreach (var route in GatheredTimedRoutes)
            {
                var routeInfo = Gather_Util.Sheet_RouteInfo[route];
                if (!routeInfo.TimedInfo.Any(x => x.IsUp()))
                {
                    GatheredTimedRoutes.Remove(route);
                }
            }
        }

        /// <summary>
        /// Returns true if any timed node on a selected route is currently up.
        /// upRoutes: every currently-up route not already in GatheredTimedRoutes.
        /// nextUp: real time until the soonest window opens among routes that are
        ///         NOT currently up (null if there are none).
        /// </summary>
        public static bool AnyTimedNodes(out List<uint> upRoutes, out TimeSpan? nextUp)
        {
            upRoutes = new List<uint>();
            nextUp = null;

            if (C.GatherList.Count == 0)
                return false;

            var visited = new HashSet<uint>();
            TimeSpan? soonest = null;

            var itemList = C.GatherList.Where(x =>
                (x.Mode == GatherMode.Gather && x.GatherAmount != 0) ||
                (x.Mode == GatherMode.Keep && Utils.GetItemCount(x.ItemId) < x.KeepAmount))
                .Where(x => IsTargeted(x.ItemId))
                .ToList();

            foreach (var item in itemList)
            {
                if (!C.ItemRoutes.TryGetValue(item.ItemId, out var itemRoute))
                    continue;

                foreach (var route in itemRoute.EnabledRoutes)
                {
                    if (GatheredTimedRoutes.Contains(route))
                        continue;

                    // Same route can be shared by multiple items
                    if (!visited.Add(route))
                        continue;

                    if (!Gather_Util.Sheet_RouteInfo.TryGetValue(route, out var info))
                        continue;

                    if (info.TimedInfo.Count == 0)
                        continue;

                    if (info.TimedInfo.Any(x => x.IsUp()))
                    {
                        upRoutes.Add(route);
                        continue;
                    }

                    foreach (var window in info.TimedInfo)
                    {
                        var wait = window.TimeUntilStart();
                        if (soonest == null || wait < soonest.Value)
                            soonest = wait;
                    }
                }
            }

            nextUp = soonest;
            return upRoutes.Count > 0;
        }

        // Convenience overloads
        public static bool AnyTimedNodes(out List<uint> upRoutes) => AnyTimedNodes(out upRoutes, out _);
        public static bool AnyTimedNodes() => AnyTimedNodes(out _, out _);

        /// <summary>
        /// True when GP should be saved for an upcoming (or already up) timed node.
        /// </summary>
        public static bool ShouldHoldGpForTimedNode()
        {
            if (!C.HoldGP_TimeNodes)
                return false;

            var next = NextTimedNode();
            if (next is not { } wait)
                return false;

            // wait is at or below the hold threshold
            return wait.TotalSeconds <= C.TimedNodes_HoldTime;
        }

        /// <summary>
        /// null          -> nothing to wait for (already on an up timed route, or no timed routes exist)
        /// TimeSpan.Zero -> a different timed route is up right now
        /// otherwise     -> real time until the soonest timed route opens
        /// </summary>
        public static TimeSpan? NextTimedNode()
        {
            var anyUp = AnyTimedNodes(out var upRoutes, out var nextUp);

            // Already on a timed route that's up
            if (anyUp && upRoutes.Contains(GatherRoute.routeId))
                return null;

            // A different timed route is up right now
            if (anyUp)
                return TimeSpan.Zero;

            // Nothing up: time until the soonest opens (null if no timed routes at all)
            return nextUp;
        }
    }
}