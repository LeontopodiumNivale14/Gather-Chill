using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_253 : RouteInfo
    {
        public override uint Id => 253;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(429.87f, 568.682f);
        public override int Radius => 101;

        public override HashSet<uint> NodeIds => new()
        {
            31061,
            31062,
            31063,
            31064,
            31065,
            31066,
            31067,
            31068,
            31069,
            31070,
            31071,
            31072,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001845,
            2001846,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31061,
            },
            new NodeInfo
            {
                NodeId = 31062,
            },
            new NodeInfo
            {
                NodeId = 31063,
            },
            new NodeInfo
            {
                NodeId = 31064,
            },
            new NodeInfo
            {
                NodeId = 31065,
            },
            new NodeInfo
            {
                NodeId = 31066,
            },
            new NodeInfo
            {
                NodeId = 31067,
            },
            new NodeInfo
            {
                NodeId = 31068,
            },
            new NodeInfo
            {
                NodeId = 31069,
            },
            new NodeInfo
            {
                NodeId = 31070,
            },
            new NodeInfo
            {
                NodeId = 31071,
            },
            new NodeInfo
            {
                NodeId = 31072,
            },
        };
    }
}
