using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_68 : RouteInfo
    {
        public override uint Id => 68;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-17.1987f, -51.5267f);
        public override int Radius => 30;

        public override HashSet<uint> NodeIds => new()
        {
            30246,
            30247,
            30248,
            30249,
            30250,
            30251,
            30252,
            30253,
            30254,
            30255,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000304,
            2000306,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30246,
            },
            new NodeInfo
            {
                NodeId = 30247,
            },
            new NodeInfo
            {
                NodeId = 30248,
            },
            new NodeInfo
            {
                NodeId = 30249,
            },
            new NodeInfo
            {
                NodeId = 30250,
            },
            new NodeInfo
            {
                NodeId = 30251,
            },
            new NodeInfo
            {
                NodeId = 30252,
            },
            new NodeInfo
            {
                NodeId = 30253,
            },
            new NodeInfo
            {
                NodeId = 30254,
            },
            new NodeInfo
            {
                NodeId = 30255,
            },
        };
    }
}
