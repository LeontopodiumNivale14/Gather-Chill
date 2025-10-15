using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_317 : RouteInfo
    {
        public override uint Id => 317;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-6.16203f, -67.9367f);
        public override int Radius => 14;

        public override HashSet<uint> NodeIds => new()
        {
            31820,
            31821,
            31822,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            38285,
            38288,
            38292,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31820,
            },
            new NodeInfo
            {
                NodeId = 31821,
            },
            new NodeInfo
            {
                NodeId = 31822,
            },
        };
    }
}
