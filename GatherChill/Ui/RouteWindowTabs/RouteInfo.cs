using Dalamud.Interface.Utility;
using Dalamud.Interface.Utility.Raii;
using GatherChill.Enums;
using GatherChill.GatheringInfo;
using GatherChill.Gui.ImGuiTable;
using GatherChill.Utilities.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui.RouteWindowTabs;

internal class RouteInfo
{
    public class RouteItem
    {
        public uint RouteId { get; set; } = 0;

        public GatherPointInfo? GatherPoint =>
            SheetInfo.TryGetValue(RouteId, out var info) ? info : null;

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
        private readonly TerritoryName _territory = new();
        private readonly ItemInfo _items = new();
        private readonly TimedWindowColumn _times = new();

        public RouteTable(List<RouteItem> itemList)
        {
            List<Column<RouteItem>> headers = [_routeId, _expansion, _territory, _position, _items, _times];

            Id = "RouteTable_V2";
            Columns = headers;
            Rows = itemList;
            Sortable = true;
            Flags |= ImGuiTableFlags.Resizable;
        }

        public override float CalculateLineHeight()
        {
            return ImGui.GetFrameHeightWithSpacing();
        }
    }

    public sealed class RouteId : ColumnNumber<RouteItem>
    {
        public RouteId()
        {
            LabelKey = "Id";
            var size = ImGui.CalcTextSize("XXXX").X;
            SetFixedWidth(size);
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
            if (!SheetInfo.TryGetValue(routeId, out var info))
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
                0 => ExpansionEnum.ARR,
                1 => ExpansionEnum.HW,
                2 => ExpansionEnum.StB,
                3 => ExpansionEnum.ShB,
                4 => ExpansionEnum.EW,
                5 => ExpansionEnum.DT,
                _ => ExpansionEnum.ARR,
            };

            return expansionFlag;
        }

        public override void DrawColumn(RouteItem row)
        {
            var route = row.RouteInfo;
            if (route == null || !ExpansionInfo.TryGetValue(route.ExpansionId, out var expacInfo))
            {
                ImGui.TextDisabled("N/A");
                if (ImGui.IsItemHovered())
                    ImGui.SetTooltip($"No expansion info found for RouteId {row.RouteId}");
                return;
            }

            float scale = ImGui.GetTextLineHeightWithSpacing();

            ImGui.Image(expacInfo.Icon.Handle, new(scale));
            if (ImGui.IsItemHovered())
            {
                ImGui.SetTooltip($"{expacInfo.ExpacName}");
            }
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
                ImGui.Text($"{route.ZoneName}");
            }
        }
    }
    public sealed class ItemInfo : ColumnString<RouteItem>
    {
        public ItemInfo()
        {
            Label = "Items";
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

            var buttonSize = ImGui.GetTextLineHeightWithSpacing() - 1;
            var first = true;

            foreach (var itemId in itemIds)
            {
                if (!ExcelHelper.Sheet_Item.TryGetRow(itemId, out var itemInfo))
                    continue;

                if (!first)
                    ImGui.SameLine();
                first = false;

                var iconId = (int)itemInfo.Icon;
                var icon = Svc.Texture.GetFromGameIcon(iconId).GetWrapOrEmpty();

                ImGui.ImageButton(icon.Handle, new Vector2(buttonSize, buttonSize), 1);
                if (ImGui.IsItemHovered())
                    ImGui.SetTooltip(itemInfo.Name.ToString());
                if (ImGui.IsItemClicked(ImGuiMouseButton.Left))
                    ImGui.SetClipboardText(itemInfo.Name.ToString());
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
            Flags = ImGuiTableColumnFlags.None;
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
}
