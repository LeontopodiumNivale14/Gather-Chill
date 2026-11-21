using Dalamud.Interface.Colors;
using Dalamud.Interface.Utility;
using GatherChill.Enums;
using GatherChill.Gui;
using GatherChill.Utilities.Utility;
using System.Collections.Generic;
using static GatherChill.ConfigFiles.Config;

namespace GatherChill.Ui.Settings_Info;

public static partial class SettingsUi
{
    private static readonly string ConsumableOptions = "Consumable Options";

    #region Helpers

    private const GatherNodeKind NormalMask = GatherNodeKind.Regular;
    private const GatherNodeKind TimedMask = GatherNodeKind.Legendary | GatherNodeKind.Unspoiled;
    private const GatherNodeKind EphemeralMask = GatherNodeKind.Ephemeral;
    private const GatherNodeKind AllMask = NormalMask | TimedMask | EphemeralMask;

    private const float SubIndent = 20f;

    private static void Indented(Action draw)
    {
        var indent = SubIndent * ImGuiHelpers.GlobalScale;
        ImGui.Indent(indent);
        draw();
        ImGui.Unindent(indent);
    }

    private static void DrawGroupSeparator()
    {
        ImGui.Spacing();
        ImGui.Separator();
        ImGui.Spacing();
    }

    // Header row for a consumable: separator above (except the first group), the enable checkbox,
    // and the node buttons on their own indented line underneath.
    private static void DrawConsumableToggle(string label, ConsumableConfig c, bool separatorAbove = true)
    {
        if (separatorAbove)
            DrawGroupSeparator();

        var enabled = c.Enable;
        if (ImGui.Checkbox(label, ref enabled))
        {
            c.Enable = enabled;
            C.Save();
        }

        if (!enabled)
            return;

        Indented(() =>
        {
            ImGui.TextDisabled("Use on:");
            ImGui.SameLine();
            DrawNodeKindButtons(label, c);
        });
    }

