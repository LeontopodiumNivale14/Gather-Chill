using Dalamud.Interface.Utility.Raii;
using GatherChill.Enums;
using GatherChill.Gui;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Tools;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui.Tabs_Debug
{
    internal class ReductionEditor
    {
        public class ItemInfo
        {
            public uint ItemId { get; set; } = 0;
            public string Name { get; set; } = "???";
            public uint IconId { get; set; } = 0;
        }

        public static List<ItemInfo> ValidItems = new();
        public static Dictionary<uint, ItemInfo> ValidItemsById = new();

        private static string _pickerSearch = string.Empty;
        private const int PickerPageSize = 50;
        private static int _pickerPage = 0;

        private static bool _showExport = false;
        private static string _exportText = string.Empty;

        private static readonly ExpansionEnum[] AllExpansions = Enum.GetValues<ExpansionEnum>();

        public static void Draw()
        {
            using (var container = ImRaii.Child("Reduction Editor Container", default, true))
            {
                if (!container.Success)
                    return;

                if (ValidItems.Count == 0)
                {
                    foreach (var item in ExcelHelper.Sheet_Item)
                    {
                        if (string.IsNullOrEmpty(item.Name.ToString()))
                            continue;

                        var info = new ItemInfo()
                        {
                            ItemId = item.RowId,
                            Name = item.Name.ToString(),
                            IconId = item.Icon
                        };

                        ValidItems.Add(info);
                        ValidItemsById[item.RowId] = info;
                    }
                }

                if (ImGui.Button("Export to C#"))
                {
                    _exportText = BuildExportText();
                    _showExport = true;
                }

                ImGui.SameLine();
                ImGui.Text($"{Gather_Util.ReducableItems.Count} items tracked");

                ImGui.SameLine();
                if (ImGui.Button("Add New Item"))
                {
                    _pickerSearch = string.Empty;
                    _pickerPage = 0;
                    ImGui.OpenPopup("AddNewItemPopup");
                }

                var existingSourceIds = Gather_Util.ReducableItems
                    .SelectMany(e => e.AllItemIds())
                    .ToHashSet();

                DrawItemPickerPopup("AddNewItemPopup", selectedId =>
                {
                    Gather_Util.ReducableItems.Add(new Gather_Util.ReduceInfo
                    {
                        NormalItemId = selectedId,
                        ResultItems = new() { new(), new(), new() }
                    });
                }, excludeIds: existingSourceIds);

                using (var table = ImRaii.Table("Reduction Table", 5, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.ScrollY))
                {
                    if (!table.Success)
                        return;

                    ImGui.TableSetupColumn("Source Item");
                    ImGui.TableSetupColumn("Expansion");
                    ImGui.TableSetupColumn("Reduce 1");
                    ImGui.TableSetupColumn("Reduce 2");
                    ImGui.TableSetupColumn("Reduce 3");

                    ImGui.TableHeadersRow();

                    for (var rowIdx = 0; rowIdx < Gather_Util.ReducableItems.Count; rowIdx++)
                    {
                        var entry = Gather_Util.ReducableItems[rowIdx];

                        ImGui.PushID($"Row_{rowIdx}");
                        ImGui.TableNextRow();

                        ImGui.TableSetColumnIndex(0);
                        DrawSourceItemCell(entry, rowIdx);

                        ImGui.TableSetColumnIndex(1);
                        DrawExpansionCombo(entry, rowIdx);

                        while (entry.ResultItems.Count < 3)
                            entry.ResultItems.Add(new Gather_Util.ReduceClass());

                        for (var slot = 0; slot < 3; slot++)
                        {
                            ImGui.TableSetColumnIndex(slot + 2);
                            DrawResultSlot(entry, rowIdx, slot);
                        }

                        ImGui.PopID();
                    }
                }
            }

            DrawExportWindow();
        }

        private enum SourceSlot { Normal, Prime, Sublime }

        private static void DrawSourceItemCell(Gather_Util.ReduceInfo entry, int rowIdx)
        {
            DrawSourceItemSlot(entry, rowIdx, SourceSlot.Normal);
            DrawSourceItemSlot(entry, rowIdx, SourceSlot.Prime);
            DrawSourceItemSlot(entry, rowIdx, SourceSlot.Sublime);
        }

        private static void DrawSourceItemSlot(Gather_Util.ReduceInfo entry, int rowIdx, SourceSlot slot)
        {
            var currentId = slot switch
            {
                SourceSlot.Normal => entry.NormalItemId,
                SourceSlot.Prime => entry.PrimeItemId,
                SourceSlot.Sublime => entry.SublimeItemId,
                _ => 0u
            };

            var popupId = $"SourcePopup_{rowIdx}_{slot}";
            var label = currentId == 0
                ? $"[{slot}] Empty"
                : (ValidItemsById.TryGetValue(currentId, out var current) ? current.Name : "???");
            var icon = currentId == 0
                ? 0u
                : (ValidItemsById.TryGetValue(currentId, out var currentIcon) ? currentIcon.IconId : 0u);

            ImGui.PushID($"Source_{slot}");

            if (ImGui_Ice.ImageButtonWithText(icon, label, slot.ToString()))
            {
                _pickerSearch = string.Empty;
                _pickerPage = 0;
                ImGui.OpenPopup(popupId);
            }

            DrawItemPickerPopup(popupId, selectedId =>
            {
                switch (slot)
                {
                    case SourceSlot.Normal: entry.NormalItemId = selectedId; break;
                    case SourceSlot.Prime: entry.PrimeItemId = selectedId; break;
                    case SourceSlot.Sublime: entry.SublimeItemId = selectedId; break;
                }
            });

            // Right-click to clear this slot without opening the picker
            if (currentId != 0 && ImGui.IsItemClicked(ImGuiMouseButton.Right))
            {
                switch (slot)
                {
                    case SourceSlot.Normal: entry.NormalItemId = 0; break;
                    case SourceSlot.Prime: entry.PrimeItemId = 0; break;
                    case SourceSlot.Sublime: entry.SublimeItemId = 0; break;
                }
            }

            ImGui.PopID();
        }

        private static void DrawExpansionCombo(Gather_Util.ReduceInfo entry, int rowIdx)
        {
            var popupId = $"ExpansionPopup_{rowIdx}";
            var expansion = entry.Expansion;

            if (ImGui_Ice.ImageButtonWithText(Sheet_Expansion[expansion].IconId, expansion.ToString(), $"{entry.NormalItemId}_{entry.PrimeItemId}_Expansion"))
                ImGui.OpenPopup(popupId);

            DrawExpansionPickerPopup(popupId, selected =>
            {
                entry.Expansion = selected;
            });
        }

        private static void DrawExpansionPickerPopup(string popupId, System.Action<ExpansionEnum> onSelected)
        {
            if (!ImGui.BeginPopup(popupId))
                return;

            foreach (var exp in AllExpansions)
            {
                ImGui.PushID($"ExpPick_{exp}");

                if (ImGui_Ice.ImageButtonWithText(Sheet_Expansion[exp].IconId, exp.ToString(), string.Empty))
                {
                    onSelected(exp);
                    ImGui.CloseCurrentPopup();
                }

                ImGui.PopID();
            }

            ImGui.EndPopup();
        }

        private static void DrawResultSlot(Gather_Util.ReduceInfo entry, int rowIdx, int slot)
        {
            var reduceEntry = entry.ResultItems[slot];
            var popupId = $"ResultPopup_{rowIdx}_{slot}";

            var label = reduceEntry.ItemId == 0
                ? "Empty"
                : (ValidItemsById.TryGetValue(reduceEntry.ItemId, out var current) ? current.Name : "???");
            var icon = reduceEntry.ItemId == 0
                ? 0u
                : (ValidItemsById.TryGetValue(reduceEntry.ItemId, out var currentIcon) ? currentIcon.IconId : 0u);

            ImGui.PushID($"Result_{slot}");

            if (ImGui_Ice.ImageButtonWithText(icon, label, $"Reduce {slot + 1}"))
            {
                _pickerSearch = string.Empty;
                _pickerPage = 0;
                ImGui.OpenPopup(popupId);
            }

            DrawItemPickerPopup(popupId, selectedId =>
            {
                reduceEntry.ItemId = selectedId;
            });

            ImGui.PopID();
        }

        private static void DrawItemPickerPopup(string popupId, System.Action<uint> onSelected, HashSet<uint>? excludeIds = null)
        {
            if (!ImGui.BeginPopup(popupId))
                return;

            ImGui.SetNextItemWidth(250);
            if (ImGui.InputTextWithHint("##PickerSearch", "Search item name...", ref _pickerSearch, 128))
                _pickerPage = 0;

            IEnumerable<ItemInfo> source = ValidItems;
            if (excludeIds is { Count: > 0 })
                source = source.Where(i => !excludeIds.Contains(i.ItemId));

            var filtered = string.IsNullOrWhiteSpace(_pickerSearch)
                ? source.ToList()
                : source.Where(i => i.Name.Contains(_pickerSearch, System.StringComparison.OrdinalIgnoreCase)).ToList();

            var totalPages = System.Math.Max(1, (filtered.Count + PickerPageSize - 1) / PickerPageSize);
            _pickerPage = System.Math.Clamp(_pickerPage, 0, totalPages - 1);

            var pageItems = filtered.Skip(_pickerPage * PickerPageSize).Take(PickerPageSize);

            using (var child = ImRaii.Child("PickerResults", new Vector2(300, 300), true))
            {
                if (child.Success)
                {
                    foreach (var candidate in pageItems)
                    {
                        ImGui.PushID($"Pick_{candidate.ItemId}");
                        if (ImGui_Ice.ImageButtonWithText(candidate.IconId, candidate.Name, string.Empty))
                        {
                            onSelected(candidate.ItemId);
                            ImGui.CloseCurrentPopup();
                        }
                        ImGui.PopID();
                    }
                }
            }

            ImGui.Separator();
            if (ImGui.Button("< Prev") && _pickerPage > 0)
                _pickerPage--;
            ImGui.SameLine();
            ImGui.Text($"Page {_pickerPage + 1} / {totalPages}");
            ImGui.SameLine();
            if (ImGui.Button("Next >") && _pickerPage < totalPages - 1)
                _pickerPage++;

            ImGui.EndPopup();
        }

        private static string BuildExportText()
        {
            var sb = new StringBuilder();
            sb.AppendLine("public static List<ReduceInfo> ReducableItems = new()");
            sb.AppendLine("{");

            foreach (var entry in Gather_Util.ReducableItems)
            {
                var nameSource = entry.NormalItemId != 0 ? entry.NormalItemId
                    : entry.PrimeItemId != 0 ? entry.PrimeItemId
                    : entry.SublimeItemId;
                var comment = ValidItemsById.TryGetValue(nameSource, out var info) ? info.Name : "???";

                sb.AppendLine($"    new() // {comment}");
                sb.AppendLine("    {");

                if (entry.NormalItemId != 0)
                    sb.AppendLine($"        NormalItemId = {entry.NormalItemId},");
                if (entry.PrimeItemId != 0)
                    sb.AppendLine($"        PrimeItemId = {entry.PrimeItemId},");
                if (entry.SublimeItemId != 0)
                    sb.AppendLine($"        SublimeItemId = {entry.SublimeItemId},");

                sb.AppendLine($"        Expansion = ExpansionEnum.{entry.Expansion},");
                sb.AppendLine("        ResultItems = new()");
                sb.AppendLine("        {");

                foreach (var reduce in entry.ResultItems)
                {
                    if (reduce.ItemId == 0)
                        continue;

                    var reduceName = ValidItemsById.TryGetValue(reduce.ItemId, out var rInfo) ? rInfo.Name : "???";
                    sb.AppendLine($"            new() {{ ItemId = {reduce.ItemId} }}, // {reduceName}");
                }

                sb.AppendLine("        },");
                sb.AppendLine("    },");
            }

            sb.AppendLine("};");
            return sb.ToString();
        }

        private static void DrawExportWindow()
        {
            if (!_showExport)
                return;

            ImGui.SetNextWindowSize(new Vector2(600, 500), ImGuiCond.FirstUseEver);
            if (ImGui.Begin("Reduction Export", ref _showExport))
            {
                if (ImGui.Button("Copy to Clipboard"))
                    ImGui.SetClipboardText(_exportText);

                ImGui.Separator();
                ImGui.InputTextMultiline("##ExportText", ref _exportText, 1_000_000, new Vector2(-1, -1), ImGuiInputTextFlags.ReadOnly);
            }
            ImGui.End();
        }
    }
}