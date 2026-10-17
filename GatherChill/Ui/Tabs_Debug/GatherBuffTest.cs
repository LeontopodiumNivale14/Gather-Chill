using ECommons.ExcelServices;
using FFXIVClientStructs.FFXIV.Client.Game;
using GatherChill.Enums;
using GatherChill.Utilities.GatheringHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui.Tabs_Debug
{
    internal class GatherBuffTest
    {
        public static void Draw()
        {
            var gatherDict = Gather_Util.GathActionDict[GatherBuffId.GivingLand];

            ImGui.Text($"Giving land CD: MIN: {BuffCD(gatherDict.ClassAction[Job.MIN])} | BTN: {BuffCD(gatherDict.ClassAction[Job.BTN]):N1}");

            var hiddenSkill = Gather_Util.GathActionDict[GatherBuffId.HiddenFinder];
            foreach (var job in hiddenSkill.ClassAction)
            {
                ImGui.Text($"Job: [{job.Key}] | ID: {job.Value} | Status: {ActionAvailable(job.Value)}");
            }
        }
        private unsafe static float BuffCD(uint actionId)
        {
            // Get the recast time for an action
            var recastGroup = ActionManager.Instance()->GetRecastGroupDetail(ActionManager.Instance()->GetRecastGroup(1, actionId));

            if (recastGroup != null)
            {
                float total = recastGroup->Total;     // total cooldown duration
                float elapsed = recastGroup->Elapsed; // how much has elapsed
                float remaining = total - elapsed;    // time remaining
                bool isActive = recastGroup->IsActive; // Is Active (leaving these here because it's just nice to know/might use in future)

                return remaining;
            }
            else
            {
                return 0;
            }
        }

        private unsafe static uint ActionAvailable(uint actionId)
        {
            var am = ActionManager.Instance();
            if (am is null)
                return 999;

            return am->GetActionStatus(ActionType.Action, actionId, checkRecastActive: false, checkCastingActive: false);
        }
    }
}
