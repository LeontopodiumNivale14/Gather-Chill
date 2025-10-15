using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianForelands_398
{
    public class MIN_286 : RouteInfo
    {
        public override uint Id => 286;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 398;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(391.639f, -370.668f);
        public override int Radius => 148;

        public override HashSet<uint> NodeIds => new()
        {
            31322,
            31323,
            31324,
            31325,
            31326,
            31327,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8,
            5109,
            12554,
            12555,
            12941,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31322,
            },
            new NodeInfo
            {
                NodeId = 31323,
            },
            new NodeInfo
            {
                NodeId = 31324,
            },
            new NodeInfo
            {
                NodeId = 31325,
            },
            new NodeInfo
            {
                NodeId = 31326,
            },
            new NodeInfo
            {
                NodeId = 31327,
            },
        };
    }
}
