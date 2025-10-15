using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianForelands_398
{
    public class MIN_352 : RouteInfo
    {
        public override uint Id => 352;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 398;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-192.504f, 308.952f);
        public override int Radius => 132;

        public override HashSet<uint> NodeIds => new()
        {
            31346,
            31347,
            31348,
            31349,
            31350,
            31351,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8,
            12556,
            12560,
            13760,
            14190,
            17561,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31346,
            },
            new NodeInfo
            {
                NodeId = 31347,
            },
            new NodeInfo
            {
                NodeId = 31348,
            },
            new NodeInfo
            {
                NodeId = 31349,
            },
            new NodeInfo
            {
                NodeId = 31350,
            },
            new NodeInfo
            {
                NodeId = 31351,
            },
        };
    }
}
