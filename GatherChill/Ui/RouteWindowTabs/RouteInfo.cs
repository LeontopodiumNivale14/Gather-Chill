using Dalamud.Interface.Utility.Raii;
using ECommons.GameHelpers;
using GatherChill.Enums;
using GatherChill.GatheringInfo;
using GatherChill.Gui;
using GatherChill.Gui.ImGuiTable;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Tools;
using System.Collections.Generic;

namespace GatherChill.Ui.RouteWindowTabs;

internal class RouteInfo
{
    public class RouteItem
    {
        public uint RouteId { get; set; } = 0;

        public GatherPointInfo? GatherPoint =>
            Sheet_RouteInfo.TryGetValue(RouteId, out var info) ? info : null;

        public GatheringRoute? RouteInfo =>
            P.routeEditor.Routes.TryGetValue(RouteId, out var route) ? route : null;

        public bool Ignore => Ignore_Routes.Contains(RouteId);

        public bool IsValid => GatherPoint != null && RouteInfo != null;
    }

    public class RouteTable : Table<RouteItem>, IDisposable
    {
        private readonly RouteId _routeId = new();
        private readonly Position _position = new();
        private readonly Expansion _expansion = new();
        private readonly TerritoryName _terName = new();
        private readonly Territory _terId = new();
        private readonly ItemInfo _items = new();
        private readonly TimedWindowColumn _times = new();
        private readonly Folklore _folklore = new();
        private readonly NodeKind _nodeKind = new();

        public RouteTable(List<RouteItem> itemList)
        {
            List<Column<RouteItem>> headers = [_routeId, _expansion, _terName, _terId, _position, _items, _folklore, _nodeKind, _times];

            Id = "RouteTable_V2";
            Columns = headers;
            Rows = itemList;
            Sortable = true;
            Flags |= ImGuiTableFlags.Resizable | ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.ScrollX;
        }

        public override float CalculateLineHeight()
        {
            return ImGui.GetFrameHeight() + ImGui.GetStyle().CellPadding.Y * 2;
        }
    }
    public sealed class RouteId : ColumnNumber<RouteItem>
    {
        public RouteId()
        {
            LabelKey = "Id";
            var size = ImGui.CalcTextSize("XXXX").X;
            SetFixedWidth(size);
            Flags = ImGuiTableColumnFlags.WidthFixed;
        }

        public override int ToValue(RouteItem row) => (int)row.RouteId;

        public override void DrawColumn(RouteItem row)
        {
            if (Route_Editor.SelectedRoute == row.RouteId)
                ImGui.TableSetBgColor(ImGuiTableBgTarget.RowBg0, ImGui.GetColorU32(new Vector4(0.0f, 1.0f, 0.2f, 0.25f))); // Green
            if (row.Ignore)
                ImGui.TableSetBgColor(ImGuiTableBgTarget.RowBg0, ImGui.GetColorU32(new Vector4(0.9f, 0.5f, 0.5f, 1.0f))); // Pink? Orange?

            using (ImRaii.Disabled(row.Ignore))
            {
                if (ImGui.Button($"{row.RouteId}"))
                {
                    Route_Editor.UpdateRoute(row.RouteId);
                }
            }
        }
    }
    public sealed class Position : Column<RouteItem>
    {
        public Position()
        {
            Label = "Flag";
            using (ImRaii.PushFont(UiBuilder.IconFont))
            {
                var iconWidth = ImGui.CalcTextSize(FontAwesomeIcon.Flag.ToIconString()).X;
                Width = iconWidth + 20;
            }
            Flags = ImGuiTableColumnFlags.WidthFixed | ImGuiTableColumnFlags.NoResize;
        }

        public override int Compare(RouteItem lhs, RouteItem rhs)
        {
            var lhsScore = GetNWScore(lhs.RouteId);
            var rhsScore = GetNWScore(rhs.RouteId);
            return lhsScore.CompareTo(rhsScore);
        }

