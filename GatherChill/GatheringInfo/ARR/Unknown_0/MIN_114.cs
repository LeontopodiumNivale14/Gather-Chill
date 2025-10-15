using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_114 : RouteInfo
    {
        public override uint Id => 114;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(165.314f, -130.277f);
        public override int Radius => 9;

        public override HashSet<uint> NodeIds => new()
        {
            30840,
            30841,
            30842,
            30843,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000501,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30840,
            },
            new NodeInfo
            {
                NodeId = 30841,
            },
            new NodeInfo
            {
                NodeId = 30842,
            },
            new NodeInfo
            {
                NodeId = 30843,
            },
        };
    }
}
