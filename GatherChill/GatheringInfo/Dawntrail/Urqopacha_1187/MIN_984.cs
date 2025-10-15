using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Urqopacha_1187
{
    public class MIN_984 : RouteInfo
    {
        public override uint Id => 984;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1187;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(409.679f, 310.517f);
        public override int Radius => 164;

        public override HashSet<uint> NodeIds => new()
        {
            34809,
            34810,
            34811,
            34812,
            34813,
            34814,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            43982,
            44853,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34809,
            },
            new NodeInfo
            {
                NodeId = 34810,
            },
            new NodeInfo
            {
                NodeId = 34811,
            },
            new NodeInfo
            {
                NodeId = 34812,
            },
            new NodeInfo
            {
                NodeId = 34813,
            },
            new NodeInfo
            {
                NodeId = 34814,
            },
        };
    }
}
