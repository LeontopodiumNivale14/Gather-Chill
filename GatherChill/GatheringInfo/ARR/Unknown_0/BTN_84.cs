using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_84 : RouteInfo
    {
        public override uint Id => 84;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-38.6864f, 395.763f);
        public override int Radius => 56;

        public override HashSet<uint> NodeIds => new()
        {
            30618,
            30619,
            30620,
            30621,
            30622,
            30623,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000915,
            2000916,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30618,
            },
            new NodeInfo
            {
                NodeId = 30619,
            },
            new NodeInfo
            {
                NodeId = 30620,
            },
            new NodeInfo
            {
                NodeId = 30621,
            },
            new NodeInfo
            {
                NodeId = 30622,
            },
            new NodeInfo
            {
                NodeId = 30623,
            },
        };
    }
}
