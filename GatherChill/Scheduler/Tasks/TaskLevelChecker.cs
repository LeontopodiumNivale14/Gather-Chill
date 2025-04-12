﻿namespace GatherChill.Scheduler.Tasks
{
    internal static class TaskLevelChecker
    {
        internal static void Enqueue(uint leveLevel)
        {
            P.taskManager.Enqueue(() => LevelChecker(leveLevel));
        }

        internal static bool? LevelChecker(uint leveLevel)
        {
            if (leveLevel <= GetLevel())
            {
                return true;
            }
            else
            {
                string errorMessage = "You're not the proper level to do this leve, stopping";
                Svc.Toasts.ShowError(errorMessage);
                PluginVerbos(errorMessage);
                SchedulerMain.DisablePlugin();
            }
            return false;
        }
    }
}

