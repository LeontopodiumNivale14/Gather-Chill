using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheRaktikaGreatwood_817
{
    public class BTN_615 : RouteInfo
    {
        public override uint Id => 615;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 817;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-571.314f, -47.3672f);
        public override int Radius => 115;

        public override HashSet<uint> NodeIds => new()
        {
            32709,
            32710,
            32711,
            32712,
            32713,
            32714,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            27685,
            27834,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32709,
            },
            new NodeInfo
            {
                NodeId = 32710,
            },
            new NodeInfo
            {
                NodeId = 32711,
            },
            new NodeInfo
            {
                NodeId = 32712,
            },
            new NodeInfo
            {
                NodeId = 32713,
            },
            new NodeInfo
            {
                NodeId = 32714,
            },
        };
    }
}
