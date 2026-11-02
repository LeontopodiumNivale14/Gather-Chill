using Dalamud.Interface.Utility;
using Dalamud.Interface.Utility.Raii;
using GatherChill.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui.Settings_Info;

public static partial class SettingsUi
{
    private static readonly string CharacterOptions = "Character Options";

    private static void DrawReturnPointCombo(string label, ReturnTypes current, Action<ReturnTypes> onSelected)
    {
        ImGui.SetNextItemWidth(200 * ImGuiHelpers.GlobalScale);
        using var combo = ImRaii.Combo(label, current.ToString());
        if (!combo.Success)
            return; // only exits the combo helper now

        foreach (var value in Enum.GetValues<ReturnTypes>())
        {
            var disabled = !P.lifestream.Installed && value != ReturnTypes.None;
            var selected = current == value;

            using (ImRaii.Disabled(disabled))
            {
                if (ImGui.Selectable(value.ToString(), selected))
                {
                    onSelected(value);
                    C.Save();
                }
            }

            if (disabled && ImGui.IsItemHovered(ImGuiHoveredFlags.AllowWhenDisabled))
                ImGui.SetTooltip("Requires lifestream to be installed");

            if (selected)
                ImGui.SetItemDefaultFocus();
        }
    }

    private static void DrawReturnPoint(string comboLabel, string commandLabel, ReturnTypes current, Action<ReturnTypes> setPoint, string command, Action<string> setCommand)
    {
        DrawReturnPointCombo(comboLabel, current, setPoint);

        if (current != ReturnTypes.Custom)
            return;

        ImGui.SetNextItemWidth(200 * ImGuiHelpers.GlobalScale);
        if (!ImGui.InputText(commandLabel, ref command))
            return;

        setCommand(command);
        C.SaveDebounced();
    }

    private static readonly SettingEntry Wait_ReturnPoint = CharacterEntry(
        label: "Between Loop Return",
        category: CharacterOptions,
        keywords: ["home", "fc", "apartment", "teleport", "custom", "between", "wait", "loop"],
        draw: character => DrawReturnPoint(
            comboLabel: "Between Loop Return",
            commandLabel: "Lifestream Command##Lifestream_Between",
            current: character.WaitReturnPoint,
            setPoint: v => character.WaitReturnPoint = v,
            command: character.WaitReturn_Command,
            setCommand: v => character.WaitReturn_Command = v));

    private static readonly SettingEntry Post_ReturnPoint = CharacterEntry(
        label: "Post Loop Return",
        category: CharacterOptions,
        keywords: ["home", "fc", "apartment", "teleport", "custom", "post", "after", "loop"],
        draw: character => DrawReturnPoint(
            comboLabel: "Post Loop Return",
            commandLabel: "Lifestream Command##Lifestream_Post",
            current: character.PostReturnPoint,
            setPoint: v => character.PostReturnPoint = v,
            command: character.PostReturn_Command,
            setCommand: v => character.PostReturn_Command = v));
}
