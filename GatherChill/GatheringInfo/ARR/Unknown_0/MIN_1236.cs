using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1236 : RouteInfo
    {
        public override uint Id => 1236;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-75.1615f, 525.485f);
        public override int Radius => 65;

        public override HashSet<uint> NodeIds => new()
        {
            35333,
            35334,
            35335,
            35336,
            35337,
            35338,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47368,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35333,
            },
            new NodeInfo
            {
                NodeId = 35334,
            },
            new NodeInfo
            {
                NodeId = 35335,
            },
            new NodeInfo
            {
                NodeId = 35336,
            },
            new NodeInfo
            {
                NodeId = 35337,
            },
            new NodeInfo
            {
                NodeId = 35338,
            },
        };
    }
}
