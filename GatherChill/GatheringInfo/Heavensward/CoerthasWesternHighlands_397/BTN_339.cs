using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
    public class BTN_339 : RouteInfo
    {
        public override uint Id => 339;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 397;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(428.147f, -181.035f);
        public override int Radius => 115;

        public override HashSet<uint> NodeIds => new()
        {
            31492,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12877,
            12882,
            12884,
            13767,
            13768,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31492,
            },
        };
    }
}
