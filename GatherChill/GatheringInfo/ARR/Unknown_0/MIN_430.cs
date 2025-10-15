using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_430 : RouteInfo
    {
        public override uint Id => 430;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(280.318f, 232.184f);
        public override int Radius => 143;

        public override HashSet<uint> NodeIds => new()
        {
            31807,
            31808,
            31809,
        };

        public override HashSet<uint> ItemIds => new()
        {
            6,
            12,
            7588,
            12534,
            13750,
            17570,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31807,
            },
            new NodeInfo
            {
                NodeId = 31808,
            },
            new NodeInfo
            {
                NodeId = 31809,
            },
        };
    }
}
