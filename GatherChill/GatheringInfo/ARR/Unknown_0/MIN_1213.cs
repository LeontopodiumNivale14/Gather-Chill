using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1213 : RouteInfo
    {
        public override uint Id => 1213;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(539.745f, -79.8219f);
        public override int Radius => 61;

        public override HashSet<uint> NodeIds => new()
        {
            35293,
            35294,
            35295,
            35296,
            35297,
            35298,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47357,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35293,
            },
            new NodeInfo
            {
                NodeId = 35294,
            },
            new NodeInfo
            {
                NodeId = 35295,
            },
            new NodeInfo
            {
                NodeId = 35296,
            },
            new NodeInfo
            {
                NodeId = 35297,
            },
            new NodeInfo
            {
                NodeId = 35298,
            },
        };
    }
}
