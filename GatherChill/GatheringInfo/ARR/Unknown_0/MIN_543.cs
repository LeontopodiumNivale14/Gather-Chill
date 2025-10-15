using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_543 : RouteInfo
    {
        public override uint Id => 543;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(602.863f, 566.162f);
        public override int Radius => 15;

        public override HashSet<uint> NodeIds => new()
        {
            32320,
            32321,
            32322,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            22619,
            22627,
            22630,
            22634,
            22636,
            22639,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32320,
            },
            new NodeInfo
            {
                NodeId = 32321,
            },
            new NodeInfo
            {
                NodeId = 32322,
            },
        };
    }
}
