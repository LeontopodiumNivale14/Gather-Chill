using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheFringes_612
{
    public class BTN_505 : RouteInfo
    {
        public override uint Id => 505;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 612;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-202.33f, -632.512f);
        public override int Radius => 167;

        public override HashSet<uint> NodeIds => new()
        {
            32188,
            32189,
            32190,
            32191,
            32192,
            32193,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8,
            19868,
            19976,
            20780,
            20782,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32188,
            },
            new NodeInfo
            {
                NodeId = 32189,
            },
            new NodeInfo
            {
                NodeId = 32190,
            },
            new NodeInfo
            {
                NodeId = 32191,
            },
            new NodeInfo
            {
                NodeId = 32192,
            },
            new NodeInfo
            {
                NodeId = 32193,
            },
        };
    }
}
