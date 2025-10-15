using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_578 : RouteInfo
    {
        public override uint Id => 578;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-373.773f, -505.064f);
        public override int Radius => 97;

        public override HashSet<uint> NodeIds => new()
        {
            32530,
            32531,
            32532,
            32533,
            32534,
            32535,
            32536,
            32537,
            32538,
            32539,
            32540,
            32541,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002789,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32530,
            },
            new NodeInfo
            {
                NodeId = 32531,
            },
            new NodeInfo
            {
                NodeId = 32532,
            },
            new NodeInfo
            {
                NodeId = 32533,
            },
            new NodeInfo
            {
                NodeId = 32534,
            },
            new NodeInfo
            {
                NodeId = 32535,
            },
            new NodeInfo
            {
                NodeId = 32536,
            },
            new NodeInfo
            {
                NodeId = 32537,
            },
            new NodeInfo
            {
                NodeId = 32538,
            },
            new NodeInfo
            {
                NodeId = 32539,
            },
            new NodeInfo
            {
                NodeId = 32540,
            },
            new NodeInfo
            {
                NodeId = 32541,
            },
        };
    }
}
