using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.HeritageFound_1191
{
    public class BTN_1021 : RouteInfo
    {
        public override uint Id => 1021;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1191;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-243.864f, -736.663f);
        public override int Radius => 142;

        public override HashSet<uint> NodeIds => new()
        {
            34979,
            34980,
            34981,
        };

        public override HashSet<uint> ItemIds => new()
        {
            43925,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34979,
            },
            new NodeInfo
            {
                NodeId = 34980,
            },
            new NodeInfo
            {
                NodeId = 34981,
            },
        };
    }
}
