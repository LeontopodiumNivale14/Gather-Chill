using Dalamud.Interface;
using Dalamud.Interface.Colors;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Utility.Raii;
using GatherChill.Enums;
using GatherChill.Gui;
using GatherChill.Scheduler;
using GatherChill.Utilities.GatheringHelpers;
using GatherChill.Utilities.Utility;
using System.Collections.Generic;

namespace GatherChill.Ui;

internal class GatheringOverlay : Window
{
    private const int MenuButtonCount = 3;
    private const int UpcomingCount = 2;
    private const long UpcomingRefreshMs = 1000;
    private const ImGuiWindowFlags NoScroll = ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse;

    // Smallest width that still fits the GP panel + the Next/Then boxes. Tune to taste.
    private const float MinWindowWidth = 420f;

    public GatheringOverlay() : base($"Gather & Chill: Overlay##GatherChillOverlay")
    {
        Flags = ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse;

        // Only used the first time; after that ImGui remembers whatever the user dragged it to
        Size = new Vector2(MinWindowWidth, 200f);
        SizeCondition = ImGuiCond.FirstUseEver;

        P.windowSystem.AddWindow(this);
    }

    // Runs before the window begins, so this is the place to lock the height.
    public override void PreDraw()
    {
        var style = ImGui.GetStyle();
        var height = style.WindowPadding.Y * 2 + GetItemHeight() + style.ItemSpacing.Y + GetRowHeight();

        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(MinWindowWidth * ImGuiHelpers.GlobalScale, height),
            MaximumSize = new Vector2(float.MaxValue, height),
        };
    }

    public override void Draw()
    {
        // Top Row
        CurrentItem();

        // Bottom Row [Doing this so the GP window and the gatherables both keep the same height]
        var rowHeight = GetRowHeight();

        GPBar(rowHeight);
        ImGui.SameLine();

        // Making it to where all the stuff will fill in the rest of the space available
        var width = ImGui.GetContentRegionAvail().X;

        // Since we're using a child window... but as a container to make sure that it fits as I want it.
        // Since table creating is a PITA and creates some janky stuff
        // pushing the main window BG. (I don't fuck with this... yet. But also should be fine??)
        using var containerBg = ImRaii.PushColor(ImGuiCol.ChildBg, ImGui.GetStyle().Colors[(int)ImGuiCol.WindowBg]);
        using (var container = ImRaii.Child("Gatherable Items Container", new(width, rowHeight)))
        {
            if (!container.Success)
                return;

            ItemProgressWindow();
            NextThenWindow();
        }
    }

    #region Shared Helpers

    // Place to keep all the helpers for all the windows in a nice little spot

    // Shortens text with "..." until it fits maxWidth
    private static string Truncate(string text, float maxWidth)
    {
        if (ImGui.CalcTextSize(text).X <= maxWidth)
            return text;

        const string ellipsis = "...";
        var end = text.Length;
        while (end > 0 && ImGui.CalcTextSize(text[..end] + ellipsis).X > maxWidth)
            end--;

        return text[..end] + ellipsis;
    }

    private static string FormatTimer(TimeSpan span) => span.TotalHours >= 1 ? span.ToString(@"h\:mm\:ss") : span.ToString(@"mm\:ss");

    #endregion

    #region Height Stuff

    private static float GetItemHeight() => ImGui.GetFrameHeight() + ImGui.GetStyle().WindowPadding.Y * 2;

    // Progress child: label line + bar row + child padding
    private static float GetProgressHeight()
    {
        var style = ImGui.GetStyle();
        return ImGui.GetTextLineHeight() + style.ItemSpacing.Y + ImGui.GetFrameHeight() + style.WindowPadding.Y * 2;
    }

    // Next/Then child: label line + (icon | name + timer) row + child padding
    private static float GetNextThenHeight()
    {
        var style = ImGui.GetStyle();
        var itemRow = Math.Max(ImGui.GetFrameHeight(), ImGui.GetTextLineHeight() * 2 + style.ItemSpacing.Y);
        return ImGui.GetTextLineHeight() + style.ItemSpacing.Y + itemRow + style.WindowPadding.Y * 2;
    }

    private static float GetRowHeight()
    {
        // Tallest side wins. Right side = progress + gap between children + next/then.
        var rightContent = GetProgressHeight() + ImGui.GetStyle().ItemSpacing.Y + GetNextThenHeight();
        return Math.Max(ImGui_Ice.VerticalBarHeight(), rightContent);
    }

    private static float GetMenuButtonsWidth() => ImGui.GetFrameHeight() * MenuButtonCount + ImGui.GetStyle().ItemSpacing.X * (MenuButtonCount - 1);

    #endregion

    #region Top Row [Idle/Currently Gathering + Menu Buttons]

    private static void CurrentItem()
    {
        using var colors = ImGui_Ice.PushChildBG();
        using var itemMenu = ImRaii.Child("Gather & Chill: Overlay Menu", new(ImGui.GetContentRegionAvail().X, GetItemHeight()), true, NoScroll);
        if (!itemMenu.Success)
            return;

        CurrentItemInfo();
        MenuButtons();
    }

    private static void CurrentItemInfo()
    {
        if (Gather_Helper.State is IceState.Idle)
        {
            ImGui_Ice.StatusDot();
            ImGui.SameLine();
            ImGui.AlignTextToFramePadding();
            ImGui.Text("Idle");
            return;
        }

        var itemId = Gather_Helper.GatherRoute.itemId;
        if (itemId == 0 || !Gather_Util.Sheet_ItemInfo.TryGetValue(itemId, out var item))
            return;

        ImGui_Ice.ImageButton(item.IconId, item.Name);
        ImGui.SameLine();
        ImGui.AlignTextToFramePadding();
        ImGui.TextDisabled("Gathering: ");
        ImGui.SameLine();
        ImGui.AlignTextToFramePadding();

        // Leave room on the right for the menu buttons
        var maxNameWidth = ImGui.GetContentRegionAvail().X - GetMenuButtonsWidth() - ImGui.GetStyle().ItemSpacing.X;
        ImGui.Text(Truncate(item.Name, maxNameWidth));
    }

    private static void MenuButtons()
    {
        var width = GetMenuButtonsWidth();

        // Same line as whatever was drawn on the left, pushed to the right edge
        ImGui.SameLine();
        var x = ImGui.GetCursorPosX() + ImGui.GetContentRegionAvail().X - width;
        if (x > ImGui.GetCursorPosX())
            ImGui.SetCursorPosX(x);

        if (IconButton(FontAwesomeIcon.Home, "Settings", "Settings"))
        {
            P.mainWindow.IsOpen = true;
            P.mainWindow.BringToFront();
        }

        ImGui.SameLine();

        var isIdle = Gather_Helper.State == IceState.Idle;
        var icon = isIdle ? FontAwesomeIcon.Play : FontAwesomeIcon.Stop;
        var modeText = isIdle ? "Start" : "Stop";

        if (IconButton(icon, $"{modeText}", $"{modeText} gathering"))
        {
            if (isIdle)
                Gather_Helper.State = IceState.Start;
            else
                SchedulerMain.DisablePlugin();
        }

        ImGui.SameLine();

        if (IconButton(FontAwesomeIcon.Times, "Close Overlay", "Close Overlay"))
        {
            P.gatherOverlay.IsOpen = false;
        }
    }

    private static bool IconButton(FontAwesomeIcon icon, string id, string tooltip)
    {
        var size = new Vector2(ImGui.GetFrameHeight());

        bool clicked;
        using (ImRaii.PushFont(UiBuilder.IconFont))
            clicked = ImGui.Button($"{icon.ToIconString()}##{id}", size);

        if (ImGui.IsItemHovered())
            ImGui.SetTooltip(tooltip);

        return clicked;
    }

    #endregion

    #region Bottom Row [GP Bar | Current Progress | Next / Then Items]

    private static void GPBar(float rowHeight)
    {
        using var colors = ImGui_Ice.PushChildBG();
        ImGui_Ice.VerticalBar("GP", Utils.GetGp(), Utils.MaxGp(), framed: true, totalHeight: rowHeight);
    }

    private static void ItemProgressWindow()
    {
        using var colors = ImGui_Ice.PushChildBG();

        // Fixed height, full width of the container
        using var child = ImRaii.Child("Item Progress Bar", new(ImGui.GetContentRegionAvail().X, GetProgressHeight()), true, NoScroll);
        if (!child.Success)
            return;

        var itemId = Gather_Helper.GatherRoute.itemId;
        var progress = Gather_Helper.ItemProgress.FirstOrDefault(x => x.ItemId == itemId);
        if (progress == null)
            return;

        var total = progress.Target;
        if (total <= 0)
            return;

        // Keep: current inventory. Gather: gathered this run.
        var current = Math.Min(progress.Progress, total);

        var label = progress.Mode == GatherMode.Keep ? "Keep" : "Progress";
        Draw_XPBar(current, total, label);
    }

    #region Next / Then Items

    // The ordering only needs to be rebuilt about once a second (or when the current item changes).
    // The timers shown on screen are still read live every frame.
    private static List<uint> _upcomingIds = new();
    private static uint _cachedCurrent;
    private static long _nextRefresh;

    private static List<uint> GetUpcomingIds()
    {
        var current = Gather_Helper.State is IceState.Idle ? 0u : Gather_Helper.GatherRoute.itemId;
        var now = Environment.TickCount64;

        // Still before the refresh time and same current item -> reuse the cached order
        if (current == _cachedCurrent && now < _nextRefresh)
            return _upcomingIds;

        _cachedCurrent = current;
        _nextRefresh = now + UpcomingRefreshMs;
        _upcomingIds = BuildUpcomingIds(current);
        return _upcomingIds;
    }

    // Order: timed items that are up (GatherList order), then timed items by soonest opening, then the rest.
    private static List<uint> BuildUpcomingIds(uint current)
    {
        var up = new List<uint>();
        var waiting = new List<(uint Id, TimeSpan Timer)>();
        var normal = new List<uint>();

        foreach (var info in C.GatherList)
        {
            if (info.ItemId == current)
                continue;

            var (hasTimed, isUp, timer) = GetTimerInfo(info.ItemId);

            if (!hasTimed)
                normal.Add(info.ItemId);
            else if (isUp)
                up.Add(info.ItemId);
            else
                waiting.Add((info.ItemId, timer));
        }

        // OrderBy is stable, so ties keep GatherList order
        var result = new List<uint>(up);
        result.AddRange(waiting.OrderBy(x => x.Timer).Select(x => x.Id));
        result.AddRange(normal);

        return result.Distinct().Take(UpcomingCount).ToList();
    }

    // Timer info for one item across all of its enabled routes.
    // IsUp: true  -> Timer is how long it stays up
    //       false -> Timer is how long until it opens
    private static (bool HasTimed, bool IsUp, TimeSpan Timer) GetTimerInfo(uint itemId)
    {
        if (!C.ItemRoutes.TryGetValue(itemId, out var selection))
            return default;

        var hasTimed = false;
        TimeSpan? upFor = null;
        TimeSpan? startsIn = null;

        foreach (var routeId in selection.EnabledRoutes)
        {
            if (!Gather_Util.Sheet_RouteInfo.TryGetValue(routeId, out var route))
                continue;

            foreach (var window in route.TimedInfo)
            {
                hasTimed = true;

                if (window.IsUp())
                {
                    // Keep the LONGEST time remaining
                    var left = window.TimeUntilEnd();
                    if (upFor is null || left > upFor)
                        upFor = left;
                }
                else
                {
                    // Keep the SOONEST opening
                    var wait = window.TimeUntilStart();
                    if (startsIn is null || wait < startsIn)
                        startsIn = wait;
                }
            }
        }

        if (!hasTimed)
            return default;

        return upFor is { } remaining
            ? (true, true, remaining)
            : (true, false, startsIn ?? TimeSpan.Zero);
    }

    private static void NextThenWindow()
    {
        // Split the remaining space in half, minus the gap between the two boxes
        var avail = ImGui.GetContentRegionAvail();
        var width = MathF.Floor((avail.X - ImGui.GetStyle().ItemSpacing.X) * 0.5f);
        var size = new Vector2(width, avail.Y);

        var ids = GetUpcomingIds();
        uint? next = ids.Count > 0 ? ids[0] : null;
        uint? then = ids.Count > 1 ? ids[1] : null;

        DrawUpcomingChild("Next Item", "Next:", next, size);
        ImGui.SameLine();
        DrawUpcomingChild("Then Item", "Then:", then, size);
    }

    private static void DrawUpcomingChild(string id, string label, uint? itemId, Vector2 size)
    {
        using var colors = ImGui_Ice.PushChildBG();
        using var child = ImRaii.Child(id, size, true, NoScroll);
        if (!child.Success)
            return;

        ImGui.TextDisabled(label);

        if (itemId is not { } upcomingId || !Gather_Util.Sheet_ItemInfo.TryGetValue(upcomingId, out var item))
        {
            ImGui.TextDisabled("None");
            return;
        }

        ImGui_Ice.ImageButton(item.IconId, $"{label}{item.Name}");
        ImGui.SameLine();

        using var group = ImRaii.Group();

        // Whatever width is left to the right of the icon
        ImGui.Text(Truncate(item.Name, ImGui.GetContentRegionAvail().X));

        // Read live each frame so the countdown stays smooth even though the order is cached
        var (hasTimed, isUp, timer) = GetTimerInfo(upcomingId);
        if (!hasTimed)
        {
            ImGui.TextDisabled("No timer");
            return;
        }

        using (ImRaii.PushFont(UiBuilder.IconFont))
            ImGui.TextDisabled(FontAwesomeIcon.Clock.ToIconString());

        ImGui.SameLine();

        // Green = up right now (time left), grey = opens in (time until)
        if (isUp)
            ImGui.TextColored(ImGuiColors.ParsedGreen, FormatTimer(timer));
        else
            ImGui.TextDisabled(FormatTimer(timer));
    }

    #endregion

    #endregion
}