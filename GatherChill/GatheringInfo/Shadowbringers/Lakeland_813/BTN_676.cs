using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
    public class BTN_676 : RouteInfo
    {
        public override uint Id => 676;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 813;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(30.7812f, 124.371f);
        public override int Radius => 95;

        public override HashSet<uint> NodeIds => new()
        {
            32959,
            32960,
            32961,
            32962,
            32963,
            32964,
        };

        public override HashSet<uint> ItemIds => new()
        {
            28793,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32959,
            },
            new NodeInfo
            {
                NodeId = 32960,
            },
            new NodeInfo
            {
                NodeId = 32961,
            },
            new NodeInfo
            {
                NodeId = 32962,
            },
            new NodeInfo
            {
                NodeId = 32963,
            },
            new NodeInfo
            {
                NodeId = 32964,
            },
        };
    }
}
