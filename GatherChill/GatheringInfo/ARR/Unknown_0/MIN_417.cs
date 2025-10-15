using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_417 : RouteInfo
    {
        public override uint Id => 417;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(469.471f, 314.194f);
        public override int Radius => 134;

        public override HashSet<uint> NodeIds => new()
        {
            31769,
            31770,
            31771,
            31772,
        };

        public override HashSet<uint> ItemIds => new()
        {
            3,
            9,
            5120,
            12534,
            12535,
            12537,
            13750,
            17570,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31769,
            },
            new NodeInfo
            {
                NodeId = 31770,
            },
            new NodeInfo
            {
                NodeId = 31771,
            },
            new NodeInfo
            {
                NodeId = 31772,
            },
        };
    }
}
