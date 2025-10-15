using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.AzysLla_402
{
    public class BTN_738 : RouteInfo
    {
        public override uint Id => 738;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 402;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-780.589f, 608.759f);
        public override int Radius => 95;

        public override HashSet<uint> NodeIds => new()
        {
            33368,
            33369,
            33370,
            33371,
            33372,
            33373,
        };

        public override HashSet<uint> ItemIds => new()
        {
            31130,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33368,
            },
            new NodeInfo
            {
                NodeId = 33369,
            },
            new NodeInfo
            {
                NodeId = 33370,
            },
            new NodeInfo
            {
                NodeId = 33371,
            },
            new NodeInfo
            {
                NodeId = 33372,
            },
            new NodeInfo
            {
                NodeId = 33373,
            },
        };
    }
}
