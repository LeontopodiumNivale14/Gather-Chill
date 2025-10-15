using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.Yanxia_614
{
    public class MIN_504 : RouteInfo
    {
        public override uint Id => 504;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 614;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-51.7989f, -589.413f);
        public override int Radius => 101;

        public override HashSet<uint> NodeIds => new()
        {
            32187,
        };

        public override HashSet<uint> ItemIds => new()
        {
            19958,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32187,
            },
        };
    }
}
