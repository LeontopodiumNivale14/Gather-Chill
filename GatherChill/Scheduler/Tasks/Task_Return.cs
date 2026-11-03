using ECommons.GameHelpers;
using ECommons.Throttlers;
using GatherChill.Enums;

namespace GatherChill.Scheduler.Tasks
{
    internal class Task_Return
    {
        public static bool Lifestream_FinishedList()
        {
            if (!P.lifestream.Installed || P.lifestream.IsBusy() || !C.CharacterConfig.TryGetValue(Player.CID, out var configInfo))
            {
                ClearTask();
                return true;
            }

            if (configInfo.PostReturnPoint is ReturnTypes.None)
            {
                ClearTask();
                return false;
            }

            if (!EzThrottler.Throttle("Queue Lifestream"))
                return false;

            P.lifestream.QueueTask(configInfo.PostReturnPoint, configInfo.PostReturn_Command);
            return false;
        }

        private static void ClearTask()
        {
            Gather_Helper.State = IceState.Idle;
            P.TaskManager.Tasks.Clear();
        }

        public static bool Lifestream_ItemWait()
        {
            if (!P.lifestream.Installed || P.lifestream.IsBusy() || !C.CharacterConfig.TryGetValue(Player.CID, out var configInfo))
                return true;

            if (!EzThrottler.Throttle("Queue Lifestream"))
                return false;

            if (configInfo.WaitReturnPoint == ReturnTypes.None || (configInfo.WaitReturnPoint == ReturnTypes.Custom && configInfo.WaitReturn_Command == string.Empty))
                return true;
            else
            {
                P.lifestream.QueueTask(configInfo.WaitReturnPoint, configInfo.WaitReturn_Command);
                return false;
            }
        }
    }
}
