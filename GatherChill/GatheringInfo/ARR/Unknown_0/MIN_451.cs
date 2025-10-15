using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_451 : RouteInfo
    {
        public override uint Id => 451;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-56.2095f, -531.456f);
        public override int Radius => 26;

        public override HashSet<uint> NodeIds => new()
        {
            31969,
            31970,
            31971,
            31972,
            31973,
            31974,
            31975,
            31976,
            31977,
            31978,
            31979,
            31980,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002148,
            2002149,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31969,
            },
            new NodeInfo
            {
                NodeId = 31970,
            },
            new NodeInfo
            {
                NodeId = 31971,
            },
            new NodeInfo
            {
                NodeId = 31972,
            },
            new NodeInfo
            {
                NodeId = 31973,
            },
            new NodeInfo
            {
                NodeId = 31974,
            },
            new NodeInfo
            {
                NodeId = 31975,
            },
            new NodeInfo
            {
                NodeId = 31976,
            },
            new NodeInfo
            {
                NodeId = 31977,
            },
            new NodeInfo
            {
                NodeId = 31978,
            },
            new NodeInfo
            {
                NodeId = 31979,
            },
            new NodeInfo
            {
                NodeId = 31980,
            },
        };
    }
}
