using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
	public class MIN_730 : RouteInfo
	{
		public override uint Id => 730;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 613;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(897.693f, -880.74f);
		public override int Radius => 103;

		public override HashSet<uint> NodeIds => new()
		{
			33325,
			33326,
			33327,
			33328,
			33329,
			33330,
		};

		public override HashSet<uint> ItemIds => new()
		{
			13,
			30501,
			33232,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
