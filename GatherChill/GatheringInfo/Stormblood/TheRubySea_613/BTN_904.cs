using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
    public class BTN_904 : RouteInfo
    {
        public override uint Id => 904;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 613;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(112.16f, 289.924f);
        public override int Radius => 77;

        public override HashSet<uint> NodeIds => new()
        {
            34344,
            34345,
            34346,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34344,
            },
            new NodeInfo
            {
                NodeId = 34345,
            },
            new NodeInfo
            {
                NodeId = 34346,
            },
        };
    }
}
