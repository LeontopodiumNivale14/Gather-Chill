using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1198 : RouteInfo
    {
        public override uint Id => 1198;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(31.0995f, -863.785f);
        public override int Radius => 7;

        public override HashSet<uint> NodeIds => new()
        {
            35255,
            35256,
            35257,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            48033,
            48042,
            48063,
            48081,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35255,
            },
            new NodeInfo
            {
                NodeId = 35256,
            },
            new NodeInfo
            {
                NodeId = 35257,
            },
        };
    }
}
