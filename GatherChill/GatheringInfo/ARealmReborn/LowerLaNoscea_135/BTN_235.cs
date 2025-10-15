using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.LowerLaNoscea_135
{
    public class BTN_235 : RouteInfo
    {
        public override uint Id => 235;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 135;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(551.708f, -424.802f);
        public override int Radius => 56;

        public override HashSet<uint> NodeIds => new()
        {
            31029,
        };

        public override HashSet<uint> ItemIds => new()
        {
            7594,
            10098,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31029,
            },
        };
    }
}
