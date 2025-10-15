using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_254 : RouteInfo
    {
        public override uint Id => 254;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(386.615f, 518.719f);
        public override int Radius => 23;

        public override HashSet<uint> NodeIds => new()
        {
            31073,
            31074,
            31075,
            31076,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001847,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31073,
            },
            new NodeInfo
            {
                NodeId = 31074,
            },
            new NodeInfo
            {
                NodeId = 31075,
            },
            new NodeInfo
            {
                NodeId = 31076,
            },
        };
    }
}
