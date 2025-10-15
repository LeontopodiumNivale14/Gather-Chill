using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Kholusia_814
{
    public class BTN_926 : RouteInfo
    {
        public override uint Id => 926;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 814;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(207.844f, -429.724f);
        public override int Radius => 125;

        public override HashSet<uint> NodeIds => new()
        {
            34452,
            34454,
            34456,
        };

        public override HashSet<uint> ItemIds => new()
        {
            38788,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34452,
            },
            new NodeInfo
            {
                NodeId = 34454,
            },
            new NodeInfo
            {
                NodeId = 34456,
            },
        };
    }
}
