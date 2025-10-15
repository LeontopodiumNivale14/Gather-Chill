using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheRaktikaGreatwood_817
{
	public class BTN_619 : RouteInfo
	{
		public override uint Id => 619;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 817;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(-327.664f, 283.183f);
		public override int Radius => 124;

		public override HashSet<uint> NodeIds => new()
		{
			32733,
			32734,
			32735,
			32736,
			32737,
			32738,
		};

		public override HashSet<uint> ItemIds => new()
		{
			10,
			27753,
			27826,
			27829,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
