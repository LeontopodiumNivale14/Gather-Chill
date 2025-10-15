using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_58 : RouteInfo
    {
        public override uint Id => 58;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(319.118f, 35.6425f);
        public override int Radius => 67;

        public override HashSet<uint> NodeIds => new()
        {
            30204,
            30205,
            30206,
            30207,
            30208,
            30209,
            30210,
            30211,
            30212,
            30213,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000283,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30204,
            },
            new NodeInfo
            {
                NodeId = 30205,
            },
            new NodeInfo
            {
                NodeId = 30206,
            },
            new NodeInfo
            {
                NodeId = 30207,
            },
            new NodeInfo
            {
                NodeId = 30208,
            },
            new NodeInfo
            {
                NodeId = 30209,
            },
            new NodeInfo
            {
                NodeId = 30210,
            },
            new NodeInfo
            {
                NodeId = 30211,
            },
            new NodeInfo
            {
                NodeId = 30212,
            },
            new NodeInfo
            {
                NodeId = 30213,
            },
        };
    }
}
