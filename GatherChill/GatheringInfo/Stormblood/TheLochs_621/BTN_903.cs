using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheLochs_621
{
    public class BTN_903 : RouteInfo
    {
        public override uint Id => 903;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 621;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(-54.4207f, 409.333f);
        public override int Radius => 75;

        public override HashSet<uint> NodeIds => new()
        {
            34341,
            34342,
            34343,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34341,
            },
            new NodeInfo
            {
                NodeId = 34342,
            },
            new NodeInfo
            {
                NodeId = 34343,
            },
        };
    }
}
