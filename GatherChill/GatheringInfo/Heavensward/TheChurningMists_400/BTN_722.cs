using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheChurningMists_400
{
    public class BTN_722 : RouteInfo
    {
        public override uint Id => 722;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 400;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(134.874f, 274.358f);
        public override int Radius => 87;

        public override HashSet<uint> NodeIds => new()
        {
            33277,
            33278,
            33279,
            33280,
            33281,
            33282,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29674,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33277,
            },
            new NodeInfo
            {
                NodeId = 33278,
            },
            new NodeInfo
            {
                NodeId = 33279,
            },
            new NodeInfo
            {
                NodeId = 33280,
            },
            new NodeInfo
            {
                NodeId = 33281,
            },
            new NodeInfo
            {
                NodeId = 33282,
            },
        };
    }
}
