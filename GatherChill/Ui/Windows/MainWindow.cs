using Dalamud.Interface.Colors;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Utility.Raii;
using GatherChill.Enums;
using GatherChill.GrandCompany;
using GatherChill.Gui;
using GatherChill.Ui.Settings_Info;
using GatherChill.Ui.Tabs_MainWindow;
using GatherChill.Utilities.Tools;
using System.Collections.Generic;
using System.Reflection;
using Action = System.Action;

namespace GatherChill.Ui.Windows
{
    internal class MainWindow : Window
    {
        private const string WindowId = "###Gather&ChillMainWindow";
        private static readonly string VersionString = P.GetType().Assembly.GetName().Version?.ToString() ?? "";

        public static string MainWindowName = "Gather";

        public MainWindow() : base($"{MainWindowName} & Chill {VersionString} {WindowId}")
        {
            Flags = ImGuiWindowFlags.None;

            SizeConstraints = new()
            {
                MinimumSize = new Vector2(500, 500),
                MaximumSize = new Vector2(2000, 2000)
            };
            TitleBarButtons.Add(new() { ShowTooltip = () => ImGui.SetTooltip("♥ Ko-fi (Buy me an ice coffee)"), Icon = FontAwesomeIcon.Heart, IconOffset = new(1, 1), Click = _ => GenericHelpers.ShellStart("https://ko-fi.com/ice643269") });

            P.windowSystem.AddWindow(this);

            AllowPinning = false;
        }

        public void Dispose()
        {
            P.windowSystem.RemoveWindow(this);
        }

        public static void SetName(int id)
        {
            MainWindowName = GetNameForId(id);
            IceLogging.Verbose($"Set ID to: {id}", "Main Window", true);
        }

        private static string GetNameForId(int id)
        {
            if (id == 69)
                return "Vera";

            if (1 < id && id < 100)
                return "Forager";

            return "Gather";
        }

        public override void OnOpen()
        {
            SetName(Random.Shared.Next(1, 1000));
            WindowName = $"{MainWindowName} & Chill {VersionString} {WindowId}";
        }

        public class WindowInfo
        {
            public FontAwesomeIcon Icon { get; set; }
            public string Label { get; set; }
            public Action Draw { get; set; }
        }

        public Dictionary<WindowSelection, WindowInfo> TabItems = new()
        {
            [WindowSelection.ItemViewer] = new()
            {
                Icon = FontAwesomeIcon.Leaf,
                Label = "Item Viewer",
                Draw = () => Item_Menu.Draw()
            },
            [WindowSelection.GatherList] = new()
            {
                Icon = FontAwesomeIcon.PlayCircle,
                Label = "Gather Playlist",
                Draw = () => GatherPlaylist.Draw()
            },
            [WindowSelection.Scripts] = new()
            {
                Icon = FontAwesomeIcon.TicketAlt,
                Label = "Gather Scripts",
                Draw = () =>
                {
                    using (var child = ImRaii.Child($"Script Window"))
                    {
                        if (!child.Success)
                            return;

                        ImGui.Text($"Work in progress");
                        ImGui.Text($"Come back later");
                    }
                }
            },
            [WindowSelection.Diadem] = new()
            {
                Icon = FontAwesomeIcon.MountainSun,
                Label = "Diadem",
                Draw = () =>
                {
                    using (var child = ImRaii.Child($"Diadem Window"))
                    {
                        if (!child.Success)
                            return;

                        ImGui.Text("Not implimented yet");
                        ImGui.Text("But if I put this here, it'll constnatly remind me to do it");
                    }
                }
            },
            [WindowSelection.GrandCompany] = new()
            {
                Icon = FontAwesomeIcon.Flag,
                Label = "Grand Company",
                Draw = () => GC_Window.Draw()
            },
            [WindowSelection.SavedList] = new()
            {
                Icon = FontAwesomeIcon.Save,
                Label = "Saved List",
                Draw = () => SavedList.Draw(),
            },
            [WindowSelection.Gatherbuffs] = new()
            {
                Icon = FontAwesomeIcon.UserCog,
                Label = "Gather Buffs",
                Draw = () => GatherBuffs_Tab.Draw()
            },
            [WindowSelection.Reducables] = new()
            {
                Icon = FontAwesomeIcon.FlaskVial,
                Label = "Reducables",
                Draw = () => ReducableViewer.Draw()
            },
            [WindowSelection.GatherLog] = new()
            {
                Icon = FontAwesomeIcon.Book,
                Label = "Gather Logs",
                Draw = () => GatherLog.Draw(),
            },
            [WindowSelection.RouteEditor] = new()
            {
                Icon = FontAwesomeIcon.Route,
                Label = "Route Editor",
                Draw = () => RouteEditor.Draw()
            },
            [WindowSelection.Logs] = new()
            {
                Icon = FontAwesomeIcon.Clipboard,
                Label = "Logs",
                Draw = () => LogsWindow.Draw()
            }
        };

