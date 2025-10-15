using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_228 : RouteInfo
    {
        public override uint Id => 228;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(190.688f, 172.606f);
        public override int Radius => 61;

        public override HashSet<uint> NodeIds => new()
        {
            31016,
            31017,
            31018,
            31019,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31016,
            },
            new NodeInfo
            {
                NodeId = 31017,
            },
            new NodeInfo
            {
                NodeId = 31018,
            },
            new NodeInfo
            {
                NodeId = 31019,
            },
        };
    }
}
