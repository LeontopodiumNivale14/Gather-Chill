using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_387 : RouteInfo
    {
        public override uint Id => 387;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-365.437f, 653.2f);
        public override int Radius => 50;

        public override HashSet<uint> NodeIds => new()
        {
            31631,
            31633,
            31659,
            31661,
            31687,
            31689,
        };

        public override HashSet<uint> ItemIds => new()
        {
            11,
            12534,
            12535,
            12537,
            13750,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31631,
            },
            new NodeInfo
            {
                NodeId = 31633,
            },
            new NodeInfo
            {
                NodeId = 31659,
            },
            new NodeInfo
            {
                NodeId = 31661,
            },
            new NodeInfo
            {
                NodeId = 31687,
            },
            new NodeInfo
            {
                NodeId = 31689,
            },
        };
    }
}
