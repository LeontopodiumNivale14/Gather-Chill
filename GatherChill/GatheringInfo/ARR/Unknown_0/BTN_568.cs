using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_568 : RouteInfo
    {
        public override uint Id => 568;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-263.573f, 75.4365f);
        public override int Radius => 109;

        public override HashSet<uint> NodeIds => new()
        {
            32444,
            32445,
            32446,
            32447,
            32448,
            32449,
            32450,
            32451,
            32452,
            32453,
            32454,
            32455,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002773,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32444,
            },
            new NodeInfo
            {
                NodeId = 32445,
            },
            new NodeInfo
            {
                NodeId = 32446,
            },
            new NodeInfo
            {
                NodeId = 32447,
            },
            new NodeInfo
            {
                NodeId = 32448,
            },
            new NodeInfo
            {
                NodeId = 32449,
            },
            new NodeInfo
            {
                NodeId = 32450,
            },
            new NodeInfo
            {
                NodeId = 32451,
            },
            new NodeInfo
            {
                NodeId = 32452,
            },
            new NodeInfo
            {
                NodeId = 32453,
            },
            new NodeInfo
            {
                NodeId = 32454,
            },
            new NodeInfo
            {
                NodeId = 32455,
            },
        };
    }
}
