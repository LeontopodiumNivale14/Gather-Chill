using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_259 : RouteInfo
    {
        public override uint Id => 259;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(371.649f, 761.495f);
        public override int Radius => 18;

        public override HashSet<uint> NodeIds => new()
        {
            31111,
            31112,
            31113,
            31114,
            31115,
            31116,
            31117,
            31118,
            31119,
            31120,
            31121,
            31122,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001854,
            2001855,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31111,
            },
            new NodeInfo
            {
                NodeId = 31112,
            },
            new NodeInfo
            {
                NodeId = 31113,
            },
            new NodeInfo
            {
                NodeId = 31114,
            },
            new NodeInfo
            {
                NodeId = 31115,
            },
            new NodeInfo
            {
                NodeId = 31116,
            },
            new NodeInfo
            {
                NodeId = 31117,
            },
            new NodeInfo
            {
                NodeId = 31118,
            },
            new NodeInfo
            {
                NodeId = 31119,
            },
            new NodeInfo
            {
                NodeId = 31120,
            },
            new NodeInfo
            {
                NodeId = 31121,
            },
            new NodeInfo
            {
                NodeId = 31122,
            },
        };
    }
}
