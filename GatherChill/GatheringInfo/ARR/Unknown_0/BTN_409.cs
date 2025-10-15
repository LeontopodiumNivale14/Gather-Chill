using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_409 : RouteInfo
    {
        public override uint Id => 409;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(676.381f, -176.954f);
        public override int Radius => 129;

        public override HashSet<uint> NodeIds => new()
        {
            31746,
            31747,
            31748,
            31749,
        };

        public override HashSet<uint> ItemIds => new()
        {
            7,
            13,
            10096,
            13753,
            17571,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31746,
            },
            new NodeInfo
            {
                NodeId = 31747,
            },
            new NodeInfo
            {
                NodeId = 31748,
            },
            new NodeInfo
            {
                NodeId = 31749,
            },
        };
    }
}