        private static readonly WindowInfo SettingsTab = new()
        {
            Icon = FontAwesomeIcon.Cog,
            Label = "Settings",
            Draw = () => SettingsUi.Draw()
        };

        public override void Draw()
        {
            using var colors = ImGui_Ice.PushChildBG();
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
                colors.Push(ImGuiCol.Text, Theme_Colors.FrostWhite);
                colors.Push(ImGuiCol.PopupBg, Theme_Colors.PopupBg);
                colors.Push(ImGuiCol.Header, Theme_Colors.HeaderBg);
                colors.Push(ImGuiCol.HeaderActive, Theme_Colors.HeaderActive);
            }

            using var style = C.UseIceTheme ? ImRaii.PushStyle(ImGuiStyleVar.FrameRounding, 4.0f) : default;

            SelectableSidebar();
            ImGui.SameLine();
            DetailsInfo();
        }

        private void SelectableSidebar()
        {
            var scale = ImGuiHelpers.GlobalScale;
            bool compact = C.Compact_Icons;

            float scaledWidth = (compact ? (C.Compact_Ui ? 52 : 62) : 200) * scale;
            var height = ImGui.GetContentRegionAvail().Y;

            using var colors = ImGui_Ice.PushChildBG();

            using var sidebarSelection = ImRaii.Child("CL_SidebarSelection", new(scaledWidth, height), true);
            if (!sidebarSelection.Success)
                return;

            PluginIcon(compact);

            // Everything left over after the footer goes to the scrollable tab list
            float footerHeight = GetFooterHeight(compact);
            float listHeight = MathF.Max(0f, ImGui.GetContentRegionAvail().Y - footerHeight);

            DrawTabList(compact, listHeight);
            DrawSidebarFooter(compact);
        }

        private void DrawTabList(bool compact, float height)
        {
            using var list = ImRaii.Child("CL_TabList", new Vector2(0, height), false);
            if (!list.Success)
                return;

            if (compact)
            {
                foreach (var item in TabItems)
                {
                    if (IconTabButton(item.Value.Icon, item.Value.Label, item.Key == C.SelectedTab))
                        SelectTab(item.Key);
                }

                return;
            }

            using var table = ImRaii.Table("##tabList", 3, ImGuiTableFlags.None);
            if (!table)
                return;

            var scale = ImGuiHelpers.GlobalScale;
            ImGui.TableSetupColumn("accent", ImGuiTableColumnFlags.WidthFixed, 0.5f * scale);
            ImGui.TableSetupColumn("icon", ImGuiTableColumnFlags.WidthFixed, 24 * scale);
            ImGui.TableSetupColumn("label", ImGuiTableColumnFlags.WidthStretch);

            foreach (var item in TabItems)
            {
                if (C.Compact_Ui)
                    DrawSelectable_Icon_Small(item.Value.Icon, item.Value.Label, item.Key, false);
                else
                    DrawSelectable_Icon(item.Value.Icon, item.Value.Label, item.Key, false);
            }
        }

