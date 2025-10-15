using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_908 : RouteInfo
    {
        public override uint Id => 908;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-413.35f, -364.556f);
        public override int Radius => 8;

        public override HashSet<uint> NodeIds => new()
        {
            34373,
            34374,
            34375,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            38301,
            38306,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34373,
            },
            new NodeInfo
            {
                NodeId = 34374,
            },
            new NodeInfo
            {
                NodeId = 34375,
            },
        };
    }
}