    private static void DrawItemPicker(string id, ConsumableConfig c, (uint Id, string Name)[]? options, bool hasQuality = true)
    {
        if (!c.Enable)
            return;

        if (options == null)
        {
            ImGui.TextDisabled("Item data not loaded yet");
            return;
        }

        var selected = c.ItemId == 0
            ? default
            : Array.Find(options, o => o.Id == c.ItemId);
        var hasSelected = selected.Name != null;

        var preview = "None selected";
        if (hasSelected)
            preview = selected.Name;
        else if (c.ItemId != 0)
            preview = $"Unknown item ({c.ItemId})";

        var width = 260f * ImGuiHelpers.GlobalScale;
        var popupId = $"##{id}Popup";

        // Label on the left, then a quiet frame-colored button instead of the accent-colored one
        ImGui.AlignTextToFramePadding();
        ImGui.TextUnformatted("Item:");
        ImGui.SameLine();

        var buttonPos = ImGui.GetCursorScreenPos();

        ImGui.PushStyleColor(ImGuiCol.Button, ImGui.GetColorU32(ImGuiCol.FrameBg));
        ImGui.PushStyleColor(ImGuiCol.ButtonHovered, ImGui.GetColorU32(ImGuiCol.FrameBgHovered));
        ImGui.PushStyleColor(ImGuiCol.ButtonActive, ImGui.GetColorU32(ImGuiCol.FrameBgActive));
        ImGui.PushStyleVar(ImGuiStyleVar.ButtonTextAlign, new Vector2(0f, 0.5f));

        var opened = ImGui.Button($"{preview}##{id}Button", new Vector2(width, 0));

        ImGui.PopStyleVar();
        ImGui.PopStyleColor(3);

        if (hasSelected)
            DrawItemEffectTooltip(c.ItemId);

        if (opened)
            ImGui.OpenPopup(popupId);

        // How many of the selected item we're carrying, and whether the quality mode can use any
        if (hasSelected)
        {
            var hqCount = Utils.GetItemCount(c.ItemId, includeHq: true, includeNq: false);
            var nqCount = Utils.GetItemCount(c.ItemId, includeHq: false, includeNq: true);

            var usable = hasQuality
                ? Utils.TryResolveQuality(c.ItemId, c.Quality, out _)
                : nqCount != 0;

            var text = hasQuality
                ? $"({hqCount} HQ / {nqCount} NQ on hand)"
                : $"({nqCount} on hand)";

            ImGui.SameLine();
            if (usable)
                ImGui.TextDisabled(text);
            else
                ImGui.TextColored(ImGuiColors.DalamudRed, text);
        }

        // Open directly under the button
        ImGui.SetNextWindowPos(
            new Vector2(buttonPos.X, buttonPos.Y + ImGui.GetFrameHeight()),
            ImGuiCond.Appearing);

        if (!ImGui.BeginPopup(popupId))
            return;

        if (ImGui.IsWindowAppearing())
        {
            itemFilter = string.Empty;
            RebuildPickerRows(options); // snapshot inventory counts once per open, not every frame
            ImGui.SetKeyboardFocusHere();
        }

        // Pinned header: search, None, and the current selection
        ImGui.SetNextItemWidth(width);
        ImGui.InputTextWithHint($"##{id}Filter", "Search...", ref itemFilter, 64);

        if (ImGui.Selectable("None", c.ItemId == 0))
        {
            c.ItemId = 0;
            C.Save();
        }

        if (hasSelected)
        {
            var selectedCount = Utils.GetItemCount(selected.Id);
            var selectedLabel = selectedCount != 0
                ? $"{selected.Name} (x{selectedCount})"
                : selected.Name;
            ImGui.Selectable($"{selectedLabel}##{id}Selected", true);
            DrawItemEffectTooltip(selected.Id);
        }

        ImGui.Separator();

        // Only this part scrolls
        if (ImGui.BeginChild($"##{id}List", new Vector2(width, 250f * ImGuiHelpers.GlobalScale)))
        {
            var ownedShown = false;
            var dividerDrawn = false;

            foreach (var row in pickerRows)
            {
                if (row.Id == c.ItemId)
                    continue; // already pinned above

                if (itemFilter.Length != 0 &&
                    !row.Name.Contains(itemFilter, StringComparison.OrdinalIgnoreCase))
                    continue;

                var owned = row.Count != 0;
                if (owned)
                    ownedShown = true;

                // Thin divider between "in inventory" and everything else
                if (!owned && ownedShown && !dividerDrawn)
                {
                    ImGui.Separator();
                    dividerDrawn = true;
                }

                var rowLabel = owned ? $"{row.Name} (x{row.Count})" : row.Name;
                var clicked = ImGui.Selectable($"{rowLabel}##{row.Id}");
                DrawItemEffectTooltip(row.Id);

                if (!clicked)
                    continue;

                c.ItemId = row.Id;
                C.Save();
                ImGui.CloseCurrentPopup(); // the child isn't a popup, so Selectable won't close it for us
            }
        }
        ImGui.EndChild();

        ImGui.EndPopup();
    }

    private static void DrawRefreshInput(string id, ConsumableConfig c, int maxMinutes = 59)
    {
        if (!c.Enable)
            return;

        var minutes = (int)(c.SecondsRemaining / 60);

        ImGui.SetNextItemWidth(100f * ImGuiHelpers.GlobalScale);
        if (!ImGui.InputInt($"Refresh when under (min)##{id}", ref minutes))
            return;

        minutes = Math.Clamp(minutes, 0, maxMinutes);
        c.SecondsRemaining = (uint)(minutes * 60);
        C.Save();
    }

    private static bool ToggleButton(string label, bool isOn)
    {
        var borderColor = isOn
            ? (C.UseIceTheme
                ? ImGui.GetColorU32(Theme_Colors.OnAccentSubtle)
                : ImGui.GetColorU32(ImGuiColors.ParsedGold))
            : ImGui.GetColorU32(ImGuiCol.Border);

        var buttonColor = isOn
            ? ImGui.GetColorU32(ImGuiCol.Button)
            : ImGui.GetColorU32(ImGuiCol.Button, 0.5f);

        var textColor = isOn
            ? ImGui.GetColorU32(ImGuiCol.Text)
            : ImGui.GetColorU32(ImGuiCol.Text, 0.6f);

        ImGui.PushStyleColor(ImGuiCol.Button, buttonColor);
        ImGui.PushStyleColor(ImGuiCol.Border, borderColor);
        ImGui.PushStyleColor(ImGuiCol.Text, textColor);
        ImGui.PushStyleVar(ImGuiStyleVar.FrameBorderSize, isOn ? 1.5f : 1f);

        var clicked = ImGui.SmallButton(label);

        ImGui.PopStyleVar();
        ImGui.PopStyleColor(3);
        return clicked;
    }

