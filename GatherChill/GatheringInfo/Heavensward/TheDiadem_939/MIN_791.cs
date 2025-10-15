using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_939
{
    public class MIN_791 : RouteInfo
    {
        public override uint Id => 791;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 939;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-346.586f, 566.286f);
        public override int Radius => 2;

        public override HashSet<uint> NodeIds => new()
        {
            33686,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29943,
            31315,
            32014,
            32023,
            32034,
            32043,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33686,
            },
        };
    }
}
