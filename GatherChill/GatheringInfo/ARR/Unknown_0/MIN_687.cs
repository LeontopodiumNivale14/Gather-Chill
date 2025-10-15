using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_687 : RouteInfo
    {
        public override uint Id => 687;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(454.364f, -462.659f);
        public override int Radius => 22;

        public override HashSet<uint> NodeIds => new()
        {
            32997,
            32998,
            32999,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            29514,
            29518,
            29524,
            29529,
            29533,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32997,
            },
            new NodeInfo
            {
                NodeId = 32998,
            },
            new NodeInfo
            {
                NodeId = 32999,
            },
        };
    }
}
