using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_455 : RouteInfo
    {
        public override uint Id => 455;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(675.362f, 864.426f);
        public override int Radius => 18;

        public override HashSet<uint> NodeIds => new()
        {
            32009,
            32010,
            32011,
            32012,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002154,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32009,
            },
            new NodeInfo
            {
                NodeId = 32010,
            },
            new NodeInfo
            {
                NodeId = 32011,
            },
            new NodeInfo
            {
                NodeId = 32012,
            },
        };
    }
}
