using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_871 : RouteInfo
    {
        public override uint Id => 871;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-92.7063f, -234.137f);
        public override int Radius => 36;

        public override HashSet<uint> NodeIds => new()
        {
            34156,
            34157,
            34158,
            34159,
            34160,
            34161,
            34162,
            34163,
            34164,
            34165,
            34166,
            34167,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003149,
            2003150,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34156,
            },
            new NodeInfo
            {
                NodeId = 34157,
            },
            new NodeInfo
            {
                NodeId = 34158,
            },
            new NodeInfo
            {
                NodeId = 34159,
            },
            new NodeInfo
            {
                NodeId = 34160,
            },
            new NodeInfo
            {
                NodeId = 34161,
            },
            new NodeInfo
            {
                NodeId = 34162,
            },
            new NodeInfo
            {
                NodeId = 34163,
            },
            new NodeInfo
            {
                NodeId = 34164,
            },
            new NodeInfo
            {
                NodeId = 34165,
            },
            new NodeInfo
            {
                NodeId = 34166,
            },
            new NodeInfo
            {
                NodeId = 34167,
            },
        };
    }
}
