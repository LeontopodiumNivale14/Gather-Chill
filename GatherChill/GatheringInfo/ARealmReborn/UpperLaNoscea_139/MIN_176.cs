using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.UpperLaNoscea_139
{
    public class MIN_176 : RouteInfo
    {
        public override uint Id => 176;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 139;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-430.55f, 223.36f);
        public override int Radius => 58;

        public override HashSet<uint> NodeIds => new()
        {
            30487,
            30488,
            30489,
            30490,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5,
            5231,
            5527,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30487,
            },
            new NodeInfo
            {
                NodeId = 30488,
            },
            new NodeInfo
            {
                NodeId = 30489,
            },
            new NodeInfo
            {
                NodeId = 30490,
            },
        };
    }
}
