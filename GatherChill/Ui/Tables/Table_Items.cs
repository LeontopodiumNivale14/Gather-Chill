using Dalamud.Game.Text;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Utility.Raii;
using GatherChill.Enums;
using GatherChill.Gui;
using GatherChill.Gui.ImGuiTable;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Utility;
using SharpDX.Direct3D11;
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
        public uint QuestId => ItemInfo.RequiredQuest;
        public bool IsCollectable => ItemInfo.IsCollectable;
        public List<uint> Reduce_Sources => ItemInfo.ReducedFromItemIds;
        public uint Reduce_Into => ItemInfo.ReducesIntoItemId;
        public List<uint> NodeTypes => ItemInfo.NodeTypes;
        public uint RequiredItem => ItemInfo.ItemRequired;
        public bool IsHidden => ItemInfo.IsHidden;
        public List<Gather_Util.UptimeHelper.UptimeEntry> TimeSlot => ItemInfo.TimedSlots;
    }
    public class ItemTable : Table<GatherItems>, IDisposable
    {
        private readonly ItemNameColumn _itemName = new();
        private readonly Level _level = new();
        private readonly ItemId _id = new();
        private readonly Folklore _folklore = new();
        private readonly Uptime _uptime = new();
        private readonly Type _nodeTypes = new();
        private readonly Notes _itemNotes = new();

        public ItemTable(List<GatherItems> itemList)
        {
            List<Column<GatherItems>> headers = [_itemName, _level, _nodeTypes, _folklore, _uptime, _itemNotes, _id];
            Id = "ItemInfo_V1";
            Columns = headers;
            Rows = itemList;
            Sortable = true;
            Flags |= ImGuiTableFlags.Resizable | ImGuiTableFlags.SizingFixedFit;
        }

        public override float CalculateLineHeight()
        {
            return ImGui.GetFrameHeight() + ImGui.GetStyle().CellPadding.Y * 2;
        }
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

        private int Amount = 1;

        private void AddItemPopup(GatherItems row)
        {
            using (var popup = ImRaii.Popup("Add Item to gather list?"))
            {
                if (!popup.Success)
                    return;

                ImGui.Text($"{row.Name}");
                ImGui.SetNextItemWidth(100 * ImGuiHelpers.GlobalScale);
                ImGui.InputInt("Amount", ref Amount, 1, 10);

                using (var disabled = ImRaii.Disabled(Amount < 1))
                {
                    if (ImGui.Button("Add"))
                    {
                        P.ipcProvider.AddToGatherList([(row.ItemId, Amount)]);
                        ImGui.CloseCurrentPopup();
                    }
                }
            }
        }

        public override void DrawColumn(GatherItems row)
        {
            var nameText = row.IsCollectable ? $"{row.Name} {SeIconChar.Collectible.ToIconString()}" : row.Name;

            if (ImGui_Ice.ImageButtonWithText(row.Icon, nameText, $"{row.ItemId}_{row.Name}"))
            {
                Amount = 1;
                ImGui.OpenPopup("Add Item to gather list?");
            }
            if (ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();

                ImGui.Text($"Item ID: {row.ItemId}");
                ImGui_Ice.Table_VertCenterText($"{Utils.GetItemCount(row.ItemId)}");

                ImGui.EndTooltip();
            }
            AddItemPopup(row);
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

            return Gather_Util.UptimeHelper.IsUp(row.TimeSlot) ? UptimeEnum.Currently : UptimeEnum.Unavailable;
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
        // Then by level -> stars -> itemIds
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
                UptimeEnum.Currently => Gather_Util.UptimeHelper.UpFor(row.TimeSlot) ?? TimeSpan.Zero,
                UptimeEnum.Unavailable => Gather_Util.UptimeHelper.NextUptime(row.TimeSlot) ?? TimeSpan.MaxValue,
                _ => TimeSpan.Zero,
            };
        }

        public override void DrawColumn(GatherItems row) => Gather_Util.UptimeHelper.Draw(row.TimeSlot, "Always");
    }
    public sealed class Type : ColumnFlags<NodeTypes, GatherItems>
    {
        private NodeTypes _filterValues;
        public override NodeTypes FilterValue => _filterValues;
        public Type()
        {
            LabelKey = "Type";
            AllFlags = Enum.GetValues<NodeTypes>().Aggregate((a, b) => a | b);
            _filterValues = AllFlags;
            Flags = ImGuiTableColumnFlags.None;
        }

        public override bool ShouldShow(GatherItems row)
        {
            return (FilterValue.HasFlag(NodeTypes.Mining) && row.NodeTypes.Contains(0))
                || (FilterValue.HasFlag(NodeTypes.Quarrying) && row.NodeTypes.Contains(1))
                || (FilterValue.HasFlag(NodeTypes.Logging) && row.NodeTypes.Contains(2))
                || (FilterValue.HasFlag(NodeTypes.Harvesting) && row.NodeTypes.Contains(3));
        }

        public override void DrawColumn(GatherItems row)
        {
            var iconDict = Gather_Util.Icons_AssignmentType;
            for (int i = 0; i < row.NodeTypes.Count; i++)
            {
                if (i != 0)
                    ImGui.SameLine(0, 0.5f);

                var type = row.NodeTypes[i];
                ImGui_Ice.ImageButton(iconDict[type].IconId, $"IconType_{i}", sidePadding: 0.1f);
            }
        }

        public override void SetValue(NodeTypes value, bool enable)
        {
            if (enable)
                _filterValues |= value;
            else
                _filterValues &= ~value;
        }
    }
    public sealed class Notes : ColumnFlags<NotesEnum, GatherItems>
    {
        private NotesEnum _filterValues;
        public override NotesEnum FilterValue => _filterValues;

        public override string[] Names => Enum.GetValues<NotesEnum>()
            .Select(GetLabel)
            .ToArray();

        public Notes()
        {
            Label = "Notes";
            AllFlags = Enum.GetValues<NotesEnum>().Aggregate((a, b) => a | b);
            _filterValues = AllFlags;
            Flags = ImGuiTableColumnFlags.WidthStretch;
        }

        private static string GetLabel(NotesEnum value)
        {
            return value switch
            {
                NotesEnum.None => "None",
                NotesEnum.Collectable => "Collectable",
                NotesEnum.ReduceSource => "Reducible From",
                NotesEnum.ReduceInto => "Reduces Into",
                NotesEnum.Quest => "Requires Quest",
                NotesEnum.Tool => "Requires Tool",
                NotesEnum.Hidden => "Hidden",
                _ => $"??{value}??",
            };
        }

        // Builds the set of attributes a row has. No attributes = None.
        private static NotesEnum GetState(GatherItems row)
        {
            var state = (NotesEnum)0;

            if (CollectableDatabase.TryGet(row.ItemId, out _))
                state |= NotesEnum.Collectable;

            if (row.Reduce_Sources.Count != 0)
                state |= NotesEnum.ReduceSource;
            else if (row.Reduce_Into != 0)
                state |= NotesEnum.ReduceInto;

            if (row.QuestId != 0)
                state |= NotesEnum.Quest;

            if (row.RequiredItem != 0)
                state |= NotesEnum.Tool;

            if (row.IsHidden)
                state |= NotesEnum.Hidden;

            return state == 0 ? NotesEnum.None : state;
        }

        public override bool ShouldShow(GatherItems row)
            => (GetState(row) & _filterValues) != 0;

        public override void SetValue(NotesEnum value, bool enable)
        {
            if (enable)
                _filterValues |= value;
            else
                _filterValues &= ~value;
        }

        public override int Compare(GatherItems a, GatherItems b)
        {
            var stateA = GetState(a);
            var stateB = GetState(b);

            var byCount = BitOperations.PopCount((uint)stateA)
                .CompareTo(BitOperations.PopCount((uint)stateB));
            if (byCount != 0)
                return byCount;

            var byState = ((int)stateA).CompareTo((int)stateB);
            if (byState != 0)
                return byState;

            var byLevel = a.Level.CompareTo(b.Level);
            if (byLevel != 0)
                return byLevel;

            var byStars = a.Stars.CompareTo(b.Stars);
            if (byStars != 0)
                return byStars;

            return a.ItemId.CompareTo(b.ItemId);
        }

        public override void DrawColumn(GatherItems row)
        {
            var state = GetState(row);
            if (state == NotesEnum.None)
                return;

            var drawn = false;

            if (state.HasFlag(NotesEnum.Collectable) && CollectableDatabase.TryGet(row.ItemId, out var collectableInfo))
            {
                SameLineIfNeeded(ref drawn);
                DrawCollectable(collectableInfo);
            }

            if (state.HasFlag(NotesEnum.ReduceSource))
            {
                SameLineIfNeeded(ref drawn);
                DrawReduceSources(row);
            }

            if (state.HasFlag(NotesEnum.ReduceInto))
            {
                SameLineIfNeeded(ref drawn);
                DrawReduceInto(row);
            }

            if (state.HasFlag(NotesEnum.Quest))
            {
                SameLineIfNeeded(ref drawn);
                DrawQuest(row);
            }

            if (state.HasFlag(NotesEnum.Tool))
            {
                SameLineIfNeeded(ref drawn);
                DrawTool(row);
            }

            if (state.HasFlag(NotesEnum.Hidden))
            {
                SameLineIfNeeded(ref drawn);
                DrawHidden(row);
            }
        }

        private static void SameLineIfNeeded(ref bool drawn)
        {
            if (drawn)
                ImGui.SameLine();

            drawn = true;
        }

        private static void DrawCollectable(CollectableInfo collectableInfo)
        {
            ImGui_Ice.ImageButton(109, "Collectable");
            if (!ImGui.IsItemHovered())
                return;

            using var tooltip = ImRaii.Tooltip();
            if (!tooltip.Alive)
                return;

            ImGui.Text("Collectable");
            ImGui.Text($"Level: {collectableInfo.LevelMin} - {collectableInfo.LevelMax}");

            using var table = ImRaii.Table("Collectable Info", 3, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg);
            if (!table.Success)
                return;

            ImGui.TableSetupColumn("Quality");
            ImGui.TableSetupColumn("Exp");
            ImGui.TableSetupColumn("Scripts");
            ImGui.TableHeadersRow();

            foreach (var reward in collectableInfo.Rewards)
            {
                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                var rewardText = reward.MaxQuality != null
                    ? $"{reward.MinQuality:N0} - {reward.MaxQuality}"
                    : $"{reward.MinQuality:N0}";
                ImGui.Text(rewardText);

                ImGui.TableNextColumn();
                ImGui.Text($"{reward.Experience:N0}");

                ImGui.TableNextColumn();
                ImGui.Text($"{reward.Scrips}");
            }
        }

        private static void DrawReduceSources(GatherItems row)
        {
            ImGui_Ice.ImageButton(121, "Reducable");
            if (!ImGui.IsItemHovered())
                return;

            using var tooltip = ImRaii.Tooltip();
            if (!tooltip.Alive)
                return;

            using var table = ImRaii.Table("Reducable Tooltip Table", 3, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.RowBg | ImGuiTableFlags.Borders);
            if (!table.Success)
                return;

            ImGui.TableSetupColumn("Icon");
            ImGui.TableSetupColumn("Name");
            ImGui.TableSetupColumn("Job");

            foreach (var item in row.Reduce_Sources)
            {
                if (!Gather_Util.Sheet_ItemInfo.TryGetValue(item, out var info))
                    continue;

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                GameIcons.DrawInline(info.IconId, false);

                ImGui.TableNextColumn();
                ImGui.AlignTextToFramePadding();
                ImGui.Text($"{info.Name}");

                ImGui.TableNextColumn();
                var iconDict = Gather_Util.Icons_AssignmentType;
                var job = info.NodeTypes[0];
                ImGui_Ice.ImageButton(iconDict[job].IconId, "Button", sidePadding: 0.1f);
            }
        }

        private static void DrawReduceInto(GatherItems row)
        {
            var reducableItem = Gather_Util.Sheet_ItemInfo[row.Reduce_Into];
            ImGui_Ice.ImageButton(reducableItem.IconId, "Reduce Info");
            if (!ImGui.IsItemHovered())
                return;

            using var tooltip = ImRaii.Tooltip();
            if (!tooltip.Alive)
                return;

            ImGui.Text($"{reducableItem.Name}");
        }

        private static void DrawQuest(GatherItems row)
        {
            if (!ExcelHelper.Sheet_Quest.TryGetRow(row.QuestId, out var questInfo))
                return;

            ImGui_Ice.ImageButton(230607, "Quest Required", sidePadding: 2);
            if (!ImGui.IsItemHovered())
                return;

            using var tooltip = ImRaii.Tooltip();
            if (!tooltip.Alive)
                return;

            ImGui.Text($"Requires Quest: {questInfo.Name}");
        }

        private static void DrawTool(GatherItems row)
        {
            if (ExcelHelper.Sheet_Item.TryGetRow(row.RequiredItem, out var itemSheet))
            {
                ImGui_Ice.ImageButton(itemSheet.Icon, $"{row.Name} | {row.RequiredItem}");
                if (!ImGui.IsItemHovered())
                    return;

                using var tooltip = ImRaii.Tooltip();
                if (!tooltip.Alive)
                    return;

                ImGui.Text("Requires tool to be equipped:");
                ImGui.Text($"{itemSheet.Name}");
                return;
            }

            ImGuiEx.Icon(FontAwesomeIcon.Tools);
            if (!ImGui.IsItemHovered())
                return;

            using var fallbackTooltip = ImRaii.Tooltip();
            if (!fallbackTooltip.Alive)
                return;

            ImGui.Text("Requires tool to be equipped:");
            ImGui.Text($"ItemId: {row.RequiredItem}");
        }

        private static void DrawHidden(GatherItems row)
        {
            var icon = Gather_Util.GathActionDict[GatherBuffId.HiddenFinder].ClassAction[ECommons.ExcelServices.Job.MIN].IconId;
            ImGui_Ice.ImageButton(icon, "Hidden Item");
            if (!ImGui.IsItemHovered())
                return;

            ImGui.SetTooltip("Hidden item, might show up on node");
        }
    }
}
