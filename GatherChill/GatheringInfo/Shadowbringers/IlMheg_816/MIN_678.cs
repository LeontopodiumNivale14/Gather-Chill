using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
    public class MIN_678 : RouteInfo
    {
        public override uint Id => 678;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 816;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(55.6079f, -624.314f);
        public override int Radius => 111;

        public override HashSet<uint> NodeIds => new()
        {
            32971,
            32972,
            32973,
            32974,
            32975,
            32976,
        };

        public override HashSet<uint> ItemIds => new()
        {
            28792,
            28797,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32971,
            },
            new NodeInfo
            {
                NodeId = 32972,
            },
            new NodeInfo
            {
                NodeId = 32973,
            },
            new NodeInfo
            {
                NodeId = 32974,
            },
            new NodeInfo
            {
                NodeId = 32975,
            },
            new NodeInfo
            {
                NodeId = 32976,
            },
        };
    }
}
