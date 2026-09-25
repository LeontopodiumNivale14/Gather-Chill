using Dalamud.Interface.Utility.Raii;
using GatherChill.Gui;
using System;
using System.Collections.Generic;
using System.Text;
using static FFXIVClientStructs.FFXIV.Client.UI.Agent.AgentFishGuide;

namespace GatherChill.Ui.Settings_Info;

public static partial class SettingsUi
{
    public sealed class SettingEntry
    {
        public required string Label;
        public required string Category;
        public string[] Keywords = Array.Empty<string>();
        public required Action Draw;

        // Precomputed lowercase haystack so filtering isn't re-concatenating strings every frame.
        public string SearchHaystack => _haystack ??= string.Join(' ', new[] { Label, Category }.Concat(Keywords)).ToLowerInvariant();
        private string? _haystack;
    }

    public static List<SettingEntry> BuildRegistry() => new()
    {
        ColorTheme,
    };

    private static List<SettingEntry>? _allSettings;
    private static List<SettingEntry> AllSettings => _allSettings ??= BuildRegistry();

    private static string _searchQuery = string.Empty;
    private static List<SettingEntry> _filtered = new();
    private static bool _filterDirty = true;

    public static void Draw()
    {
        var childColors = C.UseIceTheme ? ImRaii.PushColor(ImGuiCol.ChildBg, Theme_Colors.ChildBg) : default;

        using (ImRaii.Child("Settings Ui: Window", ImGui.GetContentRegionAvail(), true))
        {
            DrawSearchBar();
            ImGui.Separator();
            ImGui.Spacing();

            if (string.IsNullOrWhiteSpace(_searchQuery))
            {
                DrawGrouped(AllSettings);
            }
            else
            {
                if (_filterDirty)
                    Refilter();

                if (_filtered.Count == 0)
                    ImGui.TextDisabled($"No settings match \"{_searchQuery}\".");
                else
                    DrawGrouped(_filtered);
            }
        }
    }

    private static void DrawSearchBar()
    {
        ImGui.SetNextItemWidth(-1);
        if (ImGui.InputTextWithHint("##settings-search", "Search settings...", ref _searchQuery, 128))
            _filterDirty = true;

        if (!string.IsNullOrEmpty(_searchQuery))
        {
            ImGui.SameLine();
            if (ImGui.SmallButton("Clear"))
            {
                _searchQuery = string.Empty;
                _filterDirty = true;
            }
        }
    }

    private static void Refilter()
    {
        var needle = _searchQuery.ToLowerInvariant();
        _filtered = AllSettings
            .Where(s => s.SearchHaystack.Contains(needle, StringComparison.Ordinal))
            .ToList();
        _filterDirty = false;
    }

    private static void DrawGrouped(List<SettingEntry> settings)
    {
        foreach (var group in settings.GroupBy(s => s.Category))
        {
            if (ImGui.CollapsingHeader(group.Key, ImGuiTreeNodeFlags.DefaultOpen))
            {
                ImGui.Indent();
                foreach (var entry in group)
                    entry.Draw();
                ImGui.Unindent();
                ImGui.Spacing();
            }
        }
    }
}
