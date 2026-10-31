using Dalamud.Interface.Utility;
using Dalamud.Interface.Utility.Raii;
using ECommons.ExcelServices;
using GatherChill.Gui;
using GatherChill.Utilities.GatheringHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui.Settings_Info;

public static partial class SettingsUi
{
    private static readonly string GatheringOptions = "Gathering Options";
    private static SettingEntry HoldGP_TimedNodes = new()
    {
        Label = "Hold GP For Timed Nodes",
        Category = GatheringOptions,
        Keywords = new[] { "GP", "Gathering Points", "Hold" },
        Draw = () =>
        {
            var v = C.HoldGP_TimeNodes;
            if (ImGui.Checkbox("Hold GP For Timed Nodes", ref v))
            {
                C.HoldGP_TimeNodes = v;
                C.Save();
            }
        }
    };

    private static SettingEntry TimedNodes_HoldTime = new()
    {
        Label = "Reserve GP Before Timed Nodes",
        Category = GatheringOptions,
        Keywords = new[] { "Cordials", "GP", "Gathering Points", "Hold", "Reserve", "Timed", "Buffs", "Skills", "Seconds" },
        Draw = () =>
        {
            var v = C.TimedNodes_HoldTime;
            using (var disabled = ImRaii.Disabled(!C.HoldGP_TimeNodes))
            {
                ImGui.SetNextItemWidth(100 * ImGuiHelpers.GlobalScale);
                if (ImGui.InputUInt("Seconds Before Timed Node", ref v, 1, 10))
                {
                    C.TimedNodes_HoldTime = v;
                    C.SaveDebounced();
                }
            }
            ImGui.SameLine();
            ImGui_Ice.IconWithTooltip(FontAwesomeIcon.QuestionCircle, "When a timed node is this many seconds away from spawning, buffs and skills that spend GP will not be used, so your GP is saved for the timed node.", false);
        }
    };

    private static SettingEntry HoldGP_Crystals = new()
    {
        Label = "Hold GP For Crystal Buff",
        Category = GatheringOptions,
        Keywords = new[] { "GP", "Gathering Points", "Hold", "Crystals", "Shards", "Clusters", "Giving Land" },
        Draw = () =>
        {
            var buff = Gather_Util.GathActionDict[Enums.GatherBuffId.GivingLand].ClassAction[Job.MIN].Name;

            var v = C.HoldGP_CrystalBuff;
            if (ImGui.Checkbox($"Hold GP For {buff}", ref v))
            {
                C.HoldGP_CrystalBuff = v;
                C.Save();
            }
        }
    };

    private static SettingEntry Crystal_HoldTime = new()
    {
        Label = "Reserve GP Before Crystal Buff",
        Category = GatheringOptions,
        Keywords = new[] { "Cordials", "GP", "Gathering Points", "Hold", "Reserve", "Timed", "Buffs", "Skills", "Seconds" },
        Draw = () =>
        {
            var buff = Gather_Util.GathActionDict[Enums.GatherBuffId.GivingLand].ClassAction[Job.MIN].Name;

            var v = C.CrystalBuff_HoldGP;
            using (var disabled = ImRaii.Disabled(!C.HoldGP_CrystalBuff))
            {
                ImGui.SetNextItemWidth(100 * ImGuiHelpers.GlobalScale);
                if (ImGui.InputUInt($"Seconds Before {buff}", ref v, 1, 10))
                {
                    C.CrystalBuff_HoldGP = v;
                    C.SaveDebounced();
                }
            }
            ImGui.SameLine();
            ImGui_Ice.IconWithTooltip(FontAwesomeIcon.QuestionCircle, $"When {buff} is this many seconds away from spawning, buffs and skills that spend GP will not be used, so your GP is saved for the node.", false);
        }
    };

    private static SettingEntry TimedNode_MinGP = new()
    {
        Label = "Wait to interact till GP [Collectable]",
        Category = GatheringOptions,
        Keywords = new[] { "GP", "Gathering Points", "Hold", "Timed Node", "Wait" },
        Draw = () =>
        {
            var v = C.TimedNode_MinGP;
            ImGui.SetNextItemWidth(100 * ImGuiHelpers.GlobalScale);
            if (ImGui.InputUInt("Wait for GP for Legendary/Unspoiled Nodes", ref v, 1, 10))
            {
                C.TimedNode_MinGP = v;
                C.SaveDebounced();
            }
            ImGui.SameLine();
            ImGui_Ice.IconWithTooltip(FontAwesomeIcon.QuestionCircle,
                "Wait for GP to be this before interacting with a legendary/unspoiled timed node\n" +
                "If the timed node has < 60s remaining, it will go ahead and interact with it, even if this condition isn't met\n" +
                "It's better to get some. Rather than none");
        }
    };

    private static SettingEntry WaitForGP_Collectables = new()
    {
        Label = "Hold GP for Collectables",
        Category = GatheringOptions,
        Keywords = new[] {"GP", "Gathering Points", "Wait", "Reserve", "True / False"},
        Draw = () =>
        {
            var v = C.WaitGP_Collectable;
            if (ImGui.Checkbox($"Wait for GP for regular collectables", ref v))
            {
                C.WaitGP_Collectable = v;
                C.Save();
            }
        }
    };

    private static SettingEntry WaitForGP_CollectableAmount = new()
    {
        Label = "GP Wait Amount [Collectable]",
        Category = GatheringOptions,
        Keywords = new[] { "GP", "Gathering Points", "Wait", "Amount", "Collectable" },
        Draw = () =>
        {
            var v = C.Collectable_WaitGP;
            using (var disabled = ImRaii.Disabled(!C.WaitGP_Collectable))
            {
                ImGui.SetNextItemWidth(100 * ImGuiHelpers.GlobalScale);
                if (ImGui.InputUInt("Wait Amount for Collectables", ref v, 1, 10))
                {
                    C.Collectable_WaitGP = v;
                    C.SaveDebounced();
                }
            }
            ImGui.SameLine();
            ImGui_Ice.IconWithTooltip(FontAwesomeIcon.QuestionCircle,
                "Amount of gp that is necessary before interacting with a normal collectable\n" +
                "Please note: These are NOT timed nodes in any form or matter. Those have a seperate option above for them in general ^\n" +
                "This is useful for things like the crystarium questline, where getting more quality is important for more items");
        }
    };

    private static SettingEntry WaitForGP_HiddenItem = new()
    {
        Label = "Wait for GP: Hidden Item",
        Category = GatheringOptions,
        Keywords = new[] { "GP", "Gathering Points", "Wait", "Reserve", "True / False" },
        Draw = () =>
        {
            var v = C.WaitGP_HiddenItem;
            if (ImGui.Checkbox($"Wait for GP for hidden Items", ref v))
            {
                C.WaitGP_HiddenItem = v;
                C.Save();
            }
            ImGui.SameLine();
            ImGui_Ice.IconWithTooltip(FontAwesomeIcon.QuestionCircle,
                "Really, only useful for the crystarium relic quest [Lv. 90 relics],\n" +
                "but will wait till you have at minimum 200 gp to ensure the item shows up");
        }
    };
}
