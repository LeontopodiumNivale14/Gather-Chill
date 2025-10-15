using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1053 : RouteInfo
    {
        public override uint Id => 1053;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-517.104f, 375.63f);
        public override int Radius => 77;

        public override HashSet<uint> NodeIds => new()
        {
            35093,
            35094,
            35095,
            35096,
            35097,
            35098,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35093,
            },
            new NodeInfo
            {
                NodeId = 35094,
            },
            new NodeInfo
            {
                NodeId = 35095,
            },
            new NodeInfo
            {
                NodeId = 35096,
            },
            new NodeInfo
            {
                NodeId = 35097,
            },
            new NodeInfo
            {
                NodeId = 35098,
            },
        };
    }
}
