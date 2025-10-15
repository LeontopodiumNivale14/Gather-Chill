using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_405 : RouteInfo
    {
        public override uint Id => 405;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(512.239f, -594.41f);
        public override int Radius => 102;

        public override HashSet<uint> NodeIds => new()
        {
            31730,
            31731,
            31732,
            31733,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5,
            11,
            7592,
            13752,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31730,
            },
            new NodeInfo
            {
                NodeId = 31731,
            },
            new NodeInfo
            {
                NodeId = 31732,
            },
            new NodeInfo
            {
                NodeId = 31733,
            },
        };
    }
}
