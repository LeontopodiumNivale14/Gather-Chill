using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_907 : RouteInfo
    {
        public override uint Id => 907;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-626.121f, 271.649f);
        public override int Radius => 14;

        public override HashSet<uint> NodeIds => new()
        {
            34370,
            34371,
            34372,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            38300,
            38310,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34370,
            },
            new NodeInfo
            {
                NodeId = 34371,
            },
            new NodeInfo
            {
                NodeId = 34372,
            },
        };
    }
}
