using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.WesternThanalan_140
{
    public class BTN_680 : RouteInfo
    {
        public override uint Id => 680;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 140;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-219.542f, -321.619f);
        public override int Radius => 31;

        public override HashSet<uint> NodeIds => new()
        {
            32983,
            32984,
            32985,
            32986,
        };

        public override HashSet<uint> ItemIds => new()
        {
            28916,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32983,
            },
            new NodeInfo
            {
                NodeId = 32984,
            },
            new NodeInfo
            {
                NodeId = 32985,
            },
            new NodeInfo
            {
                NodeId = 32986,
            },
        };
    }
}
