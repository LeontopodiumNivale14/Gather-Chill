using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Kholusia_814
{
    public class BTN_625 : RouteInfo
    {
        public override uint Id => 625;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 814;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-396.225f, -357.428f);
        public override int Radius => 104;

        public override HashSet<uint> NodeIds => new()
        {
            32759,
            32760,
            32761,
            32762,
            32763,
            32764,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            26757,
            27759,
            27830,
            27837,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32759,
            },
            new NodeInfo
            {
                NodeId = 32760,
            },
            new NodeInfo
            {
                NodeId = 32761,
            },
            new NodeInfo
            {
                NodeId = 32762,
            },
            new NodeInfo
            {
                NodeId = 32763,
            },
            new NodeInfo
            {
                NodeId = 32764,
            },
        };
    }
}
