using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.ThePeaks_620
{
    public class BTN_538 : RouteInfo
    {
        public override uint Id => 538;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 620;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(161.899f, 729.604f);
        public override int Radius => 15;

        public override HashSet<uint> NodeIds => new()
        {
            32311,
        };

        public override HashSet<uint> ItemIds => new()
        {
            22420,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32311,
            },
        };
    }
}