    // Draws the All / Normal / Timed / Ephemeral toggles for any ConsumableConfig.
    // The id keeps each group's buttons separate when several consumables are enabled at once.
    private static void DrawNodeKindButtons(string id, ConsumableConfig c)
    {
        ImGui.PushID(id);

        var changed = false;

        void Toggle(string label, GatherNodeKind mask)
        {
            // "On" only when every flag in the mask is set; a partial state counts as off
            var isOn = (c.AllowedNodes & mask) == mask;

            if (!ToggleButton(label, isOn))
                return;

            c.AllowedNodes = isOn
                ? c.AllowedNodes & ~mask
                : c.AllowedNodes | mask;
            changed = true;
        }

        Toggle("All", AllMask);
        ImGui.SameLine();
        Toggle("Normal", NormalMask);
        ImGui.SameLine();
        Toggle("Timed", TimedMask);
        ImGui.SameLine();
        Toggle("Ephemeral", EphemeralMask);

        ImGui.PopID();

        if (changed)
            C.Save();
    }

    private static void DrawConsumableToggle(string label, ConsumableConfig c)
    {
        var enabled = c.Enable;
        if (ImGui.Checkbox(label, ref enabled))
        {
            c.Enable = enabled;
            C.Save();
        }

        if (!enabled)
            return;

        Indented(() =>
        {
            ImGui.TextDisabled("Use on:");
            ImGui.SameLine();
            DrawNodeKindButtons(label, c);
        });
    }
    private static void DrawQualitySelector(string id, ConsumableConfig c)
    {
        if (!c.Enable)
            return;

        ImGui.TextUnformatted("Quality:");
        ImGui.SameLine();

        void Option(string label, ItemQuality quality)
        {
            if (!ToggleButton($"{label}##{id}Quality", c.Quality == quality))
                return;

            c.Quality = quality;
            C.Save();
        }

        Option("Prefer HQ", ItemQuality.PreferHQ);
        ImGui.SameLine();
        Option("HQ Only", ItemQuality.HQOnly);
        ImGui.SameLine();
        Option("NQ Only", ItemQuality.NQOnly);
    }

    private static string itemFilter = string.Empty;
    private static List<(uint Id, string Name, int Count)> pickerRows = new();

    private static void DrawGroupEnd(ConsumableConfig c)
    {
        if (!c.Enable)
            return;

        ImGui.Spacing();
        ImGui.Separator();
        ImGui.Spacing();
    }

