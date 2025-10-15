using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_195 : RouteInfo
    {
        public override uint Id => 195;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(108.052f, 140.477f);
        public override int Radius => 56;

        public override HashSet<uint> NodeIds => new()
        {
            30345,
            30346,
            30347,
            30348,
        };

        public override HashSet<uint> ItemIds => new()
        {
            3,
            9,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30345,
            },
            new NodeInfo
            {
                NodeId = 30346,
            },
            new NodeInfo
            {
                NodeId = 30347,
            },
            new NodeInfo
            {
                NodeId = 30348,
            },
        };
    }
}
