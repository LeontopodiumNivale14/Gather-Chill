using Dalamud.Interface.Utility.Raii;
using GatherChill.Utilities.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Utilities.GatheringHelpers;

// I keep using this shit in multiple places, might as well might my life simple have it in one local area
public static partial class Gather_Util
{
    public static class UptimeHelper
    {
        private readonly record struct UpcomingSlot(EorzeaTimeWindow Window, DateTime Start, DateTime End, bool IsUp);

        /// <summary>
        /// Windows from the routes the user has enabled for this item (playlist use).
        /// </summary>
        public static List<EorzeaTimeWindow> GetEnabledWindows(uint itemId) => GetWindows(C.ItemRoutes[itemId].EnabledRoutes);

        /// <summary>
        /// Windows from any set of routes (saved list, or anything else with its own route set).
        /// De-duplicated, since routes can share a window.
        /// </summary>
        public static List<EorzeaTimeWindow> GetWindows(IEnumerable<uint> routeIds)
        {
            var result = new List<EorzeaTimeWindow>();

            foreach (var routeId in routeIds)
            {
                if (!Sheet_RouteInfo.TryGetValue(routeId, out var routeInfo))
                    continue;

                foreach (var window in routeInfo.TimedInfo)
                {
                    if (!result.Contains(window))
                        result.Add(window);
                }
            }

            return result;
        }

        public static bool IsUp(List<EorzeaTimeWindow> windows) => UpFor(windows) is not null;

        public static string FormatCountdown(TimeSpan span)
        {
            if (span.TotalHours >= 1)
                return $"{(int)span.TotalHours}h {span.Minutes}m";

            return $"{span.Minutes}m {span.Seconds}s";
        }

        // Longest time remaining across windows that are up right now, null if none are up
        public static TimeSpan? UpFor(List<EorzeaTimeWindow> windows)
        {
            if (windows.Count == 0)
                return null;

            var remaining = windows.Max(w => w.TimeUntilEnd());
            return remaining > TimeSpan.Zero ? remaining : null;
        }

        // Soonest next opening across windows
        public static TimeSpan? NextUptime(List<EorzeaTimeWindow> windows)
        {
            if (windows.Count == 0)
                return null;

            return windows.Min(w => w.TimeUntilStart());
        }

        /// <summary>
        /// Draws the "Up for / Next Uptime" button with the upcoming-slots tooltip.
        /// </summary>
        public static void Draw(List<EorzeaTimeWindow> windows, string emptyText = "No timed routes")
        {
            ImGui.AlignTextToFramePadding();

            if (windows.Count == 0)
            {
                ImGui.TextDisabled(emptyText);
                return;
            }

            if (UpFor(windows) is { } upFor)
            {
                ImGui.Button($"Up for: {FormatCountdown(upFor)}");
            }
            else if (NextUptime(windows) is { } next)
            {
                var button = ImGui.GetStyle().Colors[(int)ImGuiCol.Button];

                // Dimmed so "up now" stands out from "not yet"
                using var colors = ImRaii.PushColor(ImGuiCol.Button, button with { W = button.W * 0.5f })
                                         .Push(ImGuiCol.ButtonHovered, button with { W = button.W * 0.5f })
                                         .Push(ImGuiCol.ButtonActive, button with { W = button.W * 0.5f });

                ImGui.Button($"Next Uptime: {FormatCountdown(next)}");
            }

            if (!ImGui.IsItemHovered())
                return;

            var slots = GetUpcoming(windows);
            if (slots.Count == 0)
                return;

            using var tooltip = ImRaii.Tooltip();
            if (tooltip.Alive)
                DrawUpcoming(slots);
        }

        private static List<UpcomingSlot> GetUpcoming(List<EorzeaTimeWindow> windows, int count = 5)
        {
            var now = DateTime.Now;
            var eorzeaDay = Utils.EorzeaMinutesToReal(Utils.EorzeaMinutesPerDay);
            var slots = new List<UpcomingSlot>();

            foreach (var window in windows)
            {
                var isUp = window.IsUp();

                // First occurrence: currently running, or the next one to open
                var start = isUp
                    ? now - (window.DurationReal - window.TimeUntilEnd())
                    : now + window.TimeUntilStart();

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

            using var table = ImRaii.Table("##UptimeHelper_Upcoming", 4, ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.RowBg);
            if (!table.Success)
                return;

            ImGui.TableSetupColumn("Status");
            ImGui.TableSetupColumn("Local");
            ImGui.TableSetupColumn("Eorzea");
            ImGui.TableSetupColumn("Duration");
            ImGui.TableHeadersRow();

            var now = DateTime.Now;

            for (var i = 0; i < slots.Count; i++)
            {
                var slot = slots[i];

                ImGui.TableNextRow();

                // Later rows fade a little so the eye lands on what's next
                var fade = slot.IsUp ? 1f : Math.Max(0.55f, 1f - i * 0.1f);
                using var color = ImRaii.PushColor(ImGuiCol.Text, ImGui.GetStyle().Colors[(int)ImGuiCol.Text] with { W = fade });

                ImGui.TableNextColumn();
                if (slot.IsUp)
                    ImGui.TextColored(new Vector4(0.4f, 0.9f, 0.4f, 1f), $"Up ({FormatCountdown(slot.End - now)} left)");
                else
                    ImGui.Text($"in {FormatCountdown(slot.Start - now)}");

                ImGui.TableNextColumn();
                ImGui.Text($"{slot.Start:t} – {slot.End:t}");

                ImGui.TableNextColumn();
                ImGui.Text(slot.Window.ToString());

                ImGui.TableNextColumn();
                ImGui.Text(FormatCountdown(slot.Window.DurationReal));
            }
        }
    }
}
