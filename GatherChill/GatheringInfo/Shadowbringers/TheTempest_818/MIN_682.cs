using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheTempest_818
{
    public class MIN_682 : RouteInfo
    {
        public override uint Id => 682;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 818;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(646.822f, 495.68f);
        public override int Radius => 1;

        public override HashSet<uint> NodeIds => new()
        {
            32988,
        };

        public override HashSet<uint> ItemIds => new()
        {
            29970,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32988,
            },
        };
    }
}
