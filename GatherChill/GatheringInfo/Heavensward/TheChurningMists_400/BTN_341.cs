using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheChurningMists_400
{
    public class BTN_341 : RouteInfo
    {
        public override uint Id => 341;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 400;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(168.413f, -686.594f);
        public override int Radius => 129;

        public override HashSet<uint> NodeIds => new()
        {
            31494,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12899,
            12943,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31494,
            },
        };
    }
}
