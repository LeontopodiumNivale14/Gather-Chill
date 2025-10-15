using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_373 : RouteInfo
    {
        public override uint Id => 373;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(675.651f, -204.936f);
        public override int Radius => 86;

        public override HashSet<uint> NodeIds => new()
        {
            31535,
            31537,
            31567,
            31569,
            31599,
            31601,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            12878,
            12879,
            13753,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31535,
            },
            new NodeInfo
            {
                NodeId = 31537,
            },
            new NodeInfo
            {
                NodeId = 31567,
            },
            new NodeInfo
            {
                NodeId = 31569,
            },
            new NodeInfo
            {
                NodeId = 31599,
            },
            new NodeInfo
            {
                NodeId = 31601,
            },
        };
    }
}
