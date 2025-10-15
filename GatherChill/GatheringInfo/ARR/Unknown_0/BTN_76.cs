using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_76 : RouteInfo
    {
        public override uint Id => 76;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(304.899f, 0.400877f);
        public override int Radius => 35;

        public override HashSet<uint> NodeIds => new()
        {
            30298,
            30299,
            30300,
            30301,
            30302,
            30303,
            30304,
            30305,
            30306,
            30307,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000317,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30298,
            },
            new NodeInfo
            {
                NodeId = 30299,
            },
            new NodeInfo
            {
                NodeId = 30300,
            },
            new NodeInfo
            {
                NodeId = 30301,
            },
            new NodeInfo
            {
                NodeId = 30302,
            },
            new NodeInfo
            {
                NodeId = 30303,
            },
            new NodeInfo
            {
                NodeId = 30304,
            },
            new NodeInfo
            {
                NodeId = 30305,
            },
            new NodeInfo
            {
                NodeId = 30306,
            },
            new NodeInfo
            {
                NodeId = 30307,
            },
        };
    }
}
