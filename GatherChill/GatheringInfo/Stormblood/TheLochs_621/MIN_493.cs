using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheLochs_621
{
    public class MIN_493 : RouteInfo
    {
        public override uint Id => 493;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 621;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-499.788f, -244.482f);
        public override int Radius => 120;

        public override HashSet<uint> NodeIds => new()
        {
            32166,
            32167,
            32168,
            32169,
            32170,
            32171,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            19957,
            24570,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32166,
            },
            new NodeInfo
            {
                NodeId = 32167,
            },
            new NodeInfo
            {
                NodeId = 32168,
            },
            new NodeInfo
            {
                NodeId = 32169,
            },
            new NodeInfo
            {
                NodeId = 32170,
            },
            new NodeInfo
            {
                NodeId = 32171,
            },
        };
    }
}
