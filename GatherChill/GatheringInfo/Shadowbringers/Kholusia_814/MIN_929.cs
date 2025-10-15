using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Kholusia_814
{
    public class MIN_929 : RouteInfo
    {
        public override uint Id => 929;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 814;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(638.853f, 350.805f);
        public override int Radius => 141;

        public override HashSet<uint> NodeIds => new()
        {
            34469,
            34470,
            34471,
        };

        public override HashSet<uint> ItemIds => new()
        {
            39811,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34469,
            },
            new NodeInfo
            {
                NodeId = 34470,
            },
            new NodeInfo
            {
                NodeId = 34471,
            },
        };
    }
}
