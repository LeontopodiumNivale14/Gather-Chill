using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1295 : RouteInfo
    {
        public override uint Id => 1295;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(152.852f, 284.393f);
        public override int Radius => 28;

        public override HashSet<uint> NodeIds => new()
        {
            35453,
            35454,
            35455,
            35456,
            35457,
            35458,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47421,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35453,
            },
            new NodeInfo
            {
                NodeId = 35454,
            },
            new NodeInfo
            {
                NodeId = 35455,
            },
            new NodeInfo
            {
                NodeId = 35456,
            },
            new NodeInfo
            {
                NodeId = 35457,
            },
            new NodeInfo
            {
                NodeId = 35458,
            },
        };
    }
}
