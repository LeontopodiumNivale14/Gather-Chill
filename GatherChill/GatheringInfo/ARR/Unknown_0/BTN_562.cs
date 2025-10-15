using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_562 : RouteInfo
    {
        public override uint Id => 562;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 2;
        public override Vector2 MapPosition => new Vector2(-41.5525f, 430.316f);
        public override int Radius => 157;

        public override HashSet<uint> NodeIds => new()
        {
            32394,
            32395,
            32396,
            32397,
            32398,
            32399,
            32400,
            32401,
            32402,
            32403,
            32404,
            32405,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002764,
            2002765,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32394,
            },
            new NodeInfo
            {
                NodeId = 32395,
            },
            new NodeInfo
            {
                NodeId = 32396,
            },
            new NodeInfo
            {
                NodeId = 32397,
            },
            new NodeInfo
            {
                NodeId = 32398,
            },
            new NodeInfo
            {
                NodeId = 32399,
            },
            new NodeInfo
            {
                NodeId = 32400,
            },
            new NodeInfo
            {
                NodeId = 32401,
            },
            new NodeInfo
            {
                NodeId = 32402,
            },
            new NodeInfo
            {
                NodeId = 32403,
            },
            new NodeInfo
            {
                NodeId = 32404,
            },
            new NodeInfo
            {
                NodeId = 32405,
            },
        };
    }
}
