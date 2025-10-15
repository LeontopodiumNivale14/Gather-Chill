using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_954 : RouteInfo
    {
        public override uint Id => 954;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-385.267f, 838.024f);
        public override int Radius => 53;

        public override HashSet<uint> NodeIds => new()
        {
            34593,
            34594,
            34595,
            34596,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003528,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34593,
            },
            new NodeInfo
            {
                NodeId = 34594,
            },
            new NodeInfo
            {
                NodeId = 34595,
            },
            new NodeInfo
            {
                NodeId = 34596,
            },
        };
    }
}
