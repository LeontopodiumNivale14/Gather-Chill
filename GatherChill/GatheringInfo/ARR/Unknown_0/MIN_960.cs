using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_960 : RouteInfo
    {
        public override uint Id => 960;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(519.541f, -83.2815f);
        public override int Radius => 56;

        public override HashSet<uint> NodeIds => new()
        {
            34637,
            34638,
            34639,
            34640,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003538,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34637,
            },
            new NodeInfo
            {
                NodeId = 34638,
            },
            new NodeInfo
            {
                NodeId = 34639,
            },
            new NodeInfo
            {
                NodeId = 34640,
            },
        };
    }
}
