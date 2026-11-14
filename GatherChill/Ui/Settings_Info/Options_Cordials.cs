using Dalamud.Interface.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui.Settings_Info;

public static partial class SettingsUi
{
    private static readonly string CordialOptions = "Cordial Options";
    private static SettingEntry Cordial_Use = new()
    {
        Label = "Use Cordials While Gathering",
        Category = CordialOptions,
        Keywords = new[] { "Cordials", "GP", "Gathering Points" },
        Draw = () =>
        {
            var v = C.Cordial_Auto;
            if (ImGui.Checkbox("Use Cordials while Gathering", ref v))
            {
                C.Cordial_Auto = v;
                C.Save();
            }
            if (v)
            {
                ImGui.Indent(15);
                var n = C.Cordial_NormalNode;
                if (ImGui.Checkbox("Allow on Normal Nodes", ref n))
                {
                    C.Cordial_NormalNode = n;
                    C.Save();
                }
                var l = C.Cordial_LegendaryNode;
                if (ImGui.Checkbox("Allow on Timed Nodes", ref l))
                {
                    C.Cordial_LegendaryNode = l;
                    C.Save();
                }
                var e = C.Cordial_EphemerialNode;
                if (ImGui.Checkbox("Allow on Ephemeral Nodes", ref e))
                {
                    C.Cordial_EphemerialNode = e;
                    C.Save();
                }
                ImGui.Unindent(15);
            }
        }
    };

    private static SettingEntry Cordial_MinGp = new()
    {
        Label = "Use Cordial at or below GP",
        Category = CordialOptions,
        Keywords = new[] {"Cordials", "GP", "Minimum", "Below"},
        Draw = () =>
        {
            var v = C.Cordial_Amount;
            ImGui.SetNextItemWidth(150 * ImGuiHelpers.GlobalScale);
            if (ImGui.InputInt("Use Cordial at or Below", ref v, 10, 100))
            {
                C.Cordial_Amount = Math.Max(0, v);
                C.SaveDebounced();
            }
        }
    };

    private static SettingEntry Cordial_Inverse = new()
    {
        Label = "Inverse Cordial Priority",
        Category = CordialOptions,
        Keywords = new[] { "Cordials", "GP", "Gathering Points", "Hi-Cordial", "Watered Cordial", "Inverse" },
        Draw = () =>
        {
            var v = C.Cordial_Inverse;
            if (ImGui.Checkbox("Inverse Priority (Watered -> Regular -> Hi)", ref v))
            {
                C.Cordial_Inverse = v;
                C.Save();
            }
        }
    };

    private static SettingEntry Cordial_Overcap = new()
    {
        Label = "Cordial: Prevent Overcap",
        Category = CordialOptions,
        Keywords = new[] { "Cordials", "GP", "Gathering Points", "Overcap", "Prevent" },
        Draw = () =>
        {
            var v = C.Cordial_PreventOvercap;
            if (ImGui.Checkbox("Prevent Overcapping GP", ref v))
            {
                C.Cordial_PreventOvercap = v;
                C.Save();
            }
        }
    };
}
