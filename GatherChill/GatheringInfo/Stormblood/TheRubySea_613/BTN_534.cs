using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
    public class BTN_534 : RouteInfo
    {
        public override uint Id => 534;
        public override uint ExpansionId => 2;
        public override uint ZoneId => 613;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(765.207f, -210.558f);
        public override int Radius => 128;

        public override HashSet<uint> NodeIds => new()
        {
            32302,
            32303,
            32304,
            32305,
            32306,
            32307,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            20781,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32302,
            },
            new NodeInfo
            {
                NodeId = 32303,
            },
            new NodeInfo
            {
                NodeId = 32304,
            },
            new NodeInfo
            {
                NodeId = 32305,
            },
            new NodeInfo
            {
                NodeId = 32306,
            },
            new NodeInfo
            {
                NodeId = 32307,
            },
        };
    }
}
