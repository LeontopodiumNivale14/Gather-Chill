using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_573 : RouteInfo
    {
        public override uint Id => 573;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-209.338f, 487.62f);
        public override int Radius => 46;

        public override HashSet<uint> NodeIds => new()
        {
            32490,
            32491,
            32492,
            32493,
            32494,
            32495,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002780,
            2002781,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32490,
            },
            new NodeInfo
            {
                NodeId = 32491,
            },
            new NodeInfo
            {
                NodeId = 32492,
            },
            new NodeInfo
            {
                NodeId = 32493,
            },
            new NodeInfo
            {
                NodeId = 32494,
            },
            new NodeInfo
            {
                NodeId = 32495,
            },
        };
    }
}
