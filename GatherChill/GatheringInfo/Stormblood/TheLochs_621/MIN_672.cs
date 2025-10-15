using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Stormblood.TheLochs_621
{
	public class MIN_672 : RouteInfo
	{
		public override uint Id => 672;
		public override uint ExpansionId => 2;
		public override uint ZoneId => 621;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(59.9191f, -312.089f);
		public override int Radius => 97;

		public override HashSet<uint> NodeIds => new()
		{
			32935,
			32936,
			32937,
			32938,
			32939,
			32940,
		};

		public override HashSet<uint> ItemIds => new()
		{
			28784,
			28786,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
