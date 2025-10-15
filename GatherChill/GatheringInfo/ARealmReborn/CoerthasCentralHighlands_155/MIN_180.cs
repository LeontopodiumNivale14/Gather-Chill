using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CoerthasCentralHighlands_155
{
    public class MIN_180 : RouteInfo
    {
        public override uint Id => 180;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 155;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(138.483f, -78.8638f);
        public override int Radius => 48;

        public override HashSet<uint> NodeIds => new()
        {
            30504,
            30505,
            30506,
            30507,
        };

        public override HashSet<uint> ItemIds => new()
        {
            3,
            5141,
            5168,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30504,
            },
            new NodeInfo
            {
                NodeId = 30505,
            },
            new NodeInfo
            {
                NodeId = 30506,
            },
            new NodeInfo
            {
                NodeId = 30507,
            },
        };
    }
}
