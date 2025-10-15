using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.UpperLaNoscea_139
{
    public class MIN_167 : RouteInfo
    {
        public override uint Id => 167;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 139;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-422.977f, 95.2932f);
        public override int Radius => 66;

        public override HashSet<uint> NodeIds => new()
        {
            30463,
            30464,
            30465,
            30466,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5,
            5152,
            5156,
            5157,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30463,
            },
            new NodeInfo
            {
                NodeId = 30464,
            },
            new NodeInfo
            {
                NodeId = 30465,
            },
            new NodeInfo
            {
                NodeId = 30466,
            },
        };
    }
}
