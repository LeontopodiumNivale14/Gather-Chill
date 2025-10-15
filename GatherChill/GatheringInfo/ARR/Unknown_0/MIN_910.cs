using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_910 : RouteInfo
    {
        public override uint Id => 910;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(450.449f, -593.286f);
        public override int Radius => 5;

        public override HashSet<uint> NodeIds => new()
        {
            34379,
            34380,
            34381,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            38280,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34379,
            },
            new NodeInfo
            {
                NodeId = 34380,
            },
            new NodeInfo
            {
                NodeId = 34381,
            },
        };
    }
}
