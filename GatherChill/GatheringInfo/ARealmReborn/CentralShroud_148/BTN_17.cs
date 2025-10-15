using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.CentralShroud_148
{
    public class BTN_17 : RouteInfo
    {
        public override uint Id => 17;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 148;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-56.2858f, -63.2038f);
        public override int Radius => 70;

        public override HashSet<uint> NodeIds => new()
        {
            30031,
            30032,
            30033,
            30310,
        };

        public override HashSet<uint> ItemIds => new()
        {
            4,
            4805,
            5385,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30031,
            },
            new NodeInfo
            {
                NodeId = 30032,
            },
            new NodeInfo
            {
                NodeId = 30033,
            },
            new NodeInfo
            {
                NodeId = 30310,
            },
        };
    }
}
