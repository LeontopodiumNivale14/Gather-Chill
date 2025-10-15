using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_444 : RouteInfo
    {
        public override uint Id => 444;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-54.235f, 322.087f);
        public override int Radius => 46;

        public override HashSet<uint> NodeIds => new()
        {
            31911,
            31912,
            31913,
            31914,
            31915,
            31916,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002136,
            2002137,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31911,
            },
            new NodeInfo
            {
                NodeId = 31912,
            },
            new NodeInfo
            {
                NodeId = 31913,
            },
            new NodeInfo
            {
                NodeId = 31914,
            },
            new NodeInfo
            {
                NodeId = 31915,
            },
            new NodeInfo
            {
                NodeId = 31916,
            },
        };
    }
}
