using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.MiddleLaNoscea_134
{
    public class BTN_248 : RouteInfo
    {
        public override uint Id => 248;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 134;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(143.702f, 246.228f);
        public override int Radius => 68;

        public override HashSet<uint> NodeIds => new()
        {
            31056,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5394,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31056,
            },
        };
    }
}
