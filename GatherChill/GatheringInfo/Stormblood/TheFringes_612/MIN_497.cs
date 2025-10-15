using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheFringes_612
{
	public class MIN_497 : RouteInfo
	{
		public override uint Id => 497;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 612;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(187.543f, -659.979f);
		public override int Radius => 55;

		public override HashSet<uint> NodeIds => new()
		{
			32180,
		};

		public override HashSet<uint> ItemIds => new()
		{
			19968,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
