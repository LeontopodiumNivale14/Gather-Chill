using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_561 : RouteInfo
    {
        public override uint Id => 561;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(481.172f, 47.1742f);
        public override int Radius => 31;

        public override HashSet<uint> NodeIds => new()
        {
            32388,
            32389,
            32390,
            32391,
            32392,
            32393,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002762,
            2002763,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32388,
            },
            new NodeInfo
            {
                NodeId = 32389,
            },
            new NodeInfo
            {
                NodeId = 32390,
            },
            new NodeInfo
            {
                NodeId = 32391,
            },
            new NodeInfo
            {
                NodeId = 32392,
            },
            new NodeInfo
            {
                NodeId = 32393,
            },
        };
    }
}
