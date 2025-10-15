using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheAzimSteppe_622
{
    public class BTN_523 : RouteInfo
    {
        public override uint Id => 523;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 622;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(283.659f, -259.556f);
        public override int Radius => 15;

        public override HashSet<uint> NodeIds => new()
        {
            32271,
        };

        public override HashSet<uint> ItemIds => new()
        {
            19852,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32271,
            },
        };
    }
}
