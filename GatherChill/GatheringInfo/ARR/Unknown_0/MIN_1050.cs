using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1050 : RouteInfo
    {
        public override uint Id => 1050;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(81.9756f, -481.425f);
        public override int Radius => 80;

        public override HashSet<uint> NodeIds => new()
        {
            35075,
            35076,
            35077,
            35078,
            35079,
            35080,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35075,
            },
            new NodeInfo
            {
                NodeId = 35076,
            },
            new NodeInfo
            {
                NodeId = 35077,
            },
            new NodeInfo
            {
                NodeId = 35078,
            },
            new NodeInfo
            {
                NodeId = 35079,
            },
            new NodeInfo
            {
                NodeId = 35080,
            },
        };
    }
}
