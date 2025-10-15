using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_364 : RouteInfo
    {
        public override uint Id => 364;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-95.7041f, 606.857f);
        public override int Radius => 49;

        public override HashSet<uint> NodeIds => new()
        {
            31518,
            31520,
            31550,
            31552,
            31582,
            31584,
        };

        public override HashSet<uint> ItemIds => new()
        {
            11,
            12878,
            12879,
            13753,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31518,
            },
            new NodeInfo
            {
                NodeId = 31520,
            },
            new NodeInfo
            {
                NodeId = 31550,
            },
            new NodeInfo
            {
                NodeId = 31552,
            },
            new NodeInfo
            {
                NodeId = 31582,
            },
            new NodeInfo
            {
                NodeId = 31584,
            },
        };
    }
}
