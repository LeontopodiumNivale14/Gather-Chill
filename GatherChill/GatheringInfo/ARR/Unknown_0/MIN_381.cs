using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_381 : RouteInfo
    {
        public override uint Id => 381;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(341.777f, -289.851f);
        public override int Radius => 73;

        public override HashSet<uint> NodeIds => new()
        {
            31617,
            31619,
            31645,
            31647,
            31673,
            31675,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            12534,
            12535,
            12537,
            13750,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31617,
            },
            new NodeInfo
            {
                NodeId = 31619,
            },
            new NodeInfo
            {
                NodeId = 31645,
            },
            new NodeInfo
            {
                NodeId = 31647,
            },
            new NodeInfo
            {
                NodeId = 31673,
            },
            new NodeInfo
            {
                NodeId = 31675,
            },
        };
    }
}
