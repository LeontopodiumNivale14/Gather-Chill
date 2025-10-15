using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheLochs_621
{
    public class BTN_736 : RouteInfo
    {
        public override uint Id => 736;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 621;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-19.8197f, -116.515f);
        public override int Radius => 106;

        public override HashSet<uint> NodeIds => new()
        {
            33356,
            33357,
            33358,
            33359,
            33360,
            33361,
        };

        public override HashSet<uint> ItemIds => new()
        {
            31125,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33356,
            },
            new NodeInfo
            {
                NodeId = 33357,
            },
            new NodeInfo
            {
                NodeId = 33358,
            },
            new NodeInfo
            {
                NodeId = 33359,
            },
            new NodeInfo
            {
                NodeId = 33360,
            },
            new NodeInfo
            {
                NodeId = 33361,
            },
        };
    }
}
