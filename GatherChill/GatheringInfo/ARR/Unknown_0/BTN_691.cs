using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_691 : RouteInfo
    {
        public override uint Id => 691;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-850.276f, 215.878f);
        public override int Radius => 21;

        public override HashSet<uint> NodeIds => new()
        {
            33009,
            33010,
            33011,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            29541,
            29545,
            29552,
            29557,
            29561,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33009,
            },
            new NodeInfo
            {
                NodeId = 33010,
            },
            new NodeInfo
            {
                NodeId = 33011,
            },
        };
    }
}
