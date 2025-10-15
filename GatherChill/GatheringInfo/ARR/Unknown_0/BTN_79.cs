using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_79 : RouteInfo
    {
        public override uint Id => 79;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(434.728f, 458.056f);
        public override int Radius => 87;

        public override HashSet<uint> NodeIds => new()
        {
            30574,
            30575,
            30576,
            30577,
            30578,
            30579,
            30580,
            30581,
            30582,
            30583,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000467,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30574,
            },
            new NodeInfo
            {
                NodeId = 30575,
            },
            new NodeInfo
            {
                NodeId = 30576,
            },
            new NodeInfo
            {
                NodeId = 30577,
            },
            new NodeInfo
            {
                NodeId = 30578,
            },
            new NodeInfo
            {
                NodeId = 30579,
            },
            new NodeInfo
            {
                NodeId = 30580,
            },
            new NodeInfo
            {
                NodeId = 30581,
            },
            new NodeInfo
            {
                NodeId = 30582,
            },
            new NodeInfo
            {
                NodeId = 30583,
            },
        };
    }
}
