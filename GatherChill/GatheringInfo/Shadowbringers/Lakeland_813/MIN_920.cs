using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
    public class MIN_920 : RouteInfo
    {
        public override uint Id => 920;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 813;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-186.594f, -290.509f);
        public override int Radius => 141;

        public override HashSet<uint> NodeIds => new()
        {
            34416,
            34418,
            34420,
        };

        public override HashSet<uint> ItemIds => new()
        {
            39805,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34416,
            },
            new NodeInfo
            {
                NodeId = 34418,
            },
            new NodeInfo
            {
                NodeId = 34420,
            },
        };
    }
}
