using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Shadowbringers.TheTempest_818
{
	public class MIN_928 : RouteInfo
	{
		public override uint Id => 928;
		public override uint ExpansionId => 3;
		public override uint ZoneId => 818;
		public override uint GatherType => 0;
		public override Vector2 MapPosition => new Vector2(374.918f, -855.479f);
		public override int Radius => 115;

		public override HashSet<uint> NodeIds => new()
		{
			34463,
			34464,
			34465,
			34466,
			34467,
			34468,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12,
			38824,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
