using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_1196 : RouteInfo
    {
        public override uint Id => 1196;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-145.641f, 349.373f);
        public override int Radius => 24;

        public override HashSet<uint> NodeIds => new()
        {
            35249,
            35250,
            35251,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            48018,
            48027,
            48045,
            48054,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 35249,
            },
            new NodeInfo
            {
                NodeId = 35250,
            },
            new NodeInfo
            {
                NodeId = 35251,
            },
        };
    }
}
