using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheLochs_621
{
	public class BTN_527 : RouteInfo
	{
		public override uint Id => 527;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 621;
		public override uint GatherType => 2;
		public override Vector2 MapPosition => new Vector2(-480.022f, -438.414f);
		public override int Radius => 26;

		public override HashSet<uint> NodeIds => new()
		{
			32275,
		};

		public override HashSet<uint> ItemIds => new()
		{
			19934,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
