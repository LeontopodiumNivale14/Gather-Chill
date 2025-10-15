using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.SouthShroud_153
{
    public class BTN_242 : RouteInfo
    {
        public override uint Id => 242;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 153;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-200.202f, 78.699f);
        public override int Radius => 46;

        public override HashSet<uint> NodeIds => new()
        {
            31036,
        };

        public override HashSet<uint> ItemIds => new()
        {
            7611,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31036,
            },
        };
    }
}
