using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_73 : RouteInfo
    {
        public override uint Id => 73;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(198.409f, -48.3962f);
        public override int Radius => 33;

        public override HashSet<uint> NodeIds => new()
        {
            30278,
            30279,
            30280,
            30281,
            30282,
            30283,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000294,
            2000295,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30278,
            },
            new NodeInfo
            {
                NodeId = 30279,
            },
            new NodeInfo
            {
                NodeId = 30280,
            },
            new NodeInfo
            {
                NodeId = 30281,
            },
            new NodeInfo
            {
                NodeId = 30282,
            },
            new NodeInfo
            {
                NodeId = 30283,
            },
        };
    }
}
