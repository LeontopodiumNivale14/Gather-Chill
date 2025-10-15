using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.HeritageFound_1191
{
    public class MIN_982 : RouteInfo
    {
        public override uint Id => 982;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1191;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(379.311f, 16.1173f);
        public override int Radius => 148;

        public override HashSet<uint> NodeIds => new()
        {
            34797,
            34798,
            34799,
            34800,
            34801,
            34802,
        };

        public override HashSet<uint> ItemIds => new()
        {
            11,
            44006,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34797,
            },
            new NodeInfo
            {
                NodeId = 34798,
            },
            new NodeInfo
            {
                NodeId = 34799,
            },
            new NodeInfo
            {
                NodeId = 34800,
            },
            new NodeInfo
            {
                NodeId = 34801,
            },
            new NodeInfo
            {
                NodeId = 34802,
            },
        };
    }
}
