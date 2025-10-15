using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Urqopacha_1187
{
    public class MIN_974 : RouteInfo
    {
        public override uint Id => 974;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1187;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-457.06f, -523.355f);
        public override int Radius => 157;

        public override HashSet<uint> NodeIds => new()
        {
            34749,
            34750,
            34751,
            34752,
            34753,
            34754,
        };

        public override HashSet<uint> ItemIds => new()
        {
            10,
            43899,
            43992,
            44850,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34749,
            },
            new NodeInfo
            {
                NodeId = 34750,
            },
            new NodeInfo
            {
                NodeId = 34751,
            },
            new NodeInfo
            {
                NodeId = 34752,
            },
            new NodeInfo
            {
                NodeId = 34753,
            },
            new NodeInfo
            {
                NodeId = 34754,
            },
        };
    }
}
