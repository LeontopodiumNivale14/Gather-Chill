using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EasternThanalan_145
{
    public class MIN_168 : RouteInfo
    {
        public override uint Id => 168;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 145;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(244.573f, -242.136f);
        public override int Radius => 65;

        public override HashSet<uint> NodeIds => new()
        {
            30467,
            30468,
            30469,
            30470,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2,
            5125,
            5521,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30467,
            },
            new NodeInfo
            {
                NodeId = 30468,
            },
            new NodeInfo
            {
                NodeId = 30469,
            },
            new NodeInfo
            {
                NodeId = 30470,
            },
        };
    }
}
