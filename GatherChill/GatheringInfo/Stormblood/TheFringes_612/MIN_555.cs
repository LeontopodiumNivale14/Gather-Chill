using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheFringes_612
{
    public class MIN_555 : RouteInfo
    {
        public override uint Id => 555;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 612;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(293.3f, 436.551f);
        public override int Radius => 19;

        public override HashSet<uint> NodeIds => new()
        {
            32368,
        };

        public override HashSet<uint> ItemIds => new()
        {
            24240,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32368,
            },
        };
    }
}
