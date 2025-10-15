using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
    public class BTN_646 : RouteInfo
    {
        public override uint Id => 646;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 816;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(398.212f, 175.918f);
        public override int Radius => 20;

        public override HashSet<uint> NodeIds => new()
        {
            32805,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32805,
            },
        };
    }
}
