using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
    public class BTN_642 : RouteInfo
    {
        public override uint Id => 642;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 816;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(405.04f, 841.91f);
        public override int Radius => 107;

        public override HashSet<uint> NodeIds => new()
        {
            32795,
            32796,
            32797,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32795,
            },
            new NodeInfo
            {
                NodeId = 32796,
            },
            new NodeInfo
            {
                NodeId = 32797,
            },
        };
    }
}
