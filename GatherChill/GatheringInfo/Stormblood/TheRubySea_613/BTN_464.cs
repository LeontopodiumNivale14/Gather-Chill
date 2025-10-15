using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
    public class BTN_464 : RouteInfo
    {
        public override uint Id => 464;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 613;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(581.035f, 444.313f);
        public override int Radius => 114;

        public override HashSet<uint> NodeIds => new()
        {
            32071,
            32072,
            32073,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32071,
            },
            new NodeInfo
            {
                NodeId = 32072,
            },
            new NodeInfo
            {
                NodeId = 32073,
            },
        };
    }
}
