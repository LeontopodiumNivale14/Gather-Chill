using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.Yanxia_614
{
	public class BTN_474 : RouteInfo
	{
		public override uint Id => 474;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 614;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(-552.324f, -496.928f);
		public override int Radius => 116;

		public override HashSet<uint> NodeIds => new()
		{
			32101,
			32102,
			32103,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
