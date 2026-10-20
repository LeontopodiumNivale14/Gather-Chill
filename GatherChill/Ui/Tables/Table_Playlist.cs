using Dalamud.Game.Text;
using Dalamud.Interface.Utility;
using GatherChill.ConfigFiles;
using GatherChill.Gui;
using GatherChill.Gui.ImGuiTable;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Utility;
using System.Collections.Generic;

namespace GatherChill.Ui.Tables;

internal class Table_GatherPlaylist
{
    public class GatherPlaylistTable : Table<Config.ItemInfo>
    {
        public Config.ItemInfo? ItemToRemove { get; set; }

        public GatherPlaylistTable(List<Config.ItemInfo> items, ImGuiEx.RealtimeDragDrop<Config.ItemInfo> dragDrop, Action<uint> openRoutePopup)
        {
            Id = "GatherPlaylist_V1";
            Sortable = false;
            ScrollFreezeCols = 0;

            Flags = ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.Resizable;

            Columns =
            [
                new OrderColumn(dragDrop, items),
                new ItemColumn(),
                new GatherAmountColumn(),
                new HaveColumn(),
                new RoutesColumn(openRoutePopup),
                new RemoveColumn(item => ItemToRemove = item),
            ];

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

    public sealed class GatherAmountColumn : Column<Config.ItemInfo>
    {
        public GatherAmountColumn()
        {
            Label = "Gather";
            SetFixedWidth(110 * ImGuiHelpers.GlobalScale);
            Flags = ImGuiTableColumnFlags.WidthFixed;
        }

        public override void DrawColumn(Config.ItemInfo row)
        {
            var amount = row.GatherAmount;
            ImGui.SetNextItemWidth(100);
            if (!ImGui.InputInt("##GatherAmount", ref amount, 1, 10))
                return;

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