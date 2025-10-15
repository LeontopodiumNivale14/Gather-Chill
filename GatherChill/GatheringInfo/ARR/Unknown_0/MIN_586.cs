using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_586 : RouteInfo
    {
        public override uint Id => 586;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-557.275f, 7.2875f);
        public override int Radius => 61;

        public override HashSet<uint> NodeIds => new()
        {
            32590,
            32591,
            32592,
            32593,
            32594,
            32595,
            32596,
            32597,
            32598,
            32599,
            32600,
            32601,
        };

        public override HashSet<uint> ItemIds => new()
        {
            2002800,
            2002801,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 32590,
            },
            new NodeInfo
            {
                NodeId = 32591,
            },
            new NodeInfo
            {
                NodeId = 32592,
            },
            new NodeInfo
            {
                NodeId = 32593,
            },
            new NodeInfo
            {
                NodeId = 32594,
            },
            new NodeInfo
            {
                NodeId = 32595,
            },
            new NodeInfo
            {
                NodeId = 32596,
            },
            new NodeInfo
            {
                NodeId = 32597,
            },
            new NodeInfo
            {
                NodeId = 32598,
            },
            new NodeInfo
            {
                NodeId = 32599,
            },
            new NodeInfo
            {
                NodeId = 32600,
            },
            new NodeInfo
            {
                NodeId = 32601,
            },
        };
    }
}
