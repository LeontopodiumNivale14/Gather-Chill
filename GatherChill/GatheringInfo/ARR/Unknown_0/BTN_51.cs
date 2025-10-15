using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_51 : RouteInfo
    {
        public override uint Id => 51;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(21.7577f, -237.006f);
        public override int Radius => 43;

        public override HashSet<uint> NodeIds => new()
        {
            30006,
            30152,
            30153,
            30154,
            30155,
            30156,
            30157,
            30158,
            30159,
            30160,
            30161,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000265,
            2000267,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30006,
            },
            new NodeInfo
            {
                NodeId = 30152,
            },
            new NodeInfo
            {
                NodeId = 30153,
            },
            new NodeInfo
            {
                NodeId = 30154,
            },
            new NodeInfo
            {
                NodeId = 30155,
            },
            new NodeInfo
            {
                NodeId = 30156,
            },
            new NodeInfo
            {
                NodeId = 30157,
            },
            new NodeInfo
            {
                NodeId = 30158,
            },
            new NodeInfo
            {
                NodeId = 30159,
            },
            new NodeInfo
            {
                NodeId = 30160,
            },
            new NodeInfo
            {
                NodeId = 30161,
            },
        };
    }
}
