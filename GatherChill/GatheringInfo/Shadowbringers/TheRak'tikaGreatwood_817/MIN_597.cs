using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheRaktikaGreatwood_817
{
	public class MIN_597 : RouteInfo
	{
		public override uint Id => 597;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 817;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(48.9276f, -481.165f);
		public override int Radius => 131;

		public override HashSet<uint> NodeIds => new()
		{
			32656,
			33609,
			33610,
		};

		public override HashSet<uint> ItemIds => new()
		{
			13,
			27805,
			30591,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
