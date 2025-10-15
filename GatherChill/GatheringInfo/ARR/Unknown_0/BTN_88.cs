using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_88 : RouteInfo
    {
        public override uint Id => 88;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-171.013f, -144.464f);
        public override int Radius => 92;

        public override HashSet<uint> NodeIds => new()
        {
            30648,
            30649,
            30650,
            30651,
            30652,
            30653,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000921,
            2000922,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30648,
            },
            new NodeInfo
            {
                NodeId = 30649,
            },
            new NodeInfo
            {
                NodeId = 30650,
            },
            new NodeInfo
            {
                NodeId = 30651,
            },
            new NodeInfo
            {
                NodeId = 30652,
            },
            new NodeInfo
            {
                NodeId = 30653,
            },
        };
    }
}
