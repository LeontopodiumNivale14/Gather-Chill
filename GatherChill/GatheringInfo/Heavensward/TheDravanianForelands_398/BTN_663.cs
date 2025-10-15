using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheDravanianForelands_398
{
    public class BTN_663 : RouteInfo
    {
        public override uint Id => 663;
        public override uint ExpansionId => 1;
        public override uint ZoneId => 398;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-217.198f, 711.146f);
        public override int Radius => 103;

        public override HashSet<uint> NodeIds => new()
        {
            32881,
            32882,
            32883,
            32884,
            32885,
            32886,
        };

        public override HashSet<uint> ItemIds => new()
        {
            28776,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32881,
            },
            new NodeInfo
            {
                NodeId = 32882,
            },
            new NodeInfo
            {
                NodeId = 32883,
            },
            new NodeInfo
            {
                NodeId = 32884,
            },
            new NodeInfo
            {
                NodeId = 32885,
            },
            new NodeInfo
            {
                NodeId = 32886,
            },
        };
    }
}
