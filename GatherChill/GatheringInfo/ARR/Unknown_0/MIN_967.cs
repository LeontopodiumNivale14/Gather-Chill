using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_967 : RouteInfo
    {
        public override uint Id => 967;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(408.828f, 192.874f);
        public override int Radius => 16;

        public override HashSet<uint> NodeIds => new()
        {
            34699,
            34700,
            34701,
            34702,
            34703,
            34704,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003548,
            2003549,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34699,
            },
            new NodeInfo
            {
                NodeId = 34700,
            },
            new NodeInfo
            {
                NodeId = 34701,
            },
            new NodeInfo
            {
                NodeId = 34702,
            },
            new NodeInfo
            {
                NodeId = 34703,
            },
            new NodeInfo
            {
                NodeId = 34704,
            },
        };
    }
}
