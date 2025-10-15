using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1052 : RouteInfo
    {
        public override uint Id => 1052;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-672.073f, -511.745f);
        public override int Radius => 68;

        public override HashSet<uint> NodeIds => new()
        {
            35087,
            35088,
            35089,
            35090,
            35091,
            35092,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35087,
            },
            new NodeInfo
            {
                NodeId = 35088,
            },
            new NodeInfo
            {
                NodeId = 35089,
            },
            new NodeInfo
            {
                NodeId = 35090,
            },
            new NodeInfo
            {
                NodeId = 35091,
            },
            new NodeInfo
            {
                NodeId = 35092,
            },
        };
    }
}
