using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_62 : RouteInfo
    {
        public override uint Id => 62;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-48.4727f, 466.067f);
        public override int Radius => 42;

        public override HashSet<uint> NodeIds => new()
        {
            30238,
            30239,
            30240,
            30241,
            30242,
            30243,
            30244,
            30245,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000291,
            2000293,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30238,
            },
            new NodeInfo
            {
                NodeId = 30239,
            },
            new NodeInfo
            {
                NodeId = 30240,
            },
            new NodeInfo
            {
                NodeId = 30241,
            },
            new NodeInfo
            {
                NodeId = 30242,
            },
            new NodeInfo
            {
                NodeId = 30243,
            },
            new NodeInfo
            {
                NodeId = 30244,
            },
            new NodeInfo
            {
                NodeId = 30245,
            },
        };
    }
}
