using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
    public class MIN_284 : RouteInfo
    {
        public override uint Id => 284;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 397;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(338.778f, 276.86f);
        public override int Radius => 156;

        public override HashSet<uint> NodeIds => new()
        {
            31310,
            31311,
            31312,
            31313,
            31314,
            31315,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            5108,
            5126,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31310,
            },
            new NodeInfo
            {
                NodeId = 31311,
            },
            new NodeInfo
            {
                NodeId = 31312,
            },
            new NodeInfo
            {
                NodeId = 31313,
            },
            new NodeInfo
            {
                NodeId = 31314,
            },
            new NodeInfo
            {
                NodeId = 31315,
            },
        };
    }
}
