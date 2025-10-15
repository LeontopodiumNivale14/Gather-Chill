using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianForelands_398
{
    public class BTN_769 : RouteInfo
    {
        public override uint Id => 769;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 398;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-320.871f, 695.344f);
        public override int Radius => 1;

        public override HashSet<uint> NodeIds => new()
        {
            31513,
        };

        public override HashSet<uint> ItemIds => new()
        {
            32995,
            32996,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31513,
            },
        };
    }
}
