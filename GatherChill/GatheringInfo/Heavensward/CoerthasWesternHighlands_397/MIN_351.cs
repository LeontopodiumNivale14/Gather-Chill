using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
    public class MIN_351 : RouteInfo
    {
        public override uint Id => 351;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 397;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(1.10968f, 281.159f);
        public override int Radius => 159;

        public override HashSet<uint> NodeIds => new()
        {
            31340,
            31341,
            31342,
            31343,
            31344,
            31345,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            12557,
            13760,
            17558,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31340,
            },
            new NodeInfo
            {
                NodeId = 31341,
            },
            new NodeInfo
            {
                NodeId = 31342,
            },
            new NodeInfo
            {
                NodeId = 31343,
            },
            new NodeInfo
            {
                NodeId = 31344,
            },
            new NodeInfo
            {
                NodeId = 31345,
            },
        };
    }
}
