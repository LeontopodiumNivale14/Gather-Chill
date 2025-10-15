using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_447 : RouteInfo
    {
        public override uint Id => 447;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(322.196f, 349.498f);
        public override int Radius => 47;

        public override HashSet<uint> NodeIds => new()
        {
            31941,
            31942,
            31943,
            31944,
            31945,
            31946,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002141,
            2002142,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31941,
            },
            new NodeInfo
            {
                NodeId = 31942,
            },
            new NodeInfo
            {
                NodeId = 31943,
            },
            new NodeInfo
            {
                NodeId = 31944,
            },
            new NodeInfo
            {
                NodeId = 31945,
            },
            new NodeInfo
            {
                NodeId = 31946,
            },
        };
    }
}
