using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.WesternLaNoscea_138
{
    public class MIN_210 : RouteInfo
    {
        public override uint Id => 210;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 138;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(702.076f, 401.806f);
        public override int Radius => 60;

        public override HashSet<uint> NodeIds => new()
        {
            30548,
            30549,
            30550,
            30551,
        };

        public override HashSet<uint> ItemIds => new()
        {
            3,
            9,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30548,
            },
            new NodeInfo
            {
                NodeId = 30549,
            },
            new NodeInfo
            {
                NodeId = 30550,
            },
            new NodeInfo
            {
                NodeId = 30551,
            },
        };
    }
}
