using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianForelands_398
{
    public class BTN_768 : RouteInfo
    {
        public override uint Id => 768;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 398;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(331.428f, 375.351f);
        public override int Radius => 1;

        public override HashSet<uint> NodeIds => new()
        {
            31512,
        };

        public override HashSet<uint> ItemIds => new()
        {
            32992,
            32993,
            32994,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31512,
            },
        };
    }
}
