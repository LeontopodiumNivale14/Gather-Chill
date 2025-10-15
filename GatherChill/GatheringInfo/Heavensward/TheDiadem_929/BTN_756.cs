using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_929
{
    public class BTN_756 : RouteInfo
    {
        public override uint Id => 756;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 929;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-594.202f, 443.605f);
        public override int Radius => 11;

        public override HashSet<uint> NodeIds => new()
        {
            33586,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29944,
            31316,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33586,
            },
        };
    }
}
