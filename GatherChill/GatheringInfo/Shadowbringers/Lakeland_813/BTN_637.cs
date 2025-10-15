using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
    public class BTN_637 : RouteInfo
    {
        public override uint Id => 637;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 813;
        public override uint GatherType => 5;
        public override Vector2 MapPosition => new Vector2(-416.159f, 839.164f);
        public override int Radius => 90;

        public override HashSet<uint> NodeIds => new()
        {
            32780,
            32781,
            32782,
        };

        public override HashSet<uint> ItemIds => new()
        {
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32780,
            },
            new NodeInfo
            {
                NodeId = 32781,
            },
            new NodeInfo
            {
                NodeId = 32782,
            },
        };
    }
}
