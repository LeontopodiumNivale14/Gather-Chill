using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_127 : RouteInfo
    {
        public override uint Id => 127;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-784.005f, -174.372f);
        public override int Radius => 40;

        public override HashSet<uint> NodeIds => new()
        {
            30942,
            30943,
            30944,
            30945,
            30946,
            30947,
            30948,
            30949,
            30950,
            30951,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000902,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30942,
            },
            new NodeInfo
            {
                NodeId = 30943,
            },
            new NodeInfo
            {
                NodeId = 30944,
            },
            new NodeInfo
            {
                NodeId = 30945,
            },
            new NodeInfo
            {
                NodeId = 30946,
            },
            new NodeInfo
            {
                NodeId = 30947,
            },
            new NodeInfo
            {
                NodeId = 30948,
            },
            new NodeInfo
            {
                NodeId = 30949,
            },
            new NodeInfo
            {
                NodeId = 30950,
            },
            new NodeInfo
            {
                NodeId = 30951,
            },
        };
    }
}
