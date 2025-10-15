using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_53 : RouteInfo
    {
        public override uint Id => 53;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(160.794f, -63.5794f);
        public override int Radius => 14;

        public override HashSet<uint> NodeIds => new()
        {
            30172,
            30173,
            30174,
            30175,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000271,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30172,
            },
            new NodeInfo
            {
                NodeId = 30173,
            },
            new NodeInfo
            {
                NodeId = 30174,
            },
            new NodeInfo
            {
                NodeId = 30175,
            },
        };
    }
}
