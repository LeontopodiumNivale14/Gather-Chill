using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_971 : RouteInfo
    {
        public override uint Id => 971;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-581.545f, 384.153f);
        public override int Radius => 72;

        public override HashSet<uint> NodeIds => new()
        {
            34727,
            34728,
            34729,
            34730,
            34731,
            34732,
            34733,
            34734,
            34735,
            34736,
            34737,
            34738,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003554,
            2003555,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34727,
            },
            new NodeInfo
            {
                NodeId = 34728,
            },
            new NodeInfo
            {
                NodeId = 34729,
            },
            new NodeInfo
            {
                NodeId = 34730,
            },
            new NodeInfo
            {
                NodeId = 34731,
            },
            new NodeInfo
            {
                NodeId = 34732,
            },
            new NodeInfo
            {
                NodeId = 34733,
            },
            new NodeInfo
            {
                NodeId = 34734,
            },
            new NodeInfo
            {
                NodeId = 34735,
            },
            new NodeInfo
            {
                NodeId = 34736,
            },
            new NodeInfo
            {
                NodeId = 34737,
            },
            new NodeInfo
            {
                NodeId = 34738,
            },
        };
    }
}
