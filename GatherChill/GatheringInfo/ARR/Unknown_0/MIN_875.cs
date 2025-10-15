using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_875 : RouteInfo
    {
        public override uint Id => 875;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-183.122f, -704.732f);
        public override int Radius => 18;

        public override HashSet<uint> NodeIds => new()
        {
            34190,
            34191,
            34192,
            34193,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003156,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34190,
            },
            new NodeInfo
            {
                NodeId = 34191,
            },
            new NodeInfo
            {
                NodeId = 34192,
            },
            new NodeInfo
            {
                NodeId = 34193,
            },
        };
    }
}
