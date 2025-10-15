using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Kholusia_814
{
    public class BTN_632 : RouteInfo
    {
        public override uint Id => 632;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 814;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-68.796f, 291.724f);
        public override int Radius => 28;

        public override HashSet<uint> NodeIds => new()
        {
            32771,
        };

        public override HashSet<uint> ItemIds => new()
        {
            27835,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32771,
            },
        };
    }
}
