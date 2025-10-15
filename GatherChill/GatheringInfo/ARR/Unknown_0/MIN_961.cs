using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_961 : RouteInfo
    {
        public override uint Id => 961;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(266.024f, -205.297f);
        public override int Radius => 52;

        public override HashSet<uint> NodeIds => new()
        {
            34641,
            34642,
            34643,
            34644,
            34645,
            34646,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003539,
            2003540,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34641,
            },
            new NodeInfo
            {
                NodeId = 34642,
            },
            new NodeInfo
            {
                NodeId = 34643,
            },
            new NodeInfo
            {
                NodeId = 34644,
            },
            new NodeInfo
            {
                NodeId = 34645,
            },
            new NodeInfo
            {
                NodeId = 34646,
            },
        };
    }
}
