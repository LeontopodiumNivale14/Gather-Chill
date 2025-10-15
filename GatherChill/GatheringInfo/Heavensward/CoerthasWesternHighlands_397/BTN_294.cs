using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
    public class BTN_294 : RouteInfo
    {
        public override uint Id => 294;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 397;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(445.474f, 467.94f);
        public override int Radius => 119;

        public override HashSet<uint> NodeIds => new()
        {
            31370,
            31371,
            31372,
            31373,
            31374,
            31375,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            5384,
            5403,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31370,
            },
            new NodeInfo
            {
                NodeId = 31371,
            },
            new NodeInfo
            {
                NodeId = 31372,
            },
            new NodeInfo
            {
                NodeId = 31373,
            },
            new NodeInfo
            {
                NodeId = 31374,
            },
            new NodeInfo
            {
                NodeId = 31375,
            },
        };
    }
}
