using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_126 : RouteInfo
    {
        public override uint Id => 126;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-715.314f, -20.6487f);
        public override int Radius => 29;

        public override HashSet<uint> NodeIds => new()
        {
            30938,
            30939,
            30940,
            30941,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000901,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30938,
            },
            new NodeInfo
            {
                NodeId = 30939,
            },
            new NodeInfo
            {
                NodeId = 30940,
            },
            new NodeInfo
            {
                NodeId = 30941,
            },
        };
    }
}
