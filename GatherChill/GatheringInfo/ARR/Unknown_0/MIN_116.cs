using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_116 : RouteInfo
    {
        public override uint Id => 116;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(310.712f, -188.364f);
        public override int Radius => 33;

        public override HashSet<uint> NodeIds => new()
        {
            30854,
            30855,
            30856,
            30857,
            30858,
            30859,
            30860,
            30861,
            30862,
            30863,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000503,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30854,
            },
            new NodeInfo
            {
                NodeId = 30855,
            },
            new NodeInfo
            {
                NodeId = 30856,
            },
            new NodeInfo
            {
                NodeId = 30857,
            },
            new NodeInfo
            {
                NodeId = 30858,
            },
            new NodeInfo
            {
                NodeId = 30859,
            },
            new NodeInfo
            {
                NodeId = 30860,
            },
            new NodeInfo
            {
                NodeId = 30861,
            },
            new NodeInfo
            {
                NodeId = 30862,
            },
            new NodeInfo
            {
                NodeId = 30863,
            },
        };
    }
}
