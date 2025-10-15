using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.AmhAraeng_815
{
    public class MIN_591 : RouteInfo
    {
        public override uint Id => 591;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 815;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(444.044f, 74.4195f);
        public override int Radius => 113;

        public override HashSet<uint> NodeIds => new()
        {
            32620,
            32621,
            32622,
            32623,
            32624,
            32625,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            26752,
            27817,
            27954,
            28200,
            28202,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32620,
            },
            new NodeInfo
            {
                NodeId = 32621,
            },
            new NodeInfo
            {
                NodeId = 32622,
            },
            new NodeInfo
            {
                NodeId = 32623,
            },
            new NodeInfo
            {
                NodeId = 32624,
            },
            new NodeInfo
            {
                NodeId = 32625,
            },
        };
    }
}
