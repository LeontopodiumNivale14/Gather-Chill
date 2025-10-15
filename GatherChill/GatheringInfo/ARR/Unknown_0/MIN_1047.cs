using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1047 : RouteInfo
    {
        public override uint Id => 1047;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-169.253f, -173.563f);
        public override int Radius => 78;

        public override HashSet<uint> NodeIds => new()
        {
            35057,
            35058,
            35059,
            35060,
            35061,
            35062,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35057,
            },
            new NodeInfo
            {
                NodeId = 35058,
            },
            new NodeInfo
            {
                NodeId = 35059,
            },
            new NodeInfo
            {
                NodeId = 35060,
            },
            new NodeInfo
            {
                NodeId = 35061,
            },
            new NodeInfo
            {
                NodeId = 35062,
            },
        };
    }
}
