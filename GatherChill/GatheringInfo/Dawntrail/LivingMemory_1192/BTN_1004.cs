using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.LivingMemory_1192
{
    public class BTN_1004 : RouteInfo
    {
        public override uint Id => 1004;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1192;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-225.64f, -179.957f);
        public override int Radius => 138;

        public override HashSet<uint> NodeIds => new()
        {
            34929,
            34930,
            34931,
            34932,
            34933,
            34934,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8,
            43983,
            44039,
            44041,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34929,
            },
            new NodeInfo
            {
                NodeId = 34930,
            },
            new NodeInfo
            {
                NodeId = 34931,
            },
            new NodeInfo
            {
                NodeId = 34932,
            },
            new NodeInfo
            {
                NodeId = 34933,
            },
            new NodeInfo
            {
                NodeId = 34934,
            },
        };
    }
}
