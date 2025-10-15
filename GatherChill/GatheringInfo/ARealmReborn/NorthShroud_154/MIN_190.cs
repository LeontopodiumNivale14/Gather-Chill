using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.NorthShroud_154
{
    public class MIN_190 : RouteInfo
    {
        public override uint Id => 190;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 154;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(399.37f, 80.5178f);
        public override int Radius => 39;

        public override HashSet<uint> NodeIds => new()
        {
            30540,
            30541,
            30542,
            30543,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5,
            5131,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30540,
            },
            new NodeInfo
            {
                NodeId = 30541,
            },
            new NodeInfo
            {
                NodeId = 30542,
            },
            new NodeInfo
            {
                NodeId = 30543,
            },
        };
    }
}
