using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.YakTel_1189
{
    public class BTN_988 : RouteInfo
    {
        public override uint Id => 988;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1189;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-37.2224f, 1.64051f);
        public override int Radius => 154;

        public override HashSet<uint> NodeIds => new()
        {
            34833,
            34834,
            34835,
            34836,
            34837,
            34838,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            43991,
            44016,
            44854,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34833,
            },
            new NodeInfo
            {
                NodeId = 34834,
            },
            new NodeInfo
            {
                NodeId = 34835,
            },
            new NodeInfo
            {
                NodeId = 34836,
            },
            new NodeInfo
            {
                NodeId = 34837,
            },
            new NodeInfo
            {
                NodeId = 34838,
            },
        };
    }
}
