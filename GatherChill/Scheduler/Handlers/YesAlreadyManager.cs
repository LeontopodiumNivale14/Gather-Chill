using ECommons.EzSharedDataManager;
using GatherChill.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatherChill.Scheduler.Handlers
{
    internal static class YesAlreadyManager
    {
        private static bool WasChanged = false;
        internal static void Tick()
        {
            if (WasChanged)
            {
                if (Gather_Helper.State == IceState.Idle)
                {
                    WasChanged = false;
                    Unlock();
                }
            }
            else
            {
                if (Gather_Helper.State != IceState.Idle)
                {
                    WasChanged = true;
                    Lock();
                }
            }
        }
        internal static void Lock()
        {
            if (EzSharedData.TryGet<HashSet<string>>("YesAlready.StopRequests", out var data))
            {
                data.Add(P.Name);
            }
        }

        internal static void Unlock()
        {
            if (EzSharedData.TryGet<HashSet<string>>("YesAlready.StopRequests", out var data))
            {
                data.Remove(P.Name);
            }
        }
    }
}
