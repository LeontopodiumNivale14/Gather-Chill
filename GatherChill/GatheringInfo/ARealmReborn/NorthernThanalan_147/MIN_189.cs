using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.NorthernThanalan_147
{
    public class MIN_189 : RouteInfo
    {
        public override uint Id => 189;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 147;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(92.6268f, 142.529f);
        public override int Radius => 65;

        public override HashSet<uint> NodeIds => new()
        {
            30536,
            30537,
            30538,
            30539,
        };

        public override HashSet<uint> ItemIds => new()
        {
            4,
            5116,
            5263,
            5439,
            5458,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30536,
            },
            new NodeInfo
            {
                NodeId = 30537,
            },
            new NodeInfo
            {
                NodeId = 30538,
            },
            new NodeInfo
            {
                NodeId = 30539,
            },
        };
    }
}
