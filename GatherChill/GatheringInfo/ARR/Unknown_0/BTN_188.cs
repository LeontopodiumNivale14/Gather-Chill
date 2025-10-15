using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_188 : RouteInfo
    {
        public override uint Id => 188;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-308.933f, -208.261f);
        public override int Radius => 59;

        public override HashSet<uint> NodeIds => new()
        {
            30341,
            30342,
            30343,
            30344,
        };

        public override HashSet<uint> ItemIds => new()
        {
            4,
            10,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30341,
            },
            new NodeInfo
            {
                NodeId = 30342,
            },
            new NodeInfo
            {
                NodeId = 30343,
            },
            new NodeInfo
            {
                NodeId = 30344,
            },
        };
    }
}
