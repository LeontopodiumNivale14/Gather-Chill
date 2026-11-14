using Dalamud.Interface.Utility;
using Dalamud.Interface.Utility.Raii;
using ECommons.ExcelServices;
using GatherChill.Enums;
using GatherChill.Gui;
using GatherChill.Utilities.GatheringHelpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui.Tabs_MainWindow
{
    internal class GatherBuffs_Tab
    {
        #region State

        private const string RulePayload = "GC_RULE";
        private const string FolderPayload = "GC_BUFF_FOLDER";
        private const string ExportPrefix = "GCBUFFS:";
        private const ImGuiDragDropFlags LineFlags = ImGuiDragDropFlags.AcceptBeforeDelivery | ImGuiDragDropFlags.AcceptNoDrawDefaultRect;

        private static float _modeWindow = 100f;
        private static readonly List<Job> GatherJobs = new() { Job.MIN, Job.BTN };
        internal static Job _selectedJob = GatherJobs[0];

        // The buffs the planner actually knows about, in display order
        private static readonly GatherBuffId[] DisplayBuffs =
        {
            GatherBuffId.FieldMasteryI, GatherBuffId.FieldMasteryII, GatherBuffId.FieldMasteryIII,
            GatherBuffId.YieldI, GatherBuffId.YieldII,
            GatherBuffId.BoonIncrease_1, GatherBuffId.BoonIncrease_2,
            GatherBuffId.Tidings,
            GatherBuffId.BYII,
            GatherBuffId.BonusIntegrity, GatherBuffId.BonusIntegrity_Chance,
        };

        private static readonly GatherNodeKind[] NodeKinds = Enum.GetValues<GatherNodeKind>();

        // -1 = the default (fallback) profile, otherwise an index into C.User_BuffRules
        private static int _selectedRule = -1;

        private static int _draggedRule = -1;
        private static int _draggedFolder = -1;

        private static int _renameRule = -1;
        private static int _renameFolder = -1;
        private static string _renameBuffer = "";
        private static bool _openRenamePopup = false;

        #endregion

        #region Main Draw

        public static void Draw()
        {
            using var child = ImRaii.Child("Gather Buffs: Child Container", default);
            if (!child.Success)
                return;

            using var colors = ImGui_Ice.PushChildBG();
            var availWidth = ImGui.GetContentRegionAvail().X;

            using (var childSelection = ImRaii.Child("Gather Buffs: Selection", new(availWidth, _modeWindow), true))
            {
                if (!childSelection.Success)
                    return;

                DrawJobSelector();
                ImGui.SameLine();
                if (ImGui.Button("Replays"))
                    P.replayWindow.IsOpen = true;
                _modeWindow = ImGui.GetCursorPosY() + ImGui.GetStyle().WindowPadding.Y;
            }

            var scale = ImGuiHelpers.GlobalScale;
            var remaining = ImGui.GetContentRegionAvail();

            DrawRuleColumn(220 * scale, remaining.Y);
            ImGui.SameLine();

            using (var childEditor = ImRaii.Child("Gather Buffs: Editor", ImGui.GetContentRegionAvail(), true))
            {
                if (!childEditor.Success)
                    return;

                DrawEditor();
            }
        }

        private static void DrawJobSelector()
        {
            for (var i = 0; i < GatherJobs.Count; i++)
            {
                if (i != 0)
                    ImGui.SameLine();

                var job = GatherJobs[i];
                var isSelected = _selectedJob == job;

                if (ImGui_Ice.ImageButton(Gather_Util.Job_IconIds[job].IconId, $"{job}", isSelected, 1))
                    _selectedJob = job;
            }

            ImGui.SameLine();
            if (ImGui.Button("Open calculator"))
                CalculatorWindow.OpenFor(_selectedRule);
        }

        #endregion

        #region Rule Column

        private static void DrawRuleColumn(float width, float height)
        {
            var style = ImGui.GetStyle();
            var padding = style.WindowPadding.Y * 2;

            // Footer is two rows of buttons
            var topHeight = ImGui.GetFrameHeight() + padding;
            var bottomHeight = ImGui.GetFrameHeight() * 2 + style.ItemSpacing.Y + padding;
            var middleHeight = Math.Max(height - topHeight - bottomHeight - style.ItemSpacing.Y * 2, 50 * ImGuiHelpers.GlobalScale);

            // Open + draw at the parent level so the popup IDs match
            if (_openRenamePopup)
            {
                ImGui.OpenPopup("RenameEntry");
                _openRenamePopup = false;
            }
            DrawRenamePopup();

            using var group = ImRaii.Group();

            // Default is pinned: it never scrolls away and can't be moved, renamed or deleted
            using (var top = ImRaii.Child("##RuleDefault", new Vector2(width, topHeight), true))
            {
                if (top.Success && ImGui.Selectable("Default (fallback)", _selectedRule == -1))
                    Select(-1);
            }

            using (var middle = ImRaii.Child("##RuleScroll", new Vector2(width, middleHeight), true))
            {
                if (middle.Success)
                    DrawRuleEntries();
            }

            using (var bottom = ImRaii.Child("##RuleFooter", new Vector2(width, bottomHeight), true))
            {
                if (bottom.Success)
                    DrawRuleFooter();
            }
        }

        private static void DrawRuleEntries()
        {
            var rules = C.User_BuffRules;
            var folders = C.User_BuffFolders;

            if (rules.Count == 0 && folders.Count == 0)
                ImGui.TextDisabled("No rules yet.");

            // Edits are applied after drawing so the lists never change while they're being iterated
            var deleteIndex = -1;
            var duplicateIndex = -1;
            var deleteFolder = -1;

            foreach (var folder in folders)
                DrawFolder(folder, ref deleteIndex, ref duplicateIndex, ref deleteFolder);

            // Root rules: no folder, or the folder no longer exists
            for (var i = 0; i < rules.Count; i++)
            {
                if (rules[i].FolderId is { } folderId && folders.Any(f => f.Id == folderId))
                    continue;

                DrawRuleRow(i, ref deleteIndex, ref duplicateIndex);
            }

            DrawRootDropZone();

            if (duplicateIndex != -1)
            {
                var source = rules[duplicateIndex];

                rules.Insert(duplicateIndex + 1, new Gather_Util.BuffRule
                {
                    Name = $"{source.Name} (copy)",
                    Enabled = source.Enabled,
                    FolderId = source.FolderId,
                    Kind = source.Kind,
                    MinIntegrity = source.MinIntegrity,
                    MaxIntegrity = source.MaxIntegrity,
                    Profile = CloneProfile(source.Profile),
                });

                Select(duplicateIndex + 1);
                Changed();
            }

            if (deleteIndex != -1)
            {
                rules.RemoveAt(deleteIndex);
                Select(-1);
                Changed();
            }

            if (deleteFolder != -1)
            {
                // Rules inside move to the root
                foreach (var rule in rules.Where(r => r.FolderId == deleteFolder))
                    rule.FolderId = null;

                folders.RemoveAll(f => f.Id == deleteFolder);
                Changed();
            }
        }

        private static void DrawFolder(Gather_Util.BuffFolder folder, ref int deleteIndex, ref int duplicateIndex, ref int deleteFolder)
        {
            using var id = ImRaii.PushId($"bfolder_{folder.Id}");
            using var node = ImRaii.TreeNode($"{folder.Name}###bfolder_{folder.Id}");

            // All of these attach to the tree node, so they must come right after it
            FolderDragSource(folder);
            FolderDropTarget(folder);
            DrawFolderContextMenu(folder, ref deleteFolder);

            if (!node.Success)
                return;

            var rules = C.User_BuffRules;
            for (var i = 0; i < rules.Count; i++)
            {
                if (rules[i].FolderId == folder.Id)
                    DrawRuleRow(i, ref deleteIndex, ref duplicateIndex);
            }
        }

        private static void DrawRuleRow(int index, ref int deleteIndex, ref int duplicateIndex)
        {
            var rule = C.User_BuffRules[index];
            using var id = ImRaii.PushId($"rule_{index}");

            var enabled = rule.Enabled;
            if (ImGui.Checkbox("##enabled", ref enabled))
            {
                rule.Enabled = enabled;
                Changed();
            }

            if (ImGui.IsItemHovered())
                ImGui.SetTooltip("Off = this rule is ignored when matching nodes");

            ImGui.SameLine();
            if (ImGui.Selectable($"{rule.Name}###select", _selectedRule == index))
                Select(index);

            // These attach to the Selectable, so they must come right after it
            RuleDragSource(index, rule);
            RuleDropTarget(index);
            DrawRuleContextMenu(index, rule, ref deleteIndex, ref duplicateIndex);
        }

        private static void DrawRuleFooter()
        {
            var spacing = ImGui.GetStyle().ItemSpacing.X;
            var half = (ImGui.GetContentRegionAvail().X - spacing) * 0.5f;
            var rules = C.User_BuffRules;

            if (ImGui.Button("Add Rule", new Vector2(half, 0)))
            {
                // New rules start as a copy of the default, since a rule replaces the default entirely
                rules.Add(new Gather_Util.BuffRule
                {
                    Name = $"Rule {rules.Count + 1}",
                    Profile = CloneProfile(C.User_AutoProfile),
                });
                Select(rules.Count - 1);
                Changed();
            }

            ImGui.SameLine();
            if (ImGui.Button("New Folder", new Vector2(half, 0)))
            {
                var folders = C.User_BuffFolders;
                var nextId = folders.Count == 0 ? 1 : folders.Max(f => f.Id) + 1;

                folders.Add(new Gather_Util.BuffFolder { Id = nextId, Name = $"Folder {nextId}" });
                Changed();
            }

            var selected = _selectedRule >= 0 && _selectedRule < rules.Count ? rules[_selectedRule] : null;

            if (ImGui.Button(selected != null ? "Export" : "Export All", new Vector2(half, 0)))
            {
                if (selected != null)
                    ExportRules(new[] { selected }, $"Copied \"{selected.Name}\" to the clipboard.");
                else
                    ExportRules(rules, "Copied every rule to the clipboard.");
            }

            if (ImGui.IsItemHovered())
                ImGui.SetTooltip(selected != null ? $"Copy \"{selected.Name}\" to the clipboard" : "Copy every rule to the clipboard");

            ImGui.SameLine();
            if (ImGui.Button("Import", new Vector2(half, 0)))
                ImportRules();

            if (ImGui.IsItemHovered())
                ImGui.SetTooltip("Add the rules copied to the clipboard");
        }

        private static void Select(int index)
        {
            _selectedRule = index;
        }

        private static Gather_Util.ProfileClass CloneProfile(Gather_Util.ProfileClass source)
        {
            var json = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<Gather_Util.ProfileClass>(json) ?? new();
        }

        #endregion

        #region Menus And Popups

        private static void DrawRuleContextMenu(int index, Gather_Util.BuffRule rule, ref int deleteIndex, ref int duplicateIndex)
        {
            if (!ImGui.BeginPopupContextItem($"##rule_ctx_{index}"))
                return;

            if (ImGui.MenuItem("Rename"))
            {
                _renameRule = index;
                _renameFolder = -1;
                _renameBuffer = rule.Name;
                _openRenamePopup = true;
            }

            if (ImGui.MenuItem("Duplicate"))
                duplicateIndex = index;

            if (ImGui.MenuItem("Export"))
                ExportRules(new[] { rule }, $"Copied \"{rule.Name}\" to the clipboard.");

            if (ImGui.BeginMenu("Move to folder"))
            {
                if (ImGui.MenuItem("(None)", "", rule.FolderId == null))
                {
                    rule.FolderId = null;
                    Changed();
                }

                foreach (var folder in C.User_BuffFolders)
                {
                    if (!ImGui.MenuItem(folder.Name, "", rule.FolderId == folder.Id))
                        continue;

                    rule.FolderId = folder.Id;
                    Changed();
                }

                ImGui.EndMenu();
            }

            ImGui.Separator();

            if (ImGui.MenuItem("Delete"))
                deleteIndex = index;

            ImGui.EndPopup();
        }

        private static void DrawFolderContextMenu(Gather_Util.BuffFolder folder, ref int deleteFolder)
        {
            if (!ImGui.BeginPopupContextItem($"##bfolder_ctx_{folder.Id}"))
                return;

            if (ImGui.MenuItem("Rename"))
            {
                _renameFolder = folder.Id;
                _renameRule = -1;
                _renameBuffer = folder.Name;
                _openRenamePopup = true;
            }

            if (ImGui.MenuItem("Export folder"))
            {
                var inside = C.User_BuffRules.Where(r => r.FolderId == folder.Id);
                ExportRules(inside, $"Copied the rules in \"{folder.Name}\" to the clipboard.");
            }

            ImGui.Separator();

            if (ImGui.MenuItem("Delete (rules move to root)"))
                deleteFolder = folder.Id;

            ImGui.EndPopup();
        }

        private static void DrawRenamePopup()
        {
            if (!ImGui.BeginPopup("RenameEntry"))
                return;

            ImGui.SetNextItemWidth(180 * ImGuiHelpers.GlobalScale);

            // Start typing right away
            if (ImGui.IsWindowAppearing())
                ImGui.SetKeyboardFocusHere();

            var submitted = ImGui.InputText("##RenameInput", ref _renameBuffer, 64, ImGuiInputTextFlags.EnterReturnsTrue);

            if (submitted && !string.IsNullOrWhiteSpace(_renameBuffer))
            {
                var name = _renameBuffer.Trim();
                var rules = C.User_BuffRules;

                if (_renameFolder != -1)
                {
                    if (C.User_BuffFolders.FirstOrDefault(f => f.Id == _renameFolder) is { } folder)
                        folder.Name = name;
                }
                // _renameRule < Count reads "the index still points at a real rule"
                else if (_renameRule >= 0 && _renameRule < rules.Count)
                {
                    rules[_renameRule].Name = name;
                }

                Changed();
                ImGui.CloseCurrentPopup();
            }

            ImGui.EndPopup();
        }

        #endregion

        #region Drag And Drop

        private static void RuleDragSource(int index, Gather_Util.BuffRule rule)
        {
            if (!ImGui.BeginDragDropSource())
                return;

            _draggedRule = index;
            ImGui.SetDragDropPayload(RulePayload, BitConverter.GetBytes(index));
            ImGui.TextUnformatted(rule.Name);
            ImGui.EndDragDropSource();
        }

        private static void RuleDropTarget(int target)
        {
            var after = IsMouseInLowerHalf();

            if (!ImGui.BeginDragDropTarget())
                return;

            var payload = ImGui.AcceptDragDropPayload(RulePayload, LineFlags);

            // No line when hovering the rule you're dragging
            if (!payload.IsNull && _draggedRule != target)
            {
                DrawInsertionLine(after);

                if (payload.IsDelivery())
                    MoveRule(_draggedRule, target, after);
            }

            ImGui.EndDragDropTarget();
        }

        private static void FolderDragSource(Gather_Util.BuffFolder folder)
        {
            if (!ImGui.BeginDragDropSource())
                return;

            _draggedFolder = folder.Id;
            ImGui.SetDragDropPayload(FolderPayload, BitConverter.GetBytes(folder.Id));
            ImGui.TextUnformatted(folder.Name);
            ImGui.EndDragDropSource();
        }

        // Folders accept rules (move inside, default highlight) and other folders (reorder, line)
        private static void FolderDropTarget(Gather_Util.BuffFolder target)
        {
            var after = IsMouseInLowerHalf();

            if (!ImGui.BeginDragDropTarget())
                return;

            var rules = C.User_BuffRules;

            var rulePayload = ImGui.AcceptDragDropPayload(RulePayload);
            // _draggedRule < Count reads "the dragged index still points at a real rule"
            if (!rulePayload.IsNull && _draggedRule >= 0 && _draggedRule < rules.Count)
            {
                rules[_draggedRule].FolderId = target.Id;
                Changed();
            }

            var folderPayload = ImGui.AcceptDragDropPayload(FolderPayload, LineFlags);
            if (!folderPayload.IsNull && _draggedFolder != target.Id)
            {
                DrawInsertionLine(after);

                if (folderPayload.IsDelivery())
                    MoveFolder(_draggedFolder, target.Id, after);
            }

            ImGui.EndDragDropTarget();
        }

        // Empty space at the bottom of the list: drop a rule here to move it to the root
        private static void DrawRootDropZone()
        {
            var height = Math.Max(24 * ImGuiHelpers.GlobalScale, ImGui.GetContentRegionAvail().Y);
            ImGui.InvisibleButton("##RuleRootDrop", new Vector2(-1, height));

            if (!ImGui.BeginDragDropTarget())
                return;

            var rules = C.User_BuffRules;
            var payload = ImGui.AcceptDragDropPayload(RulePayload);

            if (!payload.IsNull && _draggedRule >= 0 && _draggedRule < rules.Count)
            {
                rules[_draggedRule].FolderId = null;
                Changed();
            }

            ImGui.EndDragDropTarget();
        }

        /// <summary>Moves a rule next to another one and into that rule's folder. The selection follows the rule, not the index.</summary>
        private static void MoveRule(int from, int target, bool after)
        {
            var rules = C.User_BuffRules;

            // from >= Count reads "the dragged index is stale"
            if (from < 0 || from >= rules.Count || target < 0 || target >= rules.Count || from == target)
                return;

            var selected = _selectedRule >= 0 && _selectedRule < rules.Count ? rules[_selectedRule] : null;
            var moving = rules[from];
            var targetRule = rules[target];

            rules.RemoveAt(from);

            // Look the target up again, since removing the dragged rule shifted the indices
            var targetIndex = rules.IndexOf(targetRule);
            rules.Insert(after ? targetIndex + 1 : targetIndex, moving);
            moving.FolderId = targetRule.FolderId;

            _selectedRule = selected is null ? -1 : rules.IndexOf(selected);
            Changed();
        }

        private static void MoveFolder(int fromId, int targetId, bool after)
        {
            var folders = C.User_BuffFolders;
            var moving = folders.FirstOrDefault(f => f.Id == fromId);
            var target = folders.FirstOrDefault(f => f.Id == targetId);

            if (moving is null || target is null || moving == target)
                return;

            folders.Remove(moving);

            var targetIndex = folders.IndexOf(target);
            folders.Insert(after ? targetIndex + 1 : targetIndex, moving);
            Changed();
        }

        // Top edge for "before", bottom edge for "after", offset by half the item spacing so the gap lines up
        private static void DrawInsertionLine(bool after)
        {
            var min = ImGui.GetItemRectMin();
            var max = ImGui.GetItemRectMax();
            var halfGap = ImGui.GetStyle().ItemSpacing.Y * 0.5f;
            var y = after ? max.Y + halfGap : min.Y - halfGap;

            ImGui.GetWindowDrawList().AddLine(
                new Vector2(min.X, y),
                new Vector2(max.X, y),
                ImGui.GetColorU32(ImGuiCol.DragDropTarget),
                2f * ImGuiHelpers.GlobalScale);
        }

        // Screen Y grows downward, so "mouse Y > midpoint" = the mouse is in the bottom half = drop AFTER the item
        private static bool IsMouseInLowerHalf()
        {
            var min = ImGui.GetItemRectMin();
            var max = ImGui.GetItemRectMax();
            return ImGui.GetMousePos().Y > (min.Y + max.Y) * 0.5f;
        }

        #endregion

        #region Import / Export

        private static void ExportRules(IEnumerable<Gather_Util.BuffRule> rules, string message)
        {
            var list = rules.ToList();
            if (list.Count == 0)
            {
                Notify.Error("Nothing to export yet.");
                return;
            }

            var json = JsonConvert.SerializeObject(list);
            ImGui.SetClipboardText(ExportPrefix + Convert.ToBase64String(Encoding.UTF8.GetBytes(json)));
            Notify.Success(message);
        }

        private static void ImportRules()
        {
            var text = ImGui.GetClipboardText()?.Trim();

            if (string.IsNullOrEmpty(text) || !text.StartsWith(ExportPrefix))
            {
                Notify.Error("The clipboard doesn't contain exported rules.");
                return;
            }

            try
            {
                var json = Encoding.UTF8.GetString(Convert.FromBase64String(text[ExportPrefix.Length..]));
                var imported = JsonConvert.DeserializeObject<List<Gather_Util.BuffRule>>(json);

                if (imported is null || imported.Count == 0)
                {
                    Notify.Error("No rules found in the clipboard.");
                    return;
                }

                foreach (var rule in imported)
                {
                    rule.Profile.Migrate();

                    // Folder IDs from someone else's config would point at nothing here
                    rule.FolderId = null;
                    C.User_BuffRules.Add(rule);
                }

                Select(C.User_BuffRules.Count - 1);
                Changed();
                Notify.Success($"Imported {imported.Count} rule(s).");
            }
            catch (Exception)
            {
                Notify.Error("Couldn't read the clipboard. It may be incomplete or from a different version.");
            }
        }

        #endregion

        #region Editor

        private static void DrawEditor()
        {
            var rules = C.User_BuffRules;

            // Selected rule was removed somewhere else, fall back to the default
            // _selectedRule >= Count reads "index is past the end of the list"
            if (_selectedRule >= rules.Count)
                Select(-1);

            if (_selectedRule == -1)
            {
                ImGui.TextUnformatted("Default profile: used whenever no rule matches.");
                ImGui.Separator();
                DrawResetButton(C.User_AutoProfile);
                DrawProfile(C.User_AutoProfile);
                return;
            }

            var rule = rules[_selectedRule];

            var name = rule.Name;
            ImGui.SetNextItemWidth(250 * ImGuiHelpers.GlobalScale);
            if (ImGui.InputText("Rule name", ref name, 64))
            {
                rule.Name = name;
                Changed();
            }

            ImGui.Separator();
            DrawConditions(rule);
            ImGui.Separator();
            DrawResetButton(rule.Profile);
            DrawProfile(rule.Profile);
        }

        private static void DrawConditions(Gather_Util.BuffRule rule)
        {
            ImGui.TextUnformatted("Conditions (all must match)");

            var scale = ImGuiHelpers.GlobalScale;
            var preview = rule.Kind?.ToString() ?? "Any";

            ImGui.SetNextItemWidth(150 * scale);
            using (var combo = ImRaii.Combo("Node kind", preview))
            {
                if (combo.Success)
                {
                    if (ImGui.Selectable("Any", rule.Kind is null))
                    {
                        rule.Kind = null;
                        Changed();
                    }

                    foreach (var kind in NodeKinds)
                    {
                        if (!ImGui.Selectable(kind.ToString(), rule.Kind == kind))
                            continue;

                        rule.Kind = kind;
                        Changed();
                    }
                }
            }

            rule.MinIntegrity = DrawOptionalInt("At least (max integrity)", "min", rule.MinIntegrity, 4);
            rule.MaxIntegrity = DrawOptionalInt("At most (max integrity)", "max", rule.MaxIntegrity, 6);

            // lo > hi reads "the minimum is ABOVE the maximum", which no node can satisfy
            if (rule.MinIntegrity is { } lo && rule.MaxIntegrity is { } hi && lo > hi)
                ImGuiEx.Text("Minimum is above the maximum, so this rule can never match.");
        }

        /// <summary>A checkbox that turns an optional number on/off, plus the number itself. Returns the new value.</summary>
        private static int? DrawOptionalInt(string label, string id, int? value, int defaultValue)
        {
            ImGui.PushID(id);

            var isSet = value.HasValue;
            if (ImGui.Checkbox("##set", ref isSet))
            {
                value = isSet ? defaultValue : null;
                Changed();
            }

            ImGui.SameLine();

            if (value is { } current)
            {
                var edited = current;
                ImGui.SetNextItemWidth(100 * ImGuiHelpers.GlobalScale);
                if (ImGui.InputInt(label, ref edited, 1))
                {
                    value = Math.Clamp(edited, 1, 99);
                    Changed();
                }
            }
            else
            {
                ImGui.TextUnformatted($"{label}: not set");
            }

            ImGui.PopID();
            return value;
        }

        private static void DrawResetButton(Gather_Util.ProfileClass profile)
        {
            if (!ImGui.Button("Reset buffs to recommended"))
                return;

            // Only the per-buff settings reset. BY II amount and the Tidings threshold stay as the user set them.
            profile.Buffs = Gather_Util.ProfileClass.CreateDefaultBuffs();
            Changed();
        }

        private static void DrawProfile(Gather_Util.ProfileClass profile)
        {
            using var table = ImRaii.Table("Table: Buff Profile", 3, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg);
            if (!table.Success)
                return;

            ImGui.TableSetupColumn("");
            ImGui.TableSetupColumn("Buff");
            ImGui.TableSetupColumn("Setting");
            ImGui.TableHeadersRow();

            var scale = ImGuiHelpers.GlobalScale;

            foreach (var buff in DisplayBuffs)
            {
                if (!profile.Buffs.TryGetValue(buff, out var settings))
                {
                    settings = new Gather_Util.BuffSettings();
                    profile.Buffs[buff] = settings;
                }

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.PushID($"{buff}");

                var enabled = settings.Enabled;
                if (ImGui.Checkbox("##state", ref enabled))
                {
                    settings.Enabled = enabled;
                    Changed();
                }

                ImGui.TableNextColumn();
                DrawBuffLabel(buff, enabled);

                ImGui.TableNextColumn();

                var minIntegrity = (int)settings.MinIntegrity;
                ImGui.SetNextItemWidth(100 * scale);
                if (ImGui.InputInt("Min integrity (0 = any)", ref minIntegrity, 1))
                {
                    settings.MinIntegrity = (uint)Math.Clamp(minIntegrity, 0, 99);
                    Changed();
                }

                if (buff == GatherBuffId.Tidings)
                {
                    var minBoon = profile.TidingsMinBoon;
                    ImGui.SetNextItemWidth(100 * scale);
                    if (ImGui.InputInt("Minimum boon %", ref minBoon, 1, 10))
                    {
                        profile.TidingsMinBoon = Math.Clamp(minBoon, 0, 100);
                        Changed();
                    }
                }

                if (buff == GatherBuffId.BYII)
                {
                    var amount = profile.BYII_Amount;
                    ImGui.SetNextItemWidth(100 * scale);
                    if (ImGui.InputInt("Bountiful amount", ref amount, 1))
                    {
                        profile.BYII_Amount = Math.Clamp(amount, 1, 3);
                        Changed();
                    }
                }

                // Only the repeatable buffs have a cast cap, everything else is a one-shot
                if (buff is GatherBuffId.BYII or GatherBuffId.BonusIntegrity)
                {
                    var maxUse = (int)settings.MaxUse;
                    ImGui.SetNextItemWidth(100 * scale);
                    if (ImGui.InputInt("Max casts (0 = no cap)", ref maxUse, 1))
                    {
                        settings.MaxUse = (uint)Math.Max(maxUse, 0);
                        Changed();
                    }
                }

                ImGui.PopID();
            }
        }

        private static void DrawBuffLabel(GatherBuffId buff, bool enabled)
        {
            var hasInfo = Gather_Util.GathActionDict.TryGetValue(buff, out var info);
            if (!hasInfo || !info.ClassAction.TryGetValue(_selectedJob, out var classInfo))
            {
                ImGui.TextUnformatted($"{buff}");
                return;
            }

            ImGui_Ice.ImageButtonWithText(classInfo.IconId, classInfo.Name, classInfo.Name, enabled, 1);
        }

        #endregion

        /// <summary>Any edit to a profile or rule: save, and refresh the calculator.</summary>
        private static void Changed()
        {
            C.SaveDebounced();
            CalculatorWindow.MarkDirty();
        }
    }
}