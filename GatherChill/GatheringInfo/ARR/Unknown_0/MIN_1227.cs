using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1227 : RouteInfo
    {
        public override uint Id => 1227;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-249.493f, -21.982f);
        public override int Radius => 30;

        public override HashSet<uint> NodeIds => new()
        {
            35325,
            35326,
            35327,
            35328,
            35329,
            35330,
            35331,
            35332,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47365,
            47366,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35325,
            },
            new NodeInfo
            {
                NodeId = 35326,
            },
            new NodeInfo
            {
                NodeId = 35327,
            },
            new NodeInfo
            {
                NodeId = 35328,
            },
            new NodeInfo
            {
                NodeId = 35329,
            },
            new NodeInfo
            {
                NodeId = 35330,
            },
            new NodeInfo
            {
                NodeId = 35331,
            },
            new NodeInfo
            {
                NodeId = 35332,
            },
        };
    }
}
