using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1069 : RouteInfo
    {
        public override uint Id => 1069;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-704.974f, 573.797f);
        public override int Radius => 73;

        public override HashSet<uint> NodeIds => new()
        {
            35195,
            35196,
            35197,
            35198,
            35199,
            35200,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35195,
            },
            new NodeInfo
            {
                NodeId = 35196,
            },
            new NodeInfo
            {
                NodeId = 35197,
            },
            new NodeInfo
            {
                NodeId = 35198,
            },
            new NodeInfo
            {
                NodeId = 35199,
            },
            new NodeInfo
            {
                NodeId = 35200,
            },
        };
    }
}
