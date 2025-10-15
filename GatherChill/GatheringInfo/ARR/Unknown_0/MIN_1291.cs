using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1291 : RouteInfo
    {
        public override uint Id => 1291;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(131.692f, -748.242f);
        public override int Radius => 20;

        public override HashSet<uint> NodeIds => new()
        {
            35357,
            35358,
            35359,
            35360,
            35361,
            35362,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47417,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35357,
            },
            new NodeInfo
            {
                NodeId = 35358,
            },
            new NodeInfo
            {
                NodeId = 35359,
            },
            new NodeInfo
            {
                NodeId = 35360,
            },
            new NodeInfo
            {
                NodeId = 35361,
            },
            new NodeInfo
            {
                NodeId = 35362,
            },
        };
    }
}
