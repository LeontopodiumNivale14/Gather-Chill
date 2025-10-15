using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_1269 : RouteInfo
    {
        public override uint Id => 1269;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-232.691f, 189.003f);
        public override int Radius => 36;

        public override HashSet<uint> NodeIds => new()
        {
            35415,
            35416,
            35417,
            35418,
            35419,
            35420,
            35421,
            35422,
        };

        public override HashSet<uint> ItemIds => new()
        {
            47396,
            47397,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35415,
            },
            new NodeInfo
            {
                NodeId = 35416,
            },
            new NodeInfo
            {
                NodeId = 35417,
            },
            new NodeInfo
            {
                NodeId = 35418,
            },
            new NodeInfo
            {
                NodeId = 35419,
            },
            new NodeInfo
            {
                NodeId = 35420,
            },
            new NodeInfo
            {
                NodeId = 35421,
            },
            new NodeInfo
            {
                NodeId = 35422,
            },
        };
    }
}
