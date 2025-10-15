using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1195 : RouteInfo
    {
        public override uint Id => 1195;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-659.97f, 373.022f);
        public override int Radius => 24;

        public override HashSet<uint> NodeIds => new()
        {
            35246,
            35247,
            35248,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            48015,
            48057,
            48066,
            48072,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35246,
            },
            new NodeInfo
            {
                NodeId = 35247,
            },
            new NodeInfo
            {
                NodeId = 35248,
            },
        };
    }
}
