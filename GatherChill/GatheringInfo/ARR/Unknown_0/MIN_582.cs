using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_582 : RouteInfo
    {
        public override uint Id => 582;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-774.333f, 200.871f);
        public override int Radius => 30;

        public override HashSet<uint> NodeIds => new()
        {
            32562,
            32563,
            32564,
            32565,
            32566,
            32567,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002794,
            2002795,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32562,
            },
            new NodeInfo
            {
                NodeId = 32563,
            },
            new NodeInfo
            {
                NodeId = 32564,
            },
            new NodeInfo
            {
                NodeId = 32565,
            },
            new NodeInfo
            {
                NodeId = 32566,
            },
            new NodeInfo
            {
                NodeId = 32567,
            },
        };
    }
}
