using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.IlMheg_816
{
    public class BTN_921 : RouteInfo
    {
        public override uint Id => 921;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 816;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-230.762f, -572.924f);
        public override int Radius => 132;

        public override HashSet<uint> NodeIds => new()
        {
            34421,
            34422,
            34423,
            34424,
            34425,
            34426,
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
                NodeId = 34421,
            },
            new NodeInfo
            {
                NodeId = 34422,
            },
            new NodeInfo
            {
                NodeId = 34423,
            },
            new NodeInfo
            {
                NodeId = 34424,
            },
            new NodeInfo
            {
                NodeId = 34425,
            },
            new NodeInfo
            {
                NodeId = 34426,
            },
        };
    }
}
