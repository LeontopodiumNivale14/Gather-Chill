using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.ThePeaks_620
{
    public class BTN_777 : RouteInfo
    {
        public override uint Id => 777;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 620;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(454.637f, -731.747f);
        public override int Radius => 1;

        public override HashSet<uint> NodeIds => new()
        {
            33028,
        };

        public override HashSet<uint> ItemIds => new()
        {
            33009,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33028,
            },
        };
    }
}
