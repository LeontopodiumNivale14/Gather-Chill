using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_859 : RouteInfo
    {
        public override uint Id => 859;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(84.9831f, -689.341f);
        public override int Radius => 82;

        public override HashSet<uint> NodeIds => new()
        {
            34054,
            34055,
            34056,
            34057,
            34058,
            34059,
            34060,
            34061,
            34062,
            34063,
            34064,
            34065,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003131,
            2003132,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34054,
            },
            new NodeInfo
            {
                NodeId = 34055,
            },
            new NodeInfo
            {
                NodeId = 34056,
            },
            new NodeInfo
            {
                NodeId = 34057,
            },
            new NodeInfo
            {
                NodeId = 34058,
            },
            new NodeInfo
            {
                NodeId = 34059,
            },
            new NodeInfo
            {
                NodeId = 34060,
            },
            new NodeInfo
            {
                NodeId = 34061,
            },
            new NodeInfo
            {
                NodeId = 34062,
            },
            new NodeInfo
            {
                NodeId = 34063,
            },
            new NodeInfo
            {
                NodeId = 34064,
            },
            new NodeInfo
            {
                NodeId = 34065,
            },
        };
    }
}
