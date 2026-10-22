using Dalamud.Game.ClientState.Conditions;
using ECommons.GameHelpers;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using GatherChill.Utilities.Tools;
using GatherChill.Utilities.Traveling;
using GatherChill.Utilities.Utility;
using static ECommons.UIHelpers.AddonMasterImplementations.AddonMaster;

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
        public static bool Task_GroundToAetheryte(Vector3 pos, Vector3 aetheryte, bool waitForBusy = true, float distance = 2.0f)
        {
            const string tag = "Navmesh: Ground Move";

            if (!P.navmesh.Installed)
            {
                IceLogging.Info("We seem to be missing navmesh... so we're just going to exit here", tag);
                return true;
            }
            else if (P.navmesh.IsRunning())
            {
                if (Player.IsMoving && waitForBusy)
                {
                    if (EzThrottler.Throttle("Throttle message tehe"))
                        IceLogging.Verbose("We're currently moving, and we were told to wait for us to NOT be moving so... yeah, we waiting", tag);

                    return false;
                }
                else if (!waitForBusy && (Player.DistanceTo(aetheryte) <= distance))
                {
                    if (EzThrottler.Throttle("Telling navmesh to stop"))
                    {
                        IceLogging.Verbose("We're within stopping distance, so stopping navmesh", tag);
                        P.navmesh.PathStop();
                    }
                }
            }
            else if (!P.navmesh.IsReady())
            {
                if (EzThrottler.Throttle("Waiting on navmesh", 1000))
                {
                    var navProgress = P.navmesh.BuildProgress();
                    IceLogging.Debug($"Waiting for navmesh to finish building. Currently at: {navProgress:N2}", tag);
                }
            }
            else if (!P.navmesh.IsRunning())
            {
                if (Player.DistanceTo(aetheryte) <= distance)
                {
                    IceLogging.Verbose("We've met the distance threshold, continuing on", tag);
                    return true;
                }
                else
                {
                    if (EzThrottler.Throttle("Telling navmesh to start"))
                    {
                        P.navmesh.SetTolerance(0.25f);
                        IceLogging.Verbose("We're setting the tolerance to 0.25f here", tag);
                        P.navmesh.PathfindAndMoveTo(pos, false);
                    }
                }
            }

            return false;
        }
        public static unsafe bool LeaveIdyllshire(uint aetherShard)
        {
            const string tag = "Navmesh: Using Aethernet";

            var DrivanianHinterlands = 399;
            var destinationShard = 92;
            var interactDistance = 11;

            if (TravelUtil.Aethernet.TryGetValue(aetherShard, out var goalShard))
            {
                var agent = AgentTelepotTown.Instance();
                if (Player.Territory.RowId == DrivanianHinterlands)
                {
                    IceLogging.Debug("We've reached our destination", tag);
                    return true;
                }
                if (agent != null && agent->Data != null)
                {
                    if (EzThrottler.Throttle("Finding proper aethernet", 1000))
                    {
                        var data = agent->Data;

                        for (byte i = 0; i < data->AetheryteCount; i++)
                        {
                            var entry = data->Entries[i];
                            if (entry.AetheryteId == destinationShard)
                            {
                                IceLogging.Verbose($"We found the destination we were looking for: [{aetherShard}]. Initializing teleport", tag);
                                agent->TeleportToAetheryte(i);
                            }
                        }
                    }
                }
                else if (GenericHelpers.TryGetAddonMaster<SelectString>(out var selectString) && selectString.IsAddonReady)
                {
                    // This is always going to be the top entry for aethernet travel, so can just hardcode this (can't wait for evercold to bite me in the ass for this)
                    var menu = selectString.Entries[0];
                    if (EzThrottler.Throttle("Selecting entry"))
                    {
                        IceLogging.Verbose("Selecting the aethernet selection from the select string", tag);
                        menu.Select();
                    }

                }
                else
                {
                    var aethernet = Svc.Objects.Where(x => x.BaseId == aetherShard).FirstOrDefault();
                    bool validShard = aethernet != null;
                    bool interactable = validShard && Player.DistanceTo(aethernet.Position) <= interactDistance;

                    if (aethernet != null && Player.DistanceTo(aethernet) <= 11)
                    {
                        if (Player.Mounted || Player.IsJumping)
                        {
                            Utils.Dismount();
                            return false;
                        }
                        else if (!Player.IsBusy)
                        {
                            if (EzThrottler.Throttle("Selecting aetheryte"))
                                IceLogging.Verbose("Selecting the aetheryte for travel", tag);

                            Utils.TargetgameObject(aethernet);
                            Utils.InteractWithObject(aethernet);
                        }
                    }
                    else
                    {
                        if (EzThrottler.Throttle("Targeting aethershard"))
                        {
                            IceLogging.Debug($"Waiting... Shard Valid? {validShard} | Interact Distance? {interactable}", tag);
                            if (aethernet != null)
                            {
                                IceLogging.Debug($"{Player.DistanceTo(aethernet)} <= {interactDistance}", tag);
                            }
                        }
                        if (!P.navmesh.IsRunning() && !Player.IsBusy)
                        {
                            P.navmesh.PathfindAndMoveCloseTo(aethernet.Position, false, interactDistance);
                        }
                    }
                }
            }
            else
            {
                bool goalValid = TravelUtil.Aethernet.ContainsKey(aetherShard);

                if (EzThrottler.Throttle("Error Aethernet", 1000))
                    IceLogging.Error($"We... seem to be missing aethernet info. Goal Shard [MoveTo] = {goalValid}", tag);
            }

            return false;
        }
    }
}
