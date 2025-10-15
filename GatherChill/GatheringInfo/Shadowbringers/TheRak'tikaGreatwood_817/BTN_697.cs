using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheRaktikaGreatwood_817
{
    public class BTN_697 : RouteInfo
    {
        public override uint Id => 697;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 817;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(-872.934f, 439.087f);
        public override int Radius => 17;

        public override HashSet<uint> NodeIds => new()
        {
            33034,
            33035,
            33036,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33034,
            },
            new NodeInfo
            {
                NodeId = 33035,
            },
            new NodeInfo
            {
                NodeId = 33036,
            },
        };
    }
}
