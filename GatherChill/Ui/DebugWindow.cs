using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Interface.Colors;
using Dalamud.Interface.Utility.Raii;
using ECommons.GameHelpers;
using ECommons.UIHelpers.AddonMasterImplementations;
using GatherChill.Scheduler;
using GatherChill.Utilities;
using GatherChill.Utilities.Tools;
using Lumina.Excel.Sheets;
using System.Collections.Generic;
using System.Text;
using static GatherChill.Utilities.Tools.IceLogging;

namespace GatherChill.Ui;

internal class DebugWindow : Window
{
    public DebugWindow() :
        base($"Gather & Chill Debug {P.GetType().Assembly.GetName().Version} ###GatherChillDebug")
    {
        Flags = ImGuiWindowFlags.None;
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(100, 100),
            MaximumSize = new Vector2(3000, 3000)
        };
        P.windowSystem.AddWindow(this);
    }

    public void Dispose() { P.windowSystem.RemoveWindow(this); }

    public override void Draw()
    {
        if (ImGui.BeginTabBar("Gather & Chill: Debug Tabs"))
        {
            if (ImGui.BeginTabItem("Task Info"))
            {
                TaskInfoDetails();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("Log Viewer"))
            {
                Ui_LogViewer.Draw_Debug();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("Destination Log"))
            {
                DestinationLogViewer();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("Gathering Table"))
            {
                DrawGatherPointTable();
                ImGui.EndTabItem();
            }

            ImGui.EndTabBar();
        }
    }

    public void DrawGatherPointTable()
    {
        if (ImGui.BeginTable("GatherPointTable", 8,
            ImGuiTableFlags.Borders |
            ImGuiTableFlags.RowBg |
            ImGuiTableFlags.Resizable |
            ImGuiTableFlags.ScrollY |
            ImGuiTableFlags.Sortable | 
            ImGuiTableFlags.SizingFixedFit))
        {
            // Setup columns
            ImGui.TableSetupColumn("ID");
            ImGui.TableSetupColumn("Node IDs");
            ImGui.TableSetupColumn("Type");
            ImGui.TableSetupColumn("Level");
            ImGui.TableSetupColumn("Zone Name");
            ImGui.TableSetupColumn("Place Name");
            ImGui.TableSetupColumn("Expansion");
            ImGui.TableSetupColumn("Items");
            ImGui.TableSetupScrollFreeze(0, 1);
            ImGui.TableHeadersRow();

            // Get sorting specs
            var sortSpecs = ImGui.TableGetSortSpecs();
            IEnumerable<KeyValuePair<uint, GatherPointInfo>> sortedData = SheetInfo;

            if (sortSpecs.SpecsCount > 0)
            {
                var spec = sortSpecs.Specs;
                sortedData = spec.ColumnIndex switch
                {
                    0 => spec.SortDirection == ImGuiSortDirection.Ascending
                        ? SheetInfo.OrderBy(x => x.Key)
                        : SheetInfo.OrderByDescending(x => x.Key),
                    1 => spec.SortDirection == ImGuiSortDirection.Ascending
                        ? SheetInfo.OrderBy(x => x.Value.NodeIds?.FirstOrDefault() ?? 0)
                        : SheetInfo.OrderByDescending(x => x.Value.NodeIds?.FirstOrDefault() ?? 0),
                    2 => spec.SortDirection == ImGuiSortDirection.Ascending
                        ? SheetInfo.OrderBy(x => x.Value.Type)
                        : SheetInfo.OrderByDescending(x => x.Value.Type),
                    3 => spec.SortDirection == ImGuiSortDirection.Ascending
                        ? SheetInfo.OrderBy(x => x.Value.Level)
                        : SheetInfo.OrderByDescending(x => x.Value.Level),
                    4 => spec.SortDirection == ImGuiSortDirection.Ascending
                        ? SheetInfo.OrderBy(x => x.Value.ZoneName)
                        : SheetInfo.OrderByDescending(x => x.Value.ZoneName),
                    5 => spec.SortDirection == ImGuiSortDirection.Ascending
                        ? SheetInfo.OrderBy(x => x.Value.PlaceName)
                        : SheetInfo.OrderByDescending(x => x.Value.PlaceName),
                    6 => spec.SortDirection == ImGuiSortDirection.Ascending
                        ? SheetInfo.OrderBy(x => x.Value.ExpId)
                        : SheetInfo.OrderByDescending(x => x.Value.ExpId),
                    _ => SheetInfo
                };
            }

            // Draw rows
            foreach (var kvp in sortedData)
            {
                ImGui.TableNextRow();

                ImGui.TableNextColumn();
                ImGui.Text(kvp.Key.ToString());

                ImGui.TableNextColumn();
                // Display all NodeIds as comma-separated list
                if (kvp.Value.NodeIds != null && kvp.Value.NodeIds.Count > 0)
                {
                    ImGui.Text(string.Join(", ", kvp.Value.NodeIds));
                }
                else
                {
                    ImGui.Text("");
                }

                ImGui.TableNextColumn();
                ImGui.Text(kvp.Value.Type.ToString());

                ImGui.TableNextColumn();
                ImGui.Text(kvp.Value.Level.ToString());

                ImGui.TableNextColumn();
                ImGui.Text(kvp.Value.ZoneName ?? "");

                ImGui.TableNextColumn();
                ImGui.Text(kvp.Value.PlaceName ?? "");

                ImGui.TableNextColumn();
                ImGui.TextDisabled($"{kvp.Value.ExpId}");
                ImGui.SameLine();
                ImGui.Text(kvp.Value.ExpansionName);

                ImGui.TableNextColumn();
                if (kvp.Value.ItemIds != null && kvp.Value.ItemIds.Count > 0)
                {
                    ImGui.Text(string.Join(", ", kvp.Value.ItemIds));
                }
                else
                {
                    ImGui.Text("");
                }
            }

            ImGui.EndTable();
        }
    }

    public void TaskInfoDetails()
    {
        ImGui.Text($"Running task: {P.taskManager.NumQueuedTasks != 0} | Amount of queue'd task: {P.taskManager.NumQueuedTasks}");
        string currentTask = P.taskManager.CurrentTask?.Name ?? "";
        ImGui.Text($"Current task running: {currentTask}");
        ImGui.Text($"Current State: {SchedulerMain.State}");
        ImGui.Text($"ItemId set: {SchedulerMain.ItemId}");
        ImGui.Text($"Task Count: {P.taskManager.Tasks.Count}");
    }

    private static void DestinationLogViewer()
    {
        ImGuiTableFlags flags = ImGuiTableFlags.RowBg |
                                ImGuiTableFlags.Borders |
                                ImGuiTableFlags.ScrollY |
                                ImGuiTableFlags.SizingFixedFit;

        if (ImGui.BeginTable("Destination Log Viewer", 5, flags))
        {
            ImGui.TableSetupColumn("Timestamp");
            ImGui.TableSetupColumn("Start");
            ImGui.TableSetupColumn("Destination");
            ImGui.TableSetupColumn("Distance");

            ImGui.TableHeadersRow();

            var filteredLogs = DestinationLogs.Logs.AsEnumerable();
            var entryNumber = 0;

            foreach (var log in filteredLogs.OrderByDescending(l => l.Timestamp))
            {
                ImGui.TableNextRow();

                ImGui.PushID($"{log.PlayerDestination}_{entryNumber}");

                ImGui.TableSetColumnIndex(0);
                ImGui_Util.Table_VertCenterText(log.Timestamp.ToString("HH:mm:ss"));

                ImGui.TableNextColumn();
                ImGui_Util.Table_VertCenterText($"X: {log.PlayerStart.X:N2}, Y: {log.PlayerStart.Y:N2}, Z: {log.PlayerStart.Z:N2}");

                ImGui.TableNextColumn();
                ImGui_Util.Table_VertCenterText($"X: {log.PlayerDestination.X:N2}, Y: {log.PlayerDestination.Y:N2}, Z: {log.PlayerDestination.Z:N2}");

                ImGui.TableNextColumn();
                ImGui_Util.Table_VertCenterText($"{log.Distance}");

                ImGui.TableNextColumn();
                if (ImGui.Button("Copy Info"))
                {
                    var clipboardText = new StringBuilder();
                    clipboardText.AppendLine($"Start: X: {log.PlayerStart.X:N2}, Y: {log.PlayerStart.Y:N2}, Z: {log.PlayerStart.Z:N2}");
                    clipboardText.Append($"End: X: {log.PlayerDestination.X:N2}, Y: {log.PlayerDestination.Y:N2}, Z: {log.PlayerDestination.Z:N2}");
                    ImGui.SetClipboardText($"{clipboardText}");
                    Notify.Success("Log copied to clipbard");
                }
                ImGui.PopID();

                entryNumber += 1;
            }

            ImGui.EndTable();
        }
    }
}