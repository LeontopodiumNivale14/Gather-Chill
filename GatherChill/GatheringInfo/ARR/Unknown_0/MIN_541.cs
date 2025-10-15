using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_541 : RouteInfo
    {
        public override uint Id => 541;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-504.577f, -398.716f);
        public override int Radius => 8;

        public override HashSet<uint> NodeIds => new()
        {
            32314,
            32315,
            32316,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            22616,
            22623,
            22624,
            22625,
            22641,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32314,
            },
            new NodeInfo
            {
                NodeId = 32315,
            },
            new NodeInfo
            {
                NodeId = 32316,
            },
        };
    }
}
