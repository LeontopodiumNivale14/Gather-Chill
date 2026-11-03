using Dalamud.Interface.Utility;
using GatherChill.Gui;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui.Settings_Info;

public static partial class SettingsUi
{
    private static readonly string RepairOption = "Repair & Spiritbond Options";

    private static SettingEntry SelfRepair = new()
    {
        Label = "Self Repair",
        Category = RepairOption,
        Keywords = new[] { "Repair", "Self", "Gear" },
        Draw = () =>
        {
            var v = C.SelfRepair;
            if (ImGui.Checkbox("Self Repair", ref v))
            {
                C.SelfRepair = v;
                C.Save();
            }
        }
    };

    private static SettingEntry DarkMatter = new()
    {
        Label = "Minimum Dark Matter",
        Category = RepairOption,
        Keywords = new[] { "Repair", "Gear", "Dark Matter", "Minimum" },
        Draw = () =>
        {
            var v = C.Minimum_DarkMatter;
            ImGui.SetNextItemWidth(150 * ImGuiHelpers.GlobalScale);
            if (ImGui.InputInt("Minimum Dark Matter", ref v, 1, 10))
            {
                C.Minimum_DarkMatter = v;
                C.SaveDebounced();
            }
            ImGui.SameLine();
            ImGui_Ice.IconWithTooltip(FontAwesomeIcon.QuestionCircle, "Minimum amount of dark matter to allow repairing", false);
        }
    };

    private static SettingEntry RepairAmount = new()
    {
        Label = "Minimum Dark Matter",
        Category = RepairOption,
        Keywords = new[] { "Repair", "Gear", "Dark Matter", "Minimum", "Amount" },
        Draw = () =>
        {
            var v = C.Repair_Amount;
            ImGui.SetNextItemWidth(150 * ImGuiHelpers.GlobalScale);
            if (ImGui.InputInt("Repair at %", ref v, 1, 10))
            {
                C.Repair_Amount = v;
                C.SaveDebounced();
            }
            ImGui.SameLine();
            ImGui_Ice.IconWithTooltip(FontAwesomeIcon.QuestionCircle, "What do you want your gear to be at to repair", false);
        }
    };

    private static SettingEntry Spiritbond_Extract = new()
    {
        Label = "Extract Materia",
        Category = RepairOption,
        Keywords = new[] { "Extract", "Materia", "Spiritbond" },
        Draw = () =>
        {
            var v = C.Extract_Spiritbond;
            if (ImGui.Checkbox("Extract Spiritbonded Gear", ref v))
            {
                C.Extract_Spiritbond = v;
                C.Save();
            }
        }
    };
}
