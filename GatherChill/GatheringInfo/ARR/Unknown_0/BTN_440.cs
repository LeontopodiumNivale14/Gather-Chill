using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_440 : RouteInfo
    {
        public override uint Id => 440;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(521.638f, -137.229f);
        public override int Radius => 38;

        public override HashSet<uint> NodeIds => new()
        {
            31885,
            31886,
            31887,
            31888,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002131,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31885,
            },
            new NodeInfo
            {
                NodeId = 31886,
            },
            new NodeInfo
            {
                NodeId = 31887,
            },
            new NodeInfo
            {
                NodeId = 31888,
            },
        };
    }
}
