using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_880 : RouteInfo
    {
        public override uint Id => 880;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(295.578f, 86.7282f);
        public override int Radius => 60;

        public override HashSet<uint> NodeIds => new()
        {
            34228,
            34229,
            34230,
            34231,
            34232,
            34233,
            34234,
            34235,
            34236,
            34237,
            34238,
            34239,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003163,
            2003164,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34228,
            },
            new NodeInfo
            {
                NodeId = 34229,
            },
            new NodeInfo
            {
                NodeId = 34230,
            },
            new NodeInfo
            {
                NodeId = 34231,
            },
            new NodeInfo
            {
                NodeId = 34232,
            },
            new NodeInfo
            {
                NodeId = 34233,
            },
            new NodeInfo
            {
                NodeId = 34234,
            },
            new NodeInfo
            {
                NodeId = 34235,
            },
            new NodeInfo
            {
                NodeId = 34236,
            },
            new NodeInfo
            {
                NodeId = 34237,
            },
            new NodeInfo
            {
                NodeId = 34238,
            },
            new NodeInfo
            {
                NodeId = 34239,
            },
        };
    }
}
