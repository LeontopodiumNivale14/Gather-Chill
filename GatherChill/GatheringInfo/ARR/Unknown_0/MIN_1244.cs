using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1244 : RouteInfo
    {
        public override uint Id => 1244;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-555.915f, 645.703f);
        public override int Radius => 70;

        public override HashSet<uint> NodeIds => new()
        {
            35345,
            35346,
            35347,
            35348,
            35349,
            35350,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47375,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35345,
            },
            new NodeInfo
            {
                NodeId = 35346,
            },
            new NodeInfo
            {
                NodeId = 35347,
            },
            new NodeInfo
            {
                NodeId = 35348,
            },
            new NodeInfo
            {
                NodeId = 35349,
            },
            new NodeInfo
            {
                NodeId = 35350,
            },
        };
    }
}
