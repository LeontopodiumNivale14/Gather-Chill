using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CentralShroud_148
{
    public class BTN_249 : RouteInfo
    {
        public override uint Id => 249;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 148;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(165.278f, 406.083f);
        public override int Radius => 75;

        public override HashSet<uint> NodeIds => new()
        {
            31057,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9518,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31057,
            },
        };
    }
}
