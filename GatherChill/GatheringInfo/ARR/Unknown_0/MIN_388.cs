using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_388 : RouteInfo
    {
        public override uint Id => 388;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-441.522f, 717.834f);
        public override int Radius => 105;

        public override HashSet<uint> NodeIds => new()
        {
            31632,
            31634,
            31660,
            31662,
            31688,
            31690,
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
                NodeId = 31632,
            },
            new NodeInfo
            {
                NodeId = 31634,
            },
            new NodeInfo
            {
                NodeId = 31660,
            },
            new NodeInfo
            {
                NodeId = 31662,
            },
            new NodeInfo
            {
                NodeId = 31688,
            },
            new NodeInfo
            {
                NodeId = 31690,
            },
        };
    }
}
