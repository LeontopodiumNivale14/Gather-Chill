using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheRaktikaGreatwood_817
{
	public class BTN_631 : RouteInfo
	{
		public override uint Id => 631;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 817;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(661.386f, 3.32934f);
		public override int Radius => 10;

		public override HashSet<uint> NodeIds => new()
		{
			32770,
		};

		public override HashSet<uint> ItemIds => new()
		{
			27828,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
