using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_970 : RouteInfo
    {
        public override uint Id => 970;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(581.531f, -92.6412f);
        public override int Radius => 97;

        public override HashSet<uint> NodeIds => new()
        {
            34721,
            34722,
            34723,
            34724,
            34725,
            34726,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003552,
            2003553,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34721,
            },
            new NodeInfo
            {
                NodeId = 34722,
            },
            new NodeInfo
            {
                NodeId = 34723,
            },
            new NodeInfo
            {
                NodeId = 34724,
            },
            new NodeInfo
            {
                NodeId = 34725,
            },
            new NodeInfo
            {
                NodeId = 34726,
            },
        };
    }
}
