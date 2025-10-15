using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
    public class MIN_533 : RouteInfo
    {
        public override uint Id => 533;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 613;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-218.767f, 710.968f);
        public override int Radius => 99;

        public override HashSet<uint> NodeIds => new()
        {
            32296,
            32297,
            32298,
            32299,
            32300,
            32301,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            20781,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32296,
            },
            new NodeInfo
            {
                NodeId = 32297,
            },
            new NodeInfo
            {
                NodeId = 32298,
            },
            new NodeInfo
            {
                NodeId = 32299,
            },
            new NodeInfo
            {
                NodeId = 32300,
            },
            new NodeInfo
            {
                NodeId = 32301,
            },
        };
    }
}
