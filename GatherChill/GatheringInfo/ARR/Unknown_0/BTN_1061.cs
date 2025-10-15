using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1061 : RouteInfo
    {
        public override uint Id => 1061;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(450.783f, 220.795f);
        public override int Radius => 48;

        public override HashSet<uint> NodeIds => new()
        {
            35143,
            35144,
            35145,
            35146,
            35147,
            35148,
            35149,
            35150,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35143,
            },
            new NodeInfo
            {
                NodeId = 35144,
            },
            new NodeInfo
            {
                NodeId = 35145,
            },
            new NodeInfo
            {
                NodeId = 35146,
            },
            new NodeInfo
            {
                NodeId = 35147,
            },
            new NodeInfo
            {
                NodeId = 35148,
            },
            new NodeInfo
            {
                NodeId = 35149,
            },
            new NodeInfo
            {
                NodeId = 35150,
            },
        };
    }
}
