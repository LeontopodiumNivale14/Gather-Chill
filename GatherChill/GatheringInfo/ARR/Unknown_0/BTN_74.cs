using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_74 : RouteInfo
    {
        public override uint Id => 74;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(174.786f, -129.342f);
        public override int Radius => 22;

        public override HashSet<uint> NodeIds => new()
        {
            30284,
            30285,
            30286,
            30287,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000313,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30284,
            },
            new NodeInfo
            {
                NodeId = 30285,
            },
            new NodeInfo
            {
                NodeId = 30286,
            },
            new NodeInfo
            {
                NodeId = 30287,
            },
        };
    }
}
