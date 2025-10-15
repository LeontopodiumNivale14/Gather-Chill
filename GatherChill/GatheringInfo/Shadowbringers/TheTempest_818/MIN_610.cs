using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheTempest_818
{
	public class MIN_610 : RouteInfo
	{
		public override uint Id => 610;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 818;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(566.525f, -690.416f);
		public override int Radius => 15;

		public override HashSet<uint> NodeIds => new()
		{
			32684,
		};

		public override HashSet<uint> ItemIds => new()
		{
			27704,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
