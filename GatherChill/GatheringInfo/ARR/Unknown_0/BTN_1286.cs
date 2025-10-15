using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1286 : RouteInfo
    {
        public override uint Id => 1286;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-335.594f, 554.98f);
        public override int Radius => 72;

        public override HashSet<uint> NodeIds => new()
        {
            35435,
            35436,
            35437,
            35438,
            35439,
            35440,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47406,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35435,
            },
            new NodeInfo
            {
                NodeId = 35436,
            },
            new NodeInfo
            {
                NodeId = 35437,
            },
            new NodeInfo
            {
                NodeId = 35438,
            },
            new NodeInfo
            {
                NodeId = 35439,
            },
            new NodeInfo
            {
                NodeId = 35440,
            },
        };
    }
}
