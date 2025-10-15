using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_402 : RouteInfo
    {
        public override uint Id => 402;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(84.551f, 555.866f);
        public override int Radius => 216;

        public override HashSet<uint> NodeIds => new()
        {
            31718,
            31719,
            31720,
            31721,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5,
            11,
            4816,
            5395,
            13752,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31718,
            },
            new NodeInfo
            {
                NodeId = 31719,
            },
            new NodeInfo
            {
                NodeId = 31720,
            },
            new NodeInfo
            {
                NodeId = 31721,
            },
        };
    }
}
