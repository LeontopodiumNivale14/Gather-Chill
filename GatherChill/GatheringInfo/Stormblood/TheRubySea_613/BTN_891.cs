using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
    public class BTN_891 : RouteInfo
    {
        public override uint Id => 891;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 613;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(-496.181f, -379.899f);
        public override int Radius => 110;

        public override HashSet<uint> NodeIds => new()
        {
            34308,
            34309,
            34310,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34308,
            },
            new NodeInfo
            {
                NodeId = 34309,
            },
            new NodeInfo
            {
                NodeId = 34310,
            },
        };
    }
}
