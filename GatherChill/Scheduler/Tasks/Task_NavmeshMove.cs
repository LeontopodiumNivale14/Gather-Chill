using Dalamud.Game.ClientState.Conditions;
using ECommons.GameHelpers;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Traveling;

namespace GatherChill.Scheduler.Tasks
{
    internal class Task_NavmeshMove
    {
        public static bool TeleportToArea(uint aetheryteId)
        {
            const string tag = "Navmesh: Teleporting";

            if (TravelUtil.Aethernet.TryGetValue(aetheryteId, out var aetherInfo))
            {
                var territoryId = aetherInfo.TerritoryId;
                var currentTerritory = Player.Territory.RowId;
                if (Player.Available)
                {
                    if (currentTerritory == territoryId)
                    {
                        IceLogging.Verbose("We are currently in the correct territory, continuing on", tag);
                        return true;
                    }
                    else
                    {
                        bool isBusy = Player.IsBusy;
                        bool inBetweenAreas = Svc.Condition[ConditionFlag.BetweenAreas] || Svc.Condition[ConditionFlag.BetweenAreas51];

                        if (!isBusy && !inBetweenAreas)
                        {
                            InvokeTeleport(aetheryteId, territoryId, tag);
                        }
                        else
                        {
                            if (EzThrottler.Throttle("Waiting for teleport", 1000))
                                IceLogging.Verbose("Waiting for teleport to finish so we can check the state..", tag);
                        }
                    }
                }
            }
            else
            {
                if (EzThrottler.Throttle("Aethernet Teleport Error", 2000))
                    IceLogging.Error($"No aetheryte was found under ID: [{aetheryteId}]", tag);
            }

            return false;
        }
        private static unsafe void InvokeTeleport(uint aetheryteId, uint territoryId, string tag)
        {
            if (EzThrottler.Throttle("Attempting to teleport"))
            {
                IceLogging.Verbose($"Initializing the teleport to: {territoryId}", tag);
                Telepo.Instance()->Teleport(aetheryteId, 0);
            }
        }
    }
}
