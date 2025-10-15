using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Kholusia_814
{
    public class MIN_594 : RouteInfo
    {
        public override uint Id => 594;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 814;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(70.0658f, 512.043f);
        public override int Radius => 122;

        public override HashSet<uint> NodeIds => new()
        {
            32638,
            32639,
            32640,
            32641,
            32642,
            32643,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8,
            27696,
            27801,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32638,
            },
            new NodeInfo
            {
                NodeId = 32639,
            },
            new NodeInfo
            {
                NodeId = 32640,
            },
            new NodeInfo
            {
                NodeId = 32641,
            },
            new NodeInfo
            {
                NodeId = 32642,
            },
            new NodeInfo
            {
                NodeId = 32643,
            },
        };
    }
}
