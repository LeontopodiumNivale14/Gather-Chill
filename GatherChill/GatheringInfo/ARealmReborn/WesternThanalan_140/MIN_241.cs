using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.WesternThanalan_140
{
    public class MIN_241 : RouteInfo
    {
        public override uint Id => 241;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 140;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-128.888f, 343.679f);
        public override int Radius => 50;

        public override HashSet<uint> NodeIds => new()
        {
            31035,
        };

        public override HashSet<uint> ItemIds => new()
        {
            19,
            7766,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31035,
            },
        };
    }
}
