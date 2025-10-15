using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheRubySea_613
{
	public class MIN_487 : RouteInfo
	{
		public override uint Id => 487;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 613;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(131.777f, 720.618f);
		public override int Radius => 111;

		public override HashSet<uint> NodeIds => new()
		{
			32135,
			32136,
			32137,
			32138,
			32139,
			32140,
		};

		public override HashSet<uint> ItemIds => new()
		{
			13,
			17942,
			19951,
			24567,
			35847,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
