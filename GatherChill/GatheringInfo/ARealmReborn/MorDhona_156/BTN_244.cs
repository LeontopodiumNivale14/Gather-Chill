using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.MorDhona_156
{
    public class BTN_244 : RouteInfo
    {
        public override uint Id => 244;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 156;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-347.805f, -348.68f);
        public override int Radius => 31;

        public override HashSet<uint> NodeIds => new()
        {
            31041,
            31042,
            31043,
            31044,
        };

        public override HashSet<uint> ItemIds => new()
        {
            6,
            2001412,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31041,
            },
            new NodeInfo
            {
                NodeId = 31042,
            },
            new NodeInfo
            {
                NodeId = 31043,
            },
            new NodeInfo
            {
                NodeId = 31044,
            },
        };
    }
}
