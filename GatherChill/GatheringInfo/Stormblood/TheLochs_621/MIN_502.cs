using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheLochs_621
{
    public class MIN_502 : RouteInfo
    {
        public override uint Id => 502;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 621;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-14.9199f, 372.378f);
        public override int Radius => 34;

        public override HashSet<uint> NodeIds => new()
        {
            32185,
        };

        public override HashSet<uint> ItemIds => new()
        {
            19907,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32185,
            },
        };
    }
}
