using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_575 : RouteInfo
    {
        public override uint Id => 575;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-618.995f, 267.515f);
        public override int Radius => 40;

        public override HashSet<uint> NodeIds => new()
        {
            32508,
            32509,
            32510,
            32511,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002784,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32508,
            },
            new NodeInfo
            {
                NodeId = 32509,
            },
            new NodeInfo
            {
                NodeId = 32510,
            },
            new NodeInfo
            {
                NodeId = 32511,
            },
        };
    }
}
