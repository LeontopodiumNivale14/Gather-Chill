using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.UpperLaNoscea_139
{
    public class MIN_658 : RouteInfo
    {
        public override uint Id => 658;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 139;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-604.127f, 22.8553f);
        public override int Radius => 43;

        public override HashSet<uint> NodeIds => new()
        {
            32859,
            32860,
            32861,
            32862,
        };

        public override HashSet<uint> ItemIds => new()
        {
            28770,
            28771,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32859,
            },
            new NodeInfo
            {
                NodeId = 32860,
            },
            new NodeInfo
            {
                NodeId = 32861,
            },
            new NodeInfo
            {
                NodeId = 32862,
            },
        };
    }
}
