using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
    public class MIN_724 : RouteInfo
    {
        public override uint Id => 724;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 397;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-490.146f, 55.2148f);
        public override int Radius => 99;

        public override HashSet<uint> NodeIds => new()
        {
            33289,
            33290,
            33291,
            33292,
            33293,
            33294,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            30498,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33289,
            },
            new NodeInfo
            {
                NodeId = 33290,
            },
            new NodeInfo
            {
                NodeId = 33291,
            },
            new NodeInfo
            {
                NodeId = 33292,
            },
            new NodeInfo
            {
                NodeId = 33293,
            },
            new NodeInfo
            {
                NodeId = 33294,
            },
        };
    }
}
