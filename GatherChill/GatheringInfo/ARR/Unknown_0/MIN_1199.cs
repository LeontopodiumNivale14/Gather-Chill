using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1199 : RouteInfo
    {
        public override uint Id => 1199;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-606.623f, -371.146f);
        public override int Radius => 7;

        public override HashSet<uint> NodeIds => new()
        {
            35258,
            35259,
            35260,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            48021,
            48048,
            48075,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35258,
            },
            new NodeInfo
            {
                NodeId = 35259,
            },
            new NodeInfo
            {
                NodeId = 35260,
            },
        };
    }
}
