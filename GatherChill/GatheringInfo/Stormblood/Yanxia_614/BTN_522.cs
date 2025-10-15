using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.Yanxia_614
{
    public class BTN_522 : RouteInfo
    {
        public override uint Id => 522;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 614;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(366.203f, 203.634f);
        public override int Radius => 55;

        public override HashSet<uint> NodeIds => new()
        {
            32270,
        };

        public override HashSet<uint> ItemIds => new()
        {
            19860,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32270,
            },
        };
    }
}
