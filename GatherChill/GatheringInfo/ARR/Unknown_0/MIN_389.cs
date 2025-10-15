using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_389 : RouteInfo
    {
        public override uint Id => 389;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-469.599f, 500.412f);
        public override int Radius => 177;

        public override HashSet<uint> NodeIds => new()
        {
            31635,
            31636,
            31637,
            31663,
            31664,
            31665,
            31691,
            31692,
            31693,
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
                NodeId = 31635,
            },
            new NodeInfo
            {
                NodeId = 31636,
            },
            new NodeInfo
            {
                NodeId = 31637,
            },
            new NodeInfo
            {
                NodeId = 31663,
            },
            new NodeInfo
            {
                NodeId = 31664,
            },
            new NodeInfo
            {
                NodeId = 31665,
            },
            new NodeInfo
            {
                NodeId = 31691,
            },
            new NodeInfo
            {
                NodeId = 31692,
            },
            new NodeInfo
            {
                NodeId = 31693,
            },
        };
    }
}
