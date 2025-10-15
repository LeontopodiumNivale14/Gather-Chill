using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_103 : RouteInfo
    {
        public override uint Id => 103;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(217.214f, -93.1593f);
        public override int Radius => 16;

        public override HashSet<uint> NodeIds => new()
        {
            30766,
            30767,
            30768,
            30769,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000485,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30766,
            },
            new NodeInfo
            {
                NodeId = 30767,
            },
            new NodeInfo
            {
                NodeId = 30768,
            },
            new NodeInfo
            {
                NodeId = 30769,
            },
        };
    }
}
