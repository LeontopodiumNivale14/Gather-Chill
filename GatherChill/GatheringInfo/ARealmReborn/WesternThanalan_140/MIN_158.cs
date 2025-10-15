using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.WesternThanalan_140
{
    public class MIN_158 : RouteInfo
    {
        public override uint Id => 158;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 140;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(300.024f, -223.742f);
        public override int Radius => 64;

        public override HashSet<uint> NodeIds => new()
        {
            30429,
            30430,
            30431,
            30432,
        };

        public override HashSet<uint> ItemIds => new()
        {
            7,
            5111,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30429,
            },
            new NodeInfo
            {
                NodeId = 30430,
            },
            new NodeInfo
            {
                NodeId = 30431,
            },
            new NodeInfo
            {
                NodeId = 30432,
            },
        };
    }
}
