using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Kholusia_814
{
    public class BTN_618 : RouteInfo
    {
        public override uint Id => 618;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 814;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-252.581f, 735.12f);
        public override int Radius => 110;

        public override HashSet<uint> NodeIds => new()
        {
            32727,
            32728,
            32729,
            32730,
            32731,
            32732,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            27832,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32727,
            },
            new NodeInfo
            {
                NodeId = 32728,
            },
            new NodeInfo
            {
                NodeId = 32729,
            },
            new NodeInfo
            {
                NodeId = 32730,
            },
            new NodeInfo
            {
                NodeId = 32731,
            },
            new NodeInfo
            {
                NodeId = 32732,
            },
        };
    }
}
