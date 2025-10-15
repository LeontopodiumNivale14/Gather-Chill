using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
    public class MIN_924 : RouteInfo
    {
        public override uint Id => 924;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 816;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-508.83f, 754.943f);
        public override int Radius => 162;

        public override HashSet<uint> NodeIds => new()
        {
            34440,
            34442,
            34444,
        };

        public override HashSet<uint> ItemIds => new()
        {
            38790,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34440,
            },
            new NodeInfo
            {
                NodeId = 34442,
            },
            new NodeInfo
            {
                NodeId = 34444,
            },
        };
    }
}
