using Dalamud.Interface.Utility.Raii;
using ECommons.ExcelServices;
using ECommons.GameHelpers;
using FFXIVClientStructs.FFXIV.Client.Game;
using GatherChill.Gui;
using GatherChill.Utilities;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Utility;
using static ECommons.UIHelpers.AddonMasterImplementations.AddonMaster;

namespace GatherChill.Ui.Tabs_Debug
{
    internal class GatheringWindow
    {
        private static Gather_Util.GatherProfile DebugProfile = new();
        private static int GatherAmount = 0;
        private static string DebugSummary = "No buff";

        public static unsafe void Draw()
        {
            ImGui.SetNextItemWidth(100);
            ImGui.InputInt("Gather Amount", ref GatherAmount, 1, 10);
            ImGui.SameLine();
            if (ImGui.Button("Reset Profile"))
            {
                DebugSummary = "No buff";
                DebugProfile = new();
            }
            if (ImGui.Button(DebugSummary))
            {
                ImGui.SetClipboardText(DebugSummary);
            }

            if (GenericHelpers.TryGetAddonMaster<Utilities.Gathering>(out var gathering) && gathering.IsAddonReady)
            {
                using (var table = ImRaii.Table("Gathering Items", 7, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
                {
                    if (!table.Success)
                        return;

                    ImGui.TableSetupColumn("Name");
                    ImGui.TableSetupColumn("Amount");
                    ImGui.TableSetupColumn("Lv");
                    ImGui.TableSetupColumn("Gather %");
                    ImGui.TableSetupColumn("Boon %");
                    ImGui.TableSetupColumn("Hidden");
                    ImGui.TableSetupColumn("Rare");
                    ImGui.TableSetupColumn("");

                    var itemList = gathering.GatheredItems;
                    for (int i = 0; i < itemList.Count(); i++)
                    {
                        var item = itemList[i];

                        ImGui.TableNextRow();
                        ImGui.TableSetColumnIndex(0);
                        ImGui.Text($"{item.ItemName}");

                        ImGui.TableNextColumn();
                        ImGui.Text($"{item.GatherAmount:N0}");

                        ImGui.TableNextColumn();
                        ImGui.Text($"{item.ItemLevel}");

                        ImGui.TableNextColumn();
                        ImGui.Text($"{item.GatherChance}");

                        ImGui.TableNextColumn();
                        ImGui.Text($"{item.BoonChance}");

                        ImGui.TableNextColumn();
                        ImGui.Text($"{item.IsHidden}");

                        if (item.ItemLevel != 0)
                        {
                            ImGui.TableNextColumn();
                            if (ImGui.SmallButton($"Generate Info##{item.ItemName}"))
                            {
                                var level = 100;
                                var gp = Utils.GetGp();
                                var integrity = gathering.CurrentIntegrity;
                                var gather = item.GatherChance;
                                var boon = item.BoonChance;
                                var autoProfile = C.User_AutoProfile;

                                var result = Gather_Util.PlanBuffs(level, gp, gathering.TotalIntegrity, item.GatherChance, item.BoonChance, GatherAmount, autoProfile, item.GatherAmount);
                                DebugProfile = result.Profile;
                                DebugSummary = $"in: gp={gp}, integrity={gathering.TotalIntegrity}, gather={gather:N0}, boon={boon:N0}, need={GatherAmount:N0} | out: expected={result.ExpectedYield:0.00}, gp={result.Gp}, meetsNeed={result.MeetsNeed}, buffs={DebugProfile.BuffPriority.Count}";
                            }
                        }
                    }
                }

                using (var buffTable = ImRaii.Table("Buff Info", 3, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
                {
                    if (!buffTable.Success)
                        return;

                    ImGui.TableSetupColumn("Buff");
                    ImGui.TableSetupColumn("Use Amount");

                    ImGui.TableHeadersRow();
                    foreach (var buff in DebugProfile.BuffPriority)
                    {
                        var profileInfo = DebugProfile.GatheringBuffs[buff];
                        var actionInfo = Gather_Util.GathActionDict[buff];
                        var job = actionInfo.ClassAction[Job.BTN];

                        ImGui.TableNextRow();
                        ImGui.TableSetColumnIndex(0);
                        ImGui_Ice.ImageButtonWithText(job.IconId, job.Name, job.Name);

                        ImGui.TableNextColumn();
                        ImGui.Text($"{DebugProfile.GatheringBuffs[buff].MaxUse}");

                        ImGui.TableNextColumn();
                        ImGui.Text($"ID: {ActionAvailable(job.ActionId)}");
                        
                    }

                }
            }
            else if (GenericHelpers.TryGetAddonMaster<GatheringMasterpiece>(out var collectable) && collectable.IsAddonReady)
            {

            }
            else
            {
                ImGui.Text("Neither collectables, or normal gathering window is visible. Please open one of them...");
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
