using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_884 : RouteInfo
    {
        public override uint Id => 884;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(215.963f, -478.817f);
        public override int Radius => 31;

        public override HashSet<uint> NodeIds => new()
        {
            34270,
            34271,
            34272,
            34273,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003169,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34270,
            },
            new NodeInfo
            {
                NodeId = 34271,
            },
            new NodeInfo
            {
                NodeId = 34272,
            },
            new NodeInfo
            {
                NodeId = 34273,
            },
        };
    }
}
