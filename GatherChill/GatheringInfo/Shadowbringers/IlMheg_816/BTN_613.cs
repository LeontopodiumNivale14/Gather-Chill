using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
    public class BTN_613 : RouteInfo
    {
        public override uint Id => 613;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 816;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(523.386f, -816.254f);
        public override int Radius => 120;

        public override HashSet<uint> NodeIds => new()
        {
            32697,
            32698,
            32699,
            32700,
            32701,
            32702,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            27684,
            27831,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32697,
            },
            new NodeInfo
            {
                NodeId = 32698,
            },
            new NodeInfo
            {
                NodeId = 32699,
            },
            new NodeInfo
            {
                NodeId = 32700,
            },
            new NodeInfo
            {
                NodeId = 32701,
            },
            new NodeInfo
            {
                NodeId = 32702,
            },
        };
    }
}
