using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1278 : RouteInfo
    {
        public override uint Id => 1278;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(35.883f, 642.047f);
        public override int Radius => 60;

        public override HashSet<uint> NodeIds => new()
        {
            35423,
            35424,
            35425,
            35426,
            35427,
            35428,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47399,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35423,
            },
            new NodeInfo
            {
                NodeId = 35424,
            },
            new NodeInfo
            {
                NodeId = 35425,
            },
            new NodeInfo
            {
                NodeId = 35426,
            },
            new NodeInfo
            {
                NodeId = 35427,
            },
            new NodeInfo
            {
                NodeId = 35428,
            },
        };
    }
}
