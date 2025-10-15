using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheLochs_621
{
    public class BTN_483 : RouteInfo
    {
        public override uint Id => 483;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 621;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(41.536f, 163.525f);
        public override int Radius => 9;

        public override HashSet<uint> NodeIds => new()
        {
            32122,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32122,
            },
        };
    }
}
