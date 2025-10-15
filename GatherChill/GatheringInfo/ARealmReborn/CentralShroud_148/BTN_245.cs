using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CentralShroud_148
{
    public class BTN_245 : RouteInfo
    {
        public override uint Id => 245;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 148;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(454f, -65.9229f);
        public override int Radius => 53;

        public override HashSet<uint> NodeIds => new()
        {
            31053,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8031,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31053,
            },
        };
    }
}
