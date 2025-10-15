using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.YakTel_1189
{
    public class BTN_998 : RouteInfo
    {
        public override uint Id => 998;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1189;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(645.919f, 61.0355f);
        public override int Radius => 153;

        public override HashSet<uint> NodeIds => new()
        {
            34893,
            34894,
            34895,
            34896,
            34897,
            34898,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            43981,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34893,
            },
            new NodeInfo
            {
                NodeId = 34894,
            },
            new NodeInfo
            {
                NodeId = 34895,
            },
            new NodeInfo
            {
                NodeId = 34896,
            },
            new NodeInfo
            {
                NodeId = 34897,
            },
            new NodeInfo
            {
                NodeId = 34898,
            },
        };
    }
}
