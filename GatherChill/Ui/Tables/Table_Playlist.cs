using Dalamud.Game.Text;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Utility.Raii;
using GatherChill.ConfigFiles;
using GatherChill.Enums;
using GatherChill.Gui;
using GatherChill.Gui.ImGuiTable;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Utility;
using System.Collections.Generic;
using static GatherChill.Ui.Tables.Table_Items;

namespace GatherChill.Ui.Tables;

internal class Table_GatherPlaylist
{
    public class GatherPlaylistTable : Table<Config.ItemInfo>
    {
        public Config.ItemInfo? ItemToRemove { get; set; }

        public GatherPlaylistTable(List<Config.ItemInfo> items, ImGuiEx.RealtimeDragDrop<Config.ItemInfo> dragDrop, Action<uint> openRoutePopup, bool timed)
        {
            Id = timed ? "GatherPlaylist_Timed_V1" : "GatherPlaylist_V1";
            Sortable = false;
            ScrollFreezeCols = 0;

            Flags = ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.Resizable;

            var columns = new List<Column<Config.ItemInfo>>
            {
                new OrderColumn(dragDrop, items),
                new ItemColumn(),
                new GatherModeColumn(),
                new GatherAmountColumn(),
                new HaveColumn(),
            };

            if (timed)
                columns.Add(new UptimeColumn());

            columns.Add(new RoutesColumn(openRoutePopup));
            columns.Add(new RemoveColumn(item => ItemToRemove = item));

            Columns = [.. columns];
            Rows = items;
        }

        // Just a way for me ot make sure that it's always refreshing the list when it needs to (because the list itself is dynamic)
        public void Refresh() => IsFilterDirty = true;

        public override float CalculateLineHeight()
            => ImGui.GetFrameHeight() + ImGui.GetStyle().CellPadding.Y * 2;
    }

    public sealed class OrderColumn : Column<Config.ItemInfo>
    {
        private readonly ImGuiEx.RealtimeDragDrop<Config.ItemInfo> _dragDrop;
        private readonly List<Config.ItemInfo> _items;

        public OrderColumn(ImGuiEx.RealtimeDragDrop<Config.ItemInfo> dragDrop, List<Config.ItemInfo> items)
        {
            _dragDrop = dragDrop;
            _items = items;
            Label = "##Order";
            SetFixedWidth(30 * ImGuiHelpers.GlobalScale);
            Flags = ImGuiTableColumnFlags.WidthFixed;
        }

        // Header is intentionally blank
        public override bool DrawFilter() => false;

        public override void DrawColumn(Config.ItemInfo row)
        {
            var index = _items.IndexOf(row);
            if (index < 0)
                return;

            var uniqueId = row.ItemId.ToString();

            _dragDrop.NextRow();
            _dragDrop.SetRowColor(uniqueId);
            _dragDrop.DrawButtonDummy(uniqueId, _items, index);
        }
    }

    public sealed class ItemColumn : Column<Config.ItemInfo>
    {
        public ItemColumn()
        {
            Label = "Item";
            SetStretchWidth();
            Flags = ImGuiTableColumnFlags.WidthFixed;
        }

        public override void DrawColumn(Config.ItemInfo row)
        {
            if (!Gather_Util.Sheet_ItemInfo.TryGetValue(row.ItemId, out var sheetInfo))
            {
                ImGui.TextDisabled($"Unknown item {row.ItemId}");
                return;
            }

            var nameText = sheetInfo.IsCollectable ? $"{sheetInfo.Name} {SeIconChar.Collectible.ToIconString()}" : sheetInfo.Name;

            ImGui_Ice.ImageButtonWithText(sheetInfo.IconId, nameText, sheetInfo.Name);
        }
    }

    public sealed class GatherModeColumn : Column<Config.ItemInfo>
    {
        private static readonly string[] ModeNames = Enum.GetNames<GatherMode>();

        public GatherModeColumn()
        {
            Label = "Mode";

            var textWidth = 0f;
            foreach (var name in ModeNames)
                textWidth = Math.Max(textWidth, ImGui.CalcTextSize(name).X);

            var style = ImGui.GetStyle();
            var comboWidth = textWidth + style.FramePadding.X * 2 + ImGui.GetFrameHeight();

            SetFixedWidth(comboWidth + style.CellPadding.X * 2);
            Flags = ImGuiTableColumnFlags.WidthFixed;
        }

