using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheFringes_612
{
    public class BTN_513 : RouteInfo
    {
        public override uint Id => 513;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 612;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-623.37f, 425.711f);
        public override int Radius => 108;

        public override HashSet<uint> NodeIds => new()
        {
            32236,
            32237,
            32238,
            32239,
            32240,
            32241,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8,
            19869,
            19870,
            19932,
            24571,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32236,
            },
            new NodeInfo
            {
                NodeId = 32237,
            },
            new NodeInfo
            {
                NodeId = 32238,
            },
            new NodeInfo
            {
                NodeId = 32239,
            },
            new NodeInfo
            {
                NodeId = 32240,
            },
            new NodeInfo
            {
                NodeId = 32241,
            },
        };
    }
}
