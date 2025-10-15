using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.Yanxia_614
{
    public class MIN_491 : RouteInfo
    {
        public override uint Id => 491;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 614;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(809.157f, -80.6078f);
        public override int Radius => 111;

        public override HashSet<uint> NodeIds => new()
        {
            32159,
            32160,
            32161,
            32162,
            32163,
            32164,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8,
            17943,
            19954,
            24571,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32159,
            },
            new NodeInfo
            {
                NodeId = 32160,
            },
            new NodeInfo
            {
                NodeId = 32161,
            },
            new NodeInfo
            {
                NodeId = 32162,
            },
            new NodeInfo
            {
                NodeId = 32163,
            },
            new NodeInfo
            {
                NodeId = 32164,
            },
        };
    }
}
