using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
    public class MIN_321 : RouteInfo
    {
        public override uint Id => 321;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 397;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(737.549f, -247.179f);
        public override int Radius => 60;

        public override HashSet<uint> NodeIds => new()
        {
            31462,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5163,
            5226,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31462,
            },
        };
    }
}
