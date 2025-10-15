using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Shaaloani_1190
{
    public class BTN_1028 : RouteInfo
    {
        public override uint Id => 1028;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1190;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(509.286f, -52.8059f);
        public override int Radius => 79;

        public override HashSet<uint> NodeIds => new()
        {
            34988,
        };

        public override HashSet<uint> ItemIds => new()
        {
            43929,
            44234,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34988,
            },
        };
    }
}
