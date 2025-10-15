using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_64 : RouteInfo
    {
        public override uint Id => 64;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(26.252f, 393.216f);
        public override int Radius => 48;

        public override HashSet<uint> NodeIds => new()
        {
            30130,
            30131,
            30132,
            30133,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000296,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30130,
            },
            new NodeInfo
            {
                NodeId = 30131,
            },
            new NodeInfo
            {
                NodeId = 30132,
            },
            new NodeInfo
            {
                NodeId = 30133,
            },
        };
    }
}