        private static void SelectTab(WindowSelection tab)
        {
            C.SelectedTab = tab;
            C.SaveDebounced();
        }

        private static void ToggleCompact()
        {
            C.Compact_Icons = !C.Compact_Icons;
            C.SaveDebounced();
        }

        private static float GetIconButtonSide(FontAwesomeIcon icon)
        {
            float scale = ImGuiHelpers.GlobalScale;

            ImGui.PushFont(UiBuilder.IconFont);
            var iconSize = ImGui.CalcTextSize(icon.ToIconString());
            ImGui.PopFont();

            // Same formula as IconTabButton so the footer matches the tab buttons
            float padding = (C.Compact_Ui ? 4f : 9f) * scale;
            return MathF.Max(iconSize.X, iconSize.Y) + padding * 2f;
        }

        private static float GetFooterHeight(bool compact)
        {
            float scale = ImGuiHelpers.GlobalScale;
            var style = ImGui.GetStyle();

            float side = GetIconButtonSide(FontAwesomeIcon.AngleDoubleLeft);
            float gap = (C.Compact_Ui ? 2f : 4f) * scale;
            float separator = style.ItemSpacing.Y * 2f + 1f;
            float slack = 6f * scale;

            // Compact: toggle stacked above settings. Expanded: one shared row.
            float buttons = compact ? side * 2f + gap : side;

            return separator + buttons + style.ItemSpacing.Y + slack;
        }

        private static void DrawSidebarFooter(bool compact)
        {
            ImGui.Separator();

            bool settingsSelected = C.SelectedTab == WindowSelection.Settings;

            if (compact)
            {
                if (IconTabButton(FontAwesomeIcon.AngleDoubleRight, "Expand sidebar", false))
                    ToggleCompact();

                if (IconTabButton(SettingsTab.Icon, SettingsTab.Label, settingsSelected))
                    SelectTab(WindowSelection.Settings);

                return;
            }

            float side = GetIconButtonSide(FontAwesomeIcon.AngleDoubleLeft);
            float rowWidth = ImGui.GetContentRegionAvail().X - side - ImGui.GetStyle().ItemSpacing.X;

            if (SettingsRow(rowWidth, side, settingsSelected))
                SelectTab(WindowSelection.Settings);

            ImGui.SameLine();

            if (IconTabButton(FontAwesomeIcon.AngleDoubleLeft, "Collapse sidebar", false))
                ToggleCompact();
        }

        private static bool SettingsRow(float width, float height, bool isSelected)
        {
            float scale = ImGuiHelpers.GlobalScale;

            var pos = ImGui.GetCursorScreenPos();
            bool clicked = ImGui.InvisibleButton("##tab_Settings", new Vector2(width, height));

            bool hovered = ImGui.IsItemHovered();
            var drawList = ImGui.GetWindowDrawList();
            var rounding = ImGui.GetStyle().FrameRounding;

            // --- Background (same colors the table rows use) ---
            if (isSelected)
                drawList.AddRectFilled(pos, pos + new Vector2(width, height), ImGui.GetColorU32(ImGuiCol.HeaderActive), rounding);
            else if (hovered)
                drawList.AddRectFilled(pos, pos + new Vector2(width, height), ImGui.GetColorU32(ImGuiCol.HeaderHovered), rounding);

            // --- Accent bar (selected only) ---
            float accentWidth = 4f * scale;
            if (isSelected)
            {
                var accent = C.UseIceTheme ? Theme_Colors.SidebarAccent : ImGuiColors.ParsedGold;
                drawList.AddRectFilled(pos, pos + new Vector2(accentWidth, height), ImGui.GetColorU32(accent));
            }

            // --- Icon, centered in a 24px slot after the accent bar ---
            float iconSlot = 24f * scale;

            ImGui.PushFont(UiBuilder.IconFont);
            var iconSize = ImGui.CalcTextSize(SettingsTab.Icon.ToIconString());
            var iconPos = new Vector2(
                pos.X + accentWidth + (iconSlot - iconSize.X) * 0.5f,
                pos.Y + (height - iconSize.Y) * 0.5f);
            drawList.AddText(iconPos, ImGui.GetColorU32(ImGuiCol.Text), SettingsTab.Icon.ToIconString());
            ImGui.PopFont();

            // --- Label ---
            var labelPos = new Vector2(
                pos.X + accentWidth + iconSlot + 4f * scale,
                pos.Y + (height - ImGui.GetTextLineHeight()) * 0.5f);
            drawList.AddText(labelPos, ImGui.GetColorU32(ImGuiCol.Text), SettingsTab.Label);

            return clicked;
        }

