using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheLochs_621
{
	public class MIN_556 : RouteInfo
	{
		public override uint Id => 556;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 621;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(13.2268f, -398.324f);
		public override int Radius => 41;

		public override HashSet<uint> NodeIds => new()
		{
			32369,
		};

		public override HashSet<uint> ItemIds => new()
		{
			24241,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
