using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_322 : RouteInfo
    {
        public override uint Id => 322;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-86.0036f, -14.8303f);
        public override int Radius => 16;

        public override HashSet<uint> NodeIds => new()
        {
            33842,
            33846,
            33847,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            38291,
            38294,
            38297,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33842,
            },
            new NodeInfo
            {
                NodeId = 33846,
            },
            new NodeInfo
            {
                NodeId = 33847,
            },
        };
    }
}
