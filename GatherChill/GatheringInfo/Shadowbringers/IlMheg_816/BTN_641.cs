using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
    public class BTN_641 : RouteInfo
    {
        public override uint Id => 641;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 816;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(428.951f, 423.112f);
        public override int Radius => 120;

        public override HashSet<uint> NodeIds => new()
        {
            32792,
            32793,
            32794,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32792,
            },
            new NodeInfo
            {
                NodeId = 32793,
            },
            new NodeInfo
            {
                NodeId = 32794,
            },
        };
    }
}
