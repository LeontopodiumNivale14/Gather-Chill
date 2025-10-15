using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_411 : RouteInfo
    {
        public override uint Id => 411;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(296.467f, -743.771f);
        public override int Radius => 104;

        public override HashSet<uint> NodeIds => new()
        {
            31754,
            31755,
            31756,
            31757,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5,
            11,
            5545,
            13753,
            17571,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31754,
            },
            new NodeInfo
            {
                NodeId = 31755,
            },
            new NodeInfo
            {
                NodeId = 31756,
            },
            new NodeInfo
            {
                NodeId = 31757,
            },
        };
    }
}
