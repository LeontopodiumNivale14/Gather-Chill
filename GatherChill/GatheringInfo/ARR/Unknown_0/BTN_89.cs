using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_89 : RouteInfo
    {
        public override uint Id => 89;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(138.02f, -521.15f);
        public override int Radius => 95;

        public override HashSet<uint> NodeIds => new()
        {
            30654,
            30655,
            30656,
            30657,
            30658,
            30659,
            30660,
            30661,
            30662,
            30663,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000923,
            2000924,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30654,
            },
            new NodeInfo
            {
                NodeId = 30655,
            },
            new NodeInfo
            {
                NodeId = 30656,
            },
            new NodeInfo
            {
                NodeId = 30657,
            },
            new NodeInfo
            {
                NodeId = 30658,
            },
            new NodeInfo
            {
                NodeId = 30659,
            },
            new NodeInfo
            {
                NodeId = 30660,
            },
            new NodeInfo
            {
                NodeId = 30661,
            },
            new NodeInfo
            {
                NodeId = 30662,
            },
            new NodeInfo
            {
                NodeId = 30663,
            },
        };
    }
}
