using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheLochs_621
{
    public class BTN_898 : RouteInfo
    {
        public override uint Id => 898;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 621;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(71.57f, -210.506f);
        public override int Radius => 102;

        public override HashSet<uint> NodeIds => new()
        {
            34329,
            34330,
            34331,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34329,
            },
            new NodeInfo
            {
                NodeId = 34330,
            },
            new NodeInfo
            {
                NodeId = 34331,
            },
        };
    }
}
