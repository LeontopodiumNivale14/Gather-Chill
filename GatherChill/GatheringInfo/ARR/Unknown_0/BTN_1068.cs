using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1068 : RouteInfo
    {
        public override uint Id => 1068;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(517.836f, 637.01f);
        public override int Radius => 87;

        public override HashSet<uint> NodeIds => new()
        {
            35189,
            35190,
            35191,
            35192,
            35193,
            35194,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35189,
            },
            new NodeInfo
            {
                NodeId = 35190,
            },
            new NodeInfo
            {
                NodeId = 35191,
            },
            new NodeInfo
            {
                NodeId = 35192,
            },
            new NodeInfo
            {
                NodeId = 35193,
            },
            new NodeInfo
            {
                NodeId = 35194,
            },
        };
    }
}
