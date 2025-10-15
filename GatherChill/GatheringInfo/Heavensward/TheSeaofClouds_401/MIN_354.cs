using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.TheSeaofClouds_401
{
	public class MIN_354 : RouteInfo
	{
		public override uint Id => 354;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 401;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(615.714f, 399.396f);
		public override int Radius => 127;

		public override HashSet<uint> NodeIds => new()
		{
			31364,
			31365,
			31366,
			31367,
			31368,
			31369,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12,
			13,
			12533,
			12632,
			13760,
			17560,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
