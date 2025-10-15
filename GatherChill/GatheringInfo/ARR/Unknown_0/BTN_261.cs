using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_261 : RouteInfo
    {
        public override uint Id => 261;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(249.936f, 490.664f);
        public override int Radius => 53;

        public override HashSet<uint> NodeIds => new()
        {
            31127,
            31128,
            31129,
            31130,
            31131,
            31132,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001857,
            2001858,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31127,
            },
            new NodeInfo
            {
                NodeId = 31128,
            },
            new NodeInfo
            {
                NodeId = 31129,
            },
            new NodeInfo
            {
                NodeId = 31130,
            },
            new NodeInfo
            {
                NodeId = 31131,
            },
            new NodeInfo
            {
                NodeId = 31132,
            },
        };
    }
}
