using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Thavnair_957
{
    public class BTN_627 : RouteInfo
    {
        public override uint Id => 627;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 957;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(381.055f, 268.24f);
        public override int Radius => 66;

        public override HashSet<uint> NodeIds => new()
        {
            34362,
        };

        public override HashSet<uint> ItemIds => new()
        {
            37819,
            39906,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34362,
            },
        };
    }
}
