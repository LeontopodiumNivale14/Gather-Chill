using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_953 : RouteInfo
    {
        public override uint Id => 953;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(42.6002f, 337.852f);
        public override int Radius => 119;

        public override HashSet<uint> NodeIds => new()
        {
            34581,
            34582,
            34583,
            34584,
            34585,
            34586,
            34587,
            34588,
            34589,
            34590,
            34591,
            34592,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003527,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34581,
            },
            new NodeInfo
            {
                NodeId = 34582,
            },
            new NodeInfo
            {
                NodeId = 34583,
            },
            new NodeInfo
            {
                NodeId = 34584,
            },
            new NodeInfo
            {
                NodeId = 34585,
            },
            new NodeInfo
            {
                NodeId = 34586,
            },
            new NodeInfo
            {
                NodeId = 34587,
            },
            new NodeInfo
            {
                NodeId = 34588,
            },
            new NodeInfo
            {
                NodeId = 34589,
            },
            new NodeInfo
            {
                NodeId = 34590,
            },
            new NodeInfo
            {
                NodeId = 34591,
            },
            new NodeInfo
            {
                NodeId = 34592,
            },
        };
    }
}
