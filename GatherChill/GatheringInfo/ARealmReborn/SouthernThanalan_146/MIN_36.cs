using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.SouthernThanalan_146
{
    public class MIN_36 : RouteInfo
    {
        public override uint Id => 36;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 146;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-165.691f, -151.591f);
        public override int Radius => 48;

        public override HashSet<uint> NodeIds => new()
        {
            30038,
            30996,
            30997,
            30998,
        };

        public override HashSet<uint> ItemIds => new()
        {
            6,
            5271,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30038,
            },
            new NodeInfo
            {
                NodeId = 30996,
            },
            new NodeInfo
            {
                NodeId = 30997,
            },
            new NodeInfo
            {
                NodeId = 30998,
            },
        };
    }
}
