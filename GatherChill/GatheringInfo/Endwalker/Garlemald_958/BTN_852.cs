using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.Garlemald_958
{
    public class BTN_852 : RouteInfo
    {
        public override uint Id => 852;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 958;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(677.665f, -782.637f);
        public override int Radius => 18;

        public override HashSet<uint> NodeIds => new()
        {
            34039,
        };

        public override HashSet<uint> ItemIds => new()
        {
            36304,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34039,
            },
        };
    }
}
