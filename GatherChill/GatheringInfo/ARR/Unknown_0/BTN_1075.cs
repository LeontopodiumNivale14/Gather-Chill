using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1075 : RouteInfo
    {
        public override uint Id => 1075;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(562.226f, -910.828f);
        public override int Radius => 53;

        public override HashSet<uint> NodeIds => new()
        {
            35231,
            35232,
            35233,
            35234,
            35235,
            35236,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35231,
            },
            new NodeInfo
            {
                NodeId = 35232,
            },
            new NodeInfo
            {
                NodeId = 35233,
            },
            new NodeInfo
            {
                NodeId = 35234,
            },
            new NodeInfo
            {
                NodeId = 35235,
            },
            new NodeInfo
            {
                NodeId = 35236,
            },
        };
    }
}
