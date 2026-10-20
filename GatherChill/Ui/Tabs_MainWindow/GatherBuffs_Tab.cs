using Dalamud.Interface.Utility;
using Dalamud.Interface.Utility.Raii;
using ECommons.ExcelServices;
using GatherChill.Enums;
using GatherChill.Gui;
using GatherChill.Utilities.GatheringHelpers;
using System.Collections.Generic;

namespace GatherChill.Ui.Tabs_MainWindow
{
    internal class GatherBuffs_Tab
    {
        private static float _modeWindow = 100f;
        private static List<Job> GatherJobs = new() { Job.MIN, Job.BTN };
        private static Job _selectedJob = GatherJobs[0];

        public static void Draw()
        {
            using (var child = ImRaii.Child("Gather Buffs: Child Container", default))
            {
                if (!child.Success)
                    return;

                var childColors = C.UseIceTheme ? ImRaii.PushColor(ImGuiCol.ChildBg, Theme_Colors.ChildBg) : default;
                var rightColumnAvail = ImGui.GetContentRegionAvail();
                float availWidth = rightColumnAvail.X;

                using (var child_Selection = ImRaii.Child("Gather Buffs: Selection", new(availWidth, _modeWindow), true))
                {
                    if (!child_Selection.Success)
                        return;

                    bool auto = true;
                    ImGui.Checkbox("Use Auto Buffs", ref auto);

                    for (int i = 0; i < GatherJobs.Count; i++)
                    {
                        if (i != 0)
                            ImGui.SameLine();

                        var job = GatherJobs[i];

                        bool isSelected = _selectedJob == job;
                        if (ImGui_Ice.ImageButton(Gather_Util.Job_IconIds[job].IconId, $"{job}", isSelected, 1))
                        {
                            _selectedJob = job;
                        }
                    }

                    _modeWindow = ImGui.GetCursorPosY() + ImGui.GetStyle().WindowPadding.Y;
                }

                using (var child_BuffInfo = ImRaii.Child("Gather Buffs: Details Window", ImGui.GetContentRegionAvail(), true))
                {
                    if (!child_BuffInfo.Success)
                        return;

                    ImGui.Text("Tehe, WIP");

                    var config = C.User_AutoProfile;

                    using (var table = ImRaii.Table("Table: Auto Gather Buff Stuff", 3, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
                    {
                        if (!table.Success)
                            return;

                        ImGui.TableSetupColumn("");
                        ImGui.TableSetupColumn("Buff");
                        ImGui.TableSetupColumn("Condition");

                        ImGui.TableHeadersRow();

                        foreach (var buff in config.BuffsEnabled)
                        {
                            ImGui.TableNextRow();
                            ImGui.TableSetColumnIndex(0);
                            ImGui.PushID($"{buff.Key}");

                            var enabled = buff.Value;
                            if (ImGui.Checkbox($"##{buff.Key}_state", ref enabled))
                            {
                                config.BuffsEnabled[buff.Key] = enabled;
                                C.SaveDebounced();
                            }

                            ImGui.TableNextColumn();
                            var info = Gather_Util.GathActionDict[buff.Key];
                            var classInfo = info.ClassAction[_selectedJob];
                            ImGui_Ice.ImageButtonWithText(classInfo.IconId, classInfo.Name, classInfo.Name, enabled, 1);

                            var globalScale = ImGuiHelpers.GlobalScale;

                            if (buff.Key is GatherBuffId.Tidings)
                            {
                                ImGui.TableNextColumn();
                                var tidingChance = config.TidingsChance;

                                ImGui.SetNextItemWidth(100 * globalScale);
                                if (ImGui.InputInt("Minimum %", ref tidingChance, 1, 10))
                                {
                                    if (tidingChance < 0)
                                        tidingChance = 0;
                                    if (tidingChance > 100)
                                        tidingChance = 100;

                                    config.TidingsChance = tidingChance;
                                    C.SaveDebounced();
                                }
                            }
                            else if (buff.Key is GatherBuffId.BYII)
                            {
                                ImGui.TableNextColumn();
                                var byII_Amount = config.BYII_Amount;

                                ImGui.SetNextItemWidth(100 * globalScale);
                                if (ImGui.InputInt($"{classInfo.Name} amount", ref byII_Amount, 1))
                                {
                                    if (byII_Amount < 1)
                                        byII_Amount = 1;
                                    if (byII_Amount > 3)
                                        byII_Amount = 3;

                                    config.BYII_Amount = byII_Amount;
                                    C.SaveDebounced();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
