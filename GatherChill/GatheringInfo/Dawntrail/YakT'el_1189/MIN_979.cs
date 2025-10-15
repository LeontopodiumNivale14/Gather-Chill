using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.YakTel_1189
{
    public class MIN_979 : RouteInfo
    {
        public override uint Id => 979;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1189;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-411.227f, -564.06f);
        public override int Radius => 190;

        public override HashSet<uint> NodeIds => new()
        {
            34779,
            34780,
            34781,
            34782,
            34783,
            34784,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            43902,
            44034,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34779,
            },
            new NodeInfo
            {
                NodeId = 34780,
            },
            new NodeInfo
            {
                NodeId = 34781,
            },
            new NodeInfo
            {
                NodeId = 34782,
            },
            new NodeInfo
            {
                NodeId = 34783,
            },
            new NodeInfo
            {
                NodeId = 34784,
            },
        };
    }
}
