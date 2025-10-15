using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.ARR.Unknown_0
{
    public class MIN_386 : RouteInfo
    {
        public override uint Id => 386;
        public override uint ExpansionId => 0;
        public override uint ZoneId => 0;
        public override uint GatherType => 0;
        public override Vector2 MapPosition => new Vector2(-343.502f, -192.339f);
        public override int Radius => 139;

        public override HashSet<uint> NodeIds => new()
        {
            31628,
            31629,
            31630,
            31656,
            31657,
            31658,
            31684,
            31685,
            31686,
        };

        public override HashSet<uint> ItemIds => new()
        {
            12,
            12534,
            12535,
            12537,
            13750,
        };

        public override List<NodeInfo> Nodes => new()
        {
            new NodeInfo
            {
                NodeId = 31628,
            },
            new NodeInfo
            {
                NodeId = 31629,
            },
            new NodeInfo
            {
                NodeId = 31630,
            },
            new NodeInfo
            {
                NodeId = 31656,
            },
            new NodeInfo
            {
                NodeId = 31657,
            },
            new NodeInfo
            {
                NodeId = 31658,
            },
            new NodeInfo
            {
                NodeId = 31684,
            },
            new NodeInfo
            {
                NodeId = 31685,
            },
            new NodeInfo
            {
                NodeId = 31686,
            },
        };
    }
}
