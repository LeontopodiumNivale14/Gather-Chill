using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
    public class MIN_498 : RouteInfo
    {
        public override uint Id => 498;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 613;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-328.779f, -822.012f);
        public override int Radius => 41;

        public override HashSet<uint> NodeIds => new()
        {
            32181,
        };

        public override HashSet<uint> ItemIds => new()
        {
            19970,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32181,
            },
        };
    }
}
