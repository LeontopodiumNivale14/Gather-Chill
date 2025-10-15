using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_65 : RouteInfo
    {
        public override uint Id => 65;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(13.5143f, 215.177f);
        public override int Radius => 50;

        public override HashSet<uint> NodeIds => new()
        {
            30134,
            30135,
            30136,
            30137,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000298,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30134,
            },
            new NodeInfo
            {
                NodeId = 30135,
            },
            new NodeInfo
            {
                NodeId = 30136,
            },
            new NodeInfo
            {
                NodeId = 30137,
            },
        };
    }
}
