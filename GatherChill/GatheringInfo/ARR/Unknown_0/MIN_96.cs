using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_96 : RouteInfo
    {
        public override uint Id => 96;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(266.833f, 44.0638f);
        public override int Radius => 32;

        public override HashSet<uint> NodeIds => new()
        {
            30708,
            30709,
            30710,
            30711,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000474,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30708,
            },
            new NodeInfo
            {
                NodeId = 30709,
            },
            new NodeInfo
            {
                NodeId = 30710,
            },
            new NodeInfo
            {
                NodeId = 30711,
            },
        };
    }
}
