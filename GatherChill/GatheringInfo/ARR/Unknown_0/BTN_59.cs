using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_59 : RouteInfo
    {
        public override uint Id => 59;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(89.4464f, 264.208f);
        public override int Radius => 31;

        public override HashSet<uint> NodeIds => new()
        {
            30214,
            30215,
            30216,
            30217,
            30218,
            30219,
            30220,
            30221,
            30222,
            30223,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000285,
            2000287,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30214,
            },
            new NodeInfo
            {
                NodeId = 30215,
            },
            new NodeInfo
            {
                NodeId = 30216,
            },
            new NodeInfo
            {
                NodeId = 30217,
            },
            new NodeInfo
            {
                NodeId = 30218,
            },
            new NodeInfo
            {
                NodeId = 30219,
            },
            new NodeInfo
            {
                NodeId = 30220,
            },
            new NodeInfo
            {
                NodeId = 30221,
            },
            new NodeInfo
            {
                NodeId = 30222,
            },
            new NodeInfo
            {
                NodeId = 30223,
            },
        };
    }
}
