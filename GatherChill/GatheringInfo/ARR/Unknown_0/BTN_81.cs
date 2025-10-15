using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_81 : RouteInfo
    {
        public override uint Id => 81;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(195.39f, 336.061f);
        public override int Radius => 117;

        public override HashSet<uint> NodeIds => new()
        {
            30594,
            30595,
            30596,
            30597,
            30598,
            30599,
            30600,
            30601,
            30602,
            30603,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000911,
            2000912,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30594,
            },
            new NodeInfo
            {
                NodeId = 30595,
            },
            new NodeInfo
            {
                NodeId = 30596,
            },
            new NodeInfo
            {
                NodeId = 30597,
            },
            new NodeInfo
            {
                NodeId = 30598,
            },
            new NodeInfo
            {
                NodeId = 30599,
            },
            new NodeInfo
            {
                NodeId = 30600,
            },
            new NodeInfo
            {
                NodeId = 30601,
            },
            new NodeInfo
            {
                NodeId = 30602,
            },
            new NodeInfo
            {
                NodeId = 30603,
            },
        };
    }
}
