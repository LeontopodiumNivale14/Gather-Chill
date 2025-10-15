using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
    public class MIN_778 : RouteInfo
    {
        public override uint Id => 778;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 397;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(650.331f, -60.3473f);
        public override int Radius => 127;

        public override HashSet<uint> NodeIds => new()
        {
            33029,
            33030,
            33621,
        };

        public override HashSet<uint> ItemIds => new()
        {
            32988,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33029,
            },
            new NodeInfo
            {
                NodeId = 33030,
            },
            new NodeInfo
            {
                NodeId = 33621,
            },
        };
    }
}
