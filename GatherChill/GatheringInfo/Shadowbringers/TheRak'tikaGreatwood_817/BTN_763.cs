using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheRaktikaGreatwood_817
{
	public class BTN_763 : RouteInfo
	{
		public override uint Id => 763;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 817;
		public override uint GatherType => 3;
		public override Vector2 MapPosition => new Vector2(611.65f, -409.174f);
		public override int Radius => 2;

		public override HashSet<uint> NodeIds => new()
		{
			33593,
		};

		public override HashSet<uint> ItemIds => new()
		{
			32953,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
