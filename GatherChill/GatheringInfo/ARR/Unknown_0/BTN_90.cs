using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_90 : RouteInfo
    {
        public override uint Id => 90;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(196.969f, -553.654f);
        public override int Radius => 52;

        public override HashSet<uint> NodeIds => new()
        {
            30664,
            30665,
            30666,
            30667,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000925,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30664,
            },
            new NodeInfo
            {
                NodeId = 30665,
            },
            new NodeInfo
            {
                NodeId = 30666,
            },
            new NodeInfo
            {
                NodeId = 30667,
            },
        };
    }
}
