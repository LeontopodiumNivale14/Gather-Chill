using Dalamud.Interface.ImGuiFileDialog;
using Dalamud.Interface.Textures;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Utility.Raii;
using ECommons.ExcelServices;
using GatherChill.Enums;
using GatherChill.Gui;
using GatherChill.Ui.Tabs_MainWindow;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Replay;
using GatherChill.Utilities.Tools;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace GatherChill.Ui
{
    /// <summary>
    /// The replay browser itself. Drawn by the standalone window and by the Replays tab in the logs window,
    /// each with its own instance, so their selections don't affect each other.
    /// </summary>
    internal class ReplayViewer
    {
        private sealed class Entry
        {
            public string Path = "";
            public ReplaySession Session = null!;
            public bool External;   // opened from outside the Replays folder, never deleted or pruned
        }

        private readonly FileDialogManager _dialog = new();

        private readonly List<Entry> _saved = new();
        private readonly List<Entry> _external = new();

        private Entry? _selected;

        // -1 = events that happen between nodes (cordials, desynth, notes), otherwise an index into the session's nodes
        private int _nodeIndex;

        private bool _showDecisions = true;

        #region Draw

        public void Draw()
        {
            _dialog.Draw();

            using var colors = ImGui_Ice.PushChildBG();

            DrawToolbar();
            ImGui.Separator();

            var scale = ImGuiHelpers.GlobalScale;
            var avail = ImGui.GetContentRegionAvail();

            using (var left = ImRaii.Child("##ReplayList", new Vector2(260 * scale, avail.Y), true))
            {
                if (left.Success)
                    DrawList();
            }

            ImGui.SameLine();

            using (var right = ImRaii.Child("##ReplayDetail", ImGui.GetContentRegionAvail(), true))
            {
                if (!right.Success)
                    return;

                if (_selected is null)
                {
                    ImGui.TextDisabled("Pick a replay on the left, or open a file.");
                    return;
                }

                DrawSession(_selected.Session);
            }
        }

        private void DrawToolbar()
        {
            var scale = ImGuiHelpers.GlobalScale;

            if (ImGui.Button("Open file..."))
            {
                _dialog.OpenFileDialog("Open replay", $"Replay{{{ReplayStore.Extension}}},.*", (ok, path) =>
                {
                    if (ok && !string.IsNullOrEmpty(path))
                        OpenExternal(path);
                });
            }

            ImGui.SameLine();
            if (ImGui.Button("Open folder"))
                OpenFolder();

            ImGui.SameLine();
            if (ImGui.Button("Refresh"))
                Refresh();

            ImGui.SameLine();
            ImGui.TextDisabled("|");
            ImGui.SameLine();

            var recording = C.Replay_Enabled;
            if (ImGui.Checkbox("Record replays", ref recording))
            {
                C.Replay_Enabled = recording;
                C.SaveDebounced();
            }

            ImGui.SameLine();
            var keep = C.Replay_KeepAmount;
            ImGui.SetNextItemWidth(90 * scale);
            if (ImGui.InputInt("Keep last", ref keep, 1, 5))
            {
                C.Replay_KeepAmount = Math.Clamp(keep, 1, 200);
                C.SaveDebounced();
            }

            if (ImGui.IsItemHovered())
                ImGui.SetTooltip("Older replays are deleted the next time a session is saved.");
        }

        #endregion

        #region List

        private void DrawList()
        {
            // Edits are applied after drawing so the lists never change while they're being iterated
            Entry? remove = null;

            if (_saved.Count == 0)
                ImGui.TextDisabled("No saved replays yet.");

            foreach (var entry in _saved)
                DrawEntry(entry, ref remove);

            if (_external.Count > 0)
            {
                ImGui.Separator();
                ImGui.TextDisabled("Opened files");

                foreach (var entry in _external)
                    DrawEntry(entry, ref remove);
            }

            if (remove is null)
                return;

            if (remove.External)
            {
                _external.Remove(remove);
            }
            else
            {
                try
                {
                    File.Delete(remove.Path);
                }
                catch (Exception ex)
                {
                    Notify.Error($"Couldn't delete the replay: {ex.Message}");
                }

                Refresh();
            }

            if (_selected == remove)
                _selected = null;
        }

        private void DrawEntry(Entry entry, ref Entry? remove)
        {
            var session = entry.Session;
            using var id = ImRaii.PushId(entry.Path);

            var label = $"{session.StartedUtc.ToLocalTime():MMM d  HH:mm}  ({session.Nodes.Count} nodes)";
            if (ImGui.Selectable(label, _selected == entry))
                Select(entry);

            if (ImGui.IsItemHovered())
            {
                var items = session.Nodes.Sum(n => n.ItemsAfter - n.ItemsBefore);
                var length = session.EndedUtc - session.StartedUtc;
                ImGui.SetTooltip($"{Path.GetFileName(entry.Path)}\nLength: {length:hh\\:mm\\:ss}\nItems gathered: {items}\nEnded: {session.EndReason}");
            }

            if (!ImGui.BeginPopupContextItem("##ctx"))
                return;

            if (ImGui.MenuItem("Open folder"))
                OpenFolder(entry.External ? Path.GetDirectoryName(entry.Path) : null);

            if (ImGui.MenuItem(entry.External ? "Close" : "Delete"))
                remove = entry;

            ImGui.EndPopup();
        }

        private void Select(Entry entry)
        {
            _selected = entry;
            _nodeIndex = entry.Session.Nodes.Count > 0 ? 0 : -1;
        }

        /// <summary>Reloads the saved replays from disk. Opened files are left alone.</summary>
        public void Refresh()
        {
            var selectedPath = _selected is { External: false } ? _selected.Path : null;

            _saved.Clear();

            foreach (var file in ReplayStore.List())
            {
                var session = ReplayStore.Load(file.FullName);
                if (session != null)
                    _saved.Add(new Entry { Path = file.FullName, Session = session });
            }

            if (selectedPath != null)
                _selected = _saved.FirstOrDefault(e => e.Path == selectedPath);
        }

        private void OpenExternal(string path)
        {
            var session = ReplayStore.Load(path);
            if (session is null)
            {
                Notify.Error("Couldn't read that replay. The file may be damaged or from a newer version.");
                return;
            }

            _external.RemoveAll(e => e.Path == path);

            var entry = new Entry { Path = path, Session = session, External = true };
            _external.Add(entry);
            Select(entry);
        }

        private static void OpenFolder(string? folder = null)
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = folder ?? ReplayStore.Folder, UseShellExecute = true });
            }
            catch (Exception ex)
            {
                Notify.Error($"Couldn't open the folder: {ex.Message}");
            }
        }

        #endregion

        #region Session and node

        private void DrawSession(ReplaySession session)
        {
            ImGui.TextUnformatted($"{session.Name}  |  {session.Nodes.Count} nodes  |  ended: {session.EndReason}");

            if (session.Nodes.Count == 0 && session.Events.Count == 0)
            {
                ImGui.TextDisabled("This replay has no recorded nodes.");
                return;
            }

            DrawNodePicker(session);
            ImGui.Separator();

            if (_nodeIndex < 0 || _nodeIndex >= session.Nodes.Count)
            {
                ImGui.TextDisabled("Events between nodes. Times are from the start of the session.");
                DrawEvents(session.Events, FallbackJob);
                return;
            }

            DrawNode(session.Nodes[_nodeIndex]);
        }

        private void DrawNodePicker(ReplaySession session)
        {
            var first = session.Events.Count > 0 ? -1 : 0;
            var last = session.Nodes.Count - 1;

            _nodeIndex = Math.Clamp(_nodeIndex, first, Math.Max(last, first));

            if (ImGui.ArrowButton("##prevNode", ImGuiDir.Left))
                _nodeIndex = Math.Max(first, _nodeIndex - 1);

            ImGui.SameLine();

            var preview = _nodeIndex < 0 ? $"Between nodes ({session.Events.Count})" : NodeLabel(session.Nodes[_nodeIndex], _nodeIndex);

            ImGui.SetNextItemWidth(420 * ImGuiHelpers.GlobalScale);
            using (var combo = ImRaii.Combo("##nodePicker", preview))
            {
                if (combo.Success)
                {
                    if (session.Events.Count > 0 && ImGui.Selectable($"Between nodes ({session.Events.Count})", _nodeIndex == -1))
                        _nodeIndex = -1;

                    for (var i = 0; i < session.Nodes.Count; i++)
                    {
                        if (ImGui.Selectable($"{NodeLabel(session.Nodes[i], i)}##node{i}", _nodeIndex == i))
                            _nodeIndex = i;
                    }
                }
            }

            ImGui.SameLine();

            if (ImGui.ArrowButton("##nextNode", ImGuiDir.Right))
                _nodeIndex = Math.Min(last, _nodeIndex + 1);
        }

        private static string NodeLabel(ReplayNode node, int index)
        {
            var gained = node.ItemsAfter - node.ItemsBefore;
            return $"#{index + 1}  {node.ItemName}  |  +{gained}  |  {node.DurationMs / 1000.0:F1}s";
        }

        private void DrawNode(ReplayNode node)
        {
            var gained = node.ItemsAfter - node.ItemsBefore;
            var job = JobFor(node);

            ImGui.TextUnformatted($"{node.ItemName}  |  {node.Kind}{(node.IsCollectable ? " (collectable)" : "")}  |  {node.DurationMs / 1000.0:F1}s");
            ImGui.TextUnformatted($"Items {node.ItemsBefore} -> {node.ItemsAfter} (+{gained})  |  Ended: {node.EndReason}");

            if (node.Plans.Count > 0)
            {
                // Re-plans on the same node are listed below, but the headline is the first plan, the one made when the window opened
                var expected = node.Plans[0].ExpectedYield;
                ImGui.TextUnformatted($"Expected {expected:F1}  vs  actual {gained}" + (node.Plans.Count > 1 ? $"  ({node.Plans.Count} plans on this node)" : ""));

                ImGui.Separator();
                DrawPlans(node, job);
            }

            ImGui.Separator();
            DrawNodeItems(node);

            ImGui.Separator();

            var show = _showDecisions;
            if (ImGui.Checkbox("Show skipped-buff decisions", ref show))
                _showDecisions = show;

            DrawEvents(node.Events, job);
        }

        #endregion

        #region Job and icons

        // Backup in case the job wasn't saved in the replay (I didn't think bout adding specific jobs prior to this, but good backup)
        private static Job FallbackJob => GatherBuffs_Tab._selectedJob;

        private static Job JobFor(ReplayNode node)
            => node.Job is Job.MIN or Job.BTN ? node.Job : FallbackJob;

        // The Action icon for the event, helps not just gloss over shit. Returns null if has no actions (plans, notes, desynth)
        private static uint? IconFor(ReplayEvent ev, Job job)
        {
            if (ev.Kind == ReplayEventKind.Swing)
                return job is Job.MIN or Job.BTN ? (uint)Gather_Util.Job_IconIds[job].IconId : null;

            if (ev.Kind is not (ReplayEventKind.BuffCast or ReplayEventKind.Decision))
                return null;

            return ActionIcon(ev.Label, job);
        }

        private static uint? ActionIcon(string label, Job job)
        {
            if (!Enum.TryParse<GatherBuffId>(label, out var buff))
                return null;

            if (!Gather_Util.GathActionDict.TryGetValue(buff, out var info) || !info.ClassAction.TryGetValue(job, out var classInfo))
                return null;

            return (uint)classInfo.IconId;
        }

        #endregion

        #region Plans

        private static void DrawPlans(ReplayNode node, Job job)
        {
            for (var i = 0; i < node.Plans.Count; i++)
            {
                var plan = node.Plans[i];
                var flags = i == 0 ? ImGuiTreeNodeFlags.DefaultOpen : ImGuiTreeNodeFlags.None;

                using var tree = ImRaii.TreeNode($"Plan {i + 1}: {plan.ProfileName}  |  expected {plan.ExpectedYield:F1} / need {plan.Need}###plan{i}", flags);
                if (!tree.Success)
                    continue;

                ImGui.TextUnformatted($"Level {plan.Level} | GP {plan.Gp} | Integrity {plan.Attempts}/{plan.MaxIntegrity} | Gather {plan.GatherChance}% | Boon {plan.BoonChance}% | Base yield {plan.BaseYield} | Need {plan.Need}");
                ImGui.TextUnformatted($"Planned GP {plan.PlannedGp} | Meets need: {plan.MeetsNeed}");
                ImGui.TextUnformatted("Buffs already up: " + (plan.ActiveBuffs.Count == 0 ? "none" : string.Join(", ", plan.ActiveBuffs)));

                // 0 = the Bountiful Yield bonus was never measured on this plan
                if (plan.BYIIObserved > 0)
                {
                    var matches = plan.BYIIPlanned == plan.BYIIObserved;
                    var color = matches ? new Vector4(0.4f, 0.9f, 0.4f, 1f) : new Vector4(1f, 0.8f, 0.3f, 1f);
                    var text = matches
                        ? $"Bountiful Yield: game gave +{plan.BYIIObserved}, matches the plan"
                        : $"Bountiful Yield: game gave +{plan.BYIIObserved}, but the plan assumed +{plan.BYIIPlanned}";

                    using (ImRaii.PushColor(ImGuiCol.Text, color))
                        ImGui.TextUnformatted(text);
                }

                ImGui.TextUnformatted("Rules:");
                if (plan.RuleReasons.Count == 0)
                    ImGui.TextDisabled("  none defined");

                foreach (var reason in plan.RuleReasons)
                    ImGui.BulletText(reason);

                ImGui.TextUnformatted("Cast order:");
                if (plan.Buffs.Count == 0)
                    ImGui.TextDisabled("  none");

                foreach (var buff in plan.Buffs)
                    DrawBuffEntry(buff, job);

                if (ImGui.Button($"Open in calculator##plan{i}"))
                    CalculatorWindow.LoadSnapshot(plan, node.ItemName);
            }
        }

        // Stored as a string to make sure shit doesn't get corrupted, shown as action name + how many times it's expected to resolve
        private static void DrawBuffEntry(string entry, Job job)
        {
            using var id = ImRaii.PushId($"buff_{entry}");

            var split = entry.IndexOf(' ');
            var name = split < 0 ? entry : entry[..split];
            var suffix = split < 0 ? "" : entry[split..];

            if (Enum.TryParse<GatherBuffId>(name, out var buff)
                && Gather_Util.GathActionDict.TryGetValue(buff, out var info)
                && info.ClassAction.TryGetValue(job, out var classInfo))
            {
                ImGui_Ice.ImageButtonWithText(classInfo.IconId, $"{classInfo.Name}{suffix}", $"replay_{entry}", true, 1);
                return;
            }

            ImGui.TextUnformatted(entry);
        }

        #endregion

        #region Timeline

        private void DrawEvents(List<ReplayEvent> events, Job job)
        {
            var scale = ImGuiHelpers.GlobalScale;

            var flags = ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.ScrollY;
            var size = new Vector2(0, Math.Max(ImGui.GetContentRegionAvail().Y, 150 * scale));

            using var table = ImRaii.Table("Table: Replay Events", 6, flags, size);
            if (!table.Success)
                return;

            ImGui.TableSetupScrollFreeze(0, 1);
            ImGui.TableSetupColumn("Time", ImGuiTableColumnFlags.WidthFixed, 60 * scale);
            ImGui.TableSetupColumn("Event", ImGuiTableColumnFlags.WidthFixed, 210 * scale);
            ImGui.TableSetupColumn("GP", ImGuiTableColumnFlags.WidthFixed, 50 * scale);
            ImGui.TableSetupColumn("Integrity", ImGuiTableColumnFlags.WidthFixed, 65 * scale);
            ImGui.TableSetupColumn("Items", ImGuiTableColumnFlags.WidthFixed, 110 * scale);
            ImGui.TableSetupColumn("Detail", ImGuiTableColumnFlags.WidthStretch);
            ImGui.TableHeadersRow();

            var rowId = 0;

            foreach (var ev in events)
            {
                rowId++;
                var isDecision = ev.Kind == ReplayEventKind.Decision;
                if (isDecision && !_showDecisions)
                    continue;

                // Skipped-buff decisions are the noisy rows, so they're dimmed
                using var dim = ImRaii.PushColor(ImGuiCol.Text, ImGui.GetColorU32(ImGuiCol.TextDisabled), isDecision);

                ImGui.TableNextRow();

                ImGui.TableNextColumn();
                ImGui.AlignTextToFramePadding();
                ImGui.TextUnformatted($"{ev.Ms / 1000.0:F1}s");

                ImGui.TableNextColumn();
                DrawEventName(ev, job);

                ImGui.TableNextColumn();
                ImGui.AlignTextToFramePadding();
                ImGui.TextUnformatted($"{ev.Gp}");

                ImGui.TableNextColumn();
                ImGui.AlignTextToFramePadding();
                ImGui.TextUnformatted(ev.MaxIntegrity > 0 ? $"{ev.Integrity}/{ev.MaxIntegrity}" : "");

                ImGui.TableNextColumn();
                ImGui.AlignTextToFramePadding();
                ImGui.TextUnformatted(ItemsText(ev));

                ImGui.TableNextColumn();
                ImGui.AlignTextToFramePadding();
                ImGui.TextWrapped(ev.Detail);

                if (ev.Items is { Count: > 0 } windowItems)
                    DrawWindowItems(windowItems, rowId);
            }
        }

        // Everything the gathering window offered on this node: how it started, what changed (a hidden item showing up), and how it ended
        private static void DrawNodeItems(ReplayNode node)
        {
            var snapshots = node.Events.Where(e => e.Items is { Count: > 0 }).ToList();
            if (snapshots.Count == 0)
            {
                ImGui.TextDisabled("No window item data in this node (replays recorded before this was added don't have it, and collectables don't record it).");
                return;
            }

            using var tree = ImRaii.TreeNode($"Items in the window ({snapshots.Count} snapshot{(snapshots.Count == 1 ? "" : "s")})###nodeItems", ImGuiTreeNodeFlags.DefaultOpen);
            if (!tree.Success)
                return;

            for (var i = 0; i < snapshots.Count; i++)
            {
                var snapshot = snapshots[i];
                ImGui.TextDisabled($"{snapshot.Ms / 1000.0:F1}s  {(i == 0 ? "window opened" : snapshot.Detail)}");

                // The first snapshot and the last one are drawn in full; the ones in between are only a line each
                if (i == 0 || i == snapshots.Count - 1)
                    DrawItemRows(snapshot.Items!, $"snap{i}");
            }
        }

        // What the gathering window was offering. Hidden items are highlighted, since those are the ones that appear after a Hidden Finder cast.
        private static void DrawWindowItems(List<ReplayItemSlot> items, int rowId)
        {
            using var id = ImRaii.PushId($"windowItems{rowId}");

            using var tree = ImRaii.TreeNode("Item details");
            if (!tree.Success)
                return;

            DrawItemRows(items, $"row{rowId}");
        }

        private static void DrawItemRows(List<ReplayItemSlot> items, string idPrefix)
        {
            var hiddenColor = new Vector4(1f, 0.8f, 0.3f, 1f);

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                using var id = ImRaii.PushId($"{idPrefix}_{i}");

                var tags = "";
                if (item.Hidden)
                    tags += "  [hidden]";
                if (item.Rare)
                    tags += "  [rare]";
                if (item.Collectable)
                    tags += "  [collectable]";
                if (!item.Enabled)
                    tags += "  [unavailable]";

                // The icon comes from the item sheet, so replays don't have to store it
                if (ItemIcon(item.ItemId) is var icon && icon != 0)
                {
                    if (GameIcons.Draw(icon, MathF.Round(ImGui.GetFrameHeight()), grey: false))
                        ImGui.SameLine();
                }

                ImGui.AlignTextToFramePadding();
                using var color = ImRaii.PushColor(ImGuiCol.Text, hiddenColor, item.Hidden);
                ImGui.TextUnformatted($"{item.Name}  |  gather {item.GatherChance}%  |  boon {item.BoonChance}%  |  +{item.Amount}/swing{tags}");
            }
        }

        private static uint ItemIcon(uint itemId)
            => (uint)(ExcelHelper.Sheet_Item.GetRowOrDefault(itemId)?.Icon ?? 0);

        private static void DrawEventName(ReplayEvent ev, Job job)
        {
            if (IconFor(ev, job) is { } icon)
            {
                if (GameIcons.Draw(icon, MathF.Round(ImGui.GetFrameHeight()), grey: ev.Kind == ReplayEventKind.Decision))
                    ImGui.SameLine();
            }

            ImGui.AlignTextToFramePadding();
            ImGui.TextUnformatted(EventName(ev, job));
        }

        private static string EventName(ReplayEvent ev, Job job) => ev.Kind switch
        {
            ReplayEventKind.Plan => $"Plan: {ev.Label}",
            ReplayEventKind.Decision => FriendlyName(ev.Label, job),
            ReplayEventKind.BuffCast => $"Cast {FriendlyName(ev.Label, job)}",
            ReplayEventKind.Swing => "Swing",
            ReplayEventKind.Desynth => $"Desynth {ev.Label}",
            _ => ev.Label,
        };

        private static string ItemsText(ReplayEvent ev)
        {
            if (ev.Kind == ReplayEventKind.Swing)
                return $"+{ev.ItemDelta} (inv {ev.InventoryCount})";

            if (ev.Progression is { } progression)
                return $"Prog {progression}";

            return "";
        }

        private static string FriendlyName(string label, Job job)
        {
            if (Enum.TryParse<GatherBuffId>(label, out var buff)
                && Gather_Util.GathActionDict.TryGetValue(buff, out var info)
                && info.ClassAction.TryGetValue(job, out var classInfo))
            {
                return classInfo.Name;
            }

            return label;
        }

        #endregion
    }
}