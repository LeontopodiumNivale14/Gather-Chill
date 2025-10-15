using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_947 : RouteInfo
    {
        public override uint Id => 947;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(-212.813f, 244.567f);
        public override int Radius => 58;

        public override HashSet<uint> NodeIds => new()
        {
            34523,
            34524,
            34525,
            34526,
            34527,
            34528,
            34529,
            34530,
            34531,
            34532,
            34533,
            34534,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003518,
            2003519,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34523,
            },
            new NodeInfo
            {
                NodeId = 34524,
            },
            new NodeInfo
            {
                NodeId = 34525,
            },
            new NodeInfo
            {
                NodeId = 34526,
            },
            new NodeInfo
            {
                NodeId = 34527,
            },
            new NodeInfo
            {
                NodeId = 34528,
            },
            new NodeInfo
            {
                NodeId = 34529,
            },
            new NodeInfo
            {
                NodeId = 34530,
            },
            new NodeInfo
            {
                NodeId = 34531,
            },
            new NodeInfo
            {
                NodeId = 34532,
            },
            new NodeInfo
            {
                NodeId = 34533,
            },
            new NodeInfo
            {
                NodeId = 34534,
            },
        };
    }
}
