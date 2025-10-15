using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheChurningMists_400
{
    public class BTN_299 : RouteInfo
    {
        public override uint Id => 299;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 400;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-608.503f, 777.507f);
        public override int Radius => 91;

        public override HashSet<uint> NodeIds => new()
        {
            31709,
        };

        public override HashSet<uint> ItemIds => new()
        {
            16722,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31709,
            },
        };
    }
}
