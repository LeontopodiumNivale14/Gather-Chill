using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.EasternThanalan_145
{
    public class BTN_38 : RouteInfo
    {
        public override uint Id => 38;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 145;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-262.291f, 281.397f);
        public override int Radius => 52;

        public override HashSet<uint> NodeIds => new()
        {
            30102,
            30103,
            30104,
            30114,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2,
            4787,
            5563,
            7031,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30102,
            },
            new NodeInfo
            {
                NodeId = 30103,
            },
            new NodeInfo
            {
                NodeId = 30104,
            },
            new NodeInfo
            {
                NodeId = 30114,
            },
        };
    }
}
