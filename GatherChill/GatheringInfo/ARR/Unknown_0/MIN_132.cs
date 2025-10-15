using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_132 : RouteInfo
    {
        public override uint Id => 132;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(459.694f, -396.062f);
        public override int Radius => 95;

        public override HashSet<uint> NodeIds => new()
        {
            30982,
            30983,
            30984,
            30985,
            30986,
            30987,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2000909,
            2000910,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 30982,
            },
            new NodeInfo
            {
                NodeId = 30983,
            },
            new NodeInfo
            {
                NodeId = 30984,
            },
            new NodeInfo
            {
                NodeId = 30985,
            },
            new NodeInfo
            {
                NodeId = 30986,
            },
            new NodeInfo
            {
                NodeId = 30987,
            },
        };
    }
}
