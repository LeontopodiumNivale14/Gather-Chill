using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Endwalker.UltimaThule_960
{
    public class BTN_848 : RouteInfo
    {
        public override uint Id => 848;
        public override uint ExpansionId => 4;
        public override uint ZoneId => 960;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-362.277f, 357.564f);
        public override int Radius => 90;

        public override HashSet<uint> NodeIds => new()
        {
            34030,
            34031,
            34032,
            34033,
            34034,
            34035,
        };

        public override HashSet<uint> ItemIds => new()
        {
            13,
            36097,
            36206,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34030,
            },
            new NodeInfo
            {
                NodeId = 34031,
            },
            new NodeInfo
            {
                NodeId = 34032,
            },
            new NodeInfo
            {
                NodeId = 34033,
            },
            new NodeInfo
            {
                NodeId = 34034,
            },
            new NodeInfo
            {
                NodeId = 34035,
            },
        };
    }
}
