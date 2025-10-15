using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_258 : RouteInfo
    {
        public override uint Id => 258;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(616.299f, 146.772f);
        public override int Radius => 26;

        public override HashSet<uint> NodeIds => new()
        {
            31107,
            31108,
            31109,
            31110,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001853,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31107,
            },
            new NodeInfo
            {
                NodeId = 31108,
            },
            new NodeInfo
            {
                NodeId = 31109,
            },
            new NodeInfo
            {
                NodeId = 31110,
            },
        };
    }
}
