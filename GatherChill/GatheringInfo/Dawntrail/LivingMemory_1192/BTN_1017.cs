using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.LivingMemory_1192
{
    public class BTN_1017 : RouteInfo
    {
        public override uint Id => 1017;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1192;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(271.215f, -705.897f);
        public override int Radius => 113;

        public override HashSet<uint> NodeIds => new()
        {
            34967,
            34968,
            34969,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8,
            43934,
            46248,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34967,
            },
            new NodeInfo
            {
                NodeId = 34968,
            },
            new NodeInfo
            {
                NodeId = 34969,
            },
        };
    }
}
