using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_588 : RouteInfo
    {
        public override uint Id => 588;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-398.825f, 849.218f);
        public override int Radius => 63;

        public override HashSet<uint> NodeIds => new()
        {
            32614,
            32615,
            32616,
            32617,
            32618,
            32619,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002803,
            2002804,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32614,
            },
            new NodeInfo
            {
                NodeId = 32615,
            },
            new NodeInfo
            {
                NodeId = 32616,
            },
            new NodeInfo
            {
                NodeId = 32617,
            },
            new NodeInfo
            {
                NodeId = 32618,
            },
            new NodeInfo
            {
                NodeId = 32619,
            },
        };
    }
}
