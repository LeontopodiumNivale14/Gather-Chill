using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_965 : RouteInfo
    {
        public override uint Id => 965;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-447.56f, -107.945f);
        public override int Radius => 215;

        public override HashSet<uint> NodeIds => new()
        {
            34675,
            34676,
            34677,
            34678,
            34679,
            34680,
            34681,
            34682,
            34683,
            34684,
            34685,
            34686,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003545,
            2003546,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34675,
            },
            new NodeInfo
            {
                NodeId = 34676,
            },
            new NodeInfo
            {
                NodeId = 34677,
            },
            new NodeInfo
            {
                NodeId = 34678,
            },
            new NodeInfo
            {
                NodeId = 34679,
            },
            new NodeInfo
            {
                NodeId = 34680,
            },
            new NodeInfo
            {
                NodeId = 34681,
            },
            new NodeInfo
            {
                NodeId = 34682,
            },
            new NodeInfo
            {
                NodeId = 34683,
            },
            new NodeInfo
            {
                NodeId = 34684,
            },
            new NodeInfo
            {
                NodeId = 34685,
            },
            new NodeInfo
            {
                NodeId = 34686,
            },
        };
    }
}
