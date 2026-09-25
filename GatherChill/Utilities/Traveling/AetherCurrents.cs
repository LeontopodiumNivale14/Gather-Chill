using ECommons.GameHelpers;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using GatherChill.Utilities.Tools;
using System.Collections.Generic;
using System.Linq;

namespace GatherChill.Utilities.Traveling;

public static partial class TravelUtil
{
    public class AetherCurrent
    {
        public uint ID { get; set; }

        public unsafe bool Unlocked()
        {
            var pState = PlayerState.Instance();
            if (pState is null)
                return false;

            return pState->IsAetherCurrentUnlocked(ID);
        }
    }

    public static Dictionary<uint, List<AetherCurrent>> FlightInfo = new();

    public static void MakeFlightInfo()
    {
        foreach (var row in ExcelHelper.Sheet_AethercurrentComplete)
        {
            var territoryId = row.Territory.RowId;
            if (territoryId == 0)
                continue;

            var currents = new List<AetherCurrent>();

            foreach (var aetherCurrentRef in row.AetherCurrents)
            {
                if (!aetherCurrentRef.IsValid)
                    continue;

                currents.Add(new AetherCurrent { ID = aetherCurrentRef.RowId });
            }

            if (currents.Count > 0)
                FlightInfo[territoryId] = currents;
        }
    }

    public static bool HasFlightUnlocked(uint territoryId)
    {
        if (!FlightInfo.TryGetValue(territoryId, out var currents))
            return false; // zone has no aether current system (ARR zones, instances, etc.)

        foreach (var current in currents)
        {
            if (!current.Unlocked())
                return false;
        }

        return true;
    }
}