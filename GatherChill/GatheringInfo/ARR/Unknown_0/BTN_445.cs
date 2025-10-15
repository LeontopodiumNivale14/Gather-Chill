using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_445 : RouteInfo
    {
        public override uint Id => 445;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-462.905f, 406.582f);
        public override int Radius => 100;

        public override HashSet<uint> NodeIds => new()
        {
            31917,
            31918,
            31919,
            31920,
            31921,
            31922,
            31923,
            31924,
            31925,
            31926,
            31927,
            31928,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002138,
            2002139,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31917,
            },
            new NodeInfo
            {
                NodeId = 31918,
            },
            new NodeInfo
            {
                NodeId = 31919,
            },
            new NodeInfo
            {
                NodeId = 31920,
            },
            new NodeInfo
            {
                NodeId = 31921,
            },
            new NodeInfo
            {
                NodeId = 31922,
            },
            new NodeInfo
            {
                NodeId = 31923,
            },
            new NodeInfo
            {
                NodeId = 31924,
            },
            new NodeInfo
            {
                NodeId = 31925,
            },
            new NodeInfo
            {
                NodeId = 31926,
            },
            new NodeInfo
            {
                NodeId = 31927,
            },
            new NodeInfo
            {
                NodeId = 31928,
            },
        };
    }
}
