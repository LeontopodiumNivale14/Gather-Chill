using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheLochs_621
{
	public class BTN_526 : RouteInfo
	{
		public override uint Id => 526;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 621;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-731.46f, -625.497f);
		public override int Radius => 64;

		public override HashSet<uint> NodeIds => new()
		{
			32274,
		};

		public override HashSet<uint> ItemIds => new()
		{
			17948,
			19857,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
