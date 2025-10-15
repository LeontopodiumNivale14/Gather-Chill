using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.ThePeaks_620
{
    public class BTN_524 : RouteInfo
    {
        public override uint Id => 524;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 620;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(128.766f, -246.946f);
        public override int Radius => 29;

        public override HashSet<uint> NodeIds => new()
        {
            32272,
        };

        public override HashSet<uint> ItemIds => new()
        {
            19918,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32272,
            },
        };
    }
}
