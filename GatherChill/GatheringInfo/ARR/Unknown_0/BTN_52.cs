using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_52 : RouteInfo
    {
        public override uint Id => 52;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(66.99f, -43.4489f);
        public override int Radius => 90;

        public override HashSet<uint> NodeIds => new()
        {
            30007,
            30162,
            30163,
            30164,
            30165,
            30166,
            30167,
            30168,
            30169,
            30170,
            30171,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000268,
            2000270,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30007,
            },
            new NodeInfo
            {
                NodeId = 30162,
            },
            new NodeInfo
            {
                NodeId = 30163,
            },
            new NodeInfo
            {
                NodeId = 30164,
            },
            new NodeInfo
            {
                NodeId = 30165,
            },
            new NodeInfo
            {
                NodeId = 30166,
            },
            new NodeInfo
            {
                NodeId = 30167,
            },
            new NodeInfo
            {
                NodeId = 30168,
            },
            new NodeInfo
            {
                NodeId = 30169,
            },
            new NodeInfo
            {
                NodeId = 30170,
            },
            new NodeInfo
            {
                NodeId = 30171,
            },
        };
    }
}
