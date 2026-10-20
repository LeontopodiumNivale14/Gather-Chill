using Dalamud.Game.Text;
using Dalamud.Interface.Utility;
using FFXIVClientStructs.FFXIV.Client.Game;
using GatherChill.Enums;
using GatherChill.Gui;
using GatherChill.Gui.ImGuiTable;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Utility;
using System.Collections.Generic;
using static GatherChill.Ui.Tables.Table_Items;

namespace GatherChill.Ui.Tables;

internal class Table_GatherLog
{
    public class Gatherables
    {
        public uint ItemId { get; set; } = 0;
        public ItemClass ItemInfo => Gather_Util.Sheet_ItemInfo[ItemId];
        public List<uint> RouteList => ItemInfo.RouteInfo;
        public uint Icon => ItemInfo.IconId;
        public string Name => ItemInfo.Name;
        public ExpansionIds FirstExpansion => ItemInfo.Expansion;
        public ushort GatheringRow => ItemInfo.GatheringRow;
    }

    public class GatherLogTable : Table<Gatherables>, IDisposable
    {
        private readonly GatherId _itemId = new();
        private readonly Expansion _expansion = new();
        private readonly ItemNameColumn _itemName = new();
        private readonly HasGathered _hasGathered = new();

        public GatherLogTable(List<Gatherables> itemList)
        {
            List<Column<Gatherables>> headers = [_itemId, _expansion, _itemName, _hasGathered];
            Id = "Gatherables_V1";
            Columns = headers;
            Sortable = true;
            Rows = itemList;
            Flags |= ImGuiTableFlags.Resizable | ImGuiTableFlags.SizingFixedFit;
        }

        public override float CalculateLineHeight()
        {
            return ImGui.GetFrameHeight() + ImGui.GetStyle().CellPadding.Y * 2;
        }

        public List<Gatherables> GetVisibleItems()
        {
            if (FilteredRows == null)
                return [];

            return [.. FilteredRows];
        }
    }

    public sealed class GatherId : ColumnNumber<Gatherables>
    {
        public GatherId()
        {
            LabelKey = "GatherId";
            var size = ImGui.CalcTextSize("XXXXX").X;
            SetFixedWidth(size);
            Flags = ImGuiTableColumnFlags.WidthFixed;
        }

        public override int ToValue(Gatherables row) => (int)row.ItemId;

        public override void DrawColumn(Gatherables row)
        {
            ImGui.AlignTextToFramePadding();
            ImGui.Text($"{row.GatheringRow}");
        }
    }
    public sealed class Expansion : ColumnFlags<ExpansionEnum, Gatherables>
    {
        private ExpansionEnum _filterValue;

        public Expansion()
        {
            Label = "Exp";
            SetFixedWidth(50);
            AllFlags = Enum.GetValues<ExpansionEnum>().Aggregate((a, b) => a | b);
            _filterValue = AllFlags;
            Flags = ImGuiTableColumnFlags.WidthFixed;
        }

        public override string NameKeySpace => "ImGuiTable.ColumnExpansion";
        public override ExpansionEnum FilterValue => _filterValue;

        public override void SetValue(ExpansionEnum value, bool enable)
        {
            if (enable)
                _filterValue |= value;
            else
                _filterValue &= ~value;
        }

        public ExpansionEnum ToExpansion(Gatherables row)
        {
            var expansionFlag = row.FirstExpansion switch
            {
                ExpansionIds.ARR => ExpansionEnum.ARR,
                ExpansionIds.HW => ExpansionEnum.HW,
                ExpansionIds.StB => ExpansionEnum.StB,
                ExpansionIds.ShB => ExpansionEnum.ShB,
                ExpansionIds.EW => ExpansionEnum.EW,
                ExpansionIds.DT => ExpansionEnum.DT,
                _ => ExpansionEnum.ARR,
            };

            return expansionFlag;
        }

        public override void DrawColumn(Gatherables row)
        {
            if (!Sheet_Expansion.TryGetValue(row.FirstExpansion, out var expacInfo))
            {
                ImGui.TextDisabled("N/A");
                if (ImGui.IsItemHovered())
                    ImGui.SetTooltip($"No expansion info found for ItemId {row.ItemId}");
                return;
            }

            var scale = ImGui.GetFrameHeight();

            ImGui.Image(expacInfo.Icon.Handle, new(scale));
            if (ImGui.IsItemHovered())
                ImGui.SetTooltip($"{expacInfo.Name}");
        }

        public override int Compare(Gatherables a, Gatherables b)
            => ((int)ToExpansion(a)).CompareTo((int)ToExpansion(b));

        public override bool ShouldShow(Gatherables row)
        {
            var value = ToExpansion(row);
            return FilterValue.HasFlag(value);
        }
    }
    public sealed class ItemNameColumn : ColumnString<Gatherables>
    {
        public ItemNameColumn()
        {
            Label = "Name";
            Flags = ImGuiTableColumnFlags.None;
        }

        public override string ToName(Gatherables row)
        {
            var name = row.Name;
            if (name == null)
                return "N/A";
            else
                return row.Name;
        }

        public override void DrawColumn(Gatherables row)
        {
            var nameText = row.ItemInfo.IsCollectable ? $"{row.Name} {SeIconChar.Collectible.ToIconString()}" : row.Name; 

            ImGui_Ice.ImageButtonWithText(row.Icon, nameText, $"{row.ItemId}_{row.Name}");
            if (ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();

                ImGui.Text($"Item ID: {row.ItemId}");
                if (Utils.GetItemCount(row.ItemId, out var count))
                {
                    ImGui.Text($"Have: {count:N0}");
                }

                ImGui.EndTooltip();
            }
        }
    }
    public sealed class HasGathered : ColumnYesNo<Gatherables>
    {
        public HasGathered()
        {
            var scale = ImGuiHelpers.GlobalScale;
            SetFixedWidth(75 * scale);
            LabelKey = "Collected";
        }

        private static unsafe bool IsUnlocked(uint gatheringRow)
        {
            return QuestManager.IsGatheringItemGathered((ushort)gatheringRow);
        }

        public override bool ToBool(Gatherables row) => IsUnlocked(row.GatheringRow);

        public override void DrawColumn(Gatherables row)
        {
            var unlocked = IsUnlocked(row.GatheringRow);
            if (unlocked)
            {
                ImGuiEx.Icon(EColor.Green, FontAwesomeIcon.CheckCircle);
            }
            else
            {
                ImGuiEx.Icon(EColor.Red, FontAwesomeIcon.Times);
            }
        }
    }
}
