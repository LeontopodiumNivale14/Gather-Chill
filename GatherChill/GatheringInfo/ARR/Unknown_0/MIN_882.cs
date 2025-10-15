using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_882 : RouteInfo
    {
        public override uint Id => 882;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-85.5899f, -210.335f);
        public override int Radius => 27;

        public override HashSet<uint> NodeIds => new()
        {
            34252,
            34253,
            34254,
            34255,
            34256,
            34257,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003166,
            2003167,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34252,
            },
            new NodeInfo
            {
                NodeId = 34253,
            },
            new NodeInfo
            {
                NodeId = 34254,
            },
            new NodeInfo
            {
                NodeId = 34255,
            },
            new NodeInfo
            {
                NodeId = 34256,
            },
            new NodeInfo
            {
                NodeId = 34257,
            },
        };
    }
}
