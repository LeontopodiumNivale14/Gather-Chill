using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_256 : RouteInfo
    {
        public override uint Id => 256;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(286.242f, -358.376f);
        public override int Radius => 24;

        public override HashSet<uint> NodeIds => new()
        {
            31083,
            31084,
            31085,
            31086,
            31087,
            31088,
            31089,
            31090,
            31091,
            31092,
            31093,
            31094,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2001850,
            2001851,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31083,
            },
            new NodeInfo
            {
                NodeId = 31084,
            },
            new NodeInfo
            {
                NodeId = 31085,
            },
            new NodeInfo
            {
                NodeId = 31086,
            },
            new NodeInfo
            {
                NodeId = 31087,
            },
            new NodeInfo
            {
                NodeId = 31088,
            },
            new NodeInfo
            {
                NodeId = 31089,
            },
            new NodeInfo
            {
                NodeId = 31090,
            },
            new NodeInfo
            {
                NodeId = 31091,
            },
            new NodeInfo
            {
                NodeId = 31092,
            },
            new NodeInfo
            {
                NodeId = 31093,
            },
            new NodeInfo
            {
                NodeId = 31094,
            },
        };
    }
}
