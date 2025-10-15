using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_867 : RouteInfo
    {
        public override uint Id => 867;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(361.204f, -116.734f);
        public override int Radius => 60;

        public override HashSet<uint> NodeIds => new()
        {
            34128,
            34129,
            34130,
            34131,
            34132,
            34133,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003143,
            2003144,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34128,
            },
            new NodeInfo
            {
                NodeId = 34129,
            },
            new NodeInfo
            {
                NodeId = 34130,
            },
            new NodeInfo
            {
                NodeId = 34131,
            },
            new NodeInfo
            {
                NodeId = 34132,
            },
            new NodeInfo
            {
                NodeId = 34133,
            },
        };
    }
}
