using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EasternLaNoscea_137
{
    public class BTN_220 : RouteInfo
    {
        public override uint Id => 220;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 137;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-197.246f, 304.557f);
        public override int Radius => 82;

        public override HashSet<uint> NodeIds => new()
        {
            31008,
        };

        public override HashSet<uint> ItemIds => new()
        {
            6148,
            7724,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31008,
            },
        };
    }
}
