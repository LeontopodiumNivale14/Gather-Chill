using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianForelands_398
{
    public class BTN_719 : RouteInfo
    {
        public override uint Id => 719;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 398;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(361.864f, -249.192f);
        public override int Radius => 100;

        public override HashSet<uint> NodeIds => new()
        {
            33259,
            33260,
            33261,
            33262,
            33263,
            33264,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29669,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33259,
            },
            new NodeInfo
            {
                NodeId = 33260,
            },
            new NodeInfo
            {
                NodeId = 33261,
            },
            new NodeInfo
            {
                NodeId = 33262,
            },
            new NodeInfo
            {
                NodeId = 33263,
            },
            new NodeInfo
            {
                NodeId = 33264,
            },
        };
    }
}
