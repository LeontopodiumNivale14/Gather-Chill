using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_69 : RouteInfo
    {
        public override uint Id => 69;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(69.9732f, 1.05508f);
        public override int Radius => 48;

        public override HashSet<uint> NodeIds => new()
        {
            30256,
            30257,
            30258,
            30259,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000307,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30256,
            },
            new NodeInfo
            {
                NodeId = 30257,
            },
            new NodeInfo
            {
                NodeId = 30258,
            },
            new NodeInfo
            {
                NodeId = 30259,
            },
        };
    }
}
