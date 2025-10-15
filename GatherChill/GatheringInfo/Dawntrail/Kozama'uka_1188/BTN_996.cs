using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Dawntrail.Kozamauka_1188
{
    public class BTN_996 : RouteInfo
    {
        public override uint Id => 996;
        public override uint ExpansionId => 5;
        public override uint ZoneId => 1188;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-787.825f, 326.299f);
        public override int Radius => 138;

        public override HashSet<uint> NodeIds => new()
        {
            34881,
            34882,
            34883,
            34884,
            34885,
            34886,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            44040,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34881,
            },
            new NodeInfo
            {
                NodeId = 34882,
            },
            new NodeInfo
            {
                NodeId = 34883,
            },
            new NodeInfo
            {
                NodeId = 34884,
            },
            new NodeInfo
            {
                NodeId = 34885,
            },
            new NodeInfo
            {
                NodeId = 34886,
            },
        };
    }
}
