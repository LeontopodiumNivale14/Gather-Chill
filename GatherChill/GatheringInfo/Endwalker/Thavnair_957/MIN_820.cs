using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Thavnair_957
{
    public class MIN_820 : RouteInfo
    {
        public override uint Id => 820;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 957;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-455.007f, -423.778f);
        public override int Radius => 181;

        public override HashSet<uint> NodeIds => new()
        {
            33918,
            33919,
            33920,
            33921,
            33922,
            33923,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            35600,
            36175,
            36670,
            41069,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33918,
            },
            new NodeInfo
            {
                NodeId = 33919,
            },
            new NodeInfo
            {
                NodeId = 33920,
            },
            new NodeInfo
            {
                NodeId = 33921,
            },
            new NodeInfo
            {
                NodeId = 33922,
            },
            new NodeInfo
            {
                NodeId = 33923,
            },
        };
    }
}
