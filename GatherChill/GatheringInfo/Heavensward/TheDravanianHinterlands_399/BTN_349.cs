using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianHinterlands_399
{
    public class BTN_349 : RouteInfo
    {
        public override uint Id => 349;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 399;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(816.709f, 61.4744f);
        public override int Radius => 133;

        public override HashSet<uint> NodeIds => new()
        {
            31502,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12904,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31502,
            },
        };
    }
}
