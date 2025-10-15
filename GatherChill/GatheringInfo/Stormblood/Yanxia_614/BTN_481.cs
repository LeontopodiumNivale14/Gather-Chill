using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.Yanxia_614
{
	public class BTN_481 : RouteInfo
	{
		public override uint Id => 481;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 614;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(-487.35f, 795.319f);
		public override int Radius => 47;

		public override HashSet<uint> NodeIds => new()
		{
			32120,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
