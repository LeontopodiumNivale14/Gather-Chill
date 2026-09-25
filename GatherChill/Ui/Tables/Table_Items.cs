using Dalamud.Interface.Textures;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Utility.Raii;
using GatherChill.Gui;
using GatherChill.Gui.ImGuiTable;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Utility;
using System.Collections.Generic;

namespace GatherChill.Ui.Tables;

internal class Table_Items
{
    public class GatherItems
    {
        public uint ItemId { get; set; } = 0;
        private ItemClass ItemInfo => Gather_Util.Sheet_ItemInfo[ItemId];
        public List<uint> RouteList => ItemInfo.RouteInfo;
        public uint Icon => ItemInfo.IconId;
        public string Name => ItemInfo.Name;
        public int Level => ItemInfo.Level;
        public int Stars => ItemInfo.Star;
        public List<EorzeaTimeWindow> TimeSlot => ItemInfo.TimedSlots;
    }
    public class ItemTable : Table<GatherItems>, IDisposable
    {
        private readonly ItemNameColumn _itemName = new();
        private readonly Level _level = new();
        private readonly ItemId _id = new();
        private readonly Folklore _folklore = new();
        private readonly Uptime _uptime = new();

        public ItemTable(List<GatherItems> itemList)
        {
            List<Column<GatherItems>> headers = [_id, _itemName, _level, _folklore, _uptime];
            Id = "ItemInfo_V1";
            Columns = headers;
            Rows = itemList;
            Sortable = true;
            Flags |= ImGuiTableFlags.Resizable | ImGuiTableFlags.SizingFixedFit;
        }

        public override float CalculateLineHeight()
        {
            return ImGui.GetFrameHeightWithSpacing();
        }
    }

    [Flags]
    public enum LevelEnum
    {
        Lv_1 = 1 << 0,
        Lv_6 = 1 << 1,
        Lv_11 = 1 << 2,
        Lv_16 = 1 << 3,
        Lv_21 = 1 << 4,
        Lv_26 = 1 << 5,
        Lv_31 = 1 << 6,
        Lv_36 = 1 << 7,
        Lv_41 = 1 << 8,
        Lv_46 = 1 << 9,
        Lv_51 = 1 << 10,
        Lv_56 = 1 << 11,
        Lv_61 = 1 << 12,
        Lv_66 = 1 << 13,
        Lv_71 = 1 << 14,
        Lv_76 = 1 << 15,
        Lv_81 = 1 << 16,
        Lv_86 = 1 << 17,
        Lv_91 = 1 << 18,
        Lv_96 = 1 << 19,
    }
    [Flags]
    public enum UptimeEnum
    {
        Always = 1 << 0,
        Currently = 1 << 1,
        Unavailable = 1 << 2,
    }

    public sealed class ItemNameColumn : ColumnString<GatherItems>
    {
        public ItemNameColumn()
        {
            Label = "Name";
            Flags = ImGuiTableColumnFlags.None;
        }

        public override string ToName(GatherItems row)
        {
            var name = row.Name;
            if (name == null)
                return "N/A";
            else
                return row.Name;
        }

