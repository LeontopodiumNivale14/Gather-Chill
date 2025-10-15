using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_863 : RouteInfo
    {
        public override uint Id => 863;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-365.357f, 36.1591f);
        public override int Radius => 59;

        public override HashSet<uint> NodeIds => new()
        {
            34088,
            34089,
            34090,
            34091,
            34092,
            34093,
            34094,
            34095,
            34096,
            34097,
            34098,
            34099,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003138,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34088,
            },
            new NodeInfo
            {
                NodeId = 34089,
            },
            new NodeInfo
            {
                NodeId = 34090,
            },
            new NodeInfo
            {
                NodeId = 34091,
            },
            new NodeInfo
            {
                NodeId = 34092,
            },
            new NodeInfo
            {
                NodeId = 34093,
            },
            new NodeInfo
            {
                NodeId = 34094,
            },
            new NodeInfo
            {
                NodeId = 34095,
            },
            new NodeInfo
            {
                NodeId = 34096,
            },
            new NodeInfo
            {
                NodeId = 34097,
            },
            new NodeInfo
            {
                NodeId = 34098,
            },
            new NodeInfo
            {
                NodeId = 34099,
            },
        };
    }
}
