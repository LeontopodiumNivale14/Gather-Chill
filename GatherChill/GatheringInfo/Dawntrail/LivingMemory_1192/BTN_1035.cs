using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.LivingMemory_1192
{
    public class BTN_1035 : RouteInfo
    {
        public override uint Id => 1035;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1192;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(346.403f, -204.298f);
        public override int Radius => 34;

        public override HashSet<uint> NodeIds => new()
        {
            34995,
        };

        public override HashSet<uint> ItemIds => new()
        {
            44138,
            44844,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34995,
            },
        };
    }
}
