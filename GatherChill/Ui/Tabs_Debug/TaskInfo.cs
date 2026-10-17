using GatherChill.Scheduler;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Ui.Tabs_Debug;

internal class TaskInfo
{
    public static void Draw()
    {
        ImGui.Text($"Running task: {P.TaskManager.NumQueuedTasks != 0} | Amount of queue'd task: {P.TaskManager.NumQueuedTasks}");
        string currentTask = P.TaskManager.CurrentTask?.Name ?? "";
        ImGui.Text($"Current task running: {currentTask}");
        ImGui.Text($"Current State: {Gather_Helper.State}");
        ImGui.Text($"ItemId set: {Gather_Helper.GatherRoute.itemId}");
        ImGui.Text($"Task Count: {P.TaskManager.Tasks.Count}");
    }
}
