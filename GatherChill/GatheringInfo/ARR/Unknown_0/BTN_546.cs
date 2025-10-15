using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_546 : RouteInfo
    {
        public override uint Id => 546;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(158.789f, -54.9516f);
        public override int Radius => 11;

        public override HashSet<uint> NodeIds => new()
        {
            32329,
            32330,
            32331,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            22644,
            22652,
            22655,
            22664,
            22666,
            23170,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32329,
            },
            new NodeInfo
            {
                NodeId = 32330,
            },
            new NodeInfo
            {
                NodeId = 32331,
            },
        };
    }
}
