using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_77 : RouteInfo
    {
        public override uint Id => 77;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(462.316f, 289.811f);
        public override int Radius => 128;

        public override HashSet<uint> NodeIds => new()
        {
            30560,
            30561,
            30562,
            30563,
            30564,
            30565,
            30566,
            30567,
            30568,
            30569,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000464,
            2000465,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30560,
            },
            new NodeInfo
            {
                NodeId = 30561,
            },
            new NodeInfo
            {
                NodeId = 30562,
            },
            new NodeInfo
            {
                NodeId = 30563,
            },
            new NodeInfo
            {
                NodeId = 30564,
            },
            new NodeInfo
            {
                NodeId = 30565,
            },
            new NodeInfo
            {
                NodeId = 30566,
            },
            new NodeInfo
            {
                NodeId = 30567,
            },
            new NodeInfo
            {
                NodeId = 30568,
            },
            new NodeInfo
            {
                NodeId = 30569,
            },
        };
    }
}
