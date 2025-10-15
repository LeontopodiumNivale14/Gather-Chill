using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1057 : RouteInfo
    {
        public override uint Id => 1057;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-127.554f, -361.225f);
        public override int Radius => 62;

        public override HashSet<uint> NodeIds => new()
        {
            35117,
            35118,
            35119,
            35120,
            35121,
            35122,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35117,
            },
            new NodeInfo
            {
                NodeId = 35118,
            },
            new NodeInfo
            {
                NodeId = 35119,
            },
            new NodeInfo
            {
                NodeId = 35120,
            },
            new NodeInfo
            {
                NodeId = 35121,
            },
            new NodeInfo
            {
                NodeId = 35122,
            },
        };
    }
}
