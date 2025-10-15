using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Kozamauka_1188
{
    public class MIN_976 : RouteInfo
    {
        public override uint Id => 976;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1188;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-684.97f, -317.334f);
        public override int Radius => 144;

        public override HashSet<uint> NodeIds => new()
        {
            34761,
            34762,
            34763,
            34764,
            34765,
            34766,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            43900,
            44003,
            44851,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34761,
            },
            new NodeInfo
            {
                NodeId = 34762,
            },
            new NodeInfo
            {
                NodeId = 34763,
            },
            new NodeInfo
            {
                NodeId = 34764,
            },
            new NodeInfo
            {
                NodeId = 34765,
            },
            new NodeInfo
            {
                NodeId = 34766,
            },
        };
    }
}
