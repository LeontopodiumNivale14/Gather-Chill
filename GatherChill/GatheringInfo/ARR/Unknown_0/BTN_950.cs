using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_950 : RouteInfo
    {
        public override uint Id => 950;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(554.67f, -101.632f);
        public override int Radius => 84;

        public override HashSet<uint> NodeIds => new()
        {
            34551,
            34552,
            34553,
            34554,
            34555,
            34556,
            34557,
            34558,
            34559,
            34560,
            34561,
            34562,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003522,
            2003523,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34551,
            },
            new NodeInfo
            {
                NodeId = 34552,
            },
            new NodeInfo
            {
                NodeId = 34553,
            },
            new NodeInfo
            {
                NodeId = 34554,
            },
            new NodeInfo
            {
                NodeId = 34555,
            },
            new NodeInfo
            {
                NodeId = 34556,
            },
            new NodeInfo
            {
                NodeId = 34557,
            },
            new NodeInfo
            {
                NodeId = 34558,
            },
            new NodeInfo
            {
                NodeId = 34559,
            },
            new NodeInfo
            {
                NodeId = 34560,
            },
            new NodeInfo
            {
                NodeId = 34561,
            },
            new NodeInfo
            {
                NodeId = 34562,
            },
        };
    }
}
