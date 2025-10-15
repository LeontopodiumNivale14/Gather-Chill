using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_130 : RouteInfo
    {
        public override uint Id => 130;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(427.618f, -404.949f);
        public override int Radius => 27;

        public override HashSet<uint> NodeIds => new()
        {
            30968,
            30969,
            30970,
            30971,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000907,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30968,
            },
            new NodeInfo
            {
                NodeId = 30969,
            },
            new NodeInfo
            {
                NodeId = 30970,
            },
            new NodeInfo
            {
                NodeId = 30971,
            },
        };
    }
}
