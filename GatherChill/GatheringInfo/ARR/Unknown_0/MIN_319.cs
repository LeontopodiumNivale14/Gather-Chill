using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_319 : RouteInfo
    {
        public override uint Id => 319;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(14.653f, -139.044f);
        public override int Radius => 5;

        public override HashSet<uint> NodeIds => new()
        {
            33643,
            33840,
            33841,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            38287,
            38289,
            38299,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33643,
            },
            new NodeInfo
            {
                NodeId = 33840,
            },
            new NodeInfo
            {
                NodeId = 33841,
            },
        };
    }
}
