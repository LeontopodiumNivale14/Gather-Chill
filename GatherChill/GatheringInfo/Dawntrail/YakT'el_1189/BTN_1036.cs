using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.YakTel_1189
{
    public class BTN_1036 : RouteInfo
    {
        public override uint Id => 1036;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1189;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-81.5872f, 663.24f);
        public override int Radius => 118;

        public override HashSet<uint> NodeIds => new()
        {
            34996,
            34997,
            34998,
            34999,
            35000,
            35001,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            43990,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34996,
            },
            new NodeInfo
            {
                NodeId = 34997,
            },
            new NodeInfo
            {
                NodeId = 34998,
            },
            new NodeInfo
            {
                NodeId = 34999,
            },
            new NodeInfo
            {
                NodeId = 35000,
            },
            new NodeInfo
            {
                NodeId = 35001,
            },
        };
    }
}
