using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianForelands_398
{
    public class MIN_307 : RouteInfo
    {
        public override uint Id => 307;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 398;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(154.359f, -50.2444f);
        public override int Radius => 122;

        public override HashSet<uint> NodeIds => new()
        {
            31440,
            31450,
            31451,
        };

        public override HashSet<uint> ItemIds => new()
        {
            8,
            5214,
            5220,
            12966,
            15949,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31440,
            },
            new NodeInfo
            {
                NodeId = 31450,
            },
            new NodeInfo
            {
                NodeId = 31451,
            },
        };
    }
}
