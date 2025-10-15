using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_692 : RouteInfo
    {
        public override uint Id => 692;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(451.089f, -492.143f);
        public override int Radius => 31;

        public override HashSet<uint> NodeIds => new()
        {
            33012,
            33013,
            33014,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            29540,
            29544,
            29550,
            29555,
            29559,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33012,
            },
            new NodeInfo
            {
                NodeId = 33013,
            },
            new NodeInfo
            {
                NodeId = 33014,
            },
        };
    }
}
