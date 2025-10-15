using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.SouthernThanalan_146
{
    public class MIN_202 : RouteInfo
    {
        public override uint Id => 202;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 146;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-134.6f, -508.239f);
        public override int Radius => 61;

        public override HashSet<uint> NodeIds => new()
        {
            30483,
            30484,
            30485,
            30486,
        };

        public override HashSet<uint> ItemIds => new()
        {
            6,
            12,
            5820,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30483,
            },
            new NodeInfo
            {
                NodeId = 30484,
            },
            new NodeInfo
            {
                NodeId = 30485,
            },
            new NodeInfo
            {
                NodeId = 30486,
            },
        };
    }
}
