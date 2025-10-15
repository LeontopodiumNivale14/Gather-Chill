using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_270 : RouteInfo
    {
        public override uint Id => 270;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(390.042f, 468.698f);
        public override int Radius => 166;

        public override HashSet<uint> NodeIds => new()
        {
            31201,
            31202,
            31203,
            31204,
            31205,
            31206,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001825,
            2001826,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31201,
            },
            new NodeInfo
            {
                NodeId = 31202,
            },
            new NodeInfo
            {
                NodeId = 31203,
            },
            new NodeInfo
            {
                NodeId = 31204,
            },
            new NodeInfo
            {
                NodeId = 31205,
            },
            new NodeInfo
            {
                NodeId = 31206,
            },
        };
    }
}
