using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_382 : RouteInfo
    {
        public override uint Id => 382;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(221.171f, -259.331f);
        public override int Radius => 12;

        public override HashSet<uint> NodeIds => new()
        {
            31618,
            31646,
            31674,
        };

        public override HashSet<uint> ItemIds => new()
        {
            9,
            12534,
            12535,
            12537,
            13750,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31618,
            },
            new NodeInfo
            {
                NodeId = 31646,
            },
            new NodeInfo
            {
                NodeId = 31674,
            },
        };
    }
}
