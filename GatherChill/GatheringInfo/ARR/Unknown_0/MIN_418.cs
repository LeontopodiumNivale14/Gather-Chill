using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_418 : RouteInfo
    {
        public override uint Id => 418;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(691.557f, -193.984f);
        public override int Radius => 155;

        public override HashSet<uint> NodeIds => new()
        {
            31773,
            31774,
            31775,
            31776,
        };

        public override HashSet<uint> ItemIds => new()
        {
            6,
            12,
            5121,
            12534,
            12535,
            12537,
            13750,
            17570,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31773,
            },
            new NodeInfo
            {
                NodeId = 31774,
            },
            new NodeInfo
            {
                NodeId = 31775,
            },
            new NodeInfo
            {
                NodeId = 31776,
            },
        };
    }
}
