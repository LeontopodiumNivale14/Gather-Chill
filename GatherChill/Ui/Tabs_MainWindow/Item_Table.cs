using ECommons.Logging;
using GatherChill.Enums;
using GatherChill.Items;
using OtterGui;
using OtterGui.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui.Tabs_MainWindow
{
    internal class VerticalCenterColumnString : ColumnString<GatherableItem>
    {
        public override void PreDraw()
        {
            var pos = ImGui.GetCursorPosY();
            var offset = (ITEM_ICON_SIZE - ImGui.GetTextLineHeight()) / 2;
            ImGui.SetCursorPosY(pos + offset);
        }
    }

    internal class FilterColumn : ColumnFlags<FilterFlags, GatherableItem>
    {
        private FilterFlags[] FlagValues = Array.Empty<FilterFlags>();
        private string[] FlagNames = Array.Empty<string>();

        protected void SetFlags(params FilterFlags[] flags)
        {
            FlagValues = flags;
            AllFlags = FlagValues.Aggregate((f, g) => f | g);
        }

        protected void SetFlagsAndNames(params FilterFlags[] flags)
        {
            SetFlags(flags);
            SetNames(flags.Select(f => f.ToString()).ToArray());
        }

        protected void SetNames(params string[] names) => FlagNames = names;

        protected sealed override IReadOnlyList<FilterFlags> Values => FlagValues;

        protected sealed override string[] Names => FlagNames;

        public sealed override FilterFlags FilterValue => C.ItemFilter;

        protected sealed override void SetValue(FilterFlags f, bool v)
        {
            var tmp = v ? FilterValue | f : FilterValue & ~f;
            if (tmp == FilterValue)
                return;

            C.ItemFilter = tmp;
            C.Save();
        }
    }

    internal class Item_Table : Table<GatherableItem>, IDisposable
    {
        public readonly NameColumn _nameColumn = new() { Label = "Item Name" };
        public readonly TypeColumn _typeColumn = new() { Label = "Type" };
        public readonly ZoneNameColumn _zoneNameColumn = new() { Label = "Zone" };
        public readonly PlaceNamesColumn _placeNamesColumn = new() { Label = "Nodes" };
        public readonly ItemIdColumn _itemIdColumn = new() { Label = "ID" };
        public readonly RouteIdsColumn _routeIdsColumn = new() { Label = "Route IDs" };

        public Item_Table(List<GatherableItem> itemList) : base("Item_Table", itemList)
        {
            List<Column<GatherableItem>> headers = [_nameColumn, _typeColumn, _zoneNameColumn, _placeNamesColumn, _itemIdColumn, _routeIdsColumn];
            this.Headers = [.. headers];

            Sortable = true;
            Flags |= ImGuiTableFlags.Hideable | ImGuiTableFlags.Reorderable | ImGuiTableFlags.Resizable;
        }

        public void Dispose()
        {

        }

        public sealed class NameColumn : VerticalCenterColumnString
        {
            public NameColumn() => Flags |= ImGuiTableColumnFlags.NoHide;
            public override string ToName(GatherableItem g)
            {
                string name = g.Name;
                if (g.Item.IsCollectable) name += " ";
                return name;
            }

            public override void DrawColumn(GatherableItem g, int _)
            {
                if (Svc.Texture.TryGetFromGameIcon((int)g.Item.Icon, out var icon))
                {
                    ImGui.Image(icon.GetWrapOrEmpty().Handle, new(ITEM_ICON_SIZE, ITEM_ICON_SIZE));
                }
                ImGui.SameLine();
                PreDraw();
                ImGui.Selectable(this.ToName(g));
            }
        }

        public sealed class TypeColumn : FilterColumn
        {
            public override float Width => 45f;

            public TypeColumn() => SetFlagsAndNames(FilterFlags.Miner, FilterFlags.Botanist);

            public override void DrawColumn(GatherableItem g, int _)
            {
                int iconId;
                if (g.Type == CombinedGatherType.Miner) iconId = 62202;
                else if (g.Type == CombinedGatherType.Botanist) iconId = 62204;
                else return;

                if (Svc.Texture.TryGetFromGameIcon(iconId, out var icon))
                    ImGui.Image(icon.GetWrapOrEmpty().Handle, new(ITEM_ICON_SIZE, ITEM_ICON_SIZE));
            }

            public override int Compare(GatherableItem lhs, GatherableItem rhs)
                => lhs.Type.CompareTo(rhs.Type);

            public override bool FilterFunc(GatherableItem g)
            {
                return g.Type switch
                {
                    CombinedGatherType.Miner => FilterValue.HasFlag(FilterFlags.Miner),
                    CombinedGatherType.Botanist => FilterValue.HasFlag(FilterFlags.Botanist),
                    _ => false,
                };
            }
        }

        public sealed class ZoneNameColumn : VerticalCenterColumnString
        {
            public override string ToName(GatherableItem g) => g.ZoneName;
        }

        public sealed class PlaceNamesColumn : VerticalCenterColumnString
        {
            public override string ToName(GatherableItem g) => string.Join(", ", g.PlaceNames);
        }

        public sealed class ItemIdColumn : VerticalCenterColumnString
        {
            public ItemIdColumn() => Flags |= ImGuiTableColumnFlags.DefaultHide;

            public override string ToName(GatherableItem g) => g.Item.RowId.ToString();

            public override int Compare(GatherableItem lhs, GatherableItem rhs)
                => lhs.Item.RowId.CompareTo(rhs.Item.RowId);
        }

        public sealed class RouteIdsColumn : VerticalCenterColumnString
        {
            public override string ToName(GatherableItem g) => string.Join(", ", g.RouteIds);
        }
    }
}
