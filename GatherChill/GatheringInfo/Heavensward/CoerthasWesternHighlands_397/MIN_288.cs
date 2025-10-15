using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
    public class MIN_288 : RouteInfo
    {
        public override uint Id => 288;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 397;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-295.623f, -484.611f);
        public override int Radius => 152;

        public override HashSet<uint> NodeIds => new()
        {
            31334,
            31335,
            31336,
            31337,
            31338,
            31339,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            5161,
            5162,
            12531,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31334,
            },
            new NodeInfo
            {
                NodeId = 31335,
            },
            new NodeInfo
            {
                NodeId = 31336,
            },
            new NodeInfo
            {
                NodeId = 31337,
            },
            new NodeInfo
            {
                NodeId = 31338,
            },
            new NodeInfo
            {
                NodeId = 31339,
            },
        };
    }
}
