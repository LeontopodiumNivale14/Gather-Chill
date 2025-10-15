using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_416 : RouteInfo
    {
        public override uint Id => 416;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(52.6243f, 579.536f);
        public override int Radius => 181;

        public override HashSet<uint> NodeIds => new()
        {
            31765,
            31766,
            31767,
            31768,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2,
            8,
            7589,
            12534,
            12535,
            12537,
            13750,
            17570,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31765,
            },
            new NodeInfo
            {
                NodeId = 31766,
            },
            new NodeInfo
            {
                NodeId = 31767,
            },
            new NodeInfo
            {
                NodeId = 31768,
            },
        };
    }
}
