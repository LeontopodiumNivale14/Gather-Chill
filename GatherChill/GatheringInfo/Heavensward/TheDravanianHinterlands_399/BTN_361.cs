using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianHinterlands_399
{
    public class BTN_361 : RouteInfo
    {
        public override uint Id => 361;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 399;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-480.095f, -197.374f);
        public override int Radius => 131;

        public override HashSet<uint> NodeIds => new()
        {
            31430,
            31431,
            31432,
            31433,
            31434,
            31435,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            12639,
            12641,
            13762,
            17557,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31430,
            },
            new NodeInfo
            {
                NodeId = 31431,
            },
            new NodeInfo
            {
                NodeId = 31432,
            },
            new NodeInfo
            {
                NodeId = 31433,
            },
            new NodeInfo
            {
                NodeId = 31434,
            },
            new NodeInfo
            {
                NodeId = 31435,
            },
        };
    }
}
