using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EasternLaNoscea_137
{
    public class BTN_219 : RouteInfo
    {
        public override uint Id => 219;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 137;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(298.72f, 564.285f);
        public override int Radius => 72;

        public override HashSet<uint> NodeIds => new()
        {
            31007,
        };

        public override HashSet<uint> ItemIds => new()
        {
            6147,
            7733,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31007,
            },
        };
    }
}
