using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_121 : RouteInfo
    {
        public override uint Id => 121;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(75.5084f, 436.462f);
        public override int Radius => 56;

        public override HashSet<uint> NodeIds => new()
        {
            30898,
            30899,
            30900,
            30901,
            30902,
            30903,
            30904,
            30905,
            30906,
            30907,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000893,
            2000894,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30898,
            },
            new NodeInfo
            {
                NodeId = 30899,
            },
            new NodeInfo
            {
                NodeId = 30900,
            },
            new NodeInfo
            {
                NodeId = 30901,
            },
            new NodeInfo
            {
                NodeId = 30902,
            },
            new NodeInfo
            {
                NodeId = 30903,
            },
            new NodeInfo
            {
                NodeId = 30904,
            },
            new NodeInfo
            {
                NodeId = 30905,
            },
            new NodeInfo
            {
                NodeId = 30906,
            },
            new NodeInfo
            {
                NodeId = 30907,
            },
        };
    }
}
