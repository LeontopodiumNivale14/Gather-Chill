using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_581 : RouteInfo
    {
        public override uint Id => 581;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-152.154f, 707.627f);
        public override int Radius => 38;

        public override HashSet<uint> NodeIds => new()
        {
            32558,
            32559,
            32560,
            32561,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002793,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32558,
            },
            new NodeInfo
            {
                NodeId = 32559,
            },
            new NodeInfo
            {
                NodeId = 32560,
            },
            new NodeInfo
            {
                NodeId = 32561,
            },
        };
    }
}
