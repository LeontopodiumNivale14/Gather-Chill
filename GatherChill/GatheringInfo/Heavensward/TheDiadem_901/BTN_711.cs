using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_901
{
    public class BTN_711 : RouteInfo
    {
        public override uint Id => 711;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 901;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-592.814f, 442.945f);
        public override int Radius => 11;

        public override HashSet<uint> NodeIds => new()
        {
            33231,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29944,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33231,
            },
        };
    }
}
