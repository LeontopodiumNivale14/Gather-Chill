using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_280 : RouteInfo
    {
        public override uint Id => 280;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(147.383f, 5.12694f);
        public override int Radius => 78;

        public override HashSet<uint> NodeIds => new()
        {
            31279,
            31280,
            31281,
            31282,
            31283,
            31284,
            31285,
            31286,
            31287,
            31288,
            31289,
            31290,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001840,
            2001841,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31279,
            },
            new NodeInfo
            {
                NodeId = 31280,
            },
            new NodeInfo
            {
                NodeId = 31281,
            },
            new NodeInfo
            {
                NodeId = 31282,
            },
            new NodeInfo
            {
                NodeId = 31283,
            },
            new NodeInfo
            {
                NodeId = 31284,
            },
            new NodeInfo
            {
                NodeId = 31285,
            },
            new NodeInfo
            {
                NodeId = 31286,
            },
            new NodeInfo
            {
                NodeId = 31287,
            },
            new NodeInfo
            {
                NodeId = 31288,
            },
            new NodeInfo
            {
                NodeId = 31289,
            },
            new NodeInfo
            {
                NodeId = 31290,
            },
        };
    }
}
