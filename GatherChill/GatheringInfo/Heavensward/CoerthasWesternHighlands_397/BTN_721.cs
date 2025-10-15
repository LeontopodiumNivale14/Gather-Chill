using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
    public class BTN_721 : RouteInfo
    {
        public override uint Id => 721;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 397;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-598.041f, -202.77f);
        public override int Radius => 60;

        public override HashSet<uint> NodeIds => new()
        {
            33271,
            33272,
            33273,
            33274,
            33275,
            33276,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29674,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33271,
            },
            new NodeInfo
            {
                NodeId = 33272,
            },
            new NodeInfo
            {
                NodeId = 33273,
            },
            new NodeInfo
            {
                NodeId = 33274,
            },
            new NodeInfo
            {
                NodeId = 33275,
            },
            new NodeInfo
            {
                NodeId = 33276,
            },
        };
    }
}
