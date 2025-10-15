using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1194 : RouteInfo
    {
        public override uint Id => 1194;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(85.6068f, 256.05f);
        public override int Radius => 6;

        public override HashSet<uint> NodeIds => new()
        {
            35243,
            35244,
            35245,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            48012,
            48024,
            48030,
            48039,
            48060,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35243,
            },
            new NodeInfo
            {
                NodeId = 35244,
            },
            new NodeInfo
            {
                NodeId = 35245,
            },
        };
    }
}
