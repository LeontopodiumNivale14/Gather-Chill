using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_432 : RouteInfo
    {
        public override uint Id => 432;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-355.401f, -176.27f);
        public override int Radius => 112;

        public override HashSet<uint> NodeIds => new()
        {
            31814,
            31815,
            31816,
        };

        public override HashSet<uint> ItemIds => new()
        {
            3,
            9,
            5120,
            5121,
            12537,
            13750,
            17570,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31814,
            },
            new NodeInfo
            {
                NodeId = 31815,
            },
            new NodeInfo
            {
                NodeId = 31816,
            },
        };
    }
}
