using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
	public class MIN_535 : RouteInfo
	{
		public override uint Id => 535;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 613;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(-528.472f, 52.7689f);
		public override int Radius => 22;

		public override HashSet<uint> NodeIds => new()
		{
			32308,
		};

		public override HashSet<uint> ItemIds => new()
		{
			22417,
			23179,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