        private void DetailsInfo()
        {
            var selectedTab = C.SelectedTab;

            if (selectedTab == WindowSelection.Settings)
            {
                ImGui.SameLine();
                SettingsTab.Draw();
                return;
            }

            if (!TabItems.TryGetValue(selectedTab, out var tabInfo))
            {
                ImGui.Text($"Can't find the tab: {selectedTab}");
                return;
            }

            ImGui.SameLine();
            tabInfo.Draw();
        }
        private static void PluginIcon(bool compact)
        {
            const string iconPath = "GatherChill.Resources.Icon.png";

            var pluginIcon = Svc.Texture.GetFromManifestResource(Assembly.GetExecutingAssembly(), iconPath).GetWrapOrEmpty();
            if (pluginIcon == null) return;

            var sidebarWidth = ImGui.GetContentRegionAvail().X;

            // Compact: shrink the icon to fit the narrow sidebar (with a little padding).
            // Normal: keep the existing 0.75x size.
            float imageScale = 0.75f;
            if (compact)
                imageScale = MathF.Min(0.75f, (sidebarWidth - 8 * ImGuiHelpers.GlobalScale) / pluginIcon.Width);

            var imageSize = new Vector2(pluginIcon.Width, pluginIcon.Height) * imageScale;

            var offsetX = (sidebarWidth - imageSize.X) / 2.0f;
            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + offsetX);
            ImGui.Image(pluginIcon.Handle, imageSize);

