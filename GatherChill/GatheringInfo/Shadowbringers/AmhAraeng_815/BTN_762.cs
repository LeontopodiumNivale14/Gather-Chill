using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.AmhAraeng_815
{
    public class BTN_762 : RouteInfo
    {
        public override uint Id => 762;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 815;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-268.341f, -524.631f);
        public override int Radius => 1;

        public override HashSet<uint> NodeIds => new()
        {
            33592,
        };

        public override HashSet<uint> ItemIds => new()
        {
            32950,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33592,
            },
        };
    }
}
