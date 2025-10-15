using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.HeritageFound_1191
{
    public class MIN_983 : RouteInfo
    {
        public override uint Id => 983;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1191;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-294.56f, 59.368f);
        public override int Radius => 137;

        public override HashSet<uint> NodeIds => new()
        {
            34803,
            34804,
            34805,
            34806,
            34807,
            34808,
        };

        public override HashSet<uint> ItemIds => new()
        {
            11,
            43996,
            44005,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34803,
            },
            new NodeInfo
            {
                NodeId = 34804,
            },
            new NodeInfo
            {
                NodeId = 34805,
            },
            new NodeInfo
            {
                NodeId = 34806,
            },
            new NodeInfo
            {
                NodeId = 34807,
            },
            new NodeInfo
            {
                NodeId = 34808,
            },
        };
    }
}
