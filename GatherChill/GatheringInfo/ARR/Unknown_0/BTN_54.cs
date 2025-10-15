using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_54 : RouteInfo
    {
        public override uint Id => 54;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(172.371f, -113.616f);
        public override int Radius => 20;

        public override HashSet<uint> NodeIds => new()
        {
            30176,
            30177,
            30178,
            30179,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000273,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30176,
            },
            new NodeInfo
            {
                NodeId = 30177,
            },
            new NodeInfo
            {
                NodeId = 30178,
            },
            new NodeInfo
            {
                NodeId = 30179,
            },
        };
    }
}
