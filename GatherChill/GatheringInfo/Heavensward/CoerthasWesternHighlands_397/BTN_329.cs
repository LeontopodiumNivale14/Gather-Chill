using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
    public class BTN_329 : RouteInfo
    {
        public override uint Id => 329;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 397;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(153.652f, 339.106f);
        public override int Radius => 141;

        public override HashSet<uint> NodeIds => new()
        {
            31472,
            31474,
            31475,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12971,
            12972,
            12973,
            15948,
            33148,
            33149,
            33150,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31472,
            },
            new NodeInfo
            {
                NodeId = 31474,
            },
            new NodeInfo
            {
                NodeId = 31475,
            },
        };
    }
}
