using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.Yanxia_614
{
	public class BTN_518 : RouteInfo
	{
		public override uint Id => 518;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 614;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(528.946f, 259.406f);
		public override int Radius => 110;

		public override HashSet<uint> NodeIds => new()
		{
			32261,
			33343,
			33606,
		};

		public override HashSet<uint> ItemIds => new()
		{
			10,
			19916,
			23221,
			33151,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
