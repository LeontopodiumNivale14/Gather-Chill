using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_196 : RouteInfo
    {
        public override uint Id => 196;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-218.454f, 285.332f);
        public override int Radius => 69;

        public override HashSet<uint> NodeIds => new()
        {
            30377,
            30378,
            30379,
            30380,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30377,
            },
            new NodeInfo
            {
                NodeId = 30378,
            },
            new NodeInfo
            {
                NodeId = 30379,
            },
            new NodeInfo
            {
                NodeId = 30380,
            },
        };
    }
}
