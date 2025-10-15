using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.Lakeland_813
{
    public class MIN_593 : RouteInfo
    {
        public override uint Id => 593;
        public override uint ExpansionId => 3;
        public override uint ZoneId => 813;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(273.597f, 751.581f);
        public override int Radius => 121;

        public override HashSet<uint> NodeIds => new()
        {
            32632,
            32633,
            32634,
            32635,
            32636,
            32637,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            26753,
            27698,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32632,
            },
            new NodeInfo
            {
                NodeId = 32633,
            },
            new NodeInfo
            {
                NodeId = 32634,
            },
            new NodeInfo
            {
                NodeId = 32635,
            },
            new NodeInfo
            {
                NodeId = 32636,
            },
            new NodeInfo
            {
                NodeId = 32637,
            },
        };
    }
}
