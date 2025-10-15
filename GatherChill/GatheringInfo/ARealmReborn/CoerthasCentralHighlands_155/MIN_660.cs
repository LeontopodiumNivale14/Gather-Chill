using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CoerthasCentralHighlands_155
{
    public class MIN_660 : RouteInfo
    {
        public override uint Id => 660;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 155;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(157.524f, -383.834f);
        public override int Radius => 39;

        public override HashSet<uint> NodeIds => new()
        {
            32867,
            32868,
            32869,
            32870,
        };

        public override HashSet<uint> ItemIds => new()
        {
            28774,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32867,
            },
            new NodeInfo
            {
                NodeId = 32868,
            },
            new NodeInfo
            {
                NodeId = 32869,
            },
            new NodeInfo
            {
                NodeId = 32870,
            },
        };
    }
}
