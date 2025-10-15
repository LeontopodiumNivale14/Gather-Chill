using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_864 : RouteInfo
    {
        public override uint Id => 864;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(63.7854f, 623.437f);
        public override int Radius => 19;

        public override HashSet<uint> NodeIds => new()
        {
            34100,
            34101,
            34102,
            34103,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003139,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34100,
            },
            new NodeInfo
            {
                NodeId = 34101,
            },
            new NodeInfo
            {
                NodeId = 34102,
            },
            new NodeInfo
            {
                NodeId = 34103,
            },
        };
    }
}
