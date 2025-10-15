using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_547 : RouteInfo
    {
        public override uint Id => 547;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(587.136f, 573.199f);
        public override int Radius => 21;

        public override HashSet<uint> NodeIds => new()
        {
            32332,
            32333,
            32334,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            22645,
            22653,
            22656,
            22660,
            22662,
            22665,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32332,
            },
            new NodeInfo
            {
                NodeId = 32333,
            },
            new NodeInfo
            {
                NodeId = 32334,
            },
        };
    }
}
