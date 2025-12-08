using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Interface.Colors;
using Dalamud.Interface.Utility.Raii;
using ECommons.GameHelpers;
using ECommons.UIHelpers.AddonMasterImplementations;
using GatherChill.Scheduler;
using GatherChill.Utilities;
using Lumina.Excel.Sheets;
using System.Collections.Generic;

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
        DrawGatherPointTable();
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
}