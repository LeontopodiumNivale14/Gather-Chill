using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_401 : RouteInfo
    {
        public override uint Id => 401;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-311.155f, -387.814f);
        public override int Radius => 329;

        public override HashSet<uint> NodeIds => new()
        {
            31714,
            31715,
            31716,
            31717,
        };

        public override HashSet<uint> ItemIds => new()
        {
            4,
            10,
            7592,
            13752,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31714,
            },
            new NodeInfo
            {
                NodeId = 31715,
            },
            new NodeInfo
            {
                NodeId = 31716,
            },
            new NodeInfo
            {
                NodeId = 31717,
            },
        };
    }
}
