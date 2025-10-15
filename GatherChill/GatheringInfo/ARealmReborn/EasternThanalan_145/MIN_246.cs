using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EasternThanalan_145
{
    public class MIN_246 : RouteInfo
    {
        public override uint Id => 246;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 145;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(267.901f, -146.549f);
        public override int Radius => 45;

        public override HashSet<uint> NodeIds => new()
        {
            31054,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8030,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31054,
            },
        };
    }
}