        // X + Y combined gives lowest score to the most NW point
        private static float GetNWScore(uint routeId)
        {
            var pos = GetFlagPos(routeId);
            return pos.HasValue ? pos.Value.X + pos.Value.Y : float.MaxValue;
        }

        private static Vector2? GetFlagPos(uint routeId)
        {
            if (!Sheet_RouteInfo.TryGetValue(routeId, out var info))
                return null;
            return new Vector2(info.Map.X, info.Map.Y);
        }

        public override void DrawColumn(RouteItem row)
        {
            var sheetInfo = row.GatherPoint;
            if (sheetInfo == null)
            {
                ImGui.TextDisabled("N/A");
                if (ImGui.IsItemHovered())
                    ImGui.SetTooltip($"No GatherPointInfo found for RouteId {row.RouteId}");
                return;
            }

            if (ImGuiEx.IconButton(FontAwesomeIcon.Flag, $"{row.RouteId}_Flag"))
            {
                sheetInfo.Map.OpenMap($"Route {row.RouteId}");
            }
            if (ImGui.IsItemHovered())
            {
                if (ImGui.IsMouseClicked(ImGuiMouseButton.Right))
                {
                    Svc.Commands.ProcessCommand("/vnav flyflag");
                }

                ImGui.SetTooltip("Left click to set flag\n" +
                    "Right click to fly to flag");
            }
        }
    }
    public sealed class Expansion : ColumnFlags<ExpansionEnum, RouteItem>
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

        public ExpansionEnum ToExpansion(RouteItem row)
        {
            var expansionFlag = row.RouteInfo.ExpansionId switch
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

        public override void DrawColumn(RouteItem row)
        {
            var route = row.RouteInfo;
            if (route == null || !Sheet_Expansion.TryGetValue(route.ExpansionId, out var expacInfo))
            {
                ImGui.TextDisabled("N/A");
                if (ImGui.IsItemHovered())
                    ImGui.SetTooltip($"No expansion info found for RouteId {row.RouteId}");
                return;
            }

            var scale = ImGui.GetFrameHeight();

            ImGui.Image(expacInfo.Icon.Handle, new(scale));
            if (ImGui.IsItemHovered())
                ImGui.SetTooltip($"{expacInfo.Name}");
        }

        public override int Compare(RouteItem a, RouteItem b)
            => ((int)ToExpansion(a)).CompareTo((int)ToExpansion(b));

        public override bool ShouldShow(RouteItem row)
        {
            var value = ToExpansion(row);
            return FilterValue.HasFlag(value);
        }
    }
    public sealed class TerritoryName : ColumnString<RouteItem>
    {
        public TerritoryName()
        {
            Label = "Territory";
            Flags = ImGuiTableColumnFlags.None;
        }

        public override string ToName(RouteItem row)
        {
            var route = row.RouteInfo;
            if (route == null)
                return "N/A";
            else
                return route.ZoneName;
        }