        public override void DrawColumn(GatherItems row)
        {
            var frameHeight = ImGui.GetFrameHeight();
            var iconSize = new Vector2(frameHeight);

            ImGui_Ice.ImageButtonWithText(row.Icon, $"{row.Name}", $"{row.ItemId}_{row.Name}");
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
    public sealed class Level : ColumnFlags<LevelEnum, GatherItems>
    {
        private LevelEnum _filterValues;
        private static int LevelRange = 5;
        public override LevelEnum FilterValue => _filterValues;

        private static Dictionary<LevelEnum, string> LabelNames = new()
        {
            [LevelEnum.Lv_1] = "1 - 5",
            [LevelEnum.Lv_6] = "6 - 10",
            [LevelEnum.Lv_11] = "11 - 15",
            [LevelEnum.Lv_16] = "16 - 20",
            [LevelEnum.Lv_21] = "21 - 25",
            [LevelEnum.Lv_26] = "26 - 30",
            [LevelEnum.Lv_31] = "31 - 35",
            [LevelEnum.Lv_36] = "36 - 40",
            [LevelEnum.Lv_41] = "41 - 45",
            [LevelEnum.Lv_46] = "46 - 50",
            [LevelEnum.Lv_51] = "51 - 55",
            [LevelEnum.Lv_56] = "56 - 60",
            [LevelEnum.Lv_61] = "61 - 65",
            [LevelEnum.Lv_66] = "66 - 70",
            [LevelEnum.Lv_71] = "71 - 75",
            [LevelEnum.Lv_76] = "76 - 80",
            [LevelEnum.Lv_81] = "81 - 85",
            [LevelEnum.Lv_86] = "86 - 90",
            [LevelEnum.Lv_91] = "91 - 95",
            [LevelEnum.Lv_96] = "96 - 100",
        };

        public Level()
        {
            LabelKey = "Level";
            var size = ImGui.CalcTextSize("XXXX").X;
            SetFixedWidth(size);
            Flags = ImGuiTableColumnFlags.WidthFixed;

            AllFlags = Enum.GetValues<LevelEnum>().Aggregate((a, b) => a | b);
            _filterValues = AllFlags;
        }

        public override string[] Names => Enum.GetValues<LevelEnum>()
            .Select(GetLabel)
            .ToArray();

        public override void DrawColumn(GatherItems row)
        {
            ImGui.AlignTextToFramePadding();
            if (row.Stars > 0)
            {
                ImGui.Text($"{row.Level:N0}");
                ImGui.SameLine();
                using (var disabled = ImRaii.Disabled(true))
                {
                    ImGui.Text($"{row.Stars}");
                    ImGui.SameLine();
                    ImGuiEx.Icon(FontAwesomeIcon.Star);
                }
            }
            else
            {
                ImGui.Text($"{row.Level:N0}");
            }
        }

        public override bool ShouldShow(GatherItems row)
        {
            var range = GetFlag(row.Level);
            if (range == 0)
                return false;

            return _filterValues.HasFlag(range);
        }

        // Sorting by level -> stars -> star count
        // So [Lv 1 -> Lv 5 as ex]
        // Then by [No stars -> stars]
        // THEN by [1 star -> 4 star] as ex.
        // that way it's properly "level sorted" [ocd]
        public override int Compare(GatherItems a, GatherItems b)
        {
            var byLevel = a.Level.CompareTo(b.Level);
            if (byLevel != 0)
                return byLevel;

            var byStars = a.Stars.CompareTo(b.Stars);
            if (byStars != 0)
                return byStars;

            return a.ItemId.CompareTo(b.ItemId);
        }
        public override void SetValue(LevelEnum value, bool enable)
        {
            if (enable)
                _filterValues |= value;
            else
                _filterValues &= ~value;
        }
        private static string GetLabel(LevelEnum value)
        {
            if (LabelNames.TryGetValue(value, out var lvRange))
            {
                return $"Lv. {lvRange}";
            }
            else
            {
                return $"Lv. ??{value}??";
            }
        }

        private static LevelEnum GetFlag(int level)
        {
            // SHOULD NEVER HAPPEN. But in case I fuck something up here
            if (level < 1)
                return 0;

            var bit = (level - 1) / LevelRange;

            // If somehow we missed went outside the range... shouldn't happen, but might with EC coming
            if (bit >= LabelNames.Count())
                return 0;

            return (LevelEnum)(1 << bit);
        }
    }
    public sealed class ItemId : ColumnNumber<GatherItems>
    {
        public ItemId()
        {
            LabelKey = "ItemId";
            var size = ImGui.CalcTextSize("XXXXX").X;
            SetFixedWidth(size);
            Flags = ImGuiTableColumnFlags.WidthFixed;
        }

        public override int ToValue(GatherItems row) => (int)row.ItemId;

        public override void DrawColumn(GatherItems row)
        {
            ImGui.AlignTextToFramePadding();
            ImGui.Text($"{row.ItemId}");
        }
    }

    // You ever make something so fucking complicated because it looks better?
    // yeah, it's this that... and I'm still debating if I like it rn lol
    public sealed class Folklore : Column<GatherItems>
    {
        private const uint NoneId = 0;

        private static readonly Dictionary<uint, string> Shorthand = new()
        {
            [12238] = "Geological : Coerthas",
            [12239] = "Geological : Dravania",
            [12240] = "Geological : Abalathia's Spine",
            [12698] = "Botanical : Coerthas",
            [12699] = "Botanical : Dravania",
            [12700] = "Botanical : Abalathia's Spine",
            [17838] = "Geological : Gyr Abania",
            [17839] = "Geological : Othard",
            [17840] = "Botanical : Gyr Abania",
            [17841] = "Botanical : Othard",
            [26808] = "Geological : Norvrandt",
            [26809] = "Botanical : Norvrandt",
            [36598] = "Geological : Ilsabard and the Northern Empty",
            [36600] = "Geological : The Sea of Stars",
            [36601] = "Geological : The World Unsundered",
            [36602] = "Botanical : Ilsabard and the Northern Empty",
            [36604] = "Botanical : The Sea of Stars",
            [36605] = "Botanical : The World Unsundered",
            [43878] = "Geological : Yok Tural",
            [43879] = "Geological : Xak Tural",
            [43880] = "Geological : Alexandria",
            [43881] = "Botanical : Yok Tural",
            [43882] = "Botanical : Xak Tural",
            [43883] = "Botanical : Alexandria",
        };

        private readonly HashSet<uint> _selected = [];
        private List<(uint Id, string Name)>? _options;

        private List<(uint Id, string Name)> Options => _options ??= BuildOptions();

        public Folklore()
        {
            Label = "Folklore";
            _selected.Add(NoneId);
            foreach (var option in Options)
                _selected.Add(option.Id);
            Flags = ImGuiTableColumnFlags.None;
        }

        private static string GetShortName(uint id, string fullName)
        {
            return Shorthand.TryGetValue(id, out var shortName) ? shortName : fullName;
        }

        private static List<(uint Id, string Name)> BuildOptions()
        {
            var list = new List<(uint Id, string Name)> { (NoneId, "None") };

            var folklores = Gather_Util.Sheet_RouteInfo.Values
                .Select(route => route.Folklore)
                .Where(folklore => folklore.ItemId != 0)
                .GroupBy(folklore => folklore.ItemId)
                .Select(group => group.First())
                .OrderBy(folklore => folklore.ItemId);

            foreach (var folklore in folklores)
                list.Add((folklore.ItemId, GetShortName(folklore.ItemId, folklore.Name)));

            return list;
        }

        // Folklore item IDs an item's routes require (empty = none).
        private static List<uint> GetFolkloreIds(GatherItems row)
        {
            return row.RouteList
                .Select(x => Gather_Util.Sheet_RouteInfo[x].Folklore.ItemId)
                .Where(id => id != 0)
                .Distinct()
                .ToList();
        }

        public override bool ShouldShow(GatherItems row)
        {
            var ids = GetFolkloreIds(row);
            if (ids.Count == 0)
                return _selected.Contains(NoneId);

            return ids.Any(_selected.Contains);
        }

        // None sorts first, then by the lowest folklore item ID the row needs.
        public override int Compare(GatherItems a, GatherItems b)
        {
            return SortKey(a).CompareTo(SortKey(b));
        }

        private static uint SortKey(GatherItems row)
        {
            var ids = GetFolkloreIds(row);
            if (ids.Count == 0)
                return NoneId;

            return ids.Min();
        }

        public override bool DrawFilter()
        {
            using var id = ImRaii.PushId("##Filter");
            using var style = ImRaii.PushStyle(ImGuiStyleVar.FrameRounding, 0);
            ImGui.SetNextItemWidth(-Table.ArrowWidth * ImGuiHelpers.GlobalScale);

            var all = _selected.Count == Options.Count;
            using var color = ImRaii.PushColor(ImGuiCol.FrameBg, 0x803030A0, !all);
            using var combo = ImRaii.Combo(string.Empty, Label, ImGuiComboFlags.NoArrowButton);

            if (ImGui.IsItemClicked(ImGuiMouseButton.Right))
            {
                SelectAll();
                return true;
            }

            if (!all && ImGui.IsItemHovered())
                ImGui.SetTooltip("Right-click to clear filters.");

            if (!combo)
                return false;

            color.Pop();

            var ret = false;
            if (ImGui.Checkbox("Enable All", ref all))
            {
                if (all)
                    SelectAll();
                else
                    _selected.Clear();

                ret = true;
            }

            using var indent = ImRaii.PushIndent(10f);
            foreach (var (optionId, name) in Options)
            {
                var enabled = _selected.Contains(optionId);
                if (!ImGui.Checkbox($"{name}##{optionId}", ref enabled))
                    continue;

                if (enabled)
                    _selected.Add(optionId);
                else
                    _selected.Remove(optionId);

                ret = true;
            }

            return ret;
        }

        private void SelectAll()
        {
            foreach (var option in Options)
                _selected.Add(option.Id);
        }

        public override void DrawColumn(GatherItems row)
        {
            var folkloreList = row.RouteList
                .Where(x => Gather_Util.Sheet_RouteInfo[x].Folklore.ItemId != 0)
                .ToList();

            if (folkloreList.Count == 0)
                return;

            var folkloreItem = Gather_Util.Sheet_RouteInfo[folkloreList.First()].Folklore;
            var id = $"{folkloreItem.IconId}_{folkloreItem.Name}_{row.ItemId}";

            bool unlocked = Svc.UnlockState.IsItemUnlocked(folkloreItem.SheetInfo);

            if (FitsInColumn(folkloreItem.Name))
            {
                ImGui_Ice.ImageButtonWithText(folkloreItem.IconId, folkloreItem.Name, id, unlocked);
                
            }
            else
                ImGui_Ice.ImageButton(folkloreItem.IconId, id, unlocked);

            if (ImGui.IsItemHovered())
            {
                using (var tooltip = ImRaii.Tooltip())
                {
                    if (!tooltip.Alive)
                        return;

                    DrawFolkloreTable(folkloreList);
                }
            }
        }

        private static bool FitsInColumn(string name)
        {
            var available = ImGui.GetContentRegionAvail().X;
            var textWidth = ImGui.CalcTextSize(name).X;
            var iconWidth = ImGui.GetFrameHeight(); // roughly the icon's square size

            return available >= iconWidth + ImGui.GetStyle().ItemSpacing.X + textWidth;
        }

        private static void DrawFolkloreTable(List<uint> folkloreList)
        {
            using (var table = ImRaii.Table("Folklore Info", 3, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
            {
                if (!table.Success)
                    return;

                ImGui.TableSetupColumn("##Icon");
                ImGui.TableSetupColumn("Name");
                ImGui.TableSetupColumn("Unlocked");

                ImGui.TableHeadersRow();

                foreach (var folklore in folkloreList)
                {
                    var info = Gather_Util.Sheet_RouteInfo[folklore].Folklore;

                    ImGui.TableNextRow();
                    ImGui.TableSetColumnIndex(0);
                    GameIcons.DrawInline(info.IconId, false);

                    ImGui.TableNextColumn();
                    ImGui.AlignTextToFramePadding();
                    ImGui.Text($"{info.Name}");

                    ImGui.TableNextColumn();
                    var unlocked = Svc.UnlockState.IsItemUnlocked(info.SheetInfo);
                    ImGui_Ice.Icon(unlocked ? FontAwesomeIcon.Check : FontAwesomeIcon.Times);
                }
            }
        }
    }
    public sealed class Uptime : ColumnFlags<UptimeEnum, GatherItems>
    {
        private UptimeEnum _filterValues;
        public override UptimeEnum FilterValue => _filterValues;

        public Uptime()
        {
            Label = "Uptime";
            AllFlags = Enum.GetValues<UptimeEnum>().Aggregate((a, b) => a | b);
            _filterValues = AllFlags;
            Flags = ImGuiTableColumnFlags.None;
        }

        private static UptimeEnum GetState(GatherItems row)
        {
            if (row.TimeSlot.Count == 0)
                return UptimeEnum.Always;

            return UpFor(row) is not null ? UptimeEnum.Currently : UptimeEnum.Unavailable;
        }

        // Filter: show the row if its current state is one of the selected flags.
        public override bool ShouldShow(GatherItems row)
            => _filterValues.HasFlag(GetState(row));

        public override void SetValue(UptimeEnum value, bool enable)
        {
            if (enable)
                _filterValues |= value;
            else
                _filterValues &= ~value;
        }

        // Sort: Always -> Currently -> Unavailable
        // then by the most useful countdown within a state.
        // (so shorted being first, then next longest... ect)
        // Then by level -> stars -> itemIds... sheesh. I really hate myself don't I
        public override int Compare(GatherItems a, GatherItems b)
        {
            var stateA = GetState(a);
            var stateB = GetState(b);

            if (stateA != stateB)
                return SortRank(stateA).CompareTo(SortRank(stateB));

            var byCountdown = SecondaryKey(a, stateA).CompareTo(SecondaryKey(b, stateB));
            if (byCountdown != 0)
                return byCountdown;

            var byLevel = a.Level.CompareTo(b.Level);
            if (byLevel != 0)
                return byLevel;

            var byStars = a.Stars.CompareTo(b.Stars);
            if (byStars != 0)
                return byStars;

            return a.ItemId.CompareTo(b.ItemId);
        }

        // Setting the rank order here so I don't have to worry about if (forwhatever reason) I change the names/order of them in the enum
        private static int SortRank(UptimeEnum state)
        {
            return state switch
            {
                UptimeEnum.Always => 0,
                UptimeEnum.Currently => 1,
                _ => 2,
            };
        }

        // Currently: time left, least first. Unavailable: time until it opens, soonest first.
        private static TimeSpan SecondaryKey(GatherItems row, UptimeEnum state)
        {
            return state switch
            {
                UptimeEnum.Currently => UpFor(row) ?? TimeSpan.Zero,
                UptimeEnum.Unavailable => NextUptime(row) ?? TimeSpan.MaxValue,
                _ => TimeSpan.Zero,
            };
        }

        public override void DrawColumn(GatherItems row)
        {
            ImGui.AlignTextToFramePadding();
            if (row.TimeSlot.Count == 0)
            {
                using (var disabled = ImRaii.Disabled(true))
                    ImGui.Text("Always");
            }
            else
            {
                if (UpFor(row) is { } upFor)
                {
                    ImGui.Button($"Up for: {FormatCountdown(upFor)}");
                }
                else if (NextUptime(row) is { } next)
                {
                    var style = ImGui.GetStyle();
                    var disabledText = style.Colors[(int)ImGuiCol.TextDisabled];
                    var button = style.Colors[(int)ImGuiCol.Button];

                    using var colors = ImRaii.PushColor(ImGuiCol.Button, button with { W = button.W * 0.5f })
                                             .Push(ImGuiCol.ButtonHovered, button with { W = button.W * 0.5f })
                                             .Push(ImGuiCol.ButtonActive, button with { W = button.W * 0.5f });

                    ImGui.Button($"Next Uptime: {FormatCountdown(next)}");
                }
                if (ImGui.IsItemHovered())
                {
                    var slots = GetUpcoming(row);
                    if (slots.Count != 0)
                    {
                        using (var tooltip = ImRaii.Tooltip())
                        {
                            if (tooltip.Alive)
                            {
                                DrawUpcoming(slots);
                            }
                        }
                    }
                }
            }
        }
        private static string FormatCountdown(TimeSpan span)
        {
            if (span.TotalHours >= 1)
                return $"{(int)span.TotalHours}h {span.Minutes}m";

            return $"{span.Minutes}m {span.Seconds}s";
        }
        private static TimeSpan? UpFor(GatherItems row)
        {
            if (row.TimeSlot.Count == 0)
                return null;

            var remaining = row.TimeSlot.Max(w => w.TimeUntilEnd());
            return remaining > TimeSpan.Zero ? remaining : null;
        }
        private static TimeSpan? NextUptime(GatherItems row)
        {
            if (row.TimeSlot.Count == 0)
                return null;

            return row.TimeSlot.Min(w => w.TimeUntilStart());
        }
        private readonly record struct UpcomingSlot(EorzeaTimeWindow Window, DateTime Start, DateTime End, bool IsUp);
        private static List<UpcomingSlot> GetUpcoming(GatherItems row, int count = 5)
        {
            var now = DateTime.Now;
            var eorzeaDay = Utils.EorzeaMinutesToReal(Utils.EorzeaMinutesPerDay);
            var slots = new List<UpcomingSlot>();

            foreach (var window in row.TimeSlot)
            {
                var isUp = window.IsUp();

                // First occurrence: currently running, or the next one to open.
                var start = isUp
                    ? now - (window.DurationReal - window.TimeUntilEnd())
                    : now + window.TimeUntilStart();

                // Enough repeats of each window that we can't run out before hitting `count`.
                for (var i = 0; i < count; i++)
                {
                    var slotStart = start + eorzeaDay * i;
                    slots.Add(new(window, slotStart, slotStart + window.DurationReal, isUp && i == 0));
                }
            }

            return slots.OrderBy(s => s.Start).Take(count).ToList();
        }
        private static void DrawUpcoming(List<UpcomingSlot> slots)
        {
            ImGui.TextUnformatted("Upcoming uptimes");
            ImGui.Separator();

            using (var table = ImRaii.Table("##UpcomingUptimes", 4, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.RowBg))
            {
                if (!table.Success)
                    return;

                ImGui.TableSetupColumn("Status");
                ImGui.TableSetupColumn("Local");
                ImGui.TableSetupColumn("Eorzea");
                ImGui.TableSetupColumn("Duration");
                ImGui.TableHeadersRow();

                var now = DateTime.Now;
                var dim = ImGui.GetStyle().Colors[(int)ImGuiCol.TextDisabled];

                for (var i = 0; i < slots.Count; i++)
                {
                    var slot = slots[i];

                    ImGui.TableNextRow();

                    // Later rows fade a little so the eye lands on what's next.
                    var fade = slot.IsUp ? 1f : Math.Max(0.55f, 1f - i * 0.1f);
                    using var color = ImRaii.PushColor(ImGuiCol.Text, ImGui.GetStyle().Colors[(int)ImGuiCol.Text] with { W = fade });

                    // Status
                    ImGui.TableNextColumn();
                    if (slot.IsUp)
                        ImGui.TextColored(new Vector4(0.4f, 0.9f, 0.4f, 1f), $"Up ({FormatCountdown(slot.End - now)} left)");
                    else
                        ImGui.Text($"in {FormatCountdown(slot.Start - now)}");

                    // Local
                    ImGui.TableNextColumn();
                    ImGui.Text($"{slot.Start:t} – {slot.End:t}");

                    // Eorzea
                    ImGui.TableNextColumn();
                    ImGui.Text(slot.Window.ToString());

                    // Duration
                    ImGui.TableNextColumn();
                    ImGui.Text(FormatCountdown(slot.Window.DurationReal));
                }
            }
        }

    }
}
