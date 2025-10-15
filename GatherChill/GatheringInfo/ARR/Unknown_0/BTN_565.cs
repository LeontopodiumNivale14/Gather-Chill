using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_565 : RouteInfo
    {
        public override uint Id => 565;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-406.361f, 743.672f);
        public override int Radius => 60;

        public override HashSet<uint> NodeIds => new()
        {
            32422,
            32423,
            32424,
            32425,
            32426,
            32427,
            32428,
            32429,
            32430,
            32431,
            32432,
            32433,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002768,
            2002769,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32422,
            },
            new NodeInfo
            {
                NodeId = 32423,
            },
            new NodeInfo
            {
                NodeId = 32424,
            },
            new NodeInfo
            {
                NodeId = 32425,
            },
            new NodeInfo
            {
                NodeId = 32426,
            },
            new NodeInfo
            {
                NodeId = 32427,
            },
            new NodeInfo
            {
                NodeId = 32428,
            },
            new NodeInfo
            {
                NodeId = 32429,
            },
            new NodeInfo
            {
                NodeId = 32430,
            },
            new NodeInfo
            {
                NodeId = 32431,
            },
            new NodeInfo
            {
                NodeId = 32432,
            },
            new NodeInfo
            {
                NodeId = 32433,
            },
        };
    }
}
