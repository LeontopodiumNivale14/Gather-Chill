using Dalamud.Interface.Utility;
using Dalamud.Interface.Utility.Raii;
using Dalamud.Interface.Windowing;
using GatherChill.Enums;
using GatherChill.Gui;
using GatherChill.Ui.Tabs_MainWindow;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Replay;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui
{
    internal class CalculatorWindow : Window
    {
        public static CalculatorWindow? Instance { get; private set; }

        // -1 = the default (fallback) profile, otherwise an index into C.User_BuffRules
        private int _ruleIndex = -1;

        // Loaded from a replay: the profile exactly as it was when that node was planned
        private Gather_Util.ProfileClass? _snapshot;
        private string _snapshotName = "";
        private bool _useSnapshot;

        private int _level = 100;
        private int _gp = 800;
        private int _integrity = 6;
        private int _gatherChance = 100;
        private int _boonChance = 70;
        private int _baseYield = 1;
        private int _need = 40;

        private bool _dirty = true;
        private (Gather_Util.GatherProfile Profile, double ExpectedYield, int Gp, bool MeetsNeed)? _estimate;

        public CalculatorWindow() : base("Buff Calculator###GatherChill_BuffCalculator")
        {
            Instance = this;
            P.windowSystem.AddWindow(this);

            SizeConstraints = new WindowSizeConstraints
            {
                MinimumSize = new(380, 320),
                MaximumSize = new(float.MaxValue, float.MaxValue),
            };
        }

        /// <summary>Call from anywhere that edits a profile or rule so the estimate reruns.</summary>
        public static void MarkDirty()
        {
            if (Instance is not null)
                Instance._dirty = true;
        }

        /// <summary>Opens the window pointed at a rule (-1 = default profile).</summary>
        public static void OpenFor(int ruleIndex)
        {
            if (Instance is null)
                return;

            Instance._ruleIndex = ruleIndex;
            Instance._useSnapshot = false;
            Instance._dirty = true;
            Instance.IsOpen = true;
        }

        /// <summary>Fills the inputs and the profile from a recorded plan. The calculator uses max integrity for both integrity values.</summary>
        public static void LoadSnapshot(ReplayPlan plan, string itemName)
        {
            if (Instance is not { } window)
                return;

            var profile = JsonConvert.DeserializeObject<Gather_Util.ProfileClass>(plan.ProfileJson) ?? new Gather_Util.ProfileClass();
            profile.Migrate();

            window._snapshot = profile;
            window._snapshotName = $"{itemName}, {plan.ProfileName}";
            window._useSnapshot = true;

            window._level = Math.Clamp(plan.Level, 1, 100);
            window._gp = Math.Clamp(plan.Gp, 0, 2000);
            window._integrity = Math.Clamp(plan.MaxIntegrity, 1, 20);
            window._gatherChance = Math.Clamp(plan.GatherChance, 0, 100);
            window._boonChance = Math.Clamp(plan.BoonChance, 0, 100);
            window._baseYield = Math.Clamp(plan.BaseYield, 1, 10);
            window._need = Math.Clamp(plan.Need, 1, 999);

            window._dirty = true;
            window.IsOpen = true;
        }

        public override void Draw()
        {
            var rules = C.User_BuffRules;

            // _ruleIndex >= Count reads "index is past the end of the list", so the rule was deleted
            if (_ruleIndex >= rules.Count)
            {
                _ruleIndex = -1;
                _dirty = true;
            }

            DrawProfilePicker();

            var profile = CurrentProfile();
            var scale = ImGuiHelpers.GlobalScale;
            var width = 100 * scale;

            ImGui.Separator();
            DrawInt("Level", ref _level, 1, 100, width);
            DrawInt("GP when the node opens", ref _gp, 0, 2000, width);
            DrawInt("Max integrity", ref _integrity, 1, 20, width);
            DrawInt("Gather chance %", ref _gatherChance, 0, 100, width);
            DrawInt("Boon chance %", ref _boonChance, 0, 100, width);
            DrawInt("Items per hit (before buffs)", ref _baseYield, 1, 10, width);
            DrawInt("Items needed", ref _need, 1, 999, width);

            if (_dirty)
            {
                _estimate = Gather_Util.PlanBuffs(_level, _gp, _integrity, _integrity, _gatherChance, _boonChance, _need, profile, _baseYield);
                _dirty = false;
            }

            if (_estimate is not { } result)
                return;

            ImGui.Separator();
            ImGui.TextUnformatted($"Expected items: {result.ExpectedYield:F1} / {_need}");
            ImGui.TextUnformatted($"GP spent: {result.Gp}");
            ImGui.TextUnformatted(result.MeetsNeed ? "Meets the need (on average)" : "Falls short, this is the best the profile can do");

            if (result.Profile.BuffPriority.Count == 0)
            {
                ImGui.TextUnformatted("Buffs: none needed");
            }
            else
            {
                ImGui.TextUnformatted("Cast order:");
                foreach (var buff in result.Profile.BuffPriority)
                    DrawCastEntry(buff, result.Profile.MaxUses.GetValueOrDefault(buff));
            }

            ImGui.Separator();
            if (ImGui.Button("Copy report to clipboard"))
                ImGui.SetClipboardText(BuildReport(profile));
        }

        private Gather_Util.ProfileClass CurrentProfile()
        {
            if (_useSnapshot && _snapshot is not null)
                return _snapshot;

            return _ruleIndex == -1 ? C.User_AutoProfile : C.User_BuffRules[_ruleIndex].Profile;
        }

        private string CurrentProfileName()
        {
            if (_useSnapshot && _snapshot is not null)
                return $"Replay snapshot ({_snapshotName})";

            return _ruleIndex == -1 ? "Default" : C.User_BuffRules[_ruleIndex].Name;
        }

        private void DrawProfilePicker()
        {
            var rules = C.User_BuffRules;
            var preview = _useSnapshot && _snapshot is not null
                ? $"Replay: {_snapshotName}"
                : _ruleIndex == -1 ? "Default (fallback)" : rules[_ruleIndex].Name;

            ImGui.SetNextItemWidth(300 * ImGuiHelpers.GlobalScale);
            using var combo = ImRaii.Combo("Profile", preview);
            if (!combo.Success)
                return;

            if (_snapshot is not null && ImGui.Selectable($"Replay: {_snapshotName}##snapshot", _useSnapshot))
            {
                _useSnapshot = true;
                _dirty = true;
            }

            if (ImGui.Selectable("Default (fallback)", !_useSnapshot && _ruleIndex == -1))
            {
                _useSnapshot = false;
                _ruleIndex = -1;
                _dirty = true;
            }

            for (var i = 0; i < rules.Count; i++)
            {
                if (!ImGui.Selectable($"{rules[i].Name}##calc{i}", !_useSnapshot && _ruleIndex == i))
                    continue;

                _useSnapshot = false;
                _ruleIndex = i;
                _dirty = true;
            }
        }

        private void DrawInt(string label, ref int value, int min, int max, float width)
        {
            var edited = value;
            ImGui.SetNextItemWidth(width);

            if (!ImGui.InputInt(label, ref edited, 1, 10))
                return;

            value = Math.Clamp(edited, min, max);
            _dirty = true;
        }

        private static void DrawCastEntry(GatherBuffId buff, uint count)
        {
            // 0 = no cap, which for the planner's output means a free proc (Wise)
            var suffix = count == 0 ? " (when it procs)" : $" x{count}";

            ImGui.PushID($"cast_{buff}");

            if (Gather_Util.GathActionDict.TryGetValue(buff, out var info)
                && info.ClassAction.TryGetValue(GatherBuffs_Tab._selectedJob, out var classInfo))
            {
                ImGui_Ice.ImageButtonWithText(classInfo.IconId, $"{classInfo.Name}{suffix}", $"cast_{buff}", true, 1);
            }
            else
            {
                ImGui.TextUnformatted($"{buff}{suffix}");
            }

            ImGui.PopID();
        }

        private string BuildReport(Gather_Util.ProfileClass profile)
        {
            var sb = new StringBuilder();
            var rules = C.User_BuffRules;
            var rule = !_useSnapshot && _ruleIndex >= 0 && _ruleIndex < rules.Count ? rules[_ruleIndex] : null;

            sb.AppendLine("=== GatherChill buff calculator report ===");
            sb.AppendLine($"Profile: {CurrentProfileName()}");

            if (rule is not null)
            {
                sb.AppendLine($"Rule: enabled={rule.Enabled}, kind={rule.Kind?.ToString() ?? "Any"}, " +
                              $"min integrity={rule.MinIntegrity?.ToString() ?? "-"}, max integrity={rule.MaxIntegrity?.ToString() ?? "-"}");
            }

            sb.AppendLine();
            sb.AppendLine("-- Inputs --");
            sb.AppendLine($"Level {_level} | GP {_gp} | Max integrity {_integrity} | Gather {_gatherChance}% | " +
                          $"Boon {_boonChance}% | Base yield {_baseYield} | Need {_need}");
            sb.AppendLine($"BY II amount {profile.BYII_Amount} | Tidings min boon {profile.TidingsMinBoon}%");

            sb.AppendLine();
            sb.AppendLine("-- Buff availability --");
            foreach (var (id, level, gp, reason) in Gather_Util.ExplainBuffs(_level, _integrity, _boonChance, profile))
            {
                profile.Buffs.TryGetValue(id, out var s);
                var settings = s is null ? "no entry" : $"enabled={s.Enabled}, maxUse={s.MaxUse}, minIntegrity={s.MinIntegrity}";
                sb.AppendLine($"{id} (lv {level}, {gp} GP): {(reason is null ? "AVAILABLE" : "EXCLUDED - " + reason)} [{settings}]");
            }

            sb.AppendLine();
            sb.AppendLine("-- Result --");
            if (_estimate is { } r)
            {
                sb.AppendLine($"Expected {r.ExpectedYield:F2} / need {_need} | GP spent {r.Gp} | Meets need: {r.MeetsNeed}");
                sb.AppendLine("Cast order: " + (r.Profile.BuffPriority.Count == 0
                    ? "none"
                    : string.Join(", ", r.Profile.BuffPriority.Select(b =>
                    {
                        var count = r.Profile.MaxUses.GetValueOrDefault(b);
                        return count == 0 ? $"{b} (proc)" : $"{b} x{count}";
                    }))));
            }
            else
            {
                sb.AppendLine("No estimate computed yet.");
            }

            sb.AppendLine();
            sb.AppendLine("-- Profile JSON (paste back to reproduce) --");
            sb.AppendLine(JsonConvert.SerializeObject(profile, Formatting.Indented));

            return sb.ToString();
        }
    }
}