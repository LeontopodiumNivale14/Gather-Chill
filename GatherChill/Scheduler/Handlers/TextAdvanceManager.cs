using ECommons.EzSharedDataManager;
using GatherChill.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.Scheduler.Handlers
{
    internal static class TextAdvancedManager
    {
        private static bool WasChanged = false;
        internal static void Tick()
        {
            if (WasChanged)
            {
                if (Gather_Helper.State == IceState.Idle)
                {
                    WasChanged = false;
                    UnlockTA();
                }
            }
            else
            {
                if (Gather_Helper.State != IceState.Idle)
                {
                    WasChanged = true;
                    LockTA();
                }
            }
        }
        internal static void LockTA()
        {
            if (EzSharedData.TryGet<HashSet<string>>("TextAdvance.StopRequests", out var data))
            {
                data.Add(P.Name);
            }
        }

        internal static void UnlockTA()
        {
            if (EzSharedData.TryGet<HashSet<string>>("TextAdvance.StopRequests", out var data))
            {
                data.Remove(P.Name);
            }
        }
    }
}

