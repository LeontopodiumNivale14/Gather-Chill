using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_939
{
    public class MIN_790 : RouteInfo
    {
        public override uint Id => 790;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 939;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(154.72f, -3.27294f);
        public override int Radius => 870;

        public override HashSet<uint> NodeIds => new()
        {
            33648,
            33655,
            33662,
            33667,
            33669,
            33674,
            33676,
            33681,
            33688,
            33692,
            33697,
            33704,
            33711,
            33718,
            33723,
            33725,
            33730,
            33732,
            33737,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29943,
            31315,
            32014,
            32024,
            32034,
            32043,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33648,
            },
            new NodeInfo
            {
                NodeId = 33655,
            },
            new NodeInfo
            {
                NodeId = 33662,
            },
            new NodeInfo
            {
                NodeId = 33667,
            },
            new NodeInfo
            {
                NodeId = 33669,
            },
            new NodeInfo
            {
                NodeId = 33674,
            },
            new NodeInfo
            {
                NodeId = 33676,
            },
            new NodeInfo
            {
                NodeId = 33681,
            },
            new NodeInfo
            {
                NodeId = 33688,
            },
            new NodeInfo
            {
                NodeId = 33692,
            },
            new NodeInfo
            {
                NodeId = 33697,
            },
            new NodeInfo
            {
                NodeId = 33704,
            },
            new NodeInfo
            {
                NodeId = 33711,
            },
            new NodeInfo
            {
                NodeId = 33718,
            },
            new NodeInfo
            {
                NodeId = 33723,
            },
            new NodeInfo
            {
                NodeId = 33725,
            },
            new NodeInfo
            {
                NodeId = 33730,
            },
            new NodeInfo
            {
                NodeId = 33732,
            },
            new NodeInfo
            {
                NodeId = 33737,
            },
        };
    }
}
