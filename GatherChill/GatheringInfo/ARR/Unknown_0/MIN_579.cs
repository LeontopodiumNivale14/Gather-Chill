using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_579 : RouteInfo
    {
        public override uint Id => 579;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-585.726f, 185.377f);
        public override int Radius => 87;

        public override HashSet<uint> NodeIds => new()
        {
            32542,
            32543,
            32544,
            32545,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002790,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32542,
            },
            new NodeInfo
            {
                NodeId = 32543,
            },
            new NodeInfo
            {
                NodeId = 32544,
            },
            new NodeInfo
            {
                NodeId = 32545,
            },
        };
    }
}
