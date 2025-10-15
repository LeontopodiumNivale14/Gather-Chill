using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.UpperLaNoscea_139
{
    public class BTN_943 : RouteInfo
    {
        public override uint Id => 943;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 139;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(-150.788f, -19.157f);
        public override int Radius => 89;

        public override HashSet<uint> NodeIds => new()
        {
            34493,
            34494,
            34495,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34493,
            },
            new NodeInfo
            {
                NodeId = 34494,
            },
            new NodeInfo
            {
                NodeId = 34495,
            },
        };
    }
}
