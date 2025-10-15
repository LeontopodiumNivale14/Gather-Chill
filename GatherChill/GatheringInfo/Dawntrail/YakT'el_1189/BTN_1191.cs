using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.YakTel_1189
{
    public class BTN_1191 : RouteInfo
    {
        public override uint Id => 1191;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1189;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(713.242f, 42.3512f);
        public override int Radius => 111;

        public override HashSet<uint> NodeIds => new()
        {
            35240,
        };

        public override HashSet<uint> ItemIds => new()
        {
            45968,
            46243,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35240,
            },
        };
    }
}
