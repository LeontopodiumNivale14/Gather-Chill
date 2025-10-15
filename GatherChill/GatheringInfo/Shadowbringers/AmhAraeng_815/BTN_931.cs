using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.AmhAraeng_815
{
    public class BTN_931 : RouteInfo
    {
        public override uint Id => 931;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 815;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-178.521f, -137.961f);
        public override int Radius => 120;

        public override HashSet<uint> NodeIds => new()
        {
            34475,
            34476,
            34477,
        };

        public override HashSet<uint> ItemIds => new()
        {
            39813,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34475,
            },
            new NodeInfo
            {
                NodeId = 34476,
            },
            new NodeInfo
            {
                NodeId = 34477,
            },
        };
    }
}
