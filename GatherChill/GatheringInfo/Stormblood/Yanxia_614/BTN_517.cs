using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.Yanxia_614
{
	public class BTN_517 : RouteInfo
	{
		public override uint Id => 517;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 614;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(43.0443f, -473.64f);
		public override int Radius => 141;

		public override HashSet<uint> NodeIds => new()
		{
			32255,
			32256,
			32257,
			32258,
			32259,
			32260,
		};

		public override HashSet<uint> ItemIds => new()
		{
			10,
			19854,
			19855,
			19911,
			19913,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
