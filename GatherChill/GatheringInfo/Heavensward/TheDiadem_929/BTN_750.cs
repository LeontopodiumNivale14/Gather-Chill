using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDiadem_929
{
    public class BTN_750 : RouteInfo
    {
        public override uint Id => 750;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 929;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(54.3905f, -221.873f);
        public override int Radius => 918;

        public override HashSet<uint> NodeIds => new()
        {
            33490,
            33495,
            33497,
            33502,
            33504,
            33509,
            33516,
            33523,
            33530,
            33534,
            33538,
            33543,
            33545,
            33550,
            33552,
            33557,
            33564,
            33571,
            33583,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29938,
            31282,
            31290,
            31299,
            31310,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33490,
            },
            new NodeInfo
            {
                NodeId = 33495,
            },
            new NodeInfo
            {
                NodeId = 33497,
            },
            new NodeInfo
            {
                NodeId = 33502,
            },
            new NodeInfo
            {
                NodeId = 33504,
            },
            new NodeInfo
            {
                NodeId = 33509,
            },
            new NodeInfo
            {
                NodeId = 33516,
            },
            new NodeInfo
            {
                NodeId = 33523,
            },
            new NodeInfo
            {
                NodeId = 33530,
            },
            new NodeInfo
            {
                NodeId = 33534,
            },
            new NodeInfo
            {
                NodeId = 33538,
            },
            new NodeInfo
            {
                NodeId = 33543,
            },
            new NodeInfo
            {
                NodeId = 33545,
            },
            new NodeInfo
            {
                NodeId = 33550,
            },
            new NodeInfo
            {
                NodeId = 33552,
            },
            new NodeInfo
            {
                NodeId = 33557,
            },
            new NodeInfo
            {
                NodeId = 33564,
            },
            new NodeInfo
            {
                NodeId = 33571,
            },
            new NodeInfo
            {
                NodeId = 33583,
            },
        };
    }
}
