using GatherChill.Scheduler.Tasks;
using Dalamud.Game.ClientState.Objects.SubKinds;
using ECommons.ExcelServices;
using ECommons.GameHelpers;
using ECommons.Logging;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using Lumina.Excel.Sheets;
using System.Collections.Generic;
using Dalamud.Game.ClientState.Conditions;

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
            NavDestination = Vector3.Zero;
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
            NavDestination = Vector3.Zero;

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
                if (GenericThrottle)
                {
                    if (!P.taskManager.IsBusy)
                    {
                        GatheredItem = 0;

                        foreach (var item in C.GatheringList)
                        {
                            var currentAmount = GetItemCount((int)item.ItemId);
                            if (currentAmount < item.GatheringAmount)
                            {
                                PluginDebug($"Found the item: {item.ItemId}, and needing to gather this");
                                GatheredItem = item.ItemId;
                                var entry = GatheringPointBaseDict.Where(x => x.Value.Items.Contains(GatheredItem)).FirstOrDefault();
                                NodeSet = entry.Key;
                                break;
                            }
                        }

                        if (GatheredItem != 0)
                        {
                            PluginDebug("GatheredItem doesn't = 0, finding the first possible node location");
                            // TODO: insert zone check logic -> Teleporting here

                            var nodeId = GatheringPointBaseDict[NodeSet].NodeIds.ElementAt(NodeIdIndex);

                            var goalNode = Svc.Objects
                                                .Where(s => s.IsTargetable)
                                                .Where(s => s.DataId == nodeId)
                                                .FirstOrDefault();

                            // Checking to see if you're close to the node
                            var firstListing = GatheringNodeInfoList
                                                .Where(x => x.NodeId == nodeId)
                                                .FirstOrDefault();

                            if (firstListing != null)
                            {
                                PluginDebug("First listing is not null, checking the distance");
                                // Checking to see if you're a reasonable distance to the node
                                if (Player.DistanceTo(firstListing.Position) >= 75)
                                {
                                    PluginDebug("Distance to node is more than 75, traveling to the node");
                                    // Distance is to far to tell if you can go there. Traveling to the node area
                                    TaskMount_Fly.Enqueue(firstListing.LandZone, false, 40);
                                }
                                else
                                {
                                    // You're within loading range of this node, now checking to see if the node is even available to begin with
                                    PluginDebug("You're close enough to the node, checking to see if that's valid");
                                    var validNode = Svc.Objects
                                                        .Where(n => n.IsTargetable)
                                                        .Where(n => n.DataId == firstListing.NodeId)
                                                        .FirstOrDefault();

                                    if (validNode != null)
                                    {
                                        var ListNode = GatheringNodeInfoList
                                                            .Where(x => x.Position == RoundVector3(validNode.Position, 2))
                                                            .FirstOrDefault();

                                        var LandZone = ListNode.LandZone;

                                        PluginDebug("Node is valid, checking to see what the distance is");
                                        // A node was found that was both targetable AND in range. Perfect ~
                                        if (Player.DistanceTo(LandZone) > 40 || Svc.Condition[ConditionFlag.InFlight])
                                        {
                                            P.taskManager.Enqueue(() => PluginDebug("Distance to node is greater than 40, flying to node"));
                                            TaskMount_Fly.Enqueue(LandZone, true, 1);
                                        }
                                        else
                                        {
                                            P.taskManager.Enqueue(() => PluginDebug("Distance to node is less than 40, moving by ground"));
                                            TaskMoveTo.Enqueue(LandZone, "Node Location", false, 1);
                                        }

                                        TaskDisMount.Enqueue();
                                        TaskTarget.Enqueue(ListNode.NodeId);
                                        TaskGatherInteract.Enqueue(ListNode.NodeId);
                                        TaskGather.Enqueue(GatheredItem);
                                    }
                                    else
                                    {
                                        // no valid nodes are seen, increasing the index value by 1
                                        P.taskManager.Enqueue(() =>
                                        {
                                            NodeIdIndex += 1;
                                            if (GatheringPointBaseDict[NodeSet].NodeIds.Count == NodeIdIndex)
                                                NodeIdIndex = 0;
                                        });
                                    }
                                }
                            }
                        }
                        else
                        {
                            PluginLog.Information("Can gather no more items. Disabling the plugin.");
                            DisablePlugin();
                        }
                    }
                }
            }
        }
    }
}
