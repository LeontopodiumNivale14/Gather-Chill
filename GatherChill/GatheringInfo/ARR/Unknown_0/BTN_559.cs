using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_559 : RouteInfo
    {
        public override uint Id => 559;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-262.543f, 536.96f);
        public override int Radius => 62;

        public override HashSet<uint> NodeIds => new()
        {
            32372,
            32373,
            32374,
            32375,
            32376,
            32377,
            32378,
            32379,
            32380,
            32381,
            32382,
            32383,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002759,
            2002760,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32372,
            },
            new NodeInfo
            {
                NodeId = 32373,
            },
            new NodeInfo
            {
                NodeId = 32374,
            },
            new NodeInfo
            {
                NodeId = 32375,
            },
            new NodeInfo
            {
                NodeId = 32376,
            },
            new NodeInfo
            {
                NodeId = 32377,
            },
            new NodeInfo
            {
                NodeId = 32378,
            },
            new NodeInfo
            {
                NodeId = 32379,
            },
            new NodeInfo
            {
                NodeId = 32380,
            },
            new NodeInfo
            {
                NodeId = 32381,
            },
            new NodeInfo
            {
                NodeId = 32382,
            },
            new NodeInfo
            {
                NodeId = 32383,
            },
        };
    }
}
