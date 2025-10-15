using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.AmhAraeng_815
{
    public class BTN_785 : RouteInfo
    {
        public override uint Id => 785;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 815;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(350.377f, 373.984f);
        public override int Radius => 112;

        public override HashSet<uint> NodeIds => new()
        {
            33640,
            33641,
            33642,
        };

        public override HashSet<uint> ItemIds => new()
        {
            33013,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33640,
            },
            new NodeInfo
            {
                NodeId = 33641,
            },
            new NodeInfo
            {
                NodeId = 33642,
            },
        };
    }
}