            ImGui.Dummy(new Vector2(0, 5));
            ImGui.Separator();
            ImGui.Dummy(new Vector2(0, 5));
        }
        public static void DrawSelectable_Icon(FontAwesomeIcon icon, string label, WindowSelection tab, bool compact)
        {
            bool RowSelectable(string id, bool isSelected, float height = 0f)
            {
                ImGui.PushStyleColor(ImGuiCol.Header, new Vector4(0, 0, 0, 0));
                ImGui.PushStyleColor(ImGuiCol.HeaderHovered, new Vector4(0, 0, 0, 0));
                ImGui.PushStyleColor(ImGuiCol.HeaderActive, new Vector4(0, 0, 0, 0));

                bool clicked = ImGui.Selectable(id, isSelected,
                    ImGuiSelectableFlags.SpanAllColumns, new Vector2(0, height));

                ImGui.PopStyleColor(3);

                if (isSelected)
                    ImGui.TableSetBgColor(ImGuiTableBgTarget.RowBg0, ImGui.GetColorU32(ImGuiCol.HeaderActive));
                else if (ImGui.IsItemHovered())
                    ImGui.TableSetBgColor(ImGuiTableBgTarget.RowBg0, ImGui.GetColorU32(ImGuiCol.HeaderHovered));

                return clicked;
            }

            bool isSelected = C.SelectedTab == tab;
            float scale = ImGuiHelpers.GlobalScale;
            float rowHeight = 25 * scale;

            ImGui.TableNextRow();

            // --- Column 0: accent bar (paints just this cell when selected) ---
            ImGui.TableNextColumn();
            if (isSelected)
            {
                var color = C.UseIceTheme ? Theme_Colors.SidebarAccent : ImGuiColors.ParsedGold;

                ImGui.TableSetBgColor(ImGuiTableBgTarget.CellBg, ImGui.GetColorU32(color));
            }

            // --- Column 1: icon ---
            ImGui.TableNextColumn();
            ImGui.AlignTextToFramePadding();
            ImGui.Dummy(new(0.1f));
            ImGui.SameLine();
            ImGui_Ice.Icon(icon);

            // --- Column 2: label + row-spanning selectable ---
            ImGui.TableNextColumn();
            ImGui.AlignTextToFramePadding();

            string selectableLabel = compact ? $"##{tab}" : $"{label}##{tab}";
            bool clicked = RowSelectable(selectableLabel, isSelected, rowHeight);

            if (clicked)
            {
                C.SelectedTab = tab;
                C.SaveDebounced();
            }

            if (compact && ImGui.IsItemHovered())
                ImGui.SetTooltip(label);
        }
        public static void DrawSelectable_Icon_Small(FontAwesomeIcon icon, string label, WindowSelection tab, bool compact)
        {
            bool RowSelectable(string id, bool isSelected, float height = 0f)
            {
                ImGui.PushStyleColor(ImGuiCol.Header, new Vector4(0, 0, 0, 0));
                ImGui.PushStyleColor(ImGuiCol.HeaderHovered, new Vector4(0, 0, 0, 0));
                ImGui.PushStyleColor(ImGuiCol.HeaderActive, new Vector4(0, 0, 0, 0));

                // Center the label vertically within the given height
                ImGui.PushStyleVar(ImGuiStyleVar.SelectableTextAlign, new Vector2(0f, 0.5f));

                bool clicked = ImGui.Selectable(id, isSelected,
                    ImGuiSelectableFlags.SpanAllColumns, new Vector2(0, height));

                ImGui.PopStyleVar();
                ImGui.PopStyleColor(3);

                if (isSelected)
                    ImGui.TableSetBgColor(ImGuiTableBgTarget.RowBg0, ImGui.GetColorU32(ImGuiCol.HeaderActive));
                else if (ImGui.IsItemHovered())
                    ImGui.TableSetBgColor(ImGuiTableBgTarget.RowBg0, ImGui.GetColorU32(ImGuiCol.HeaderHovered));

                return clicked;
            }

            bool isSelected = C.SelectedTab == tab;
            float scale = ImGuiHelpers.GlobalScale;

            float textHeight = ImGui.GetTextLineHeight();
            float rowHeight = textHeight + 4 * scale;

            // Measure the icon with the icon font so centering is accurate
            ImGui.PushFont(UiBuilder.IconFont);
            float iconHeight = ImGui.GetTextLineHeight();
            ImGui.PopFont();
            float iconOffsetY = (rowHeight - iconHeight) * 0.5f;

            ImGui.TableNextRow(ImGuiTableRowFlags.None, rowHeight);

            // --- Column 0: accent bar (paints just this cell when selected) ---
            ImGui.TableNextColumn();
            if (isSelected)
            {
                var color = C.UseIceTheme ? Theme_Colors.SidebarAccent : ImGuiColors.ParsedGold;
                ImGui.TableSetBgColor(ImGuiTableBgTarget.CellBg, ImGui.GetColorU32(color));
            }

            // --- Column 1: icon, centered within the row ---
            ImGui.TableNextColumn();
            ImGui.SetCursorPosY(ImGui.GetCursorPosY() + iconOffsetY);
            ImGui.Dummy(new(0.1f));
            ImGui.SameLine();
            ImGui_Ice.Icon(icon);

            // --- Column 2: label + row-spanning selectable (label is now centered) ---
            ImGui.TableNextColumn();

            string selectableLabel = compact ? $"##{tab}" : $"{label}##{tab}";
            bool clicked = RowSelectable(selectableLabel, isSelected, rowHeight);

            if (clicked)
            {
                C.SelectedTab = tab;
                C.SaveDebounced();
            }

            if (compact && ImGui.IsItemHovered())
                ImGui.SetTooltip(label);
        }
        private static bool IconTabButton(FontAwesomeIcon icon, string label, bool isSelected)
        {
            float scale = ImGuiHelpers.GlobalScale;
            float availX = ImGui.GetContentRegionAvail().X;
            bool ice = C.UseIceTheme;
            bool small = C.Compact_Ui;

            string iconStr = icon.ToIconString();

            // Measure the icon first so the button size follows the font size
            ImGui.PushFont(UiBuilder.IconFont);
            var iconSize = ImGui.CalcTextSize(iconStr);
            ImGui.PopFont();

            // Compact_Ui = tighter padding around the icon + tighter spacing between buttons
            float padding = (small ? 4f : 9f) * scale;

            // Size must NOT depend on availX, otherwise the scrollbar appearing shrinks the buttons,
            // which removes the scrollbar, which grows them again (flicker loop).
            float side = MathF.Max(iconSize.X, iconSize.Y) + padding * 2f;
            var buttonSize = new Vector2(side, side);

            using var spacing = ImRaii.PushStyle(ImGuiStyleVar.ItemSpacing,
                new Vector2(ImGui.GetStyle().ItemSpacing.X, (small ? 2f : 4f) * scale));

            // Center the button horizontally in the sidebar (never go left of the content edge)
            ImGui.SetCursorPosX(ImGui.GetCursorPosX() + MathF.Max(0f, (availX - side) * 0.5f));

            var pos = ImGui.GetCursorScreenPos();
            bool clicked = ImGui.InvisibleButton($"##tab_{label}", buttonSize);

            bool hovered = ImGui.IsItemHovered();
            bool active = ImGui.IsItemActive();

            var drawList = ImGui.GetWindowDrawList();
            var rounding = ImGui.GetStyle().FrameRounding;

            // --- Colors per theme ---
            Vector4 selectedFill = ice ? Theme_Colors.DeepIceBlue : ImGui.GetStyle().Colors[(int)ImGuiCol.Button];
            Vector4 hoverFill = ice ? Theme_Colors.HeaderBg : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonHovered];
            Vector4 activeFill = ice ? Theme_Colors.ButtonActive : ImGui.GetStyle().Colors[(int)ImGuiCol.ButtonActive];
            Vector4 accent = ice ? Theme_Colors.IceBlue : ImGuiColors.ParsedGold;
            Vector4 hoverIcon = ice ? Theme_Colors.FrostWhite : ImGui.GetStyle().Colors[(int)ImGuiCol.Text];

            // --- Background ---
            if (active)
                drawList.AddRectFilled(pos, pos + buttonSize, ImGui.GetColorU32(activeFill), rounding);
            else if (hovered)
                drawList.AddRectFilled(pos, pos + buttonSize, ImGui.GetColorU32(hoverFill), rounding);
            else if (isSelected)
                drawList.AddRectFilled(pos, pos + buttonSize, ImGui.GetColorU32(selectedFill), rounding);

            // --- Border (selected only) ---
            if (isSelected)
                drawList.AddRect(pos, pos + buttonSize, ImGui.GetColorU32(accent), rounding, ImDrawFlags.None, 1.5f);

            // --- Icon ---
            var iconColor = isSelected ? accent
                          : hovered ? hoverIcon
                          : ImGuiColors.DalamudGrey;

            ImGui.PushFont(UiBuilder.IconFont);
            var iconPos = pos + (buttonSize - iconSize) * 0.5f;
            drawList.AddText(iconPos, ImGui.GetColorU32(iconColor), iconStr);
            ImGui.PopFont();

            if (hovered)
                ImGui.SetTooltip(label);

            return clicked;
        }
    }
}