        public override void DrawColumn(Config.ItemInfo row)
        {
            ImGui.SetNextItemWidth(-1);
            using var combo = ImRaii.Combo("##GatherMode", row.Mode.ToString());
            if (ImGui.IsItemHovered())
                ImGui.SetTooltip("Gather: collect this many more.\nKeep: gather until your inventory holds this many.");

            if (!combo.Success)
                return;

            foreach (var mode in Enum.GetValues<GatherMode>())
            {
                if (!ImGui.Selectable(mode.ToString(), row.Mode == mode))
                    continue;

                row.Mode = mode;
                C.SaveDebounced();
            }
        }
    }

    public sealed class GatherAmountColumn : Column<Config.ItemInfo>
    {
        public GatherAmountColumn()
        {
            Label = "Amount";
            SetFixedWidth(110 * ImGuiHelpers.GlobalScale);
            Flags = ImGuiTableColumnFlags.WidthFixed;
        }

        public override void DrawColumn(Config.ItemInfo row)
        {
            var isKeep = row.Mode == GatherMode.Keep;
            var amount = isKeep ? row.KeepAmount : row.GatherAmount;

            ImGui.SetNextItemWidth(100 * ImGuiHelpers.GlobalScale);
            if (!ImGui.InputInt("##Amount", ref amount, 1, 10))
                return;

            // Clamping this so a user can't go negative (I know there is people that will
            amount = Math.Max(amount, 0);

            if (isKeep)
                row.KeepAmount = amount;
            else
                row.GatherAmount = amount;

            C.SaveDebounced();
        }
    }

    public sealed class HaveColumn : Column<Config.ItemInfo>
    {
        public HaveColumn()
        {
            Label = "Have";
            SetFixedWidth(70 * ImGuiHelpers.GlobalScale);
            Flags = ImGuiTableColumnFlags.WidthFixed;
        }

        public override void DrawColumn(Config.ItemInfo row)
        {
            ImGui.AlignTextToFramePadding();
            ImGui.Text($"{Utils.GetItemCount(row.ItemId):N0}");
        }
    }

    public sealed class RoutesColumn : Column<Config.ItemInfo>
    {
        private readonly Action<uint> _openRoutePopup;

        public RoutesColumn(Action<uint> openRoutePopup)
        {
            _openRoutePopup = openRoutePopup;
            Label = "Route(s)";
            SetFixedWidth(120 * ImGuiHelpers.GlobalScale);
            Flags = ImGuiTableColumnFlags.WidthFixed;
        }

        public override void DrawColumn(Config.ItemInfo row)
        {
            var enabledRoutes = C.ItemRoutes[row.ItemId].EnabledRoutes;
            var count = enabledRoutes.Count();
            if (count == 0)
            {
                ImGui.TextDisabled("None");
                return;
            }

            var firstRoute = enabledRoutes.First();
            var routeText = count > 1 ? $"{firstRoute} + {count - 1}" : $"{firstRoute}";

            if (ImGui.Button(routeText))
                _openRoutePopup(row.ItemId);

            if (ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();
                ImGui.Text("Select which routes you would like to do");
                ImGui.EndTooltip();
            }

            var routeDetails = P.routeEditor.GetRoute(firstRoute);
            if (routeDetails.AetheryteId != 0)
                return;

            ImGui.TableSetBgColor(ImGuiTableBgTarget.RowBg0, ImGui.GetColorU32(EColor.Red));
            if (ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();
                ImGui.Text("We don't have an aetheryte for this currently set, so you'll need to tp here yourself");
                ImGui.EndTooltip();
            }
        }
    }

    public sealed class UptimeColumn : Column<Config.ItemInfo>
    {
        public UptimeColumn()
        {
            Label = "Uptime";
            SetFixedWidth(170 * ImGuiHelpers.GlobalScale);
            Flags = ImGuiTableColumnFlags.WidthFixed;
        }

        public override void DrawColumn(Config.ItemInfo row)
        {
            var windows = Gather_Util.UptimeHelper.GetEnabledWindows(row.ItemId);
            Gather_Util.UptimeHelper.Draw(windows, "No routes enabled");
        }
    }

    public sealed class RemoveColumn : Column<Config.ItemInfo>
    {
        private readonly Action<Config.ItemInfo> _onRemove;

        public RemoveColumn(Action<Config.ItemInfo> onRemove)
        {
            _onRemove = onRemove;
            Label = "Remove";
            SetFixedWidth(70 * ImGuiHelpers.GlobalScale);
            Flags = ImGuiTableColumnFlags.WidthStretch;
        }

        public override void DrawColumn(Config.ItemInfo row)
        {
            var shiftHeld = ImGui.IsKeyDown(ImGuiKey.LeftShift) || ImGui.IsKeyDown(ImGuiKey.RightShift);
            if (!ImGuiEx.IconButton(FontAwesomeIcon.Trash, $"Remove_{row.ItemId}", enabled: shiftHeld))
                return;

            _onRemove(row);
        }
    }
}