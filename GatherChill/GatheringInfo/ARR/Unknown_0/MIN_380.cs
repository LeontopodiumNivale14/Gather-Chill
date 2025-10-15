using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_380 : RouteInfo
    {
        public override uint Id => 380;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(264.261f, 259.832f);
        public override int Radius => 108;

        public override HashSet<uint> NodeIds => new()
        {
            31614,
            31615,
            31642,
            31643,
            31670,
            31671,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8,
            12534,
            12535,
            12537,
            13750,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31614,
            },
            new NodeInfo
            {
                NodeId = 31615,
            },
            new NodeInfo
            {
                NodeId = 31642,
            },
            new NodeInfo
            {
                NodeId = 31643,
            },
            new NodeInfo
            {
                NodeId = 31670,
            },
            new NodeInfo
            {
                NodeId = 31671,
            },
        };
    }
}
