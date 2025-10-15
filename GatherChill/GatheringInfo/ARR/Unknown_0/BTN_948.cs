using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_948 : RouteInfo
    {
        public override uint Id => 948;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(97.6912f, 345.158f);
        public override int Radius => 52;

        public override HashSet<uint> NodeIds => new()
        {
            34535,
            34536,
            34537,
            34538,
            34539,
            34540,
            34541,
            34542,
            34543,
            34544,
            34545,
            34546,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003520,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34535,
            },
            new NodeInfo
            {
                NodeId = 34536,
            },
            new NodeInfo
            {
                NodeId = 34537,
            },
            new NodeInfo
            {
                NodeId = 34538,
            },
            new NodeInfo
            {
                NodeId = 34539,
            },
            new NodeInfo
            {
                NodeId = 34540,
            },
            new NodeInfo
            {
                NodeId = 34541,
            },
            new NodeInfo
            {
                NodeId = 34542,
            },
            new NodeInfo
            {
                NodeId = 34543,
            },
            new NodeInfo
            {
                NodeId = 34544,
            },
            new NodeInfo
            {
                NodeId = 34545,
            },
            new NodeInfo
            {
                NodeId = 34546,
            },
        };
    }
}
