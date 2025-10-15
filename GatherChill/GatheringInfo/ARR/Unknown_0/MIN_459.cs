using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_459 : RouteInfo
    {
        public override uint Id => 459;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-364.105f, 679.524f);
        public override int Radius => 12;

        public override HashSet<uint> NodeIds => new()
        {
            32035,
            32036,
            32037,
            32038,
            32039,
            32040,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002159,
            2002160,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32035,
            },
            new NodeInfo
            {
                NodeId = 32036,
            },
            new NodeInfo
            {
                NodeId = 32037,
            },
            new NodeInfo
            {
                NodeId = 32038,
            },
            new NodeInfo
            {
                NodeId = 32039,
            },
            new NodeInfo
            {
                NodeId = 32040,
            },
        };
    }
}
