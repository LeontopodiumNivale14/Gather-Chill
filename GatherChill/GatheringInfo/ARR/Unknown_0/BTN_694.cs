using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_694 : RouteInfo
    {
        public override uint Id => 694;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(228.052f, 667.622f);
        public override int Radius => 39;

        public override HashSet<uint> NodeIds => new()
        {
            33018,
            33019,
            33020,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            29539,
            29546,
            29548,
            29553,
            29558,
            29563,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33018,
            },
            new NodeInfo
            {
                NodeId = 33019,
            },
            new NodeInfo
            {
                NodeId = 33020,
            },
        };
    }
}
