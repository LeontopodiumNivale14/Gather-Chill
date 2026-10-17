using Dalamud.Interface.Utility.Raii;
using ECommons.ExcelServices;
using ECommons.GameHelpers;
using FFXIVClientStructs.FFXIV.Client.Game;
using GatherChill.Enums;
using GatherChill.Gui;
using GatherChill.Scheduler;
using GatherChill.Scheduler.Handlers;
using GatherChill.Ui.Tabs_Debug;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Utility;
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

    private static readonly Dictionary<string, Action> DebugTabs = new()
    {
        ["Game: Gathering Window"] = () => GatheringWindow.Draw(),

        ["Table: Gather Points"] = () => GatherPointTable.Draw(),

        ["Logs: Destination"] = () => Tabs_Debug.Logs_Destination.Draw(),

        ["Help: Task Info"] = () => TaskInfo.Draw(),

        ["Test: Gather Buffs"] = () => GatherBuffTest.Draw(),
        ["Test: Crazy Arrow"] = () => CrazyArrow.Draw(),

        ["Editor: Reduction Items"] = () => ReductionEditor.Draw(),
        ["Editor: Aethernet"] = () => AethernetEditor.Draw(),
    };

    private static string SelectedTab = DebugTabs.First().Key;

    public override void Draw()
    {
        using var colors = C.UseIceTheme ? ImRaii.PushColor(ImGuiCol.Header, Theme_Colors.HeaderBg) : default;
        if (C.UseIceTheme)
        {
            colors.Push(ImGuiCol.HeaderHovered, Theme_Colors.HeaderHovered);
            colors.Push(ImGuiCol.HeaderActive, Theme_Colors.HeaderActive);
            colors.Push(ImGuiCol.Button, Theme_Colors.ButtonBg);
            colors.Push(ImGuiCol.ButtonHovered, Theme_Colors.ButtonHovered);
            colors.Push(ImGuiCol.ButtonActive, Theme_Colors.ButtonActive);
            colors.Push(ImGuiCol.WindowBg, Theme_Colors.DarkSlate);
            colors.Push(ImGuiCol.FrameBg, Theme_Colors.FrameBg);
            colors.Push(ImGuiCol.FrameBgHovered, Theme_Colors.FrameBgHovered);
            colors.Push(ImGuiCol.FrameBgActive, Theme_Colors.FrameBgActive);
            colors.Push(ImGuiCol.CheckMark, Theme_Colors.IceBlue);
        }

        using var style = C.UseIceTheme ? ImRaii.PushStyle(ImGuiStyleVar.FrameRounding, 4.0f) : default;

        float spacing = 10f;
        float leftPanelWidth = 200f;
        float rightPanelWidth = ImGui.GetContentRegionAvail().X - leftPanelWidth - spacing;
        float childHeight = ImGui.GetContentRegionAvail().Y;

        if (ImGui.BeginTable("Debug Window: Table Details", 2, ImGuiTableFlags.None, ImGui.GetContentRegionAvail()))
        {
            ImGui.TableSetupColumn("Selector", ImGuiTableColumnFlags.WidthFixed, 200);
            ImGui.TableSetupColumn("Details", ImGuiTableColumnFlags.WidthStretch);

            ImGui.TableNextRow();
            ImGui.TableSetColumnIndex(0);
            using var childColors = C.UseIceTheme ? ImRaii.PushColor(ImGuiCol.ChildBg, Theme_Colors.ChildBg) : default;
            if (ImGui.BeginChild("Debug Selector##DebugSelector_ChilledLeves", ImGui.GetContentRegionAvail(), true))
            {
                foreach (var viewName in DebugTabs.Keys)
                {
                    bool isSelected = (SelectedTab == viewName);
                    string label = isSelected ? $"→ {viewName}" : $"{viewName}";

                    if (ImGui.Selectable(label, isSelected))
                    {
                        SelectedTab = viewName;
                    }
                }
            }
            ImGui.EndChild();

            ImGui.TableNextColumn();
            if (ImGui.BeginChild("Debug Viewer Tab##DebugViewTabDetails", ImGui.GetContentRegionAvail(), true))
            {
                if (DebugTabs.TryGetValue(SelectedTab, out var drawAction))
                    drawAction();
                else
                    ImGui.Text("Unknown debugger tab");
            }
            ImGui.EndChild();

            ImGui.EndTable();
        }
    }
}