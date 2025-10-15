using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.NorthShroud_154
{
    public class MIN_152 : RouteInfo
    {
        public override uint Id => 152;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 154;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(358.901f, 179.453f);
        public override int Radius => 29;

        public override HashSet<uint> NodeIds => new()
        {
            30405,
            30406,
            30407,
            30408,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5,
            5133,
            5513,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30405,
            },
            new NodeInfo
            {
                NodeId = 30406,
            },
            new NodeInfo
            {
                NodeId = 30407,
            },
            new NodeInfo
            {
                NodeId = 30408,
            },
        };
    }
}
