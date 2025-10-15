using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.AmhAraeng_815
{
    public class MIN_925 : RouteInfo
    {
        public override uint Id => 925;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 815;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(350.918f, -357.879f);
        public override int Radius => 131;

        public override HashSet<uint> NodeIds => new()
        {
            34446,
            34448,
            34450,
        };

        public override HashSet<uint> ItemIds => new()
        {
            38796,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34446,
            },
            new NodeInfo
            {
                NodeId = 34448,
            },
            new NodeInfo
            {
                NodeId = 34450,
            },
        };
    }
}
