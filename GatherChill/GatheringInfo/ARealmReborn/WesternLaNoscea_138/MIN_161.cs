using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.WesternLaNoscea_138
{
    public class MIN_161 : RouteInfo
    {
        public override uint Id => 161;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 138;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(361.147f, 61.1269f);
        public override int Radius => 69;

        public override HashSet<uint> NodeIds => new()
        {
            30441,
            30442,
            30443,
            30444,
        };

        public override HashSet<uint> ItemIds => new()
        {
            3,
            5129,
            5465,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30441,
            },
            new NodeInfo
            {
                NodeId = 30442,
            },
            new NodeInfo
            {
                NodeId = 30443,
            },
            new NodeInfo
            {
                NodeId = 30444,
            },
        };
    }
}
