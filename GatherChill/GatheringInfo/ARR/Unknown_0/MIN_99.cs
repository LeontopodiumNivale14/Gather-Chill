using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_99 : RouteInfo
    {
        public override uint Id => 99;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-52.1578f, 393.169f);
        public override int Radius => 24;

        public override HashSet<uint> NodeIds => new()
        {
            30732,
            30733,
            30734,
            30735,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000479,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30732,
            },
            new NodeInfo
            {
                NodeId = 30733,
            },
            new NodeInfo
            {
                NodeId = 30734,
            },
            new NodeInfo
            {
                NodeId = 30735,
            },
        };
    }
}
