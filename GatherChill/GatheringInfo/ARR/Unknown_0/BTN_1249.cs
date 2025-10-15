using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1249 : RouteInfo
    {
        public override uint Id => 1249;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-86.0353f, -314.06f);
        public override int Radius => 60;

        public override HashSet<uint> NodeIds => new()
        {
            35369,
            35370,
            35371,
            35372,
            35373,
            35374,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47385,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35369,
            },
            new NodeInfo
            {
                NodeId = 35370,
            },
            new NodeInfo
            {
                NodeId = 35371,
            },
            new NodeInfo
            {
                NodeId = 35372,
            },
            new NodeInfo
            {
                NodeId = 35373,
            },
            new NodeInfo
            {
                NodeId = 35374,
            },
        };
    }
}
