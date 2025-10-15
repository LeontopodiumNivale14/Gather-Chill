using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_436 : RouteInfo
    {
        public override uint Id => 436;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-556.721f, -699.65f);
        public override int Radius => 46;

        public override HashSet<uint> NodeIds => new()
        {
            31845,
            31846,
            31847,
            31848,
            31849,
            31850,
            31851,
            31852,
            31853,
            31854,
            31855,
            31856,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002125,
            2002126,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31845,
            },
            new NodeInfo
            {
                NodeId = 31846,
            },
            new NodeInfo
            {
                NodeId = 31847,
            },
            new NodeInfo
            {
                NodeId = 31848,
            },
            new NodeInfo
            {
                NodeId = 31849,
            },
            new NodeInfo
            {
                NodeId = 31850,
            },
            new NodeInfo
            {
                NodeId = 31851,
            },
            new NodeInfo
            {
                NodeId = 31852,
            },
            new NodeInfo
            {
                NodeId = 31853,
            },
            new NodeInfo
            {
                NodeId = 31854,
            },
            new NodeInfo
            {
                NodeId = 31855,
            },
            new NodeInfo
            {
                NodeId = 31856,
            },
        };
    }
}
