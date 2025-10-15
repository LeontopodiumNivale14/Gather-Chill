using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_83 : RouteInfo
    {
        public override uint Id => 83;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(180.768f, 305.727f);
        public override int Radius => 15;

        public override HashSet<uint> NodeIds => new()
        {
            30608,
            30609,
            30610,
            30611,
            30612,
            30613,
            30614,
            30615,
            30616,
            30617,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000914,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30608,
            },
            new NodeInfo
            {
                NodeId = 30609,
            },
            new NodeInfo
            {
                NodeId = 30610,
            },
            new NodeInfo
            {
                NodeId = 30611,
            },
            new NodeInfo
            {
                NodeId = 30612,
            },
            new NodeInfo
            {
                NodeId = 30613,
            },
            new NodeInfo
            {
                NodeId = 30614,
            },
            new NodeInfo
            {
                NodeId = 30615,
            },
            new NodeInfo
            {
                NodeId = 30616,
            },
            new NodeInfo
            {
                NodeId = 30617,
            },
        };
    }
}
