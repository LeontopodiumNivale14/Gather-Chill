using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.AmhAraeng_815
{
    public class MIN_602 : RouteInfo
    {
        public override uint Id => 602;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 815;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(-251.805f, -500.446f);
        public override int Radius => 116;

        public override HashSet<uint> NodeIds => new()
        {
            32671,
            32672,
            32673,
            32674,
            32675,
            32676,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            27700,
            27702,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32671,
            },
            new NodeInfo
            {
                NodeId = 32672,
            },
            new NodeInfo
            {
                NodeId = 32673,
            },
            new NodeInfo
            {
                NodeId = 32674,
            },
            new NodeInfo
            {
                NodeId = 32675,
            },
            new NodeInfo
            {
                NodeId = 32676,
            },
        };
    }
}
