using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Kholusia_814
{
    public class BTN_614 : RouteInfo
    {
        public override uint Id => 614;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 814;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(305.534f, 154.945f);
        public override int Radius => 131;

        public override HashSet<uint> NodeIds => new()
        {
            32703,
            32704,
            32705,
            32706,
            32707,
            32708,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            27683,
            27816,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32703,
            },
            new NodeInfo
            {
                NodeId = 32704,
            },
            new NodeInfo
            {
                NodeId = 32705,
            },
            new NodeInfo
            {
                NodeId = 32706,
            },
            new NodeInfo
            {
                NodeId = 32707,
            },
            new NodeInfo
            {
                NodeId = 32708,
            },
        };
    }
}
