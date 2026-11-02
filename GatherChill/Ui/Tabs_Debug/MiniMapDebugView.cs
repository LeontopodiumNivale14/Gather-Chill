using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using GatherChill.Gui;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui.Tabs_Debug;

public static unsafe class MiniMapDebugView
{
    private readonly record struct Row(
        string Source,
        int Index,
        uint IconId,
        int Scale,
        short RawX,
        short RawY)
    {
        // Stored as world * 16 (see AgentMap.AddMiniMapMarker)
        public Vector2 WorldXZ => new(RawX / 16f, RawY / 16f);
    }

    public static void Draw()
    {
        var agent = AgentMap.Instance();
        if (agent == null)
        {
            ImGui.TextUnformatted("AgentMap not available");
            return;
        }

        var rows = new List<Row>();
        CollectGathering(agent, rows);
        CollectMiniMap(agent, rows);
        CollectMap(agent, rows);

        ImGui.TextUnformatted(
            $"Territory: {agent->CurrentTerritoryId}  Map: {agent->CurrentMapId}  " +
            $"MiniMap count: {agent->MiniMapMarkerCount}  Map count: {agent->MapMarkerCount}");
        ImGui.TextUnformatted($"Rows: {rows.Count}");

        const ImGuiTableFlags flags =
            ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg |
            ImGuiTableFlags.Resizable | ImGuiTableFlags.ScrollY | ImGuiTableFlags.SizingFixedFit;

        if (!ImGui.BeginTable("##MiniMapMarkers", 6, flags, new Vector2(0, 400)))
            return;

        ImGui.TableSetupScrollFreeze(0, 1);
        ImGui.TableSetupColumn("Source");
        ImGui.TableSetupColumn("Idx");
        ImGui.TableSetupColumn("IconId");
        ImGui.TableSetupColumn("Scale");
        ImGui.TableSetupColumn("Raw (X, Y)");
        ImGui.TableSetupColumn("World (X, Z)");
        ImGui.TableHeadersRow();

        foreach (var row in rows)
        {
            ImGui.TableNextRow();

            ImGui.TableNextColumn();
            ImGui.TextUnformatted(row.Source);

            ImGui.TableNextColumn();
            ImGui.TextUnformatted(row.Index.ToString());

            ImGui.TableNextColumn();
            if (row.IconId != 0)
                ImGui_Ice.ImageButton(row.IconId, "Test Button");

            ImGui.TableNextColumn();
            ImGui.TextUnformatted(row.Scale.ToString());

            ImGui.TableNextColumn();
            ImGui.TextUnformatted($"{row.RawX}, {row.RawY}");

            ImGui.TableNextColumn();
            ImGui.TextUnformatted($"{row.WorldXZ.X:F1}, {row.WorldXZ.Y:F1}");
        }

        ImGui.EndTable();
    }

    // Always show all 6 slots (no filtering) so you can see which slot lights up
    // for which node type. The circles are intentionally approximate.
    private static void CollectGathering(AgentMap* agent, List<Row> rows)
    {
        var markers = agent->MiniMapGatheringMarkers;

        // i < Length: keep going while i is SMALLER than the length
        for (var i = 0; i < markers.Length; i++)
        {
            ref var data = ref markers[i].MapMarker;
            rows.Add(new Row("Gathering", i, data.IconId, (int)data.Scale, data.X, data.Y));
        }
    }

    private static void CollectMiniMap(AgentMap* agent, List<Row> rows)
    {
        var markers = agent->MiniMapMarkers;
        var count = agent->MiniMapMarkerCount;

        // i < count: keep going while i is SMALLER than count
        for (var i = 0; i < count && i < markers.Length; i++)
        {
            ref var data = ref markers[i].MapMarker;
            rows.Add(new Row("MiniMap", i, data.IconId, (int)data.Scale, data.X, data.Y));
        }
    }

    private static void CollectMap(AgentMap* agent, List<Row> rows)
    {
        var markers = agent->MapMarkers;
        var count = agent->MapMarkerCount;

        for (var i = 0; i < count && i < markers.Length; i++)
        {
            ref var data = ref markers[i].MapMarker;
            rows.Add(new Row("Map", i, data.IconId, (int)data.Scale, data.X, data.Y));
        }
    }
}
