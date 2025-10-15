using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_407 : RouteInfo
    {
        public override uint Id => 407;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-210.18f, -426.294f);
        public override int Radius => 356;

        public override HashSet<uint> NodeIds => new()
        {
            31738,
            31739,
            31740,
            31741,
        };

        public override HashSet<uint> ItemIds => new()
        {
            4,
            10,
            7594,
            13753,
            17571,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31738,
            },
            new NodeInfo
            {
                NodeId = 31739,
            },
            new NodeInfo
            {
                NodeId = 31740,
            },
            new NodeInfo
            {
                NodeId = 31741,
            },
        };
    }
}
