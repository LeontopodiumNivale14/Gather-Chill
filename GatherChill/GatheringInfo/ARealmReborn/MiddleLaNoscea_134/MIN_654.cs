using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.MiddleLaNoscea_134
{
    public class MIN_654 : RouteInfo
    {
        public override uint Id => 654;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 134;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(149.158f, -27.7525f);
        public override int Radius => 36;

        public override HashSet<uint> NodeIds => new()
        {
            32843,
            32844,
            32845,
            32846,
        };

        public override HashSet<uint> ItemIds => new()
        {
            28766,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32843,
            },
            new NodeInfo
            {
                NodeId = 32844,
            },
            new NodeInfo
            {
                NodeId = 32845,
            },
            new NodeInfo
            {
                NodeId = 32846,
            },
        };
    }
}
