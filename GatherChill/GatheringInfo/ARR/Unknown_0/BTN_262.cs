using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_262 : RouteInfo
    {
        public override uint Id => 262;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-367.526f, 524.103f);
        public override int Radius => 210;

        public override HashSet<uint> NodeIds => new()
        {
            31133,
            31134,
            31135,
            31136,
            31137,
            31138,
            31139,
            31140,
            31141,
            31142,
            31143,
            31144,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001859,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31133,
            },
            new NodeInfo
            {
                NodeId = 31134,
            },
            new NodeInfo
            {
                NodeId = 31135,
            },
            new NodeInfo
            {
                NodeId = 31136,
            },
            new NodeInfo
            {
                NodeId = 31137,
            },
            new NodeInfo
            {
                NodeId = 31138,
            },
            new NodeInfo
            {
                NodeId = 31139,
            },
            new NodeInfo
            {
                NodeId = 31140,
            },
            new NodeInfo
            {
                NodeId = 31141,
            },
            new NodeInfo
            {
                NodeId = 31142,
            },
            new NodeInfo
            {
                NodeId = 31143,
            },
            new NodeInfo
            {
                NodeId = 31144,
            },
        };
    }
}
