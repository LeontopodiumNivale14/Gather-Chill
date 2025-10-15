using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.AmhAraeng_815
{
    public class BTN_630 : RouteInfo
    {
        public override uint Id => 630;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 815;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-108.142f, -228.658f);
        public override int Radius => 30;

        public override HashSet<uint> NodeIds => new()
        {
            32769,
        };

        public override HashSet<uint> ItemIds => new()
        {
            27822,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32769,
            },
        };
    }
}
