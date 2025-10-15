using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheFringes_612
{
	public class BTN_811 : RouteInfo
	{
		public override uint Id => 811;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 612;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(642.744f, 325.531f);
		public override int Radius => 122;

		public override HashSet<uint> NodeIds => new()
		{
			33873,
			33874,
			33875,
			33876,
			33877,
			33878,
		};

		public override HashSet<uint> ItemIds => new()
		{
			8,
			33231,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
