using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_870 : RouteInfo
    {
        public override uint Id => 870;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(284.283f, -165.843f);
        public override int Radius => 43;

        public override HashSet<uint> NodeIds => new()
        {
            34150,
            34151,
            34152,
            34153,
            34154,
            34155,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003147,
            2003148,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34150,
            },
            new NodeInfo
            {
                NodeId = 34151,
            },
            new NodeInfo
            {
                NodeId = 34152,
            },
            new NodeInfo
            {
                NodeId = 34153,
            },
            new NodeInfo
            {
                NodeId = 34154,
            },
            new NodeInfo
            {
                NodeId = 34155,
            },
        };
    }
}
