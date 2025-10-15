using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_865 : RouteInfo
    {
        public override uint Id => 865;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-11.0997f, 28.5258f);
        public override int Radius => 71;

        public override HashSet<uint> NodeIds => new()
        {
            34104,
            34105,
            34106,
            34107,
            34108,
            34109,
            34110,
            34111,
            34112,
            34113,
            34114,
            34115,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003140,
            2003141,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34104,
            },
            new NodeInfo
            {
                NodeId = 34105,
            },
            new NodeInfo
            {
                NodeId = 34106,
            },
            new NodeInfo
            {
                NodeId = 34107,
            },
            new NodeInfo
            {
                NodeId = 34108,
            },
            new NodeInfo
            {
                NodeId = 34109,
            },
            new NodeInfo
            {
                NodeId = 34110,
            },
            new NodeInfo
            {
                NodeId = 34111,
            },
            new NodeInfo
            {
                NodeId = 34112,
            },
            new NodeInfo
            {
                NodeId = 34113,
            },
            new NodeInfo
            {
                NodeId = 34114,
            },
            new NodeInfo
            {
                NodeId = 34115,
            },
        };
    }
}
