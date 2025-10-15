using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.Yanxia_614
{
	public class BTN_508 : RouteInfo
	{
		public override uint Id => 508;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 614;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(707.604f, -332.63f);
		public override int Radius => 133;

		public override HashSet<uint> NodeIds => new()
		{
			32206,
			32207,
			32208,
			32209,
			32210,
			32211,
		};

		public override HashSet<uint> ItemIds => new()
		{
			10,
			5389,
			19908,
			24568,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
