using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_365 : RouteInfo
    {
        public override uint Id => 365;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(190.395f, 488.065f);
        public override int Radius => 1;

        public override HashSet<uint> NodeIds => new()
        {
            31519,
            31521,
            31551,
            31583,
        };

        public override HashSet<uint> ItemIds => new()
        {
            11,
            12878,
            12879,
            13753,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31519,
            },
            new NodeInfo
            {
                NodeId = 31521,
            },
            new NodeInfo
            {
                NodeId = 31551,
            },
            new NodeInfo
            {
                NodeId = 31583,
            },
        };
    }
}
