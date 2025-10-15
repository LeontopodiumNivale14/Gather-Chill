using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.MorDhona_156
{
    public class MIN_227 : RouteInfo
    {
        public override uint Id => 227;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 156;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(315.867f, -528.201f);
        public override int Radius => 81;

        public override HashSet<uint> NodeIds => new()
        {
            31015,
        };

        public override HashSet<uint> ItemIds => new()
        {
            14,
            15,
            16,
            17,
            18,
            19,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31015,
            },
        };
    }
}
