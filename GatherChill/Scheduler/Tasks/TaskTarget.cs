using Dalamud.Game.ClientState.Objects.Types;

namespace GatherChill.Scheduler.Tasks
{
    internal static class TaskTarget
    {
        public static void Enqueue(ulong dataId)
        {
            Svc.Log.Debug($"Targeting {dataId}");
            IGameObject? gameObject = null;
            P.taskManager.Enqueue(() => TryGetObjectByDataId(dataId, out gameObject), "Getting Object");
            P.taskManager.Enqueue(() => PluginVerbos($"Targeting By ID. Target is: {gameObject?.DataId}"), "Plugin Verbos");
            P.taskManager.Enqueue(() => TargetgameObject(gameObject), "Targeting Object");
        }
    }
}
