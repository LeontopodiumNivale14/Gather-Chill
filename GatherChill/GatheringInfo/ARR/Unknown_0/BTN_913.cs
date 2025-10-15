using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_913 : RouteInfo
    {
        public override uint Id => 913;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(385.151f, -432.547f);
        public override int Radius => 40;

        public override HashSet<uint> NodeIds => new()
        {
            34388,
            34389,
            34390,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            38304,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34388,
            },
            new NodeInfo
            {
                NodeId = 34389,
            },
            new NodeInfo
            {
                NodeId = 34390,
            },
        };
    }
}
