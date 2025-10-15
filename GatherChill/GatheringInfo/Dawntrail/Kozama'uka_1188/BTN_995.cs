using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Kozamauka_1188
{
    public class BTN_995 : RouteInfo
    {
        public override uint Id => 995;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1188;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(531.346f, -474.524f);
        public override int Radius => 134;

        public override HashSet<uint> NodeIds => new()
        {
            34875,
            34876,
            34877,
            34878,
            34879,
            34880,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            43988,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34875,
            },
            new NodeInfo
            {
                NodeId = 34876,
            },
            new NodeInfo
            {
                NodeId = 34877,
            },
            new NodeInfo
            {
                NodeId = 34878,
            },
            new NodeInfo
            {
                NodeId = 34879,
            },
            new NodeInfo
            {
                NodeId = 34880,
            },
        };
    }
}
