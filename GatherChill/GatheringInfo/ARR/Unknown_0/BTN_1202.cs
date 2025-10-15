using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1202 : RouteInfo
    {
        public override uint Id => 1202;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-140.921f, 343.906f);
        public override int Radius => 8;

        public override HashSet<uint> NodeIds => new()
        {
            35267,
            35268,
            35269,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            48019,
            48028,
            48046,
            48055,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35267,
            },
            new NodeInfo
            {
                NodeId = 35268,
            },
            new NodeInfo
            {
                NodeId = 35269,
            },
        };
    }
}
