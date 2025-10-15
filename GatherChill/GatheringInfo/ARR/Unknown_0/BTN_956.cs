using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_956 : RouteInfo
    {
        public override uint Id => 956;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-309.611f, -489.151f);
        public override int Radius => 207;

        public override HashSet<uint> NodeIds => new()
        {
            34603,
            34604,
            34605,
            34606,
            34607,
            34608,
            34609,
            34610,
            34611,
            34612,
            34613,
            34614,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003531,
            2003532,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34603,
            },
            new NodeInfo
            {
                NodeId = 34604,
            },
            new NodeInfo
            {
                NodeId = 34605,
            },
            new NodeInfo
            {
                NodeId = 34606,
            },
            new NodeInfo
            {
                NodeId = 34607,
            },
            new NodeInfo
            {
                NodeId = 34608,
            },
            new NodeInfo
            {
                NodeId = 34609,
            },
            new NodeInfo
            {
                NodeId = 34610,
            },
            new NodeInfo
            {
                NodeId = 34611,
            },
            new NodeInfo
            {
                NodeId = 34612,
            },
            new NodeInfo
            {
                NodeId = 34613,
            },
            new NodeInfo
            {
                NodeId = 34614,
            },
        };
    }
}
