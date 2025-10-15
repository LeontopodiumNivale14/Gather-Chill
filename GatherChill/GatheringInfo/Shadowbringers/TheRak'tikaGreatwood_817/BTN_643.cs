using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheRaktikaGreatwood_817
{
	public class BTN_643 : RouteInfo
	{
		public override uint Id => 643;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 817;
		public override uint GatherType => 5;
		public override Vector2 MapPosition => new Vector2(-826.584f, 222.547f);
		public override int Radius => 118;

		public override HashSet<uint> NodeIds => new()
		{
			32798,
			32799,
			32800,
		};

		public override HashSet<uint> ItemIds => new()
		{
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
