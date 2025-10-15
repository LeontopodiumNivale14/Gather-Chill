using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.Yanxia_614
{
    public class BTN_558 : RouteInfo
    {
        public override uint Id => 558;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 614;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(342.248f, 692.747f);
        public override int Radius => 22;

        public override HashSet<uint> NodeIds => new()
        {
            32371,
        };

        public override HashSet<uint> ItemIds => new()
        {
            24243,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32371,
            },
        };
    }
}
