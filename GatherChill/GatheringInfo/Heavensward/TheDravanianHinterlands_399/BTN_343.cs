using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianHinterlands_399
{
    public class BTN_343 : RouteInfo
    {
        public override uint Id => 343;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 399;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-325.018f, 728.596f);
        public override int Radius => 181;

        public override HashSet<uint> NodeIds => new()
        {
            31496,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12897,
            12898,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31496,
            },
        };
    }
}
