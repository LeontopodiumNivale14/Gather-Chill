using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1066 : RouteInfo
    {
        public override uint Id => 1066;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(449.616f, 245.454f);
        public override int Radius => 82;

        public override HashSet<uint> NodeIds => new()
        {
            35177,
            35178,
            35179,
            35180,
            35181,
            35182,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35177,
            },
            new NodeInfo
            {
                NodeId = 35178,
            },
            new NodeInfo
            {
                NodeId = 35179,
            },
            new NodeInfo
            {
                NodeId = 35180,
            },
            new NodeInfo
            {
                NodeId = 35181,
            },
            new NodeInfo
            {
                NodeId = 35182,
            },
        };
    }
}
