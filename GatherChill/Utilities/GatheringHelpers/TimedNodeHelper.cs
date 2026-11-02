using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using System;
using System.Collections.Generic;
using System.Text;

namespace GatherChill.Utilities.GatheringHelpers;

public static partial class Gather_Util
{
    /// <summary>
    /// Active timed-node marker centers as world (X, Z).
    /// Raw values are stored as world * 16, so divide by 16.
    /// </summary>
    public static unsafe List<Vector2> GetActiveWorldXZ()
    {
        var result = new List<Vector2>();

        var agent = AgentMap.Instance();
        if (agent == null)
            return result;

        var markers = agent->MiniMapGatheringMarkers;

        // i < Length: keep looping while i is SMALLER than the slot count
        for (var i = 0; i < markers.Length; i++)
        {
            ref var data = ref markers[i].MapMarker;

            // Unused slot
            if (data.IconId == 0)
                continue;

            result.Add(new Vector2(data.X / 16f, data.Y / 16f));
        }

        return result;
    }
}
