using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EasternLaNoscea_137
{
    public class MIN_224 : RouteInfo
    {
        public override uint Id => 224;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 137;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-107.096f, 578.964f);
        public override int Radius => 112;

        public override HashSet<uint> NodeIds => new()
        {
            31012,
        };

        public override HashSet<uint> ItemIds => new()
        {
            6152,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31012,
            },
        };
    }
}
