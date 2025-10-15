using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_385 : RouteInfo
    {
        public override uint Id => 385;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-361.036f, -159.503f);
        public override int Radius => 35;

        public override HashSet<uint> NodeIds => new()
        {
            31625,
            31653,
            31681,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            12534,
            12535,
            12537,
            13750,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31625,
            },
            new NodeInfo
            {
                NodeId = 31653,
            },
            new NodeInfo
            {
                NodeId = 31681,
            },
        };
    }
}