        public override void DrawColumn(RouteItem row)
        {
            ImGui.AlignTextToFramePadding();
            var route = row.RouteInfo;
            if (route == null)
            {
                ImGui.TextDisabled("N/A");
                if (ImGui.IsItemHovered())
                    ImGui.SetTooltip($"No territory info found for RouteId {row.RouteId}");
                return;
            }
            else
            {
                ImGui.Button($"{route.ZoneName}");
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();

                    ImGui.Text($"Node Count: {route.NodeIds.Count():N0}");
                    ImGui.Text($"Locations Mapped: {route.NodeInfo.Count():N0}");
                    var maxGroupId = route.NodeInfo.Count > 0 ? route.NodeInfo.Max(x => x.GroupId) : 0;
                    ImGui.Text($"Max Group Id: {maxGroupId}");

                    ImGui.EndTooltip();
                }
            }
        }
    }
    public sealed class ItemInfo : ColumnString<RouteItem>
    {
        private const int MaxVisibleIcons = 8;
        private const string OverflowSample = "+99";
        private bool _widthCalculated;

        public ItemInfo()
        {
            Label = "Items";
            Flags = ImGuiTableColumnFlags.NoResize;
        }

        private void EnsureWidth()
        {
            if (_widthCalculated)
                return;

            var style = ImGui.GetStyle();
            var iconSize = ImGui.GetFrameHeight();
            var overflowWidth = ImGui.CalcTextSize(OverflowSample).X + style.FramePadding.X * 2;

            // N icons + the overflow button = N gaps between items.
            var width = (iconSize * MaxVisibleIcons)
                      + overflowWidth
                      + (style.ItemSpacing.X * MaxVisibleIcons)
                      + (style.CellPadding.X * 2);

            SetFixedWidth(width);
            _widthCalculated = true;
        }

        public override bool DrawFilter()
        {
            EnsureWidth();
            return base.DrawFilter();
        }

        private static IReadOnlyList<uint> GetItemIds(RouteItem row)
            => row.GatherPoint?.ItemIds ?? [];

        public override string ToName(RouteItem row)
        {
            var itemIds = GetItemIds(row);
            if (itemIds.Count == 0)
                return string.Empty;

            var names = new List<string>(itemIds.Count);
            foreach (var itemId in itemIds)
            {
                if (ExcelHelper.Sheet_Item.TryGetRow(itemId, out var itemInfo))
                    names.Add(itemInfo.Name.ToString());
            }

            return string.Join(", ", names);
        }

        public override void DrawColumn(RouteItem row)
        {
            var itemIds = GetItemIds(row);
            if (itemIds.Count == 0)
            {
                ImGui.TextDisabled("N/A");
                return;
            }

            // ImageButton with framePadding 1 adds 1px per side, so this totals one frame height.
            var buttonSize = ImGui.GetFrameHeight() - 2;
            var drawn = 0;
            var hiddenStart = itemIds.Count;

            for (var i = 0; i < itemIds.Count; i++)
            {
                if (drawn >= MaxVisibleIcons)
                {
                    hiddenStart = i;
                    break;
                }

                if (!ExcelHelper.Sheet_Item.TryGetRow(itemIds[i], out var itemInfo))
                    continue;

                if (drawn > 0)
                    ImGui.SameLine();
                drawn++;

                var icon = Svc.Texture.GetFromGameIcon((int)itemInfo.Icon).GetWrapOrEmpty();

                ImGui.ImageButton(icon.Handle, new Vector2(buttonSize, buttonSize), 1);
                if (ImGui.IsItemHovered())
                    ImGui.SetTooltip(itemInfo.Name.ToString());
                if (ImGui.IsItemClicked(ImGuiMouseButton.Left))
                    ImGui.SetClipboardText(itemInfo.Name.ToString());
            }

            DrawOverflow(itemIds, hiddenStart);
        }

        private static void DrawOverflow(IReadOnlyList<uint> itemIds, int hiddenStart)
        {
            var hiddenCount = itemIds.Count - hiddenStart;
            if (hiddenCount <= 0)
                return;

            ImGui.SameLine();
            ImGui.Button($"+{hiddenCount}##more");
            if (!ImGui.IsItemHovered())
                return;

            using var tooltip = ImRaii.Tooltip();
            if (!tooltip.Alive)
                return;

            for (var i = hiddenStart; i < itemIds.Count; i++)
            {
                if (ExcelHelper.Sheet_Item.TryGetRow(itemIds[i], out var itemInfo))
                    ImGui.Text(itemInfo.Name.ToString());
            }
        }
    }
    public sealed class TimedWindowColumn : Column<RouteItem>
    {
        private bool _filterEnabled;
        private int _filterStartHour = 0;   // 0-24
        private int _filterEndHour = 24;    // 0-24

        public TimedWindowColumn()
        {
            Label = "Time";
            Flags = ImGuiTableColumnFlags.WidthStretch;
        }

        public override bool DrawFilter()
        {
            using var id = ImRaii.PushId("##TimeFilter");
            var changed = false;

            if (ImGui.Checkbox("##FilterEnabled", ref _filterEnabled))
                changed = true;

            ImGui.SameLine();
            using (ImRaii.Disabled(!_filterEnabled))
            {
                var spacing = ImGui.GetStyle().ItemSpacing.X;
                var halfWidth = ((ImGui.GetContentRegionAvail().X - spacing) / 2f) - 10;

                ImGui.SetNextItemWidth(halfWidth);
                if (ImGui.SliderInt("##Start", ref _filterStartHour, 0, 24, $"{_filterStartHour:D2}:00"))
                {
                    _filterStartHour = Math.Clamp(_filterStartHour, 0, _filterEndHour);
                    changed = true;
                }

                ImGui.SameLine();

                ImGui.SetNextItemWidth(halfWidth);
                if (ImGui.SliderInt("##End", ref _filterEndHour, 0, 24, $"{_filterEndHour:D2}:00"))
                {
                    _filterEndHour = Math.Clamp(_filterEndHour, _filterStartHour, 24);
                    changed = true;
                }
            }

            if (ImGui.IsItemHovered())
                ImGui.SetTooltip("Ctrl+Click either slider to type an exact hour.");

            return changed;
        }

        public override bool ShouldShow(RouteItem row)
        {
            if (!_filterEnabled)
                return true;

            var windows = row.GatherPoint?.TimedInfo;
            if (windows == null || windows.Count == 0)
                return false;

            var filterStart = _filterStartHour * 100; // convert back to raw ET-minute format for comparison
            var filterEnd = _filterEndHour * 100;

            return windows.Any(w => w.Start < filterEnd && w.End > filterStart);
        }

        public override void DrawColumn(RouteItem row)
        {
            var windows = row.GatherPoint?.TimedInfo;
            if (windows == null || windows.Count == 0)
            {
                ImGui.TextDisabled("Always");
                return;
            }

            var first = true;
            foreach (var time in windows)
            {
                if (!first)
                    ImGui.SameLine();
                first = false;
                ImGui.Text($"{time.StartFormatted} - {time.EndFormatted}");
            }
        }

        public override int Compare(RouteItem lhs, RouteItem rhs)
        {
            var lhsStart = GetEarliestStart(lhs);
            var rhsStart = GetEarliestStart(rhs);
            return lhsStart.CompareTo(rhsStart);
        }

        private static int GetEarliestStart(RouteItem row)
        {
            var windows = row.GatherPoint?.TimedInfo;
            if (windows == null || windows.Count == 0)
                return int.MaxValue; // push "Always" rows to the end

            return windows.Min(w => w.Start);
        }
    }
    public sealed class Territory : Column<RouteItem>
    {
        private bool _filterEnabled = false;
        public Territory()
        {
            Label = "Ter. Id";
            Width = ImGui.CalcTextSize($"XXXXX").X;
            Flags = ImGuiTableColumnFlags.WidthFixed;
        }

        public override bool DrawFilter()
        {
            using var id = ImRaii.PushId("##TerritoryFilter");
            var changed = false;

            if (ImGui.Checkbox("##CurrentTerritory", ref _filterEnabled))
                changed = true;

            if (ImGui.IsItemHovered())
                ImGui.SetTooltip("Enable this to set to current territory");

            return changed;
        }

        public override bool ShouldShow(RouteItem row)
        {
            if (!_filterEnabled)
                return true;

            var territoryId = row.GatherPoint?.TerritoryId;
            if (territoryId == null || territoryId == 0)
                return false;

            return Player.Territory.RowId == territoryId;
        }

        public override int Compare(RouteItem lhs, RouteItem rhs)
        {
            return lhs.RouteInfo.TerritoryId.CompareTo(rhs.RouteInfo.TerritoryId);
        }

        public override void DrawColumn(RouteItem row)
        {
            ImGui.AlignTextToFramePadding();
            ImGui.Text($"{row.RouteInfo.TerritoryId:N0}");
        }
    }
    public sealed class Folklore : ColumnFlags<FolkloreEnum, RouteItem>
    {
        private FolkloreEnum _filterValue;

        public Folklore()
        {
            Label = "Folklore";
            SetFixedWidth(50);
            AllFlags = Enum.GetValues<FolkloreEnum>().Aggregate((a, b) => a | b);
            _filterValue = AllFlags;
            Flags = ImGuiTableColumnFlags.WidthFixed;
        }

        public override string NameKeySpace => "ImGuiTable.ColumnExpansion";
        public override FolkloreEnum FilterValue => _filterValue;

        public override void SetValue(FolkloreEnum value, bool enable)
        {
            if (enable)
                _filterValue |= value;
            else
                _filterValue &= ~value;
        }

        public override bool ShouldShow(RouteItem row)
        {
            var sheetInfo = row.GatherPoint;

            // Making sure that it's not null / it actually has a folklore ID
            bool hasFolklore = sheetInfo?.Folklore != null && sheetInfo.Folklore.ItemId != 0;

            if (hasFolklore)
                return FilterValue.HasFlag(FolkloreEnum.Has);
            else
                return FilterValue.HasFlag(FolkloreEnum.None);
        }

        public override void DrawColumn(RouteItem row)
        {
            var sheetInfo = row.GatherPoint;
            if (sheetInfo?.Folklore == null || sheetInfo.Folklore.ItemId == 0)
                return;

            var folklore = sheetInfo.Folklore;
            var scale = ImGui.GetFrameHeight();

            ImGui.Image(folklore.Icon.GetWrapOrEmpty().Handle, new(scale));
            if (!ImGui.IsItemHovered())
                return;

            using var tooltip = ImRaii.Tooltip();
            if (!tooltip.Alive)
                return;

            using var table = ImRaii.Table("Folklore Info", 2, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders);
            if (!table.Success)
                return;

            ImGui.TableSetupColumn("Name");
            ImGui.TableSetupColumn("Unlocked");
            ImGui.TableHeadersRow();

            ImGui.TableNextRow();

            ImGui.TableSetColumnIndex(0);
            ImGui.Text($"{folklore.Name}");

            ImGui.TableNextColumn();
            var isUnlocked = Svc.UnlockState.IsItemUnlocked(folklore.SheetInfo);
            var icon = isUnlocked ? FontAwesomeIcon.Check : FontAwesomeIcon.Times;
            var color = isUnlocked ? EColor.Green : EColor.Red;
            ImGuiEx.Icon(color, icon);
        }
    }
    public sealed class NodeKind : ColumnFlags<GatherNodeKind, RouteItem>
    {
        private GatherNodeKind _filterValue;

        public NodeKind()
        {
            Label = "Kind";
            SetFixedWidth(90);
            AllFlags = Enum.GetValues<GatherNodeKind>().Aggregate((a, b) => a | b);
            _filterValue = AllFlags;
            Flags = ImGuiTableColumnFlags.WidthFixed;
        }

        public override string NameKeySpace => "ImGuiTable.ColumnNodeKind";
        public override GatherNodeKind FilterValue => _filterValue;

        public override void SetValue(GatherNodeKind value, bool enable)
        {
            if (enable)
                _filterValue |= value;
            else
                _filterValue &= ~value;
        }

        private static GatherNodeKind GetKind(RouteItem row)
            => row.GatherPoint?.Kind ?? GatherNodeKind.Regular;

        public override bool ShouldShow(RouteItem row)
            => FilterValue.HasFlag(GetKind(row));

        public override int Compare(RouteItem lhs, RouteItem rhs)
            => ((int)GetKind(lhs)).CompareTo((int)GetKind(rhs));

        public override void DrawColumn(RouteItem row)
        {
            ImGui.AlignTextToFramePadding();

            var sheetInfo = row.GatherPoint;
            if (sheetInfo == null)
            {
                ImGui.TextDisabled("N/A");
                return;
            }

            ImGui.Text(sheetInfo.Kind.ToString());
        }
    }
}
