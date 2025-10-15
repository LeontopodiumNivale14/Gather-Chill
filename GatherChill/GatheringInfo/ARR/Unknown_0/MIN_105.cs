using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_105 : RouteInfo
    {
        public override uint Id => 105;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-404.473f, 337.508f);
        public override int Radius => 29;

        public override HashSet<uint> NodeIds => new()
        {
            30778,
            30779,
            30780,
            30781,
            30782,
            30783,
            30784,
            30785,
            30786,
            30787,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000488,
            2000489,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30778,
            },
            new NodeInfo
            {
                NodeId = 30779,
            },
            new NodeInfo
            {
                NodeId = 30780,
            },
            new NodeInfo
            {
                NodeId = 30781,
            },
            new NodeInfo
            {
                NodeId = 30782,
            },
            new NodeInfo
            {
                NodeId = 30783,
            },
            new NodeInfo
            {
                NodeId = 30784,
            },
            new NodeInfo
            {
                NodeId = 30785,
            },
            new NodeInfo
            {
                NodeId = 30786,
            },
            new NodeInfo
            {
                NodeId = 30787,
            },
        };
    }
}
