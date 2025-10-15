using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
    public class BTN_773 : RouteInfo
    {
        public override uint Id => 773;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 816;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-871.548f, 80.6135f);
        public override int Radius => 1;

        public override HashSet<uint> NodeIds => new()
        {
            33024,
        };

        public override HashSet<uint> ItemIds => new()
        {
            33002,
            33004,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33024,
            },
        };
    }
}
