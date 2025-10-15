using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
    public class BTN_344 : RouteInfo
    {
        public override uint Id => 344;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 397;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-689.589f, -606.489f);
        public override int Radius => 35;

        public override HashSet<uint> NodeIds => new()
        {
            31497,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12900,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31497,
            },
        };
    }
}
