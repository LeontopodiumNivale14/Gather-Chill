using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Kholusia_814
{
    public class BTN_684 : RouteInfo
    {
        public override uint Id => 684;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 814;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(346.78f, -15.9792f);
        public override int Radius => 1;

        public override HashSet<uint> NodeIds => new()
        {
            32990,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29978,
            30485,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32990,
            },
        };
    }
}
