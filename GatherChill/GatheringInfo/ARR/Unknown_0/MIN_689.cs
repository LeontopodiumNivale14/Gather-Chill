using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_689 : RouteInfo
    {
        public override uint Id => 689;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(237.956f, 642.877f);
        public override int Radius => 30;

        public override HashSet<uint> NodeIds => new()
        {
            33003,
            33004,
            33005,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            29513,
            29520,
            29522,
            29527,
            29532,
            29537,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33003,
            },
            new NodeInfo
            {
                NodeId = 33004,
            },
            new NodeInfo
            {
                NodeId = 33005,
            },
        };
    }
}
