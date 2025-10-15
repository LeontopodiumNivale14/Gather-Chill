using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_82 : RouteInfo
    {
        public override uint Id => 82;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(400.885f, 329.546f);
        public override int Radius => 35;

        public override HashSet<uint> NodeIds => new()
        {
            30604,
            30605,
            30606,
            30607,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000913,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30604,
            },
            new NodeInfo
            {
                NodeId = 30605,
            },
            new NodeInfo
            {
                NodeId = 30606,
            },
            new NodeInfo
            {
                NodeId = 30607,
            },
        };
    }
}
