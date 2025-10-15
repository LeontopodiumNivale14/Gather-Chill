using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_115 : RouteInfo
    {
        public override uint Id => 115;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(183.238f, -52.5357f);
        public override int Radius => 49;

        public override HashSet<uint> NodeIds => new()
        {
            30844,
            30845,
            30846,
            30847,
            30848,
            30849,
            30850,
            30851,
            30852,
            30853,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000502,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30844,
            },
            new NodeInfo
            {
                NodeId = 30845,
            },
            new NodeInfo
            {
                NodeId = 30846,
            },
            new NodeInfo
            {
                NodeId = 30847,
            },
            new NodeInfo
            {
                NodeId = 30848,
            },
            new NodeInfo
            {
                NodeId = 30849,
            },
            new NodeInfo
            {
                NodeId = 30850,
            },
            new NodeInfo
            {
                NodeId = 30851,
            },
            new NodeInfo
            {
                NodeId = 30852,
            },
            new NodeInfo
            {
                NodeId = 30853,
            },
        };
    }
}
