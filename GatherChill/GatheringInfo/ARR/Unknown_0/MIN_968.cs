using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_968 : RouteInfo
    {
        public override uint Id => 968;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-662.23f, 295.738f);
        public override int Radius => 67;

        public override HashSet<uint> NodeIds => new()
        {
            34705,
            34706,
            34707,
            34708,
            34709,
            34710,
            34711,
            34712,
            34713,
            34714,
            34715,
            34716,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003550,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34705,
            },
            new NodeInfo
            {
                NodeId = 34706,
            },
            new NodeInfo
            {
                NodeId = 34707,
            },
            new NodeInfo
            {
                NodeId = 34708,
            },
            new NodeInfo
            {
                NodeId = 34709,
            },
            new NodeInfo
            {
                NodeId = 34710,
            },
            new NodeInfo
            {
                NodeId = 34711,
            },
            new NodeInfo
            {
                NodeId = 34712,
            },
            new NodeInfo
            {
                NodeId = 34713,
            },
            new NodeInfo
            {
                NodeId = 34714,
            },
            new NodeInfo
            {
                NodeId = 34715,
            },
            new NodeInfo
            {
                NodeId = 34716,
            },
        };
    }
}
