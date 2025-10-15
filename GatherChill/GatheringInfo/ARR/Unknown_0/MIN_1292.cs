using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1292 : RouteInfo
    {
        public override uint Id => 1292;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(183.692f, 626.088f);
        public override int Radius => 29;

        public override HashSet<uint> NodeIds => new()
        {
            35363,
            35364,
            35365,
            35366,
            35367,
            35368,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47418,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35363,
            },
            new NodeInfo
            {
                NodeId = 35364,
            },
            new NodeInfo
            {
                NodeId = 35365,
            },
            new NodeInfo
            {
                NodeId = 35366,
            },
            new NodeInfo
            {
                NodeId = 35367,
            },
            new NodeInfo
            {
                NodeId = 35368,
            },
        };
    }
}
