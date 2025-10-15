using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.AzysLla_402
{
    public class MIN_764 : RouteInfo
    {
        public override uint Id => 764;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 402;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(682.043f, 255.204f);
        public override int Radius => 75;

        public override HashSet<uint> NodeIds => new()
        {
            33594,
            33595,
            33596,
            33597,
            33598,
            33599,
        };

        public override HashSet<uint> ItemIds => new()
        {
            31768,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33594,
            },
            new NodeInfo
            {
                NodeId = 33595,
            },
            new NodeInfo
            {
                NodeId = 33596,
            },
            new NodeInfo
            {
                NodeId = 33597,
            },
            new NodeInfo
            {
                NodeId = 33598,
            },
            new NodeInfo
            {
                NodeId = 33599,
            },
        };
    }
}
