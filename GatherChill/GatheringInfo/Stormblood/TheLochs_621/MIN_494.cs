using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheLochs_621
{
    public class MIN_494 : RouteInfo
    {
        public override uint Id => 494;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 621;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-412.068f, 294.312f);
        public override int Radius => 124;

        public override HashSet<uint> NodeIds => new()
        {
            31499,
            31506,
            32172,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            20011,
            23220,
            33153,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31499,
            },
            new NodeInfo
            {
                NodeId = 31506,
            },
            new NodeInfo
            {
                NodeId = 32172,
            },
        };
    }
}
