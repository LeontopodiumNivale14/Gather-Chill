using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
    public class BTN_761 : RouteInfo
    {
        public override uint Id => 761;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 813;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-812.2f, 253.056f);
        public override int Radius => 1;

        public override HashSet<uint> NodeIds => new()
        {
            33591,
        };

        public override HashSet<uint> ItemIds => new()
        {
            32954,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33591,
            },
        };
    }
}
