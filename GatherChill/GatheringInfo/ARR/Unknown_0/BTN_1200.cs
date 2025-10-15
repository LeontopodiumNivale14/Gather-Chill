using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1200 : RouteInfo
    {
        public override uint Id => 1200;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(212.843f, 296.682f);
        public override int Radius => 5;

        public override HashSet<uint> NodeIds => new()
        {
            35261,
            35262,
            35263,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            48013,
            48025,
            48031,
            48040,
            48061,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35261,
            },
            new NodeInfo
            {
                NodeId = 35262,
            },
            new NodeInfo
            {
                NodeId = 35263,
            },
        };
    }
}
