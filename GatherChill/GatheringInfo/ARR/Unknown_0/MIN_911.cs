using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_911 : RouteInfo
    {
        public override uint Id => 911;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-584.916f, 490.506f);
        public override int Radius => 64;

        public override HashSet<uint> NodeIds => new()
        {
            34382,
            34383,
            34384,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8,
            38279,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34382,
            },
            new NodeInfo
            {
                NodeId = 34383,
            },
            new NodeInfo
            {
                NodeId = 34384,
            },
        };
    }
}
