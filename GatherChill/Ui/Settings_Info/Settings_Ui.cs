using Dalamud.Interface.ImGuiFileDialog;
using Dalamud.Interface.Utility.Raii;
using ECommons.GameHelpers;
using GatherChill.ConfigFiles;
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

    // ---- Character context ----------------------------------------------------

    // null = use the logged-in character. Set this later if you add a character picker.
    private static ulong? _selectedCid;

    // Resolved once at the top of Draw(); character entries read it through CharacterEntry.
    private static Config.CharacterInformation? _character;

    private static void ResolveCharacter()
    {
        var cid = _selectedCid ?? Player.CID;
        _character = C.GetOrCreateCharacterInfo(cid); // swap C for your config instance
    }

    /// <summary>
    /// Builds a SettingEntry whose Draw receives the active character's info.
    /// Shows a disabled message instead when no character is available.
    /// </summary>
    private static SettingEntry CharacterEntry(
        string label,
        string category,
        Action<Config.CharacterInformation> draw,
        string[]? keywords = null)
    {
        return new SettingEntry
        {
            Label = label,
            Category = category,
            Keywords = keywords ?? Array.Empty<string>(),
            Draw = () =>
            {
                if (_character == null)
                {
                    ImGui.BeginDisabled();
                    ImGui.TextUnformatted($"{label} (log in to a character to edit)");
                    ImGui.EndDisabled();
                    return;
                }

                draw(_character);
            },
        };
    }

    public static List<SettingEntry> BuildRegistry() => new()
    {
        // Mount, Fly, and Fan Options
        UseMount,
        OptionalFly,
        MountMinDistance,
        MountDismountDistance,
        FlyMinDistance,
        MountSelection,
        FanSelection,

        // Cordial settings (BLEH)
        Cordial_Use,
        Cordial_MinGp,
        Cordial_Inverse,
        Cordial_Overcap,

        // Gather Buff Holding
        HoldGP_TimedNodes,
        TimedNodes_HoldTime,
        HoldGP_Crystals,
        Crystal_HoldTime,
        TimedNode_MinGP,
        WaitForGP_Collectables,
        WaitForGP_CollectableAmount,
        WaitForGP_HiddenItem,

        // Repair Settings
        SelfRepair,
        DarkMatter,
        RepairAmount,
        Spiritbond_Extract,

        // Misc
        ColorTheme,
        CompactOverlay,
        SafetyGatherDelay,

        // Character Specific
        Wait_ReturnPoint,
        Post_ReturnPoint,

        // Route Editor
        Route_SaveLoc,
        Route_UpdateAll,
        LoadExternal
    };

    private static List<SettingEntry>? _allSettings;
    private static List<SettingEntry> AllSettings => _allSettings ??= BuildRegistry();

    private static string _searchQuery = string.Empty;
    private static List<SettingEntry> _filtered = new();
    private static bool _filterDirty = true;

    private static FileDialogManager fileDialogManager = new FileDialogManager();
    private static bool _TestExport = false;

    // For some joke ideas on what to put in the search bar
    // 67 -> "You're stupid"
    // 69 -> "See, you got class"

    public static void Draw()
    {
        ResolveCharacter();

        using var colors = ImGui_Ice.PushChildBG();

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

        fileDialogManager.Draw();
    }

    private static void DrawSearchBar()
    {
        ImGui.SetNextItemWidth(-1);
        if (ImGui.InputTextWithHint("##settings-search", "Search settings...", ref _searchQuery, 128))
            _filterDirty = true;

        if (string.IsNullOrEmpty(_searchQuery))
            return;

        ImGui.SameLine();
        if (ImGui.SmallButton("Clear"))
        {
            _searchQuery = string.Empty;
            _filterDirty = true;
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
            if (!ImGui.CollapsingHeader(group.Key, ImGuiTreeNodeFlags.DefaultOpen))
                continue;

            ImGui.Indent();
            foreach (var entry in group)
                entry.Draw();
            ImGui.Unindent();
            ImGui.Spacing();
        }
    }
}