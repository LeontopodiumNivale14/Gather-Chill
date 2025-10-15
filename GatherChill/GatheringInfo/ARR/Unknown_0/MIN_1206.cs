using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1206 : RouteInfo
    {
        public override uint Id => 1206;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(424.137f, -742.792f);
        public override int Radius => 26;

        public override HashSet<uint> NodeIds => new()
        {
            35285,
            35286,
            35287,
            35288,
            35289,
            35290,
            35291,
            35292,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47354,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35285,
            },
            new NodeInfo
            {
                NodeId = 35286,
            },
            new NodeInfo
            {
                NodeId = 35287,
            },
            new NodeInfo
            {
                NodeId = 35288,
            },
            new NodeInfo
            {
                NodeId = 35289,
            },
            new NodeInfo
            {
                NodeId = 35290,
            },
            new NodeInfo
            {
                NodeId = 35291,
            },
            new NodeInfo
            {
                NodeId = 35292,
            },
        };
    }
}
