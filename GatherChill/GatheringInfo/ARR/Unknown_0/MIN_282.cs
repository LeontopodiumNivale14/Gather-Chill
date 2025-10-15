using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_282 : RouteInfo
    {
        public override uint Id => 282;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(442.251f, -86.7882f);
        public override int Radius => 51;

        public override HashSet<uint> NodeIds => new()
        {
            31303,
            31304,
            31305,
            31306,
            31307,
            31308,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001843,
            2001844,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31303,
            },
            new NodeInfo
            {
                NodeId = 31304,
            },
            new NodeInfo
            {
                NodeId = 31305,
            },
            new NodeInfo
            {
                NodeId = 31306,
            },
            new NodeInfo
            {
                NodeId = 31307,
            },
            new NodeInfo
            {
                NodeId = 31308,
            },
        };
    }
}
