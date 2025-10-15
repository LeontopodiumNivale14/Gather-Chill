using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_398 : RouteInfo
    {
        public override uint Id => 398;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-70.3965f, -14.9252f);
        public override int Radius => 21;

        public override HashSet<uint> NodeIds => new()
        {
            34352,
            34353,
            34354,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            38315,
            38318,
            38321,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34352,
            },
            new NodeInfo
            {
                NodeId = 34353,
            },
            new NodeInfo
            {
                NodeId = 34354,
            },
        };
    }
}
