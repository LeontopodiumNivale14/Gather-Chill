using GatherChill.Enums;
using System;
using System.Collections.Generic;
using System.Text;

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
        public static GatherMode gatherMode = GatherMode.Normal;
    }
}
