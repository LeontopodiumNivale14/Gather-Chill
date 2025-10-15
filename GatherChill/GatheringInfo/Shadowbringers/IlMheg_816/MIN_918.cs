using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
    public class MIN_918 : RouteInfo
    {
        public override uint Id => 918;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 816;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(509.745f, -506.073f);
        public override int Radius => 124;

        public override HashSet<uint> NodeIds => new()
        {
            34403,
            34404,
            34405,
            34406,
            34407,
            34408,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            38820,
            38823,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34403,
            },
            new NodeInfo
            {
                NodeId = 34404,
            },
            new NodeInfo
            {
                NodeId = 34405,
            },
            new NodeInfo
            {
                NodeId = 34406,
            },
            new NodeInfo
            {
                NodeId = 34407,
            },
            new NodeInfo
            {
                NodeId = 34408,
            },
        };
    }
}
