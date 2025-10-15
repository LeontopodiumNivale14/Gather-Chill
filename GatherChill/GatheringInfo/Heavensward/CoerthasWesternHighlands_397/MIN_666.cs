using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
	public class MIN_666 : RouteInfo
	{
		public override uint Id => 666;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 397;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(144.05f, -657.521f);
		public override int Radius => 104;

		public override HashSet<uint> NodeIds => new()
		{
			32899,
			32900,
			32901,
			32902,
			32903,
			32904,
		};

		public override HashSet<uint> ItemIds => new()
		{
			28778,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
