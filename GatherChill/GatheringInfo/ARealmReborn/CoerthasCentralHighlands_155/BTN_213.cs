using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CoerthasCentralHighlands_155
{
    public class BTN_213 : RouteInfo
    {
        public override uint Id => 213;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 155;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-650.919f, -406.714f);
        public override int Radius => 111;

        public override HashSet<uint> NodeIds => new()
        {
            31001,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5537,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31001,
            },
        };
    }
}
