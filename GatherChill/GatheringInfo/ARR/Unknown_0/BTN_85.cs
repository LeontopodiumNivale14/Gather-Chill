using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_85 : RouteInfo
    {
        public override uint Id => 85;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-447.838f, -134.053f);
        public override int Radius => 111;

        public override HashSet<uint> NodeIds => new()
        {
            30624,
            30625,
            30626,
            30627,
            30628,
            30629,
            30630,
            30631,
            30632,
            30633,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000917,
            2000918,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30624,
            },
            new NodeInfo
            {
                NodeId = 30625,
            },
            new NodeInfo
            {
                NodeId = 30626,
            },
            new NodeInfo
            {
                NodeId = 30627,
            },
            new NodeInfo
            {
                NodeId = 30628,
            },
            new NodeInfo
            {
                NodeId = 30629,
            },
            new NodeInfo
            {
                NodeId = 30630,
            },
            new NodeInfo
            {
                NodeId = 30631,
            },
            new NodeInfo
            {
                NodeId = 30632,
            },
            new NodeInfo
            {
                NodeId = 30633,
            },
        };
    }
}
