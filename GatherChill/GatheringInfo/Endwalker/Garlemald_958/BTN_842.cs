using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Garlemald_958
{
    public class BTN_842 : RouteInfo
    {
        public override uint Id => 842;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 958;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-276.784f, 369.559f);
        public override int Radius => 107;

        public override HashSet<uint> NodeIds => new()
        {
            33997,
            33998,
            33999,
            34000,
            34001,
            34002,
        };

        public override HashSet<uint> ItemIds => new()
        {
            11,
            35601,
            36192,
            36671,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33997,
            },
            new NodeInfo
            {
                NodeId = 33998,
            },
            new NodeInfo
            {
                NodeId = 33999,
            },
            new NodeInfo
            {
                NodeId = 34000,
            },
            new NodeInfo
            {
                NodeId = 34001,
            },
            new NodeInfo
            {
                NodeId = 34002,
            },
        };
    }
}
