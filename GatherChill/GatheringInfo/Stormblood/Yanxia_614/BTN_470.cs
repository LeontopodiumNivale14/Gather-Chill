using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.Yanxia_614
{
    public class BTN_470 : RouteInfo
    {
        public override uint Id => 470;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 614;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(19.2457f, 776.222f);
        public override int Radius => 98;

        public override HashSet<uint> NodeIds => new()
        {
            32089,
            32090,
            32091,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32089,
            },
            new NodeInfo
            {
                NodeId = 32090,
            },
            new NodeInfo
            {
                NodeId = 32091,
            },
        };
    }
}
