using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.SouthShroud_153
{
    public class MIN_48 : RouteInfo
    {
        public override uint Id => 48;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 153;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(73.7931f, 13.2688f);
        public override int Radius => 61;

        public override HashSet<uint> NodeIds => new()
        {
            30108,
            30109,
            30110,
            30462,
        };

        public override HashSet<uint> ItemIds => new()
        {
            3,
            5153,
            5154,
            5155,
            5270,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30108,
            },
            new NodeInfo
            {
                NodeId = 30109,
            },
            new NodeInfo
            {
                NodeId = 30110,
            },
            new NodeInfo
            {
                NodeId = 30462,
            },
        };
    }
}
