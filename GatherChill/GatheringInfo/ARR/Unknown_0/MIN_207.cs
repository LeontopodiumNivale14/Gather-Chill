using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_207 : RouteInfo
    {
        public override uint Id => 207;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(130.283f, -457.798f);
        public override int Radius => 57;

        public override HashSet<uint> NodeIds => new()
        {
            30512,
            30513,
            30514,
            30515,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30512,
            },
            new NodeInfo
            {
                NodeId = 30513,
            },
            new NodeInfo
            {
                NodeId = 30514,
            },
            new NodeInfo
            {
                NodeId = 30515,
            },
        };
    }
}
