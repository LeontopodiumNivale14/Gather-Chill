using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_952 : RouteInfo
    {
        public override uint Id => 952;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-16.5169f, 440.875f);
        public override int Radius => 39;

        public override HashSet<uint> NodeIds => new()
        {
            34575,
            34576,
            34577,
            34578,
            34579,
            34580,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003525,
            2003526,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34575,
            },
            new NodeInfo
            {
                NodeId = 34576,
            },
            new NodeInfo
            {
                NodeId = 34577,
            },
            new NodeInfo
            {
                NodeId = 34578,
            },
            new NodeInfo
            {
                NodeId = 34579,
            },
            new NodeInfo
            {
                NodeId = 34580,
            },
        };
    }
}
