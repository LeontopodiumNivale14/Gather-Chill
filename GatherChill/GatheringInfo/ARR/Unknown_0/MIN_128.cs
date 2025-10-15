using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_128 : RouteInfo
    {
        public override uint Id => 128;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-579.134f, -353.137f);
        public override int Radius => 84;

        public override HashSet<uint> NodeIds => new()
        {
            30952,
            30953,
            30954,
            30955,
            30956,
            30957,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000903,
            2000904,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30952,
            },
            new NodeInfo
            {
                NodeId = 30953,
            },
            new NodeInfo
            {
                NodeId = 30954,
            },
            new NodeInfo
            {
                NodeId = 30955,
            },
            new NodeInfo
            {
                NodeId = 30956,
            },
            new NodeInfo
            {
                NodeId = 30957,
            },
        };
    }
}
