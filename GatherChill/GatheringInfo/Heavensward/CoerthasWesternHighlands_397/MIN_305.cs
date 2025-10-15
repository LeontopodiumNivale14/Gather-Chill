using System.Collections.Generic;
using System.Numerics;
using GatherChill.GatheringInfo;

namespace GatherChill.Routes.Heavensward.CoerthasWesternHighlands_397
{
	public class MIN_305 : RouteInfo
	{
		public override uint Id => 305;
		public override uint ExpansionId => 1;
		public override uint ZoneId => 397;
		public override uint GatherType => 1;
		public override Vector2 MapPosition => new Vector2(48.0044f, -73.7474f);
		public override int Radius => 150;

		public override HashSet<uint> NodeIds => new()
		{
			31436,
			31447,
			31449,
		};

		public override HashSet<uint> ItemIds => new()
		{
			12,
			5218,
			5224,
			12967,
			15949,
		};

		public override List<NodeInfo> Nodes => new()
		{

		};
	}
}
