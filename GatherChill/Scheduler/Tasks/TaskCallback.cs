using GatherChill.Scheduler.Handlers;

namespace GatherChill.Scheduler.Tasks
{
    internal static class TaskCallback
    {
        public static void Enqueue(string AddonName, bool visibilty, params int[] callback_fires)
        {
            P.taskManager.Enqueue(() => GenericHandlers.FireCallback(AddonName, visibilty, callback_fires));
        }
    }
}
