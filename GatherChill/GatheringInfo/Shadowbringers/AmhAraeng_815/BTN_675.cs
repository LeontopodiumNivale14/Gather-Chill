using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.AmhAraeng_815
{
    public class BTN_675 : RouteInfo
    {
        public override uint Id => 675;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 815;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-22.3265f, 192.013f);
        public override int Radius => 99;

        public override HashSet<uint> NodeIds => new()
        {
            32953,
            32954,
            32955,
            32956,
            32957,
            32958,
        };

        public override HashSet<uint> ItemIds => new()
        {
            28794,
            28795,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32953,
            },
            new NodeInfo
            {
                NodeId = 32954,
            },
            new NodeInfo
            {
                NodeId = 32955,
            },
            new NodeInfo
            {
                NodeId = 32956,
            },
            new NodeInfo
            {
                NodeId = 32957,
            },
            new NodeInfo
            {
                NodeId = 32958,
            },
        };
    }
}
