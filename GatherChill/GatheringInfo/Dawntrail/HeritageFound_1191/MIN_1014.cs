using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.HeritageFound_1191
{
    public class MIN_1014 : RouteInfo
    {
        public override uint Id => 1014;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1191;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(260.955f, -463.981f);
        public override int Radius => 136;

        public override HashSet<uint> NodeIds => new()
        {
            34958,
            34959,
            34960,
        };

        public override HashSet<uint> ItemIds => new()
        {
            11,
            43931,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34958,
            },
            new NodeInfo
            {
                NodeId = 34959,
            },
            new NodeInfo
            {
                NodeId = 34960,
            },
        };
    }
}
