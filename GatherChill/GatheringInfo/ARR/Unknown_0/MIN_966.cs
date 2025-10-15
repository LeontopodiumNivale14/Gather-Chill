using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_966 : RouteInfo
    {
        public override uint Id => 966;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(515.154f, 611.005f);
        public override int Radius => 67;

        public override HashSet<uint> NodeIds => new()
        {
            34687,
            34688,
            34689,
            34690,
            34691,
            34692,
            34693,
            34694,
            34695,
            34696,
            34697,
            34698,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003547,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34687,
            },
            new NodeInfo
            {
                NodeId = 34688,
            },
            new NodeInfo
            {
                NodeId = 34689,
            },
            new NodeInfo
            {
                NodeId = 34690,
            },
            new NodeInfo
            {
                NodeId = 34691,
            },
            new NodeInfo
            {
                NodeId = 34692,
            },
            new NodeInfo
            {
                NodeId = 34693,
            },
            new NodeInfo
            {
                NodeId = 34694,
            },
            new NodeInfo
            {
                NodeId = 34695,
            },
            new NodeInfo
            {
                NodeId = 34696,
            },
            new NodeInfo
            {
                NodeId = 34697,
            },
            new NodeInfo
            {
                NodeId = 34698,
            },
        };
    }
}
