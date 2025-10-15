using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARealmReborn.LowerLaNoscea_135
{
    public class MIN_203 : RouteInfo
    {
        public override uint Id => 203;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 135;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(246.386f, -293.376f);
        public override int Radius => 50;

        public override HashSet<uint> NodeIds => new()
        {
            30516,
            30517,
            30518,
            30519,
        };

        public override HashSet<uint> ItemIds => new()
        {
            5,
            11,
            5816,
            5818,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30516,
            },
            new NodeInfo
            {
                NodeId = 30517,
            },
            new NodeInfo
            {
                NodeId = 30518,
            },
            new NodeInfo
            {
                NodeId = 30519,
            },
        };
    }
}
