using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_963 : RouteInfo
    {
        public override uint Id => 963;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(615.725f, 718.348f);
        public override int Radius => 80;

        public override HashSet<uint> NodeIds => new()
        {
            34659,
            34660,
            34661,
            34662,
            34663,
            34664,
            34665,
            34666,
            34667,
            34668,
            34669,
            34670,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003543,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34659,
            },
            new NodeInfo
            {
                NodeId = 34660,
            },
            new NodeInfo
            {
                NodeId = 34661,
            },
            new NodeInfo
            {
                NodeId = 34662,
            },
            new NodeInfo
            {
                NodeId = 34663,
            },
            new NodeInfo
            {
                NodeId = 34664,
            },
            new NodeInfo
            {
                NodeId = 34665,
            },
            new NodeInfo
            {
                NodeId = 34666,
            },
            new NodeInfo
            {
                NodeId = 34667,
            },
            new NodeInfo
            {
                NodeId = 34668,
            },
            new NodeInfo
            {
                NodeId = 34669,
            },
            new NodeInfo
            {
                NodeId = 34670,
            },
        };
    }
}
