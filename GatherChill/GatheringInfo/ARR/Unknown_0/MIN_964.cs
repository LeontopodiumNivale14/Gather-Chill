using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_964 : RouteInfo
    {
        public override uint Id => 964;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(697.109f, 393.746f);
        public override int Radius => 33;

        public override HashSet<uint> NodeIds => new()
        {
            34671,
            34672,
            34673,
            34674,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003544,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34671,
            },
            new NodeInfo
            {
                NodeId = 34672,
            },
            new NodeInfo
            {
                NodeId = 34673,
            },
            new NodeInfo
            {
                NodeId = 34674,
            },
        };
    }
}
