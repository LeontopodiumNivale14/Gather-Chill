using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using GatherChill.Utilities.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui.Tabs_Debug
{
    internal class GcSupply
    {
        private const int MaxItems = 64; // sanity cap in case NumItems is garbage

        private static readonly string[] SupplyLabels = ["CRP", "BSM", "ARM", "GSM", "LTW", "WVR", "ALC", "CUL"];
        private static readonly string[] ProvisioningLabels = ["MIN", "BTN", "FSH"];

        public static unsafe void Draw()
        {
            if (ImGui.Button("Open Timers?"))
            {
                var result = OpenTimers();
                IceLogging.Verbose($"Timer Result: {result}");
            }
        }

        public static unsafe bool OpenTimers()
        {
            var agentModule = AgentModule.Instance();
            if (agentModule == null)
                return false;

            var agent = agentModule->GetAgentByInternalId(AgentId.ContentsTimer);
            if (agent == null)
                return false;

            agent->Show();
            return true;
        }
    }
}
