using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Kholusia_814
{
    public class MIN_679 : RouteInfo
    {
        public override uint Id => 679;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 814;
        public override uint GatherType => 1;
        public override Vector2 MapPosition => new Vector2(262.766f, -584.078f);
        public override int Radius => 89;

        public override HashSet<uint> NodeIds => new()
        {
            32977,
            32978,
            32979,
            32980,
            32981,
            32982,
        };

        public override HashSet<uint> ItemIds => new()
        {
            28796,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32977,
            },
            new NodeInfo
            {
                NodeId = 32978,
            },
            new NodeInfo
            {
                NodeId = 32979,
            },
            new NodeInfo
            {
                NodeId = 32980,
            },
            new NodeInfo
            {
                NodeId = 32981,
            },
            new NodeInfo
            {
                NodeId = 32982,
            },
        };
    }
}
