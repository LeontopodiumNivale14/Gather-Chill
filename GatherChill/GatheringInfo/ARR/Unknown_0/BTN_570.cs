using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_570 : RouteInfo
    {
        public override uint Id => 570;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-85.722f, -462.933f);
        public override int Radius => 34;

        public override HashSet<uint> NodeIds => new()
        {
            32460,
            32461,
            32462,
            32463,
            32464,
            32465,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002775,
            2002776,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32460,
            },
            new NodeInfo
            {
                NodeId = 32461,
            },
            new NodeInfo
            {
                NodeId = 32462,
            },
            new NodeInfo
            {
                NodeId = 32463,
            },
            new NodeInfo
            {
                NodeId = 32464,
            },
            new NodeInfo
            {
                NodeId = 32465,
            },
        };
    }
}
