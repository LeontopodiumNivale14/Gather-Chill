using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Kholusia_814
{
    public class BTN_649 : RouteInfo
    {
        public override uint Id => 649;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 814;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-200.42f, 153.905f);
        public override int Radius => 90;

        public override HashSet<uint> NodeIds => new()
        {
            32815,
            32816,
            32817,
            32818,
            32819,
            32820,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            28199,
            28203,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32815,
            },
            new NodeInfo
            {
                NodeId = 32816,
            },
            new NodeInfo
            {
                NodeId = 32817,
            },
            new NodeInfo
            {
                NodeId = 32818,
            },
            new NodeInfo
            {
                NodeId = 32819,
            },
            new NodeInfo
            {
                NodeId = 32820,
            },
        };
    }
}
