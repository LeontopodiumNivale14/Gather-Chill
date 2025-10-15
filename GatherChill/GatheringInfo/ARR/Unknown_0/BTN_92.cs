using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_92 : RouteInfo
    {
        public override uint Id => 92;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(458.932f, -395.886f);
        public override int Radius => 94;

        public override HashSet<uint> NodeIds => new()
        {
            30678,
            30679,
            30680,
            30681,
            30682,
            30683,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000927,
            2000928,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30678,
            },
            new NodeInfo
            {
                NodeId = 30679,
            },
            new NodeInfo
            {
                NodeId = 30680,
            },
            new NodeInfo
            {
                NodeId = 30681,
            },
            new NodeInfo
            {
                NodeId = 30682,
            },
            new NodeInfo
            {
                NodeId = 30683,
            },
        };
    }
}
