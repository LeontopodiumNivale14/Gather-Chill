using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_110 : RouteInfo
    {
        public override uint Id => 110;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-82.9727f, -92.2295f);
        public override int Radius => 29;

        public override HashSet<uint> NodeIds => new()
        {
            30812,
            30813,
            30814,
            30815,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000496,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30812,
            },
            new NodeInfo
            {
                NodeId = 30813,
            },
            new NodeInfo
            {
                NodeId = 30814,
            },
            new NodeInfo
            {
                NodeId = 30815,
            },
        };
    }
}
