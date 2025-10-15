using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
    public class BTN_645 : RouteInfo
    {
        public override uint Id => 645;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 813;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(-493.469f, 693.2f);
        public override int Radius => 19;

        public override HashSet<uint> NodeIds => new()
        {
            32804,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32804,
            },
        };
    }
}
