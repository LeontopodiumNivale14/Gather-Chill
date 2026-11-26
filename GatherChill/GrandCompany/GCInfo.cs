using ECommons.ExcelServices;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using GatherChill.GrandCompany.Addons;
using GatherChill.Scheduler.Handlers;
using System.Collections.Generic;

namespace GatherChill.GrandCompany;

public static class GCInfo
{
    public class RequestInfo
    {
        public uint ItemId { get; set; }
        public bool Bonus { get; set; }
        public int Amount { get; set; }
    }

    public static Dictionary<Job, RequestInfo> RequestedItems = new();

    public static unsafe bool OpenTimers()
    {
        var agent = AgentContentsTimer.Instance();
        if (agent == null)
            return false;

        agent->Show();
        return true;
    }

    public static unsafe bool CloseTimers()
    {
        var agent = AgentContentsTimer.Instance();
        if (agent == null)
            return false;

        agent->Hide();
        return true;
    }

    public static unsafe void CloseGcInfo()
    {
        if (GenericHelpers.TryGetAddonMaster<ContentsInfoDetail>(out var contentInfoDetails) && contentInfoDetails.IsAddonReady)
            Ecom_Handler.CloseWindow(contentInfoDetails.Base);
    }
}
