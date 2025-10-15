using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_576 : RouteInfo
    {
        public override uint Id => 576;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-241.232f, 43.7295f);
        public override int Radius => 24;

        public override HashSet<uint> NodeIds => new()
        {
            32512,
            32513,
            32514,
            32515,
            32516,
            32517,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002785,
            2002786,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32512,
            },
            new NodeInfo
            {
                NodeId = 32513,
            },
            new NodeInfo
            {
                NodeId = 32514,
            },
            new NodeInfo
            {
                NodeId = 32515,
            },
            new NodeInfo
            {
                NodeId = 32516,
            },
            new NodeInfo
            {
                NodeId = 32517,
            },
        };
    }
}
