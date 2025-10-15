using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_372 : RouteInfo
    {
        public override uint Id => 372;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(656.569f, -168.206f);
        public override int Radius => 115;

        public override HashSet<uint> NodeIds => new()
        {
            31534,
            31536,
            31566,
            31568,
            31598,
            31600,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            12878,
            12879,
            13753,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31534,
            },
            new NodeInfo
            {
                NodeId = 31536,
            },
            new NodeInfo
            {
                NodeId = 31566,
            },
            new NodeInfo
            {
                NodeId = 31568,
            },
            new NodeInfo
            {
                NodeId = 31598,
            },
            new NodeInfo
            {
                NodeId = 31600,
            },
        };
    }
}
