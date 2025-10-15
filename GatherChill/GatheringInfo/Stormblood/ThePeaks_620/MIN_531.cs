using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.ThePeaks_620
{
    public class MIN_531 : RouteInfo
    {
        public override uint Id => 531;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 620;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(784.033f, -583.471f);
        public override int Radius => 103;

        public override HashSet<uint> NodeIds => new()
        {
            32284,
            32285,
            32286,
            32287,
            32288,
            32289,
        };

        public override HashSet<uint> ItemIds => new()
        {
            11,
            20783,
            20784,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32284,
            },
            new NodeInfo
            {
                NodeId = 32285,
            },
            new NodeInfo
            {
                NodeId = 32286,
            },
            new NodeInfo
            {
                NodeId = 32287,
            },
            new NodeInfo
            {
                NodeId = 32288,
            },
            new NodeInfo
            {
                NodeId = 32289,
            },
        };
    }
}
