using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.Yanxia_614
{
    public class MIN_735 : RouteInfo
    {
        public override uint Id => 735;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 614;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-457.717f, -559.863f);
        public override int Radius => 110;

        public override HashSet<uint> NodeIds => new()
        {
            33350,
            33351,
            33352,
            33353,
            33354,
            33355,
        };

        public override HashSet<uint> ItemIds => new()
        {
            31127,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33350,
            },
            new NodeInfo
            {
                NodeId = 33351,
            },
            new NodeInfo
            {
                NodeId = 33352,
            },
            new NodeInfo
            {
                NodeId = 33353,
            },
            new NodeInfo
            {
                NodeId = 33354,
            },
            new NodeInfo
            {
                NodeId = 33355,
            },
        };
    }
}
