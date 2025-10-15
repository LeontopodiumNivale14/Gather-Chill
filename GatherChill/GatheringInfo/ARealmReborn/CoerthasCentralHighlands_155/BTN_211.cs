using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CoerthasCentralHighlands_155
{
    public class BTN_211 : RouteInfo
    {
        public override uint Id => 211;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 155;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(383.402f, -434.948f);
        public override int Radius => 89;

        public override HashSet<uint> NodeIds => new()
        {
            30999,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5395,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30999,
            },
        };
    }
}
