using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_969 : RouteInfo
    {
        public override uint Id => 969;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-17.7047f, 532.909f);
        public override int Radius => 35;

        public override HashSet<uint> NodeIds => new()
        {
            34717,
            34718,
            34719,
            34720,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003551,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34717,
            },
            new NodeInfo
            {
                NodeId = 34718,
            },
            new NodeInfo
            {
                NodeId = 34719,
            },
            new NodeInfo
            {
                NodeId = 34720,
            },
        };
    }
}
