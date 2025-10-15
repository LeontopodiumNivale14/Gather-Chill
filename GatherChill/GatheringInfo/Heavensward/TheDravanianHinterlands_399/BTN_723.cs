using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianHinterlands_399
{
    public class BTN_723 : RouteInfo
    {
        public override uint Id => 723;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 399;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-361.895f, 560.103f);
        public override int Radius => 111;

        public override HashSet<uint> NodeIds => new()
        {
            33283,
            33284,
            33285,
            33286,
            33287,
            33288,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            30498,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33283,
            },
            new NodeInfo
            {
                NodeId = 33284,
            },
            new NodeInfo
            {
                NodeId = 33285,
            },
            new NodeInfo
            {
                NodeId = 33286,
            },
            new NodeInfo
            {
                NodeId = 33287,
            },
            new NodeInfo
            {
                NodeId = 33288,
            },
        };
    }
}
