using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1062 : RouteInfo
    {
        public override uint Id => 1062;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(500.782f, 673.023f);
        public override int Radius => 59;

        public override HashSet<uint> NodeIds => new()
        {
            35151,
            35152,
            35153,
            35154,
            35155,
            35156,
            35157,
            35158,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35151,
            },
            new NodeInfo
            {
                NodeId = 35152,
            },
            new NodeInfo
            {
                NodeId = 35153,
            },
            new NodeInfo
            {
                NodeId = 35154,
            },
            new NodeInfo
            {
                NodeId = 35155,
            },
            new NodeInfo
            {
                NodeId = 35156,
            },
            new NodeInfo
            {
                NodeId = 35157,
            },
            new NodeInfo
            {
                NodeId = 35158,
            },
        };
    }
}
