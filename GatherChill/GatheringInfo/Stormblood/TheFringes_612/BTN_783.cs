using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheFringes_612
{
	public class BTN_783 : RouteInfo
	{
		public override uint Id => 783;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 612;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(233.214f, -444.993f);
		public override int Radius => 163;

		public override HashSet<uint> NodeIds => new()
		{
			33634,
			33635,
			33636,
		};

		public override HashSet<uint> ItemIds => new()
		{
			33011,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
