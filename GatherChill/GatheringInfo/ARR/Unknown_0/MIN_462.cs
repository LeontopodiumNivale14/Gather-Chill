using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_462 : RouteInfo
    {
        public override uint Id => 462;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(96.2201f, 560.581f);
        public override int Radius => 24;

        public override HashSet<uint> NodeIds => new()
        {
            32065,
            32066,
            32067,
            32068,
            32069,
            32070,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002164,
            2002165,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32065,
            },
            new NodeInfo
            {
                NodeId = 32066,
            },
            new NodeInfo
            {
                NodeId = 32067,
            },
            new NodeInfo
            {
                NodeId = 32068,
            },
            new NodeInfo
            {
                NodeId = 32069,
            },
            new NodeInfo
            {
                NodeId = 32070,
            },
        };
    }
}
