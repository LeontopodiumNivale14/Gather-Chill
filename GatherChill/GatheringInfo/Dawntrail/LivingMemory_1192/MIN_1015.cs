using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.LivingMemory_1192
{
    public class MIN_1015 : RouteInfo
    {
        public override uint Id => 1015;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1192;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-549.185f, -519.929f);
        public override int Radius => 167;

        public override HashSet<uint> NodeIds => new()
        {
            34961,
            34962,
            34963,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            43932,
            46247,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34961,
            },
            new NodeInfo
            {
                NodeId = 34962,
            },
            new NodeInfo
            {
                NodeId = 34963,
            },
        };
    }
}
