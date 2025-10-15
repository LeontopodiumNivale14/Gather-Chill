using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_566 : RouteInfo
    {
        public override uint Id => 566;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-160.596f, 40.3687f);
        public override int Radius => 63;

        public override HashSet<uint> NodeIds => new()
        {
            32434,
            32435,
            32436,
            32437,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002770,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32434,
            },
            new NodeInfo
            {
                NodeId = 32435,
            },
            new NodeInfo
            {
                NodeId = 32436,
            },
            new NodeInfo
            {
                NodeId = 32437,
            },
        };
    }
}
