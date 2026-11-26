using Dalamud.Interface.Utility.Raii;
using ECommons.ExcelServices;
using GatherChill.Enums;
using GatherChill.GrandCompany.Tasks;
using GatherChill.Gui;
using GatherChill.Scheduler;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Utility;
using System.Collections.Generic;

namespace GatherChill.GrandCompany
{
    internal class GC_Window
    {
        private static float _modeWindow = 100f;

        private static bool GatherMIN = true;
        private static bool GatherBTN = true;

        private static string _startMessage = "";

        private static List<Job> GetShownJobs()
        {
            var jobs = new List<Job>();
            if (GatherMIN)
                jobs.Add(Job.MIN);
            if (GatherBTN)
                jobs.Add(Job.BTN);

            return jobs;
        }

        public static void Draw()
        {
            using var child = ImRaii.Child("GC Child: Container", default);
            if (!child.Success)
                return;

            var rightColumnAvail = ImGui.GetContentRegionAvail();
            float availWidth = rightColumnAvail.X;
            var shownJobs = GetShownJobs();

            using (var buttonChild = ImRaii.Child("GC Child: Buttons", new(availWidth, _modeWindow), true))
            {
                if (!buttonChild.Success)
                    return;

                if (ImGui.Button("Refresh Order"))
                {
                    if (!P.TaskManager.IsBusy)
                        Task_RefreshItems.Enqueue();
                }

                ImGui.SameLine();
                DrawGatherButtons(shownJobs);

                ImGui.Checkbox("##MIN", ref GatherMIN);
                ImGui.SameLine();
                GameIcons.DrawInline(Gather_Util.Job_IconIds[Job.MIN].IconId);

                ImGui.Checkbox("##BTN", ref GatherBTN);
                ImGui.SameLine();
                GameIcons.DrawInline(Gather_Util.Job_IconIds[Job.BTN].IconId, false);

                DrawStatus();

                _modeWindow = ImGui.GetCursorPosY() + ImGui.GetStyle().WindowPadding.Y;
            }

            using (var GCWindow = ImRaii.Child("GC Child: Table", ImGui.GetContentRegionAvail(), true))
            {
                if (!GCWindow.Success)
                    return;

                using (var table = ImRaii.Table("Grand Compnany Table", 5, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
                {
                    if (!table.Success)
                        return;

                    ImGui.TableSetupColumn("##Icon");
                    ImGui.TableSetupColumn("Job");
                    ImGui.TableSetupColumn("Item");
                    ImGui.TableSetupColumn("Need");
                    ImGui.TableSetupColumn("Have");

                    ImGui.TableHeadersRow();

                    foreach (var requestItem in GCInfo.RequestedItems)
                    {
                        if (!shownJobs.Contains(requestItem.Key))
                            continue;

                        var itemId = requestItem.Value.ItemId;
                        if (!Gather_Util.Sheet_ItemInfo.TryGetValue(itemId, out var sheetInfo))
                            continue;

                        ImGui.TableNextRow();
                        ImGui.TableSetColumnIndex(0);
                        GameIcons.DrawInline(sheetInfo.IconId, false);

                        ImGui.TableNextColumn();
                        GameIcons.DrawInline(Gather_Util.Job_IconIds[requestItem.Key].IconId, false);

                        ImGui.TableNextColumn();
                        ImGui.Text($"{sheetInfo.Name}");

                        var need = requestItem.Value.Amount;
                        var have = Utils.GetItemCount(itemId);

                        ImGui.TableNextColumn();
                        ImGui.Text($"{need}");

                        ImGui.TableNextColumn();
                        if (have >= need)
                            ImGuiEx.Text(EzColor.Green, $"{have}");
                        else
                            ImGui.Text($"{have}");
                    }
                }
            }
        }

        private static void DrawGatherButtons(List<Job> shownJobs)
        {
            var busy = GatherJobs.HasUnsettledJob || Gather_Helper.State != IceState.Idle;

            using (ImRaii.Disabled(busy))
            {
                if (ImGui.Button("Gather Needed"))
                    GC_Helper.Start(shownJobs, out _startMessage);
            }

            ImGui.SameLine();

            using (ImRaii.Disabled(!GC_Helper.IsRunning))
            {
                if (ImGui.Button("Stop"))
                    GC_Helper.Cancel();
            }
        }

        private static void DrawStatus()
        {
            if (GC_Helper.JobId == Guid.Empty)
            {
                if (_startMessage != "")
                    ImGui.TextDisabled(_startMessage);

                return;
            }

            var (state, gathered, wanted, message) = GC_Helper.Status();
            if ((GatherJobState)state == GatherJobState.Unknown)
            {
                if (_startMessage != "")
                    ImGui.TextDisabled(_startMessage);

                return;
            }

            ImGui.TextDisabled($"{(GatherJobState)state}: {gathered}/{wanted} | {message}");
        }
    }
}