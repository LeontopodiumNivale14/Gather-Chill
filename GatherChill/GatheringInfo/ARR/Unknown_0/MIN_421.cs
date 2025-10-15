using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_421 : RouteInfo
    {
        public override uint Id => 421;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(262.686f, 311.612f);
        public override int Radius => 78;

        public override HashSet<uint> NodeIds => new()
        {
            31785,
        };

        public override HashSet<uint> ItemIds => new()
        {
            14,
            15,
            18,
            12538,
            12942,
            13750,
            17570,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31785,
            },
        };
    }
}
