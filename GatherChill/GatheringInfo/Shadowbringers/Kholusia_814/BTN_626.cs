using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Kholusia_814
{
    public class BTN_626 : RouteInfo
    {
        public override uint Id => 626;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 814;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-141.599f, -685.95f);
        public override int Radius => 127;

        public override HashSet<uint> NodeIds => new()
        {
            32765,
            33619,
            33620,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            27808,
            30592,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32765,
            },
            new NodeInfo
            {
                NodeId = 33619,
            },
            new NodeInfo
            {
                NodeId = 33620,
            },
        };
    }
}
