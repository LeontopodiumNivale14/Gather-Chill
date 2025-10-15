using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_874 : RouteInfo
    {
        public override uint Id => 874;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(219.58f, -782.093f);
        public override int Radius => 39;

        public override HashSet<uint> NodeIds => new()
        {
            34178,
            34179,
            34180,
            34181,
            34182,
            34183,
            34184,
            34185,
            34186,
            34187,
            34188,
            34189,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003154,
            2003155,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34178,
            },
            new NodeInfo
            {
                NodeId = 34179,
            },
            new NodeInfo
            {
                NodeId = 34180,
            },
            new NodeInfo
            {
                NodeId = 34181,
            },
            new NodeInfo
            {
                NodeId = 34182,
            },
            new NodeInfo
            {
                NodeId = 34183,
            },
            new NodeInfo
            {
                NodeId = 34184,
            },
            new NodeInfo
            {
                NodeId = 34185,
            },
            new NodeInfo
            {
                NodeId = 34186,
            },
            new NodeInfo
            {
                NodeId = 34187,
            },
            new NodeInfo
            {
                NodeId = 34188,
            },
            new NodeInfo
            {
                NodeId = 34189,
            },
        };
    }
}
