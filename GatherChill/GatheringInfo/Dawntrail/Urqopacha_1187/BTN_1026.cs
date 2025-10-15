using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Urqopacha_1187
{
    public class BTN_1026 : RouteInfo
    {
        public override uint Id => 1026;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1187;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-779.245f, 114.247f);
        public override int Radius => 20;

        public override HashSet<uint> NodeIds => new()
        {
            34986,
        };

        public override HashSet<uint> ItemIds => new()
        {
            43926,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34986,
            },
        };
    }
}
