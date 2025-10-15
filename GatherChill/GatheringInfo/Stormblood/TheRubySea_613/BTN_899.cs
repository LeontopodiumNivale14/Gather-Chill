using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
    public class BTN_899 : RouteInfo
    {
        public override uint Id => 899;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 613;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(86.0532f, -901.293f);
        public override int Radius => 10;

        public override HashSet<uint> NodeIds => new()
        {
            34332,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34332,
            },
        };
    }
}
