using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_93 : RouteInfo
    {
        public override uint Id => 93;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(394.338f, 148.32f);
        public override int Radius => 51;

        public override HashSet<uint> NodeIds => new()
        {
            30684,
            30685,
            30686,
            30687,
            30688,
            30689,
            30690,
            30691,
            30692,
            30693,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000469,
            2000470,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30684,
            },
            new NodeInfo
            {
                NodeId = 30685,
            },
            new NodeInfo
            {
                NodeId = 30686,
            },
            new NodeInfo
            {
                NodeId = 30687,
            },
            new NodeInfo
            {
                NodeId = 30688,
            },
            new NodeInfo
            {
                NodeId = 30689,
            },
            new NodeInfo
            {
                NodeId = 30690,
            },
            new NodeInfo
            {
                NodeId = 30691,
            },
            new NodeInfo
            {
                NodeId = 30692,
            },
            new NodeInfo
            {
                NodeId = 30693,
            },
        };
    }
}