    private static void RebuildPickerRows((uint Id, string Name)[] options)
    {
        // OrderBy on a bool puts false first, so "Count == 0" is false for owned items -> owned come first
        pickerRows = options
            .Select(o => (o.Id, o.Name, Count: Utils.GetItemCount(o.Id)))
            .OrderBy(r => r.Count == 0)
            .ThenBy(r => r.Name, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }
    private static void DrawItemPicker(string label, string id, ConsumableConfig c, (uint Id, string Name)[]? options, bool hasQuality = true)
    {
        if (!c.Enable)
            return;

        if (options == null)
        {
            ImGui.TextDisabled($"{label}: item data not loaded yet");
            return;
        }

        var selected = c.ItemId == 0
            ? default
            : Array.Find(options, o => o.Id == c.ItemId);
        var hasSelected = selected.Name != null;

        var preview = "None selected";
        if (hasSelected)
            preview = selected.Name;
        else if (c.ItemId != 0)
            preview = $"Unknown item ({c.ItemId})";

        var width = 300f * ImGuiHelpers.GlobalScale;
        var popupId = $"##{id}Popup";

        // Button that looks like the combo and opens the popup
        var buttonPos = ImGui.GetCursorScreenPos();
        if (ImGui.Button($"{preview}##{id}Button", new Vector2(width, 0)))
            ImGui.OpenPopup(popupId);

        ImGui.SameLine();
        ImGui.TextUnformatted(label);

        // How many of the selected item we're carrying, and whether the quality mode can use any
        if (hasSelected)
        {
            var hqCount = Utils.GetItemCount(c.ItemId, includeHq: true, includeNq: false);
            var nqCount = Utils.GetItemCount(c.ItemId, includeHq: false, includeNq: true);

            var usable = hasQuality
                ? Utils.TryResolveQuality(c.ItemId, c.Quality, out _)
                : nqCount != 0;

            var text = hasQuality
                ? $"({hqCount} HQ / {nqCount} NQ on hand)"
                : $"({nqCount} on hand)";

            ImGui.SameLine();
            if (usable)
                ImGui.TextDisabled(text);
            else
                ImGui.TextColored(ImGuiColors.DalamudRed, text);
        }

        // Open directly under the button
        ImGui.SetNextWindowPos(
            new Vector2(buttonPos.X, buttonPos.Y + ImGui.GetFrameHeight()),
            ImGuiCond.Appearing);

        if (!ImGui.BeginPopup(popupId))
            return;

        if (ImGui.IsWindowAppearing())
        {
            itemFilter = string.Empty;
            RebuildPickerRows(options); // snapshot inventory counts once per open, not every frame
            ImGui.SetKeyboardFocusHere();
        }

        // Pinned header: search, None, and the current selection
        ImGui.SetNextItemWidth(width);
        ImGui.InputTextWithHint($"##{id}Filter", "Search...", ref itemFilter, 64);

        if (ImGui.Selectable("None", c.ItemId == 0))
        {
            c.ItemId = 0;
            C.Save();
        }

        if (hasSelected)
        {
            var selectedCount = Utils.GetItemCount(selected.Id);
            var selectedLabel = selectedCount != 0
                ? $"{selected.Name} (x{selectedCount})"
                : selected.Name;
            ImGui.Selectable($"{selectedLabel}##{id}Selected", true);
        }

        ImGui.Separator();

        // Only this part scrolls
        if (ImGui.BeginChild($"##{id}List", new Vector2(width, 250f * ImGuiHelpers.GlobalScale)))
        {
            var ownedShown = false;
            var dividerDrawn = false;

            foreach (var row in pickerRows)
            {
                if (row.Id == c.ItemId)
                    continue; // already pinned above

                if (itemFilter.Length != 0 &&
                    !row.Name.Contains(itemFilter, StringComparison.OrdinalIgnoreCase))
                    continue;

                var owned = row.Count != 0;
                if (owned)
                    ownedShown = true;

                // Thin divider between "in inventory" and everything else
                if (!owned && ownedShown && !dividerDrawn)
                {
                    ImGui.Separator();
                    dividerDrawn = true;
                }

                var rowLabel = owned ? $"{row.Name} (x{row.Count})" : row.Name;
                if (!ImGui.Selectable($"{rowLabel}##{row.Id}"))
                    continue;

                c.ItemId = row.Id;
                C.Save();
                ImGui.CloseCurrentPopup(); // the child isn't a popup, so Selectable won't close it for us
            }
        }
        ImGui.EndChild();

        ImGui.EndPopup();
    }

    #endregion

    #region Cordials

    private static SettingEntry Cordial_Use = new()
    {
        Label = "Use Cordials While Gathering",
        Category = ConsumableOptions,
        Keywords = new[] { "Cordials", "GP", "Gathering Points" },
        Draw = () => DrawConsumableToggle("Use Cordials While Gathering", C.CordialConfig)
    };

    private static SettingEntry Cordial_MinGp = new()
    {
        Label = "Use Cordial at or below GP",
        Category = ConsumableOptions,
        Keywords = new[] { "Cordials", "GP", "Minimum", "Below" },
        Draw = () =>
        {
            if (!C.CordialConfig.Enable)
                return;

            Indented(() =>
            {
                var v = C.Cordial_Amount;
                ImGui.SetNextItemWidth(150 * ImGuiHelpers.GlobalScale);
                if (ImGui.InputInt("Use Cordial at or Below", ref v, 10, 100))
                {
                    C.Cordial_Amount = Math.Max(0, v);
                    C.SaveDebounced();
                }
            });
        }
    };

    private static SettingEntry Cordial_Inverse = new()
    {
        Label = "Inverse Cordial Priority",
        Category = ConsumableOptions,
        Keywords = new[] { "Cordials", "GP", "Gathering Points", "Hi-Cordial", "Watered Cordial", "Inverse" },
        Draw = () =>
        {
            if (!C.CordialConfig.Enable)
                return;

            Indented(() =>
            {
                var v = C.Cordial_Inverse;
                if (ImGui.Checkbox("Inverse Priority (Watered -> Regular -> Hi)", ref v))
                {
                    C.Cordial_Inverse = v;
                    C.Save();
                }
            });
        }
    };

    private static SettingEntry Cordial_Overcap = new()
    {
        Label = "Cordial: Prevent Overcap",
        Category = ConsumableOptions,
        Keywords = new[] { "Cordials", "GP", "Gathering Points", "Overcap", "Prevent" },
        Draw = () =>
        {
            if (!C.CordialConfig.Enable)
                return;

            Indented(() =>
            {
                var v = C.Cordial_PreventOvercap;
                if (ImGui.Checkbox("Prevent Overcapping GP", ref v))
                {
                    C.Cordial_PreventOvercap = v;
                    C.Save();
                }
            });

            DrawGroupEnd(C.CordialConfig);
        }
    };

    #endregion

    #region Food

    private static SettingEntry Food_Usage = new()
    {
        Label = "Use Food While Gathering",
        Category = ConsumableOptions,
        Keywords = new[] { "Food", "GP", "Consumable", "Gathering", "Perception" },
        Draw = () => DrawConsumableToggle("Use Food While Gathering", C.FoodConfig)
    };

    private static SettingEntry Food_Kind = new()
    {
        Label = "What kind of food do you want to use",
        Category = ConsumableOptions,
        Keywords = new[] { "Food", "GP", "Consumable", "Gathering", "Perception" },
        Draw = () => Indented(() => DrawItemPicker("Food", C.FoodConfig, Utils.GatherFood))
    };

    private static SettingEntry Food_Quality = new()
    {
        Label = "Food quality",
        Category = ConsumableOptions,
        Keywords = new[] { "Food", "HQ", "NQ", "Quality", "High Quality" },
        Draw = () => Indented(() => DrawQualitySelector("Food", C.FoodConfig))
    };

    private static SettingEntry Food_Refresh = new()
    {
        Label = "Refresh food when time remaining is low",
        Category = ConsumableOptions,
        Keywords = new[] { "Food", "Refresh", "Duration", "Time Remaining" },
        Draw = () =>
        {
            Indented(() => DrawRefreshInput("Food", C.FoodConfig));
            DrawGroupEnd(C.FoodConfig);
        }
    };

    #endregion

    #region Potions

    private static SettingEntry Potion_Usage = new()
    {
        Label = "Use Spiritbond Potions While Gathering",
        Category = ConsumableOptions,
        Keywords = new[] { "Potion", "Spiritbond", "Medicine", "Consumable" },
        Draw = () => DrawConsumableToggle("Use Spiritbond Potions While Gathering", C.PotionConfig)
    };

    private static SettingEntry Potion_Kind = new()
    {
        Label = "What kind of potion do you want to use",
        Category = ConsumableOptions,
        Keywords = new[] { "Potion", "Spiritbond", "Medicine", "Consumable" },
        Draw = () => Indented(() => DrawItemPicker("Potion", C.PotionConfig, Utils.SpiritbondPots))
    };

    private static SettingEntry Potion_Quality = new()
    {
        Label = "Potion quality",
        Category = ConsumableOptions,
        Keywords = new[] { "Potion", "HQ", "NQ", "Quality", "High Quality" },
        Draw = () => Indented(() => DrawQualitySelector("Potion", C.PotionConfig))
    };

    private static SettingEntry Potion_Refresh = new()
    {
        Label = "Refresh potion when time remaining is low",
        Category = ConsumableOptions,
        Keywords = new[] { "Potion", "Refresh", "Duration", "Time Remaining" },
        Draw = () =>
        {
            Indented(() => DrawRefreshInput("Potion", C.PotionConfig));
            DrawGroupEnd(C.PotionConfig);
        }
    };

    #endregion

    #region Manuals

    private static SettingEntry Manual_Usage = new()
    {
        Label = "Use Manuals While Gathering",
        Category = ConsumableOptions,
        Keywords = new[] { "Manual", "Consumable", "Experience" },
        Draw = () => DrawConsumableToggle("Use Manuals While Gathering", C.ManualConfig)
    };

    private static SettingEntry Manual_Kind = new()
    {
        Label = "What kind of manual do you want to use",
        Category = ConsumableOptions,
        Keywords = new[] { "Manual", "Consumable", "Experience" },
        Draw = () => Indented(() => DrawItemPicker("Manual", C.ManualConfig, Utils.GatherManuals, hasQuality: false))
    };

    private static SettingEntry Manual_Refresh = new()
    {
        Label = "Refresh manual when time remaining is low",
        Category = ConsumableOptions,
        Keywords = new[] { "Manual", "Refresh", "Duration", "Time Remaining" },
        Draw = () =>
        {
            Indented(() => DrawRefreshInput("Manual", C.ManualConfig));
            DrawGroupEnd(C.ManualConfig);
        }
    };

    #endregion
}
