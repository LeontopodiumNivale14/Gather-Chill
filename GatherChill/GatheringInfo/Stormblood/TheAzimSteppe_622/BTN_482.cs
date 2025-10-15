using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheAzimSteppe_622
{
    public class BTN_482 : RouteInfo
    {
        public override uint Id => 482;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 622;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(5.20535f, 159.792f);
        public override int Radius => 16;

        public override HashSet<uint> NodeIds => new()
        {
            32121,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32121,
            },
        };
    }
}
