

#nullable disable
namespace GatherChill.Scheduler
{
    internal static unsafe class SchedulerMain
    {
        internal static bool AreWeTicking;
        internal static bool EnableTicking
        {
            get => AreWeTicking;
            private set => AreWeTicking = value;
        }
        internal static bool EnablePlugin()
        {
            EnableTicking = true;
            return true;
        }
        internal static bool DisablePlugin()
        {
            EnableTicking = false;

            P.navmesh.Stop();
            P.taskManager.Abort();

            NodeSet = 0;
            GatheredItem = 0;
            NodeIdIndex = 0;

            return true;
        }

        internal static string CurrentProcess = "";
        internal static uint GatheredItem = 0; // Item to be stored for later use
        internal static uint NodeSet = 0; // Keeps the gathering node set 
        internal static int NodeIdIndex = 0; // which of the nodes are you currently in on the set

        internal static void Tick()
        {
            if (AreWeTicking)
            {

            }
        }
    }
}
