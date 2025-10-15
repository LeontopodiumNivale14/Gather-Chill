using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_456 : RouteInfo
    {
        public override uint Id => 456;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(390.847f, 348.686f);
        public override int Radius => 55;

        public override HashSet<uint> NodeIds => new()
        {
            32013,
            32014,
            32015,
            32016,
            32017,
            32018,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002155,
            2002156,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32013,
            },
            new NodeInfo
            {
                NodeId = 32014,
            },
            new NodeInfo
            {
                NodeId = 32015,
            },
            new NodeInfo
            {
                NodeId = 32016,
            },
            new NodeInfo
            {
                NodeId = 32017,
            },
            new NodeInfo
            {
                NodeId = 32018,
            },
        };
    }
}
