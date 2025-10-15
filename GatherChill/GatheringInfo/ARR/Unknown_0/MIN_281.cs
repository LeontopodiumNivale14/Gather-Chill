using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_281 : RouteInfo
    {
        public override uint Id => 281;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(622.349f, -160.546f);
        public override int Radius => 59;

        public override HashSet<uint> NodeIds => new()
        {
            31291,
            31292,
            31293,
            31294,
            31295,
            31296,
            31297,
            31298,
            31299,
            31300,
            31301,
            31302,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001842,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31291,
            },
            new NodeInfo
            {
                NodeId = 31292,
            },
            new NodeInfo
            {
                NodeId = 31293,
            },
            new NodeInfo
            {
                NodeId = 31294,
            },
            new NodeInfo
            {
                NodeId = 31295,
            },
            new NodeInfo
            {
                NodeId = 31296,
            },
            new NodeInfo
            {
                NodeId = 31297,
            },
            new NodeInfo
            {
                NodeId = 31298,
            },
            new NodeInfo
            {
                NodeId = 31299,
            },
            new NodeInfo
            {
                NodeId = 31300,
            },
            new NodeInfo
            {
                NodeId = 31301,
            },
            new NodeInfo
            {
                NodeId = 31302,
            },
        };
    }
}
