using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Interface.Colors;
using Dalamud.Interface.Utility.Raii;
using ECommons.ExcelServices;
using ECommons.GameHelpers;
using ECommons.UIHelpers.AddonMasterImplementations;
using FFXIVClientStructs.FFXIV.Client.Game;
using GatherChill.Enums;
using GatherChill.Scheduler;
using GatherChill.Scheduler.Handlers;
using GatherChill.Scheduler.Tasks;
using GatherChill.Utilities;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Utility;
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

            if (ImGui.BeginTabItem("Navmesh"))
            {
                DrawNavmeshDebug();
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

            if (ImGui.BeginTabItem("Buff Info"))
            {
                BuffViewer();
                ImGui.EndTabItem();
            }

            if (ImGui.BeginTabItem("Crazy Arrow Test"))
            {
                DrawArrowDebugWindow();
            }

            ImGui.EndTabBar();
        }
    }

    private uint _NodeIdSearch = 0;

    public void DrawGatherPointTable()
    {
        if (ImGui.BeginTable("GatherPointTable", 8, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.Resizable | ImGuiTableFlags.ScrollY | ImGuiTableFlags.Sortable |  ImGuiTableFlags.SizingFixedFit))
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
            ImGui.TableSetupScrollFreeze(0, 2);
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

            ImGui.TableNextRow();
            ImGui.TableSetColumnIndex(1);
            ImGui.InputUInt("###NodeIdSearch", ref _NodeIdSearch);

            // Draw rows
            foreach (var kvp in sortedData)
            {
                if (_NodeIdSearch != 0 && !kvp.Value.NodeIds.Contains(_NodeIdSearch))
                    continue;

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
    private static void DrawNavmeshDebug()
    {
        ImGui.Text($"Installed: {P.navmesh.Installed}");
        ImGui.Text($"Fly unlocked: {NavmeshMovement.IsFlyingUnlocked()}");
        ImGui.Text($"Mount allowed here: {NavmeshMovement.CanMountInCurrentTerritory()}");
        ImGui.Text($"Can use fly movement: {NavmeshMovement.CanUseFlyMovement()}");
        ImGui.Text($"Ready: {(P.navmesh.Installed ? P.navmesh.IsReady() : false)}");
        var buildProgress = P.navmesh.Installed ? P.navmesh.GetBuildProgress() : -1f;
        ImGui.Text(buildProgress < 0f
            ? "Mesh: idle (vnavmesh loads on zone change when auto-load is on)"
            : $"Build progress: {buildProgress:P0}");
        ImGui.Text($"Path running: {(P.navmesh.Installed ? P.navmesh.IsRunning() : false)}");
        ImGui.Text($"Pathfind in progress: {(P.navmesh.Installed ? P.navmesh.IsPathfindInProgress() : false)}");
        ImGui.Text($"Owns path: {NavmeshRuntime.OwnsPath}");
        ImGui.Text($"Last move: {NavmeshRuntime.LastMoveContext}");
        ImGui.Text($"Last destination: {NavmeshRuntime.LastDestination}");
        ImGui.Text($"Last close range: {NavmeshRuntime.LastCloseRange:N2}");
        ImGui.Text($"Stuck attempts: {NavmeshRuntime.StuckAttempts}");
        if (!string.IsNullOrEmpty(NavmeshRuntime.LastFailure))
            ImGui.TextColored(ImGuiColors.DalamudRed, $"Last failure: {NavmeshRuntime.LastFailure}");

        if (NavmeshRuntime.LastDestination != default)
            ImGui.Text($"Horizontal dist: {NavmeshMovement.HorizontalDistance(NavmeshRuntime.LastDestination):N2}");

        if (ImGui.Button("Stop owned path"))
            Task_NavmeshMove.ReleaseOwnedPath();

        ImGui.SameLine();
        if (ImGui.Button("Stop all navmesh"))
            P.navmesh.StopCompletely();
    }

    public void TaskInfoDetails()
    {
        ImGui.Text($"Running task: {P.taskManager.NumQueuedTasks != 0} | Amount of queue'd task: {P.taskManager.NumQueuedTasks}");
        string currentTask = P.taskManager.CurrentTask?.Name ?? "";
        ImGui.Text($"Current task running: {currentTask}");
        ImGui.Text($"Current State: {SchedulerMain.State}");
        ImGui.Text($"ItemId set: {SchedulerMain.ItemId}");
        ImGui.Text($"Player job: {Player.ClassJob.RowId} | Auto-swap class: {C.AutoSwapGatheringClass}");
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
    private void BuffViewer()
    {
        var gatherDict = Gather_Util.GathActionDict[GatherBuffId.GivingLand];

        ImGui.Text($"Giving land CD: MIN: {BuffCD(gatherDict.ClassAction[Job.MIN])} | BTN: {BuffCD(gatherDict.ClassAction[Job.BTN]):N1}");
    }
    private unsafe float BuffCD(uint actionId)
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

    private static void DrawArrowDebugWindow()
    {
        if (ImGui.Button("Set to player Pos"))
        {
            _position = Player.Position;
        }
        ImGui.SliderFloat("Back Radius", ref _backRadius, 0.1f, 3f);
        ImGui.SliderFloat("Tip Radius", ref _tipRadius, 0.05f, 2f);
        ImGui.SliderFloat("Depth", ref _depth, 0.1f, 5f);
        ImGui.SliderFloat("Wing Spread", ref _wingSpread, 0f, 3f);
        ImGui.SliderFloat("Notch Frac", ref _notchFrac, 0.05f, 0.8f);
        ImGui.SliderFloat("Rotation", ref _rotation, 0f, MathF.PI * 2f);
        ImGui.ColorEdit4("Color Editor", ref color);

        if (Player.Available)
        {
            // Draw slightly in front of the player so you can see it
            var pos = Player.Position + new Vector3(0, 0.1f, 0);
            PictoManager.DrawArrow(_position, Player.Rotation + _rotation, _backRadius, _tipRadius, _depth, _wingSpread, _notchFrac, Utils.ToUintABGR(color));
        }
    }

    private static Vector4 color = Vector4.Zero;

    private static Vector3 _position = Vector3.Zero;
    private static float _backRadius = 1.0f;
    private static float _tipRadius = 0.35f;
    private static float _depth = 1.5f;
    private static float _wingSpread = 0.7f;
    private static float _notchFrac = 0.33f;
    private static float _rotation = 0f;
}