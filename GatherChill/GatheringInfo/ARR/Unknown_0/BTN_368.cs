using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_368 : RouteInfo
    {
        public override uint Id => 368;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(459.503f, 369.792f);
        public override int Radius => 111;

        public override HashSet<uint> NodeIds => new()
        {
            31526,
            31528,
            31558,
            31560,
            31590,
            31592,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            12878,
            12879,
            13753,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31526,
            },
            new NodeInfo
            {
                NodeId = 31528,
            },
            new NodeInfo
            {
                NodeId = 31558,
            },
            new NodeInfo
            {
                NodeId = 31560,
            },
            new NodeInfo
            {
                NodeId = 31590,
            },
            new NodeInfo
            {
                NodeId = 31592,
            },
        };
    }
}
