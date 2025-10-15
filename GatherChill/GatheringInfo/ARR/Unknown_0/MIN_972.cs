using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_972 : RouteInfo
    {
        public override uint Id => 972;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(146.906f, -85.1267f);
        public override int Radius => 32;

        public override HashSet<uint> NodeIds => new()
        {
            34739,
            34740,
            34741,
            34742,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003556,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34739,
            },
            new NodeInfo
            {
                NodeId = 34740,
            },
            new NodeInfo
            {
                NodeId = 34741,
            },
            new NodeInfo
            {
                NodeId = 34742,
            },
        };
    }
}
