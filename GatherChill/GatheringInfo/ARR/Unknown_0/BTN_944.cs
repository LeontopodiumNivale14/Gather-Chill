using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class BTN_944 : RouteInfo
    {
        public override uint Id => 944;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 3;
        public override Vector2 MapPosition => new Vector2(150.49f, -362.515f);
        public override int Radius => 95;

        public override HashSet<uint> NodeIds => new()
        {
            34501,
            34502,
            34503,
            34504,
            34505,
            34506,
            34507,
            34508,
            34509,
            34510,
            34511,
            34512,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2003513,
            2003514,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 34501,
            },
            new NodeInfo
            {
                NodeId = 34502,
            },
            new NodeInfo
            {
                NodeId = 34503,
            },
            new NodeInfo
            {
                NodeId = 34504,
            },
            new NodeInfo
            {
                NodeId = 34505,
            },
            new NodeInfo
            {
                NodeId = 34506,
            },
            new NodeInfo
            {
                NodeId = 34507,
            },
            new NodeInfo
            {
                NodeId = 34508,
            },
            new NodeInfo
            {
                NodeId = 34509,
            },
            new NodeInfo
            {
                NodeId = 34510,
            },
            new NodeInfo
            {
                NodeId = 34511,
            },
            new NodeInfo
            {
                NodeId = 34512,
            },
        };
    }
}
