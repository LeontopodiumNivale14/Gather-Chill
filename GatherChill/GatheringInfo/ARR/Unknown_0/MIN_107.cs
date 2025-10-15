using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_107 : RouteInfo
    {
        public override uint Id => 107;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-544.118f, 78.54f);
        public override int Radius => 18;

        public override HashSet<uint> NodeIds => new()
        {
            30792,
            30793,
            30794,
            30795,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000491,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30792,
            },
            new NodeInfo
            {
                NodeId = 30793,
            },
            new NodeInfo
            {
                NodeId = 30794,
            },
            new NodeInfo
            {
                NodeId = 30795,
            },
        };
    }
}
