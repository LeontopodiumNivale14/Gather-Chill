using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_973 : RouteInfo
    {
        public override uint Id => 973;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(515.107f, -495.872f);
        public override int Radius => 91;

        public override HashSet<uint> NodeIds => new()
        {
            34743,
            34744,
            34745,
            34746,
            34747,
            34748,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003557,
            2003558,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34743,
            },
            new NodeInfo
            {
                NodeId = 34744,
            },
            new NodeInfo
            {
                NodeId = 34745,
            },
            new NodeInfo
            {
                NodeId = 34746,
            },
            new NodeInfo
            {
                NodeId = 34747,
            },
            new NodeInfo
            {
                NodeId = 34748,
            },
        };
    }
}
