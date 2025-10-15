using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_279 : RouteInfo
    {
        public override uint Id => 279;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-293.566f, 500.576f);
        public override int Radius => 143;

        public override HashSet<uint> NodeIds => new()
        {
            31273,
            31274,
            31275,
            31276,
            31277,
            31278,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001838,
            2001839,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31273,
            },
            new NodeInfo
            {
                NodeId = 31274,
            },
            new NodeInfo
            {
                NodeId = 31275,
            },
            new NodeInfo
            {
                NodeId = 31276,
            },
            new NodeInfo
            {
                NodeId = 31277,
            },
            new NodeInfo
            {
                NodeId = 31278,
            },
        };
    }
}
