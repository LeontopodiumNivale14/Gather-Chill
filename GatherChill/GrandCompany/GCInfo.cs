using ECommons.ExcelServices;
using ECommons.GameHelpers;
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

    public static bool ReadRequests(ContentsInfoDetail detail, out string problem)
    {
        var slots = detail.Provisioning(out problem);
        if (slots == null)
            return false;

        var read = new Dictionary<Job, RequestInfo>();
        foreach (var slot in slots)
        {
            var job = (Job)(byte)((byte)Job.MIN + slot.Slot);
            if (job is not (Job.MIN or Job.BTN))
                continue;

            read[job] = new RequestInfo
            {
                ItemId = slot.ItemId,
                Bonus = slot.Bonus,
                Amount = slot.Amount,
            };
        }

        RequestedItems = read;
        return true;
    }
}