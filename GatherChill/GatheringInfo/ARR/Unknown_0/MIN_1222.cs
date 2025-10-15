using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1222 : RouteInfo
    {
        public override uint Id => 1222;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(147.061f, -39.0697f);
        public override int Radius => 37;

        public override HashSet<uint> NodeIds => new()
        {
            35311,
            35312,
            35313,
            35314,
            35315,
            35316,
            35317,
            35318,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47361,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35311,
            },
            new NodeInfo
            {
                NodeId = 35312,
            },
            new NodeInfo
            {
                NodeId = 35313,
            },
            new NodeInfo
            {
                NodeId = 35314,
            },
            new NodeInfo
            {
                NodeId = 35315,
            },
            new NodeInfo
            {
                NodeId = 35316,
            },
            new NodeInfo
            {
                NodeId = 35317,
            },
            new NodeInfo
            {
                NodeId = 35318,
            },
        };
    }
}
