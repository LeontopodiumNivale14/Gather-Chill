using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1248 : RouteInfo
    {
        public override uint Id => 1248;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(70.7767f, -373.056f);
        public override int Radius => 32;

        public override HashSet<uint> NodeIds => new()
        {
            35375,
            35376,
            35377,
            35378,
            35379,
            35380,
            35381,
            35382,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47385,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35375,
            },
            new NodeInfo
            {
                NodeId = 35376,
            },
            new NodeInfo
            {
                NodeId = 35377,
            },
            new NodeInfo
            {
                NodeId = 35378,
            },
            new NodeInfo
            {
                NodeId = 35379,
            },
            new NodeInfo
            {
                NodeId = 35380,
            },
            new NodeInfo
            {
                NodeId = 35381,
            },
            new NodeInfo
            {
                NodeId = 35382,
            },
        };
    }
}
