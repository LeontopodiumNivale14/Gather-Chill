using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_879 : RouteInfo
    {
        public override uint Id => 879;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-453.025f, -146.65f);
        public override int Radius => 27;

        public override HashSet<uint> NodeIds => new()
        {
            34224,
            34225,
            34226,
            34227,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003162,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34224,
            },
            new NodeInfo
            {
                NodeId = 34225,
            },
            new NodeInfo
            {
                NodeId = 34226,
            },
            new NodeInfo
            {
                NodeId = 34227,
            },
        };
    }
}
