using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_542 : RouteInfo
    {
        public override uint Id => 542;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(155.84f, -57.1385f);
        public override int Radius => 25;

        public override HashSet<uint> NodeIds => new()
        {
            32317,
            32318,
            32319,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            22618,
            22626,
            22629,
            22638,
            22640,
            23169,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32317,
            },
            new NodeInfo
            {
                NodeId = 32318,
            },
            new NodeInfo
            {
                NodeId = 32319,
            },
        };
    }
}
