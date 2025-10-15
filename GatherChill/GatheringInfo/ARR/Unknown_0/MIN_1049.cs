using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1049 : RouteInfo
    {
        public override uint Id => 1049;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-458.365f, 124.81f);
        public override int Radius => 97;

        public override HashSet<uint> NodeIds => new()
        {
            35069,
            35070,
            35071,
            35072,
            35073,
            35074,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35069,
            },
            new NodeInfo
            {
                NodeId = 35070,
            },
            new NodeInfo
            {
                NodeId = 35071,
            },
            new NodeInfo
            {
                NodeId = 35072,
            },
            new NodeInfo
            {
                NodeId = 35073,
            },
            new NodeInfo
            {
                NodeId = 35074,
            },
        };
    }
}
