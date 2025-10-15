using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.LivingMemory_1192
{
    public class MIN_1019 : RouteInfo
    {
        public override uint Id => 1019;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1192;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(707.71f, -167.189f);
        public override int Radius => 133;

        public override HashSet<uint> NodeIds => new()
        {
            34973,
            34974,
            34975,
        };

        public override HashSet<uint> ItemIds => new()
        {
            43918,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34973,
            },
            new NodeInfo
            {
                NodeId = 34974,
            },
            new NodeInfo
            {
                NodeId = 34975,
            },
        };
    }
}
