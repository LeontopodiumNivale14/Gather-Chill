using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_450 : RouteInfo
    {
        public override uint Id => 450;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-140.596f, -411.831f);
        public override int Radius => 38;

        public override HashSet<uint> NodeIds => new()
        {
            31963,
            31964,
            31965,
            31966,
            31967,
            31968,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002146,
            2002147,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31963,
            },
            new NodeInfo
            {
                NodeId = 31964,
            },
            new NodeInfo
            {
                NodeId = 31965,
            },
            new NodeInfo
            {
                NodeId = 31966,
            },
            new NodeInfo
            {
                NodeId = 31967,
            },
            new NodeInfo
            {
                NodeId = 31968,
            },
        };
    }
}
