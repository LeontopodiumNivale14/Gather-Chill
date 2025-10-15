using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_87 : RouteInfo
    {
        public override uint Id => 87;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-541.595f, -353.704f);
        public override int Radius => 100;

        public override HashSet<uint> NodeIds => new()
        {
            30638,
            30639,
            30640,
            30641,
            30642,
            30643,
            30644,
            30645,
            30646,
            30647,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000920,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30638,
            },
            new NodeInfo
            {
                NodeId = 30639,
            },
            new NodeInfo
            {
                NodeId = 30640,
            },
            new NodeInfo
            {
                NodeId = 30641,
            },
            new NodeInfo
            {
                NodeId = 30642,
            },
            new NodeInfo
            {
                NodeId = 30643,
            },
            new NodeInfo
            {
                NodeId = 30644,
            },
            new NodeInfo
            {
                NodeId = 30645,
            },
            new NodeInfo
            {
                NodeId = 30646,
            },
            new NodeInfo
            {
                NodeId = 30647,
            },
        };
    }
}
