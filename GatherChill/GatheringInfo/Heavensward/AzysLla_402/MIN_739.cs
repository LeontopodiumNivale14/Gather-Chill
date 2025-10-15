using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.AzysLla_402
{
    public class MIN_739 : RouteInfo
    {
        public override uint Id => 739;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 402;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-587.182f, 609.631f);
        public override int Radius => 74;

        public override HashSet<uint> NodeIds => new()
        {
            33374,
            33375,
            33376,
            33377,
            33378,
            33379,
        };

        public override HashSet<uint> ItemIds => new()
        {
            31132,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 33374,
            },
            new NodeInfo
            {
                NodeId = 33375,
            },
            new NodeInfo
            {
                NodeId = 33376,
            },
            new NodeInfo
            {
                NodeId = 33377,
            },
            new NodeInfo
            {
                NodeId = 33378,
            },
            new NodeInfo
            {
                NodeId = 33379,
            },
        };
    }
}
