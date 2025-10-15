using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Garlemald_958
{
    public class MIN_828 : RouteInfo
    {
        public override uint Id => 828;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 958;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-569.177f, -308.817f);
        public override int Radius => 98;

        public override HashSet<uint> NodeIds => new()
        {
            33961,
            34048,
            34049,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            36286,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33961,
            },
            new NodeInfo
            {
                NodeId = 34048,
            },
            new NodeInfo
            {
                NodeId = 34049,
            },
        };
    }
}
