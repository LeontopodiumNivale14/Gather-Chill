using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1064 : RouteInfo
    {
        public override uint Id => 1064;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-280.808f, -6.50584f);
        public override int Radius => 63;

        public override HashSet<uint> NodeIds => new()
        {
            35165,
            35166,
            35167,
            35168,
            35169,
            35170,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35165,
            },
            new NodeInfo
            {
                NodeId = 35166,
            },
            new NodeInfo
            {
                NodeId = 35167,
            },
            new NodeInfo
            {
                NodeId = 35168,
            },
            new NodeInfo
            {
                NodeId = 35169,
            },
            new NodeInfo
            {
                NodeId = 35170,
            },
        };
    }
}
