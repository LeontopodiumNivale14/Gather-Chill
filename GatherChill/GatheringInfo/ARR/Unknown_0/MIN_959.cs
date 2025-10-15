using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_959 : RouteInfo
    {
        public override uint Id => 959;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-187.672f, -265.778f);
        public override int Radius => 114;

        public override HashSet<uint> NodeIds => new()
        {
            34625,
            34626,
            34627,
            34628,
            34629,
            34630,
            34631,
            34632,
            34633,
            34634,
            34635,
            34636,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003536,
            2003537,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34625,
            },
            new NodeInfo
            {
                NodeId = 34626,
            },
            new NodeInfo
            {
                NodeId = 34627,
            },
            new NodeInfo
            {
                NodeId = 34628,
            },
            new NodeInfo
            {
                NodeId = 34629,
            },
            new NodeInfo
            {
                NodeId = 34630,
            },
            new NodeInfo
            {
                NodeId = 34631,
            },
            new NodeInfo
            {
                NodeId = 34632,
            },
            new NodeInfo
            {
                NodeId = 34633,
            },
            new NodeInfo
            {
                NodeId = 34634,
            },
            new NodeInfo
            {
                NodeId = 34635,
            },
            new NodeInfo
            {
                NodeId = 34636,
            },
        };
    }
}
