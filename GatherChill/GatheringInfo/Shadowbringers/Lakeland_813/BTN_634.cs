using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
    public class BTN_634 : RouteInfo
    {
        public override uint Id => 634;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 813;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(248.998f, -569.104f);
        public override int Radius => 27;

        public override HashSet<uint> NodeIds => new()
        {
            32773,
        };

        public override HashSet<uint> ItemIds => new()
        {
            27761,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32773,
            },
        };
    }
}
