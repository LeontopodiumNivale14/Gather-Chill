using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_422 : RouteInfo
    {
        public override uint Id => 422;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(297.094f, -353.024f);
        public override int Radius => 21;

        public override HashSet<uint> NodeIds => new()
        {
            31786,
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
                NodeId = 31786,
            },
        };
    }
}
