using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_888 : RouteInfo
    {
        public override uint Id => 888;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(197.407f, 587.234f);
        public override int Radius => 17;

        public override HashSet<uint> NodeIds => new()
        {
            34296,
            34297,
            34298,
            34299,
            34300,
            34301,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003175,
            2003176,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34296,
            },
            new NodeInfo
            {
                NodeId = 34297,
            },
            new NodeInfo
            {
                NodeId = 34298,
            },
            new NodeInfo
            {
                NodeId = 34299,
            },
            new NodeInfo
            {
                NodeId = 34300,
            },
            new NodeInfo
            {
                NodeId = 34301,
            },
        };
    }
}
