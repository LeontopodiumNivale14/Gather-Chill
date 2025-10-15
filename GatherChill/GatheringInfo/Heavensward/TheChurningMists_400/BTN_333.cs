using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheChurningMists_400
{
    public class BTN_333 : RouteInfo
    {
        public override uint Id => 333;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 400;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-611.869f, 718.873f);
        public override int Radius => 131;

        public override HashSet<uint> NodeIds => new()
        {
            31479,
            31480,
            31481,
        };

        public override HashSet<uint> ItemIds => new()
        {
            11,
            12968,
            12969,
            12970,
            15948,
            33147,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31479,
            },
            new NodeInfo
            {
                NodeId = 31480,
            },
            new NodeInfo
            {
                NodeId = 31481,
            },
        };
    }
}
