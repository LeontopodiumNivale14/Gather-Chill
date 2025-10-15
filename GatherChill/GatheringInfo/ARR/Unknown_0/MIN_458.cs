using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_458 : RouteInfo
    {
        public override uint Id => 458;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-241.413f, -614.214f);
        public override int Radius => 20;

        public override HashSet<uint> NodeIds => new()
        {
            32031,
            32032,
            32033,
            32034,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002158,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32031,
            },
            new NodeInfo
            {
                NodeId = 32032,
            },
            new NodeInfo
            {
                NodeId = 32033,
            },
            new NodeInfo
            {
                NodeId = 32034,
            },
        };
    }
}
