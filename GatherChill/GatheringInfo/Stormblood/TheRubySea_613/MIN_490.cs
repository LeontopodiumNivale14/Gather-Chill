using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
	public class MIN_490 : RouteInfo
	{
		public override uint Id => 490;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 613;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(-464.695f, -185.104f);
		public override int Radius => 190;

		public override HashSet<uint> NodeIds => new()
		{
			32153,
			32154,
			32155,
			32156,
			32157,
			32158,
		};

		public override HashSet<uint> ItemIds => new()
		{
			13,
			20006,
			23151,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
